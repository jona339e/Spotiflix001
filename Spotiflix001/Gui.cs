namespace OOPSpotiflixV2
{
    internal class Gui
    {
        //private List<Movie> movieList;
        Data data = new Data();
        private string path = @"c:\SpotiflixData.json";
        public Gui()
        {
            data.MovieList = new();
            //data.MovieList.Add(new Movie() { WWW=@"https:\\netflix.com/rambo3.mp4", Title="Rambo III", Genre ="Action", ReleaseDate=new DateTime(1988,5,25), Length=new DateTime(1,1,1, 1, 42, 0)});
            while (true)
            {
                Menu();
            }
        }

        private void Menu()
        {
            Console.WriteLine("\nMENU\n1 for movies\n2 for series\n3 for music\n4 for save\n5 for load");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    MovieMenu();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    SaveData();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    LoadData();
                    break;
                default:
                    break;
            }
        }
        private void SaveData()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(path, json);
            Console.WriteLine("File Saved Succesfully at " + path);
        }

        private void LoadData()
        {
            string json = File.ReadAllText(path);
            data = System.Text.Json.JsonSerializer.Deserialize<Data>(json);
            Console.WriteLine("File loaded succesfully from " + path);
        }

        private void MovieMenu()
        {
            Console.WriteLine("\nMOVIE MENU\n1 for list of movies\n2 for search movies\n3 for add new movie");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowMovieList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchMovie();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddMovie();
                    break;

                default:
                    break;
            }
        }

        private void AddMovie()
        {
            Movie movie = new Movie();
            movie.Title = GetString("Title: ");
            movie.Length = GetLength();
            movie.Genre = GetString("Genre: ");
            movie.ReleaseDate = GetReleaseDate();
            movie.WWW = GetString("WWW: ");

            ShowMovie(movie);
            Console.WriteLine("Confirm adding to list (Y/N)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Y:
                    data.MovieList.Add(movie);
                    break;
                default:
                    break;
            }
        }

        private void SearchMovie()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine();
            foreach (Movie movie in data.MovieList)
            {
                if (search != null)
                {
                    if (movie.Title.Contains(search) || movie.Genre.Contains(search))
                        ShowMovie(movie);
                }
            }
        }

        private DateTime GetLength()
        {
            DateTime to;
            do
            {
                Console.Write("Length (hh:mm:ss): ");
            }
            while (!DateTime.TryParse("01-01-0001 " + Console.ReadLine(), out to));
            return to;
        }
        private DateTime GetReleaseDate()
        {
            DateTime dateOnly;
            do
            {
                Console.Write("Release Date (dd/mm/yyyy): ");
            }
            while (!DateTime.TryParse(Console.ReadLine(), out dateOnly));
            return dateOnly;
        }
        private string GetString(string type)
        {
            string? input;
            do
            {
                Console.Write(type);
                input = Console.ReadLine();
            }
            while (input == null || input == "");
            return input;
        }
        private void ShowMovie(Movie m)
        {
            Console.WriteLine($"{m.Title} {m.GetLength()} {m.Genre} {m.GetReleaseDate()} {m.WWW}");
        }

        private void ShowMovieList()
        {
            foreach (Movie m in data.MovieList)
            {
                ShowMovie(m);
            }
        }
    }
}
