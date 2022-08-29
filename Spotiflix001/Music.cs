namespace Spotiflix001
{
    internal class Album : Media
    {
        public string? ArtistName { get; set; }
        public string? AlbumName { get; set; }
        public List<Music> AlbumListMusic { get; set; } = new();

        public string GetLength()
        {
            return Length.ToString("mm:ss");
        }
    }
    class Music : Album
    {

    }
}
