using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELTE.TodoList.Persistence
{
    public class List
    {
        public List()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        public Int32 Id { get; set; }
        [Required]
        [MaxLength(30)]
        public String Name { get; set; } = null!;
        public virtual ICollection<Item> Items { get; set; }
    }
}
