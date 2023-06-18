using System.Text;

namespace projectNotes.Models
{
    public class NoteComplete
    {
        public Note? Note { get; set; }
        public List<Tag>? Tags { get; set; }
        public Category? Category { get; set; }

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
            
            sb.AppendLine("Category:");
            if(Category != null)
                sb.AppendLine(" " + Category.Name);
            return sb.ToString();
        }

        public string? TagString { get; set; }

        public string? CategoryString { get; set; }

        public void ConvertTags()
        {
            if (TagString == null) { Tags = new List<Tag>(); return;}

            TagString = TagString.ToLower();
            TagString = TagString.Replace(" ", "");

            var tags = TagString.Split(',');
            Tags = new List<Tag>();
            foreach (var tag in tags)
            {
                Tags.Add(new Tag { Name = tag });
            }
        }

        public void ConvertCategory()
        {
            if (CategoryString == null) { Category = new Category(); return;}

            Category = new Category { Name = CategoryString };
        }
    }
}
