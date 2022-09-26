namespace Spotiflix001
{
    internal class Gui
    {
        Data data = new Data();
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\SpotiflixData.json";
        public Gui()
        {
            //data.MovieList.Add(new Movie() { WWW=@"https:\\netflix.com/rambo3.mp4", Title="Rambo III", Genre ="Action", ReleaseDate=new DateTime(1988,5,25), Length=new DateTime(1,1,1, 1, 42, 0)});
            while (true)
            {
                Menu();
            }
        }
        #region MainMenu&SaveLoad
        private void Menu()
        {
            Console.WriteLine("\nMENU\n1 for movies\n2 for series\n3 for music\n4 for save\n5 for load");

            switch (Console.ReadLine())
            {
                case "1":
                    MovieMenu();
                    break;
                case "2":
                    SeriesMenu();
                    break;
                case "3":
                    MusicMenu();
                    break;
                case "4":
                    SaveData();
                    break;
                case "5":
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
        #endregion

        #region movie
        private void MovieMenu()
        {
            Console.WriteLine("\nMOVIE MENU\n1 for list of movies\n2 for search movies\n3 for add new movie");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowMovieList();
                    break;
                case "2":
                    SearchMovie();
                    break;
                case "3":
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
            movie.Length = GetLength("hh:mm:ss");
            movie.Genre = GetString("Genre: ");
            movie.ReleaseDate = GetReleaseDate();
            movie.WWW = GetString("WWW: ");

            ShowMovie(movie);
            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y) data.MovieList.Add(movie);
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


        #endregion

        #region Music
        private void MusicMenu()
        {
            Console.WriteLine("\nMUSIC MENU\n1 for list of Albums\n2 for search music\n3 for add new Album");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowMusicList();
                    break;
                case "2":
                    SearchMusic();
                    break;
                case "3":
                    AddAlbum();
                    break;
                default:
                    break;
            }
        }

        private void AddAlbum()
        {
            Album album = new Album();
            album.AlbumName = GetString("Album: ");
            album.ArtistName = GetString("Artist: ");
            album.Genre = GetString("Genre: ");
            album.ReleaseDate = GetReleaseDate();
            album.WWW = GetString("WWW: ");

            ShowAlbum(album);
            
            do
            {
                album.AlbumListMusic.Add(AddMusic(album));
                Console.WriteLine("Do you wish to add more music? (Y/N)");
            } while (Console.ReadKey(true).Key == ConsoleKey.Y);

            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y) data.MusicList.Add(album);

        }
        private Music AddMusic(Album album)
        {
            Music music = new Music();
            music.AlbumName = album.AlbumName;
            music.ArtistName = album.ArtistName;
            music.Genre = album.Genre;
            music.ReleaseDate = album.ReleaseDate;
            music.WWW = album.WWW;
            music.Title = GetString("Title: ");
            music.Length = GetMusicLength("mm:ss");
            return music;
        }

        private void SearchMusic()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine();
            foreach (Album album in data.MusicList)
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (album.AlbumName.Contains(search) || album.Genre.Contains(search) || album.ArtistName.Contains(search))
                    {
                        ShowAlbum(album);
                    }

                }
                foreach (Music music in album.AlbumListMusic)
                {
                    if (music.Title.Contains(search) || music.ArtistName.Contains(search) || music.AlbumName.Contains(search) 
                        || music.Genre.Contains(search) )
                    {
                        ShowMusic(music);
                    }
                }
            }
        }

        private void ShowAlbum(Album a)
        {
            Console.WriteLine($"{a.AlbumName} {a.ArtistName} {a.Genre} {a.GetReleaseDate()} {a.WWW}");
        }
        private void ShowMusic(Music m)
        {
            Console.WriteLine($"{m.Title} {m.GetLength()} {m.AlbumName} {m.ArtistName}");
        }
        private void ShowMusicList()
        {
            foreach (Album m in data.MusicList)
            {
                Console.Write("Album: ");
                ShowAlbum(m);

                foreach (Music mu in m.AlbumListMusic)
                {
                    Console.Write("Song: ");
                    ShowMusic(mu);
                }
            }

        }

        #endregion

        #region Series
        private void SeriesMenu()
        {
            Console.WriteLine("\nSERIES MENU\n1 for list of series\n2 for search series\n3 for add new series");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowSeriesList();
                    //ShowEpisodeList();
                    break;
                case "2":
                    SearchSeries();
                    break;
                case "3":
                    AddSeries();
                    break;
                default:
                    break;
            }
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
            } while (Console.ReadKey(true).Key == ConsoleKey.Y);

            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y) data.Serieslist.Add(series);

        }
        private Episode AddEpisode()
        {
            Episode episode = new Episode();
            episode.EpisodeTitle = GetString("Episode Title: ");
            episode.ReleaseDate = GetReleaseDate();
            episode.Length = GetLength("hh:mm:ss");
            episode.EpisodeNum = GetInt("Episode Number: ");
            episode.Season = GetInt("Season Number: ");
            return episode;
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

        private void ShowSeries(Series s)
        {
            Console.WriteLine($"{s.Title} {s.Genre} {s.GetReleaseDate()} {s.WWW}");
        }
        private void ShowEpisode(Episode e)
        {
            Console.WriteLine($"{e.EpisodeTitle} {e.GetReleaseDate()} {e.GetLength()} {e.Season} {e.EpisodeNum}");
        }

        private void ShowSeriesList()
        {
            foreach (Series s in data.Serieslist)
            {
                ShowSeries(s);
                foreach (Episode e in s.Episodes)
                {
                    ShowEpisode(e);
                }

            }
        }

        #endregion

        #region Getters
        private DateTime GetLength(string type)
        {
            DateTime time;
            do
            {
                Console.Write($"Length ({type}): ");
            }
            while (!DateTime.TryParse("0001-01-01 " + Console.ReadLine(), out time));
            return time;
        }
        private DateTime GetMusicLength(string type)
        {
            DateTime time;
            do
            {
                Console.Write($"Length ({type}): ");
            }
            while (!DateTime.TryParse("0001-01-01 " + "00:" + Console.ReadLine(), out time));
            return time;
        }
        private DateTime GetReleaseDate()
        {
            DateTime date;
            string input = "";
            do
            {
                Console.Write("Release Date (dd/mm/yyyy): ");
                input = Console.ReadLine();
                if (input == "") return DateTime.Today;
            }
            while (!DateTime.TryParse(input, out date));
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
        #endregion
    }
}