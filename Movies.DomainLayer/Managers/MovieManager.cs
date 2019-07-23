using Movies.DomainLayer.Managers.DomainModels;
using Movies.DomainLayer.Managers.Enums;
using Movies.DomainLayer.Managers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.DomainLayer.Managers
{
    internal sealed class MovieManager
    {
        private static List<Movie> s_AllMovies = new List<Movie>
        {
                new Movie("Star Wars Episode IV: A New Hope", "StarWarsEpisodeIV.jpg", Genre.SciFi, 1977),
                new Movie("Star Wars Episode V: The Empire Strikes Back", "StarWarsEpisodeV.jpg", Genre.SciFi, 1980),
                new Movie("Star Wars Episode VI: Return of the Jedi", "StarWarsEpisodeVI.jpg", Genre.SciFi, 1983),
                new Movie("Star Wars: Episode I: The Phantom Menace", "StarWarsEpisodeI.jpg", Genre.SciFi, 1999),
                new Movie("Star Wars Episode II: Attack of the Clones", "StarWarsEpisodeII.jpg", Genre.SciFi, 2002),
                new Movie("Star Wars: Episode III: Revenge of the Sith", "StarWarsEpisodeIII.jpg", Genre.SciFi, 2005),
                new Movie("Olympus Has Fallen", "Olympus_Has_Fallen_poster.jpg", Genre.Action, 2013),
                new Movie("G.I. Joe: Retaliation", "GIJoeRetaliation.jpg", Genre.Action, 2013),
                new Movie("Jack the Giant Slayer", "jackgiantslayer4.jpg", Genre.Action, 2013),
                new Movie("Drive", "FileDrive2011Poster.jpg", Genre.Action, 2011),
                new Movie("Sherlock Holmes", "FileSherlock_Holmes2Poster.jpg", Genre.Action, 2009),
                new Movie("The Girl with the Dragon Tatoo", "FileThe_Girl_with_the_Dragon_Tattoo_Poster.jpg", Genre.Drama, 2011),
                new Movie("Saving Private Ryan", "SavingPrivateRyan.jpg", Genre.Drama, 1998),
                new Movie("Schindlers List", "SchindlersList.jpg", Genre.Drama, 1993),
                new Movie("Good Will Hunting", "FileGood_Will_Hunting_theatrical_poster.jpg", Genre.Drama, 1997),
                new Movie("Citizen Kane", "Citizenkane.jpg", Genre.Drama, 1941),
                new Movie("Shawshank Redemption", "FileShawshankRedemption.jpg", Genre.Drama, 1994),
                new Movie("Forest Gump", "ForrestGump.jpg", Genre.Drama, 1994),
                new Movie("We Bought a Zoo", "FileWe_Bought_a_Zoo_Poster.jpg", Genre.Drama, 2011),
                new Movie("A Beautiful Mind", "FileAbeautifulmindposter.jpg", Genre.Drama, 2001),
                new Movie("Avatar", "Avatar.jpg", Genre.SciFi, 2009),
                new Movie("Iron Man", "IronMan.jpg", Genre.SciFi, 2008),
                new Movie("Terminator 2", "Terminator2.jpg", Genre.SciFi, 1991),
                new Movie("The Dark Knight", "TheDarkKnight.jpg", Genre.SciFi, 2001),
                new Movie("The Matrix", "TheMatrix.jpg", Genre.SciFi, 1999),
                new Movie("Transformers", "Transformers.jpg", Genre.SciFi, 2007),
                new Movie("Revenge Of The Fallen", "TransformersRevengeOfTheFallen.jpg", Genre.SciFi, 2009),
                new Movie("The Dark of the Moon", "TransformersTheDarkoftheMoon.jpg", Genre.SciFi, 2011),
                new Movie("X-Men First Class", "XMenFirstClass.jpg", Genre.SciFi, 2011),
                new Movie("Snitch", "Snitch.jpg", Genre.Thriller, 2013),
                new Movie("Life Of Pi", "LifeOfPi.jpg", Genre.Drama, 2012),
                new Movie("The Call", "TheCall.jpg", Genre.Thriller, 2013),
                new Movie("Wake in Fright", "WakeInFright.jpg", Genre.Thriller, 1971),
                new Movie("Oblivion", "Oblivion.jpg", Genre.SciFi, 2013),
                new Movie("American Sniper", "AmericanSniper.jpg", Genre.Thriller, 2015),
                new Movie("Run All Night", "RunAllNight.jpg", Genre.Thriller, 2015),
                new Movie("Mission: Impossible - Rogue Nation", "MissionImpossibleRogueNation.jpg", Genre.Thriller, 2015),
                new Movie("Spectre", "Spectre.jpg", Genre.Thriller, 2015),
                new Movie("Insurgent", "Insurgent.jpg", Genre.Thriller, 2015),
                new Movie("Kill Me Three Times", "KillMeThreeTimes.jpg", Genre.Thriller, 2014),
                new Movie("Batman v Superman: Dawn of Justice", "BatmanVSupermanDawnofJustice.jpg", Genre.Action, 2016),
                new Movie("Avengers: Age of Ultron", "AvengersAgeofUltron.jpg", Genre.Action, 2015),
                new Movie("Guardians of the Galaxy", "GuardiansoftheGalaxy.jpg", Genre.Action, 2015),
                new Movie("Kingsman: The Secret Service", "KingsmanTheSecretService.jpg", Genre.Action, 2014),
                new Movie("Seventh Son", "SeventhSon.jpg", Genre.Action, 2014),
                new Movie("Maze Runner: The Scorch Trials", "MazeRunnerTheScorchTrials.jpg", Genre.Thriller, 2015)
        };

        public IEnumerable<Movie> GetMoviesByGenre(Genre genre)
        {
            if (genre == Genre.NA)
                throw new InvalidGenreException(new ExceptionMessageDetail("Please provide a valid Genre. Possible values are: " + GenreOptionsAsString(), "genre"));

            return GetAllMovies().Where(m => m.Genre == genre).OrderByDescending(m => m.Year);
        }

        public IEnumerable<Movie> GetMoviesByYear(int year)
        {
            return GetAllMovies().Where(m => m.Year == year);
        }

        private string GenreOptionsAsString()
        {
            var names = Enum.GetNames(typeof(Genre));
            var validNames = new string[names.Length - 1];
            Array.Copy(names, 1, validNames, 0, validNames.Length);
            return string.Join(", ", validNames);
        }

        private IEnumerable<Movie> GetAllMovies()
        {
            return s_AllMovies;
        }
    }
}
