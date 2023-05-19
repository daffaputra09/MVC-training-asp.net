using System.ComponentModel.DataAnnotations;

namespace WebStatisMVC.Models
{
    public class Articles
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? excerpt { get; set; }

        [DataType(DataType.Date)]
        public DateTime publish_date { get; set; }
        public string? featured_image { get; set; }
        public string? author { get; set; }
        public int time_read { get; set; }
        public string? category { get; set; }

    }
}
