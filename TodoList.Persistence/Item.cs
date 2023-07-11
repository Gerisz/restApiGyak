using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELTE.TodoList.Persistence
{
    public class Item
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(30)]
        public String Name { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        public String? Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
        public byte[]? Image { get; set; }
        public Int32 ListId { get; set; }
        [Required]
        public virtual List List { get; set; } = null!;
    }
}
