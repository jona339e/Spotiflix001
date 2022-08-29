namespace Spotiflix001
{
    internal class Gui
    {
        Data data = new Data();
        private string path = @"c:\SpotiflixData.json";
        public Gui()
        {
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
                    SeriesMenu();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    MusicMenu();
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
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(path, json);
            Console.WriteLine("File saved succesfully at " + path);
        }
        private void LoadData()
        {
            string json = File.ReadAllText(path);
            //TODO Does File Exists?
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
        private void MusicMenu()
        {
            Console.WriteLine("\nMUSIC MENU\n1 for list of music\n2 for search music\n3 for add new music");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowMusicList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchMusic();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddMusic();
                    break;
                default:
                    break;
            }
        }
        private void SeriesMenu()
        {
            Console.WriteLine("\nSERIES MENU\n1 for list of series\n2 for search series\n3 for add new series");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowSeriesList();
                    //ShowEpisodeList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchSeries();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddSeries();
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
            if (Console.ReadKey().Key == ConsoleKey.Y) data.MovieList.Add(movie);
        }
        private void AddMusic()
        {
            Music music = new Music();
            music.Title = GetString("Title: ");
            music.Length = GetLength();
            music.Genre = GetString("Genre: ");
            music.ReleaseDate = GetReleaseDate();
            music.WWW = GetString("WWW: ");

            ShowMusic(music);
            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.Y) data.MusicList.Add(music);
        }
        private void AddSeries()
        {
            Series series = new Series();
            
            series.Title = GetString("Title: ");
            series.Genre = GetString("Genre: ");
            series.ReleaseDate = GetReleaseDate();
            series.WWW = GetString("WWW: ");
            do
            {
                series.Episodes.Add(AddEpisode());
                Console.WriteLine("Do you wish to add more episodes? (Y/N)");
            } while (Console.ReadKey().Key == ConsoleKey.Y);

            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.Y) data.Serieslist.Add(series);

        }
        private Episode AddEpisode()
        {
            Episode episode = new Episode();
            episode.EpisodeTitle = GetString("Episode Title: ");
            episode.ReleaseDate = GetReleaseDate();
            episode.Length = GetLength();
            episode.EpisodeNum = GetInt("Episode Number: ");
            episode.Season = GetInt("Season Number: ");
            return episode;
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
        private void SearchMusic()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine();
            foreach (Music music in data.MusicList)
            {
                if (search != null)
                {
                    if (music.Title.Contains(search) || music.Genre.Contains(search))
                        ShowMusic(music);
                }
            }
        }
        private void SearchSeries()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine();
            foreach (Series series in data.Serieslist)
            {
                if (search != null)
                {
                    if (series.Title.Contains(search) || series.Genre.Contains(search))
                        ShowSeries(series);
                }
            }
        }

        private void ShowMovie(Movie m)
        {
            Console.WriteLine($"{m.Title} {m.GetLength()} {m.Genre} {m.GetReleaseDate()} {m.WWW}");
        }
        private void ShowMusic(Music m)
        {
            Console.WriteLine($"{m.Title} {m.Genre} {m.GetReleaseDate()} {m.WWW}");
        }
        private void ShowSeries(Series s)
        {
            Console.WriteLine($"{s.Title} {s.Genre} {s.GetReleaseDate()} {s.WWW}");
        }
        private void ShowEpisode(Episode e)
        {
            Console.WriteLine($"{e.EpisodeTitle} {e.GetReleaseDate()} {e.GetLength()} {e.Season} {e.EpisodeNum}");
        }

        private void ShowMovieList()
        {
            foreach (Movie m in data.MovieList)
            {
                ShowMovie(m);
            }
        }
        private void ShowMusicList()
        {
            foreach (Music m in data.MusicList)
            {
                ShowMusic(m);
            }
        }
        private void ShowSeriesList()
        {
            foreach (Series s in data.Serieslist)
            {
                ShowSeries(s);
            }
        }
        //private void ShowEpisodeList()
        //{

        //    foreach (Series s in data.Serieslist)
        //    {

        //        if (s == s.Episodes)
        //        {
                    
        //            foreach (Episode e in s.Episodes)
        //            {
        //                ShowEpisode(e);
        //            }

        //        }
                
        //    }
        //}

        private DateTime GetLength()
        {
            DateTime time;
            do
            {
                Console.Write("Length (hh:mm:ss): ");
            }
            while (!DateTime.TryParse("0001-01-01 " + Console.ReadLine(), out time));
            return time;
        }
        private DateTime GetReleaseDate()
        {
            DateTime date;
            do
            {
                Console.Write("Release Date (dd/mm/yyyy): ");
            }
            while (!DateTime.TryParse(Console.ReadLine(), out date));
            return date;
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
        private int GetInt(string type)
        {
            int input;
            do
            {
                Console.Write(type);
                int.TryParse(Console.ReadLine(), out input);
            }
            while (input == null || input == 0);
            return input;
        }
    }
}