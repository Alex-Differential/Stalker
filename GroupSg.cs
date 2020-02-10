using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class GroupSg
    {
        public int GsGrid { get; set; }
        public int GsSgid { get; set; }

        public virtual Grouping GsGr { get; set; }
        public virtual SeriesGame GsSg { get; set; }
    }
}
