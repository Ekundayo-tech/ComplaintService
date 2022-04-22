using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;
using System;
using ComplaintService.DataModel;

namespace ComplaintService.Provider
{
    public interface IAuthProvider
    {  
        Task<AuthResult> Login(LoginModel model); 
        Task<AuthResult> Register(RegisterModel model); 


    }
    public class AuthProvider: IAuthProvider
    {
        private readonly IComplaintBaseService _service; 
        public AuthProvider(IConfiguration config, IComplaintBaseService service)
        {
             
            _service = service;
        }

        public async Task<AuthResult> Login(LoginModel model )
        {
            var response = await _service.Post<AuthResult>("api/Auth/api/v1/login", model);

            if (response == null)
            {
                return new AuthResult
                {
                    ErrorMessage = new[] { "user does not exist" }
                };
            }

            return response;
        }

        public async Task<AuthResult> Register(RegisterModel model )
        {
            var response = await _service.Post<AuthResult>("api/Auth/api/v1/register", model);
            if(response == null)
            {
                return new AuthResult
                {
                    ErrorMessage = new[] { "registration failed" }
                };
            }
            return response;
        }
    }
}
