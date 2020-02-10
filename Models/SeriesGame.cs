using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class SeriesGame
    {
        public SeriesGame()
        {
            GroupSg = new HashSet<GroupSg>();
        }

        public int SgId { get; set; }
        public string SgName { get; set; }

        public virtual ICollection<GroupSg> GroupSg { get; set; }
    }
}
