namespace projectNotes.Models
{
    public class IndexViewModel
    {
        public List<NoteComplete>? CompleteNotes { get; set; }
        public NoteComplete? NewNote { get; set; }
        public int? UpdateNoteID { get; set; }
        public NoteComplete? UpdateNote { get; set; }
        public int? DeleteNoteID { get; set; }
    }
}