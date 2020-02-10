using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class Equipment
    {
        public Equipment()
        {
            Grouping = new HashSet<Grouping>();
        }

        public int EqId { get; set; }
        public string EqName { get; set; }
        public int EqTe { get; set; }

        public virtual TypeEquipment EqTeNavigation { get; set; }
        public virtual ICollection<Grouping> Grouping { get; set; }
    }
}
