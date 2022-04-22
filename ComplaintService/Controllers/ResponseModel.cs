using ComplaintService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplaintService.Controllers
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public object Payload { get; set; }
    }
}