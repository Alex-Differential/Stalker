using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StalkerApplication
{
    public partial class Equipment
    {
        public Equipment()
        {
            Grouping = new HashSet<Grouping>();
        }

        public int EqId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        [Display(Name = "Екіпірування")]
        
        public string EqName { get; set; }
        public int EqTe { get; set; }

        [Display(Name = "Назва екіпірування")]

        public virtual TypeEquipment EqTeNavigation { get; set; }
        public virtual ICollection<Grouping> Grouping { get; set; }
    }
}
