namespace Spotiflix001
{
    internal class Series : Media
    {
        public List<Episode> Episodes { get; set; } = new();

    }
    class Episode : Series
    {
        public string? EpisodeTitle { get; set; }
        public int Season { get; set; }
        public int EpisodeNum { get; set; }

    }


}
