namespace Spotiflix001
{
    internal class Music : Media
    {
        public string? ArtistName { get; set; }
        public string? Album { get; set; }

        public string GetLength()
        {
            return Length.ToString("mm:ss");
        }
    }
}
