using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class GroupWp
    {
        public int GwGrid { get; set; }
        public int GwWpid { get; set; }

        public virtual Grouping GwGr { get; set; }
        public virtual Weapons GwGrNavigation { get; set; }
    }
}
