using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectNotes.Models
{
    public class NoteTag
    {
        [Key]
        [Column(Order = 1)]]
        public int NoteID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int TagID { get; set; }
    }
}
