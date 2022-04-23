using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintService.Data
{
    public class Complaint
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ComplaintDescription { get; set; }
        public bool IsDeleted { get; set; }
    }
}
