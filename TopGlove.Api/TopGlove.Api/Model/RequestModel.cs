using System;

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

        public string WorkStation { get; set; }
        public string Size { get; set; }
        public string TypeOfFormer { get; set; }
    }
}
