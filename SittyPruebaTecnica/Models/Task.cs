namespace SittyPruebaTecnica.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }

}
