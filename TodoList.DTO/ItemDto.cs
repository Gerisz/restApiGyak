namespace ELTE.TodoList.DTO
{
	public class ItemDto
	{
		public Int32 Id { get; set; }

		public String Name { get; set; } = null!;

		public String? Description { get; set; }

		public DateTime Deadline { get; set; }

		public byte[]? Image { get; set; }

		public Int32 ListId { get; set; }
	}
}