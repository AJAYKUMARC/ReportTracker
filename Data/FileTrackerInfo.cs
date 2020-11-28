using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReportTracker.Data
{
    public partial class FileTrackerInfo
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string FileName { get; set; }

        [DisplayName("Bar Code")]
        public string BarCode { get; set; }

        [DisplayName("From Dept.")]
        public int DeptFrom { get; set; }

        [DisplayName("To Dept.")]
        public int DeptTo { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Updated On")]
        public DateTime UpdatedDate { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Updated By")]
        public string UpdatedBy { get; set; }

        [DisplayName("From Dept.")]
        public virtual Department DeptFromNavigation { get; set; }

        [DisplayName("To Dept.")]
        public virtual Department DeptToNavigation { get; set; }
    }
}
