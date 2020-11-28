using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReportTracker.Data
{
    public partial class Department
    {
        public Department()
        {
            FileTrackerInfoDeptFromNavigation = new HashSet<FileTrackerInfo>();
            FileTrackerInfoDeptToNavigation = new HashSet<FileTrackerInfo>();
        }

        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Updated On")]
        public DateTime UpdatedDate { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Updated By")]
        public string UpdatedBy { get; set; }

        public virtual ICollection<FileTrackerInfo> FileTrackerInfoDeptFromNavigation { get; set; }
        public virtual ICollection<FileTrackerInfo> FileTrackerInfoDeptToNavigation { get; set; }
    }
}
