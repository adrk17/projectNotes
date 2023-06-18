using System.ComponentModel.DataAnnotations;

namespace projectNotes.Models
{
    public class NoteCategory
    {
        [Key]
        public int NoteID { get; set; }
        public int CategoryID { get; set; }
       
    }
}
