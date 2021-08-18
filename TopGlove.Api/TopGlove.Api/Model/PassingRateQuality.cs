using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGlove.Api.Model
{
    public class PassingRateQualityGroup
    {
        public DateTime CreatedDateTime { get; set; }
        public string Factory { get; set; }
        public string TypeOfFormer { get; set; }
        public string Quality { get; set; }
        public string FiringOrRework { get; set; }
        public string BatchNumber { get; set; }
        public IEnumerable<ProductQuality> ProductQualities { get; set; }

    }

    public class PassingRateQuality
    {
        public DateTime CreatedDateTime { get; set; }
        public string Factory { get; set; }
        public string TypeOfFormer { get; set; }
        public int AcceptCount { get; set; }
        public int RejectCount { get; set; }
        public int TotalCount { get; set; }
        public double PassingRate { get; set; }
        public string Remark { get; set; }
        public string BatchNumber { get; set; }
    }
}
