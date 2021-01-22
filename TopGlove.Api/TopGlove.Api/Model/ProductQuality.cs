using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGlove.Api.Model
{
    public class ProductQuality
    {
        public Guid ID { get; set; }
        public int SerialNumber { get; set; }
        public string user { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string TypeOfFormer { get; set; }
        public string Factory { get; set; }
        public string FiringOrRework { get; set; }
        public string Size { get; set; }
        public string DefectDetails { get; set; }
        public string Quality { get; set; }
    }
}
