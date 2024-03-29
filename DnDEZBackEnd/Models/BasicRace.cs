namespace DnDEZBackEnd.Models
{
    public partial class BasicRace
    {
        public long Count { get; set; }
        public List<Result> Results { get; set; }
    }

    public partial class Result
    {
        public string Index { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
