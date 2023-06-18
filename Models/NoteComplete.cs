using System.Text;

namespace projectNotes.Models
{
    public class NoteComplete
    {
        public Note? Note { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Category>? Categories { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Title:");
            sb.AppendLine(" "+Note.Title);
            sb.AppendLine("Context:");
            sb.AppendLine(" "+Note.Context);
            sb.AppendLine("Tags:");
            foreach (var tag in Tags)
            {
                sb.AppendLine(" "+tag.Name);
            }
            sb.AppendLine("Categories:");
            foreach (var category in Categories)
            {
                sb.AppendLine(" "+category.Name);
            }
            return sb.ToString();
        }

    }
}
