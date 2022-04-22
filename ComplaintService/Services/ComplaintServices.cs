using ComplaintService.Data;
using ComplaintService.DataContext;
using ComplaintService.DataModel;
using ComplaintService.Dtos;
using ComplaintService.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ComplaintDto> AddUpdate(ComplaintModel model)
        {

            var res = await _db.Complaint.SingleOrDefaultAsync(x => x.UserId == model.UserId);
            if (res == null)
            {
                res = new Complaint()
                {
                    UserId = model.UserId,
                    ComplaintDescription = model.ComplaintDescription,
                    IsDeleted = model.IsDeleted
                };
                await _db.Complaint.AddAsync(res);
            }
            else
            {
                res.IsDeleted = model.IsDeleted;
                res.UserId = model.UserId;
                res.ComplaintDescription = model.ComplaintDescription;
            }
            await _db.SaveChangesAsync();
            return new ComplaintDto { ComplaintDescription = res.ComplaintDescription, UserId = res.UserId, Id = res.Id, IsDeleted = res.IsDeleted};
        }

        public async Task<ComplaintDto> Get(int Id)
        {
            var res = await _db.Complaint.SingleOrDefaultAsync(x => x.Id == Id);
            if(res != null)
            {
                return new ComplaintDto { Id = res.Id, UserId = res.UserId, ComplaintDescription = res.ComplaintDescription, IsDeleted = res.IsDeleted };
            }
            return null;
        }

        public async Task<List<Complaint>> GetAll()
        {
            return await _db.Complaint.Where(x => x.IsDeleted == false).ToListAsync();
        }
    }
}
