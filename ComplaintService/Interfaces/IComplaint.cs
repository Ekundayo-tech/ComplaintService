using ComplaintService.Data;
using ComplaintService.DataModel;
using ComplaintService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplaintService.Interfaces
{
    public interface IComplaint
    {
        Task<ComplaintDto> AddUpdate(ComplaintModel model);
        Task<ComplaintDto> Get(int Id);
        Task<List<Complaint>> GetAll();
    }
}
