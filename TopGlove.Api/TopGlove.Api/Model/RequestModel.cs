using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGlove.Api.Model
{
    public class RequestModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string User { get; set; }
    }
}
