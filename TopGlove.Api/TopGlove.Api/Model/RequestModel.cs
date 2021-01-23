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

        public string Factory { get; set; }

        public string Quality { get; set; }
        public string Defect { get; set; }
    }
}
