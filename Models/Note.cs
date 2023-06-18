using System.ComponentModel.DataAnnotations;

namespace projectNotes.Models
{
    public class Note
    {
        public int ID { get; set; }
        [MinLength(1)]
        public string Title { get; set; }
        public string Context { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int UserID { get; set; }
    }
}
