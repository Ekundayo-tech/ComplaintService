﻿using System;

namespace ComplaintService.DataModel
{
    public class ComplaintModel
    { 
        public Guid UserId { get; set; }
        public string ComplaintDescription { get; set; }
        public bool IsDeleted { get; set; }
    }
}
