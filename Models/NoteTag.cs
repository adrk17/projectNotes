using System.ComponentModel.DataAnnotations;

namespace projectNotes.Models
{
    public class NoteTag
    {
        [Key]
        public int NoteID { get; set; }
        public int TagID { get; set; }
    }
}
