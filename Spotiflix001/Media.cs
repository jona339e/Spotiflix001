namespace Spotiflix001
{
    // Abstract makes it so you can't create instances of the class
    internal abstract class Media
    {
        public string? Title { get; set; } = "test";
        public DateTime Length { get; set; }
        public string? Genre { get; set; } = "test";
        public DateTime ReleaseDate { get; set; }
        public string? WWW { get; set; } = "test";

        public string GetLength()
        {
            return Length.ToString("hh:mm");
        }
        public string GetReleaseDate()
        {
            return ReleaseDate.ToString("D");
        }
    }
}
