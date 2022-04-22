using ComplaintService.DataModel;
using ComplaintService.Dtos;
using ComplaintService.Interfaces;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComplaintController : ControllerBase
    { 
        private readonly ILogger<ComplaintController> _logger;
        private readonly IComplaint _complaint;

        public ComplaintController(ILogger<ComplaintController> logger,IComplaint complaint)
        {
            _logger = logger;
            _complaint = complaint;
        }

        [Authorize(Policy = "mabel")]
        [HttpGet("api/complain/get")]
        public ActionResult<List<ComplaintDto>> Get()
        {
            try
            {
                var dtos = _complaint.GetAll();
                return Ok(new ResponseModel { Message = "success", Status = true, Payload = dtos });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = false, Payload = null });
            }
        }

        [HttpGet("api/complain/get/{id}")]
        public ActionResult<List<ComplaintDto>> GetBy(int id)
        {
            try
            {
                var dtos = _complaint.Get(id);
                return Ok(new ResponseModel { Message = "success", Status = true, Payload = dtos });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = false, Payload = null });
            }
        }

        [HttpPost("api/complaint/addupdate")] 
        public ActionResult<ComplaintDto> Post([FromBody] ComplaintModel model)
        {
            try
            {
                var dtos = _complaint.AddUpdate(model);
                return Ok(new ResponseModel { Message = "success", Status = true, Payload = dtos });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = false, Payload = null });
            }
        }

    }
}
