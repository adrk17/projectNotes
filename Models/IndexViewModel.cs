using Microsoft.AspNetCore.Mvc;

namespace projectNotes.Models
{
    public class IndexViewModel
    {
        public List<NoteComplete>? CompleteNotes { get; set; }
        public NoteComplete? NewNote { get; set; }
        public int? UpdateNoteID { get; set; }
        public NoteComplete? UpdateNote { get; set; }
        public int? DeleteNoteID { get; set; }

        public List<Category>? FilteredCategories { get; set; }
        public List<Tag>? FilteredTags { get; set; }

  
        public string? FiltString { get; set; }
        public string? FiltOption { get; set; }

        public string? SortOption { get; set; }
    }
}