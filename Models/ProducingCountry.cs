using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StalkerApplication
{
    public partial class ProducingCountry
    {
        public ProducingCountry()
        {
            Weapons = new HashSet<Weapons>();
        }

        public int PcId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        [Display(Name = "Країна-виробник зброї")]
        public string PcName { get; set; }

        public virtual ICollection<Weapons> Weapons { get; set; }
    }
}
