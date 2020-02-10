using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class TypeWeapons
    {
        public TypeWeapons()
        {
            Weapons = new HashSet<Weapons>();
        }

        public int TwId { get; set; }
        public string TwName { get; set; }

        public virtual ICollection<Weapons> Weapons { get; set; }
    }
}
