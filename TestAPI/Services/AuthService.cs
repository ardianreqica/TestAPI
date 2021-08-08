using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Services
{
    public interface IAuthService
    {
        bool Authenticate(string username, string password);
    }



    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool Authenticate(string username, string password)
        {
            if (configuration.GetSection("BasicAuthCred:Username").Value == username & configuration.GetSection("BasicAuthCred:Password").Value == password)
                return true;

            return false;
        }


    }
}
