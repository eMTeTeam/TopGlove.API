using System;
using TopGlove.Api.Extension;

namespace TopGlove.Api.Model
{
    [DisplayName("ProductQuality")]
    public class ProductQuality
    {
        public Guid ID { get; set; }
        public int SerialNumber { get; set; }
        public string User { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string TypeOfFormer { get; set; }
        public string Factory { get; set; }
        public string FiringOrRework { get; set; }
        public string Size { get; set; }
        public string DefectDetails { get; set; }
        public string Quality { get; set; }
        public string WorkStation { get; set; }
        public string Shift { get; set; }
    }
}
