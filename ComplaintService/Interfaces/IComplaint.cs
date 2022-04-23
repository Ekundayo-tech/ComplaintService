using ComplaintService.Data;
using ComplaintService.DataModel;
using ComplaintService.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplaintService.Interfaces
{
    public interface IComplaint
    {
        ComplaintDto AddUpdate(ComplaintModel model);
        ComplaintDto Get(Guid Id);
        List<Complaint> GetAll();
    }
}
