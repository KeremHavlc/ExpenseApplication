using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.DependencyResolvers;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.HttpContext.Request.Cookies["authToken"];  // Cookie'den token al
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;  // Cookie'deki token'ý header'a ekle
                }
                return Task.CompletedTask;
            }
        };
    });
//CORS ayarlarý yapýldý.(React (front) tarafýnda cevap verebilmesi için!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .WithOrigins("http://localhost:5173") // React'in çalýþtýðý domain
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // Cookie'lerin çalýþmasý için þart!
});


//Services Registration --AUTOFAC--
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutoFacBusinessModule());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//Yapýlandýrýlan Cors ayarlarý uygulandý.
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
