using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ELTE.TodoList.Persistence;

namespace ELTE.TodoList.Web.Models
{
    public class ItemViewModel
    {
        [Key]
        public Int32 Id { get; set; }

        [Required(ErrorMessage = "A név megadása kötelező.")]
        [MaxLength(30, ErrorMessage = "A listaelem neve maximum 30 karakter lehet.")]
        public String Name { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        public String? Description { get; set; }

        [DataType(DataType.Date, ErrorMessage = "A dátumnak hónap/nap/év formátumban kell megjelennie.")]
        public DateTime Deadline { get; set; }

        public byte[]? Image { get; set; }

        [DisplayName("List")]
        public Int32 ListId { get; set; }

        public static explicit operator Item(ItemViewModel vm) => new Item
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Deadline = vm.Deadline,
            Image = vm.Image,
            ListId = vm.ListId
        };

        public static explicit operator ItemViewModel(Item i) => new ItemViewModel
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Deadline = i.Deadline,
            Image = i.Image,
            ListId = i.ListId
        };
    }
}
