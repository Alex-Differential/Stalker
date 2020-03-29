using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StalkerApplication
{
    public partial class TypeEquipment
    {
        public TypeEquipment()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int TeId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        [Display(Name = "Тип екіпірування")]
        public string TeName { get; set; }
        [Display(Name = "Назва типу екіпірування")]
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
