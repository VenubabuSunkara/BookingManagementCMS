namespace BookingManagementCMS.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string AuthorUrl { get; set; } = string.Empty;

    }
}
