using System;
using System.Collections.Generic;

namespace StalkerApplication
{
    public partial class TypeEquipment
    {
        public TypeEquipment()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int TeId { get; set; }
        public string TeName { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
