using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StalkerApplication
{
    public partial class Grouping
    {
        public Grouping()
        {
            GroupSg = new HashSet<GroupSg>();
            GroupWp = new HashSet<GroupWp>();
        }

        public int GrId { get; set; }
        [Display(Name = "Угрупування")]
        public string GrName { get; set; }
        public int GrEq { get; set; }
        [Display(Name = "Екіпірування угрупування")]
        public int GrSg { get; set; }
        [Display(Name = "Серія ігор")]
        public string GrInfo { get; set; }
        [Display(Name = "Інформація")]

        public virtual Equipment GrEqNavigation { get; set; }
        public virtual ICollection<GroupSg> GroupSg { get; set; }
        public virtual ICollection<GroupWp> GroupWp { get; set; }
    }
}
