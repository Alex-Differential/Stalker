using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StalkerApplication
{
    public partial class TypeWeapons
    {
        public TypeWeapons()
        {
            Weapons = new HashSet<Weapons>();
        }

        public int TwId { get; set; }
        [Required(ErrorMessage = "Поле повинно бути заповненим!")]
        [Display(Name = "Категорія зброї")]
        public string TwName { get; set; }
        [Display(Name = "Інформація про категорію")]

        public virtual ICollection<Weapons> Weapons { get; set; }
    }
}
