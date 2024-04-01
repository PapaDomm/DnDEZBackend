namespace DnDEZBackEnd.Models
{
    public partial class DnDBasicObject
    {
        public long Count { get; set; }
        public List<Result> Results { get; set; } = new List<Result>();
    }

    public partial class Result
    {
        public string Index { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
