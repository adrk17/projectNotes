namespace projectNotes.Models
{
    public class IndexViewModel
    {
        public List<NoteComplete>? CompleteNotes { get; set; }
        public NoteComplete? NewNote { get; set; }
        public NoteComplete? UpdateNote { get; set; }
        public NoteComplete? DeleteNote { get; set; }
    }
}