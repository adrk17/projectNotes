using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectNotes.Models
{
    public class NoteTag
    {
        public int ID { get; set; }
        public int NoteID { get; set; }
        public int TagID { get; set; }
    }
}
