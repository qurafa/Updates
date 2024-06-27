namespace Updates.Entities
{
    public class Update
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string Description { get; set; }
    }
}
