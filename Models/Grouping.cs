using System;
using System.Collections.Generic;

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
        public string GrName { get; set; }
        public int GrEq { get; set; }
        public int GrSg { get; set; }
        public string GrInfo { get; set; }

        public virtual Equipment GrEqNavigation { get; set; }
        public virtual ICollection<GroupSg> GroupSg { get; set; }
        public virtual ICollection<GroupWp> GroupWp { get; set; }
    }
}
