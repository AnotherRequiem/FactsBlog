namespace Requiem.Facts.Web.Data
{
    public class Fact: Auditable
    {
        public string Content { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public int Number { get; set; }
    }
}
