using ComplaintService.DataModel;
using ComplaintService.Dtos;
using ComplaintService.Interfaces;
using ComplaintService.Provider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComplaintService.Controllers
{
    [ApiController] 
    public class AuthController : ControllerBase
    { 
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthProvider _provider;

        public AuthController(ILogger<AuthController> logger, IAuthProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }
         
        [HttpPost("api/login")]
        public ActionResult Login(LoginModel model)
        { 
                var res = _provider.Login(model);
                return Ok(res);
        }

        [HttpPost("api/register")]
        public ActionResult Register(RegisterModel model)
        { 
                var res = _provider.Register(model );
                return Ok(res);
             
        }
    }
}
