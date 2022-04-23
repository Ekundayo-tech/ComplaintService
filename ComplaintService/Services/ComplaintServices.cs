using ComplaintService.Data;
using ComplaintService.DataContext;
using ComplaintService.DataModel;
using ComplaintService.Dtos;
using ComplaintService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintService.Services
{
    public class ComplaintServices : IComplaint
    {
        private readonly ComplaintDbContext _db;
        public ComplaintServices(ComplaintDbContext db)
        {
            _db = db;
        }
        public ComplaintDto AddUpdate(ComplaintModel model)
        {

            var res =  _db.Complaint.FirstOrDefault(x => x.UserId == model.UserId);
            if (res == null)
            {
                res = new Complaint()
                {
                    UserId = model.UserId,
                    ComplaintDescription = model.ComplaintDescription,
                    IsDeleted = model.IsDeleted
                };
                 _db.Complaint.Add(res);
            }
            else
            {
                res.IsDeleted = model.IsDeleted;
                res.UserId = model.UserId;
                res.ComplaintDescription = model.ComplaintDescription;
            }
            _db.SaveChanges();
            return new ComplaintDto { ComplaintDescription = res.ComplaintDescription, UserId = res.UserId, Id = res.Id, IsDeleted = res.IsDeleted};
        }

        public ComplaintDto Get(Guid Id)
        {
            var res =  _db.Complaint.FirstOrDefault(x => x.Id == Id);
            if(res != null)
            {
                return new ComplaintDto { Id = res.Id, UserId = res.UserId, ComplaintDescription = res.ComplaintDescription, IsDeleted = res.IsDeleted };
            }
            return null;
        }

        public List<Complaint> GetAll()
        {
            return  _db.Complaint.Where(x => x.IsDeleted == false).ToList();
        }
    }
}
