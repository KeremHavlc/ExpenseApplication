using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHandler
    {
        Token CreateToken(Guid id , string email , string roleId);
    }
}
