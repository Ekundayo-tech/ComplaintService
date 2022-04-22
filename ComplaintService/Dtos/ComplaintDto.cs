using System;

namespace ComplaintService.Dtos
{
    public class ComplaintDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public string ComplaintDescription { get; set; }
    }
}