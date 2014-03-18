using System;

namespace PX.Business.Models.DTO
{
    public class BaseDTO
    {
        public int RecordOrder { get; set; }

        public bool RecordActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string UpdatedBy { get; set; }

        public string Active
        {
            get { return RecordActive ? "Yes" : "No"; }
            set { RecordActive = value.Equals("Yes"); }
        }
    }
}
