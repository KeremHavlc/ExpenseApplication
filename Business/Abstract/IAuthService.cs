using Core.Dto;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        string Register(RegisterDto registerDto);
        Token Login(LoginDto loginDto);
    }
}
