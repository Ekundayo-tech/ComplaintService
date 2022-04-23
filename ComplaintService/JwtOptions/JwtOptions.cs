using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintService.JwtOptions
{
    public class JwtOption
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeSpan { get; set; }

    }
}
