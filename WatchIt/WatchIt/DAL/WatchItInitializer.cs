using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using WatchIt.Models;

namespace WatchIt.DAL
{
    public class WatchItInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<WatchItContext>
    {
        protected override void Seed(WatchItContext context)
        {       
            var customer = new List<Customer>
            {
         //   new Customer{FirstName="Zach",LastName="Halali",Email="Zach.halali@gmail.com",Gender = 0,Password="1", City="Ramat-Gan", Street="Hapodim"},
          //  new Customer{FirstName="Zach2",LastName="Halali2",Email="Zach.halali@gmail.com",Gender = 0,Password="12", City="Ramat-Gan", Street="Hapodim"},
           // new Customer{FirstName="Zach3",LastName="Halali3",Email="Zach.halali@gmail.com",Gender = 0,Password="123", City="Ramat-Gan", Street="Hapodim"},
            //new Customer{FirstName="Zach4",LastName="Halali4",Email="Zach.halali@gmail.com",Gender = 0,Password="1234", City="Ramat-Gan", Street="Hapodim"},        
            };

            //            customer.ForEach(s => context.Customers.Add(s));
            //          context.SaveChanges();

            var directors = new List<Director>
            {
                new Director {ID=1, Name="David Fincher", PlaceOfBirth="Denver, Colorado, USA", Description="David Fincher was born in 1962 in Denver, Colorado, and was raised in Marin County, California. When he was 18 years old he went to work for John Korty at Korty Films in Mill Valley. He subsequently worked at ILM (Industrial Light and Magic) from 1981-1983. Fincher left ILM to direct TV commercials and music videos after signing with N. Lee Lacy in Hollywood. He went on to found Propaganda in 1987 with fellow directors Dominic Sena, Greg Gold and Nigel Dick.", NominatedNum=2, BirthDate=DateTime.Parse("1995-04-10"), Image="/Images/directors/DAVID_FINCHER.jpg"},
                new Director {ID=2, Name="Christopher Nolan", PlaceOfBirth="London, England, UK", Description="Best known for his cerebral, often nonlinear, storytelling, acclaimed writer-director Christopher Nolan was born on July 30, 1970 in London, England. Over the course of 15 years of filmmaking, Nolan has gone from low-budget independent films to working on some of the biggest blockbusters ever made.", NominatedNum=5, BirthDate=DateTime.Parse("1970-07-30"), Image="/Images/directors/CHRISTOPHER_NOLAN.jpg"},
                new Director {ID=3, Name="Quentin Tarantino", PlaceOfBirth="Knoxville, Tennessee, USA", Description="Known for his unpredictable, violent films, Quentin Tarantino first earned widespread fame for 'Pulp Fiction,' before going on to direct 'Inglourious Basterds' and 'Django Unchained.", NominatedNum=6, BirthDate=DateTime.Parse("1970-07-30"), Image="/Images/directors/QUANTIN_TARANTINO.jpg"},
                new Director {ID=4, Name="Martin Scorsese", PlaceOfBirth="New York City, New York, USA", Description="Director Martin Scorsese has produced some of the most memorable films in cinema history, including the iconic 'Taxi Driver' and Academy Award-winning 'The Departed.", NominatedNum=4, BirthDate=DateTime.Parse("1949-07-22"), Image="/Images/directors/Martin Scorsese.jpg"},
                new Director {ID=5, Name="Edgar Wright", PlaceOfBirth="Dorset, England, UK", Description="Edgar Howard Wright (born 18 April 1974) is an English director, screenwriter and producer. He began making independent short films before making his first feature film A Fistful of Fingers (1995). Wright created and directed the comedy series Asylum in 1996, written with David Walliams. After directing several other television shows, Wright directed the sitcom Spaced (1999–2001), which aired for two series and starred frequent collaborators Simon Pegg and Nick Frost.", NominatedNum=0, BirthDate=DateTime.Parse("1974-04-18"), Image="/Images/directors/Edgar Wright.jpg"}
            };
            directors.ForEach(b => context.Directors.AddOrUpdate(p => p.ID, b));
            context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie { ID = 1, Title = "Fight club", Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.",
                    Genre = Genre.Drama, Image = "Images/movies/FIGHT_CLUB.jpg",
                    Price =60, Length=140, Rating=8.8, DirectorID=1, ReleaseDate = DateTime.Parse("2008-01-07") },
                new Movie { ID = 2, Title = "The Dark Knight", Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    Genre = Genre.Action, Image = "Images/movies/THE_DARK_KNIGHT.jpg",
                    Price =60, Length=140, Rating=9.0, DirectorID=2, ReleaseDate = DateTime.Parse("2015-10-06") },
                 new Movie { ID = 3, Title = "Deadpool 2", Description = "Foul-mouthed mutant mercenary Wade Wilson (AKA. Deadpool), brings together a team of fellow mutant rogues to protect a young boy with supernatural abilities from the brutal, time-traveling cyborg, Cable.",
                    Genre = Genre.Comedy, Image = "Images/movies/DEADPOOL_2.jpg",
                    Price =40, Length=119, Rating=7.9, DirectorID=3, ReleaseDate = DateTime.Parse("2018-05-17") },
                 new Movie { ID = 26, Title ="Venom" ,Description = "When Eddie Brock acquires the powers of a symbiote, he will have to release his alter-ego Venom to save his life.",
                    Genre = Genre.Superhero, Image = "Images/movies/VenWom.jpg",
                    Price =35, Length=89, Rating=7.1, DirectorID = 2 , ReleaseDate = DateTime.Parse("2018-10-14")}

            };
            movies.ForEach(b => context.Movies.AddOrUpdate(p => p.ID, b));
            context.SaveChanges();
        }
    }
}