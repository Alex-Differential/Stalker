using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class ProducingCountry
    {
        public ProducingCountry()
        {
            Weapons = new HashSet<Weapons>();
        }

        public int PcId { get; set; }
        public string PcName { get; set; }

        public virtual ICollection<Weapons> Weapons { get; set; }
    }
}
