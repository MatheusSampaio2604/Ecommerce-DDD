using System;
using System.IdentityModel.Tokens.Jwt;

namespace WebApiSite.Token
{
    public class TokenJWT
    {
        private readonly JwtSecurityToken Token;

        internal TokenJWT(JwtSecurityToken token)
        {
            Token = token;
        }

        public DateTime ValidTo => Token.ValidTo;

        public string Value => new JwtSecurityTokenHandler().WriteToken(Token);
    }
}
