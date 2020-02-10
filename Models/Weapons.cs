using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class Weapons
    {
        public Weapons()
        {
            GroupWp = new HashSet<GroupWp>();
        }

        public int WpId { get; set; }
        public string WpName { get; set; }
        public int WpTw { get; set; }
        public int WpPc { get; set; }
        public string WpWg { get; set; }
        public string WpRn { get; set; }
        public string WpMg { get; set; }
        public string WpTf { get; set; }

        public virtual ProducingCountry WpPcNavigation { get; set; }
        public virtual TypeWeapons WpTwNavigation { get; set; }
        public virtual ICollection<GroupWp> GroupWp { get; set; }
    }
}
