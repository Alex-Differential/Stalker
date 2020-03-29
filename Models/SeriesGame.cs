using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StalkerApplication
{
    public partial class SeriesGame
    {
        public SeriesGame()
        {
            GroupSg = new HashSet<GroupSg>();
        }

        public int SgId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім!")]
        [Display(Name = "Тип екіпірування")]
        public string SgName { get; set; }

        public virtual ICollection<GroupSg> GroupSg { get; set; }
    }
}
