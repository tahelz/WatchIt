using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WatchIt.Models;

namespace WatchIt.DAL
{
    public class WatchItInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<WatchItContext>
    {
        protected override void Seed(WatchItContext context)
        {
            var customers = new List<Customer>
            {
               new Customer{CustomerID=1, FirstName="Zach",  LastName="Halali",   Email="Zach.halali@gmail.com",    BirthDate = DateTime.Parse("1997-02-06"), Gender = Gender.Male,   Password="1234567", City="Ramat-Gan",  Street="Hapodim"},
               new Customer{CustomerID=2, FirstName="Yarden",LastName="Markus",   Email="Yarden.markus@gmail.com",  BirthDate = DateTime.Parse("1997-01-01"), Gender = Gender.Female, Password="1234567", City="Tel Aviv",   Street="Dizengoff"},
               new Customer{CustomerID=3, FirstName="Tahel", LastName="Zohar",    Email="Tahel.zohar@gmail.com",    BirthDate = DateTime.Parse("2000-11-06"), Gender = Gender.Female, Password="1234567", City="Haifa" },
               new Customer{CustomerID=4, FirstName="Loren", LastName="Goldstein",Email="Loren.Goldstein@gmail.com",BirthDate = DateTime.Parse("1986-01-16"), Gender = Gender.Female, Password="1234567", City="Ramat-Gan",  Street="Jabotinsky"},        
               new Customer{CustomerID=5, FirstName="admin", LastName="admin",    Email="admin@gmail.com",          BirthDate = DateTime.Parse("1956-01-07"), Gender = Gender.Other,  Password="1234567", City="Top-Secret", Street="Top-Secret", IsAdmin = true },        
            };

            customers.ForEach(b => context.Customers.AddOrUpdate(p => p.CustomerID, b));
            context.SaveChanges();

            var branches = new List<Branch>
            {
                new Branch {BranchID = 1, BranchCity= "Tel Aviv-yafo", BranchName = "Tel Aviv-yafo", BranchsPhoneNumber = "03-1855884", BranchStreet = "Dizengoff 5 ",   BranchLat = 32.074498, BranchLng = 34.784922},
                new Branch {BranchID = 2, BranchCity= "Jerusalem",     BranchName = "Jerusalem",     BranchsPhoneNumber = "02-1566342", BranchStreet = "Mamila 34",      BranchLat = 31.777988, BranchLng = 35.224271},
                new Branch {BranchID = 3, BranchCity= "Ramat Gan",     BranchName = "Ramat Gan",     BranchsPhoneNumber = "03-9125551", BranchStreet = "Haroeh 8",       BranchLat = 32.083333, BranchLng = 34.8166634},
                new Branch {BranchID = 4, BranchCity= "Givatayim",     BranchName = "Givatayim",     BranchsPhoneNumber = "09-8765942", BranchStreet = "Katzenelson 50", BranchLat = 32.075100, BranchLng = 34.807570}
            };

            branches.ForEach(b => context.Branches.AddOrUpdate(p => p.BranchID, b));
            context.SaveChanges();

            var directors = new List<Director>
            {
                new Director {ID=1, Name="David Fincher",     PlaceOfBirth="Denver, Colorado, USA",        Description="David Fincher was born in 1962 in Denver, Colorado, and was raised in Marin County, California. When he was 18 years old he went to work for John Korty at Korty Films in Mill Valley. He subsequently worked at ILM (Industrial Light and Magic) from 1981-1983. Fincher left ILM to direct TV commercials and music videos after signing with N. Lee Lacy in Hollywood. He went on to found Propaganda in 1987 with fellow directors Dominic Sena, Greg Gold and Nigel Dick.",           NominatedNum=2, BirthDate=DateTime.Parse("1995-04-10"), Image="/Images/directors/DAVID_FINCHER.jpg"},
                new Director {ID=2, Name="Christopher Nolan", PlaceOfBirth="London, England, UK",          Description="Best known for his cerebral, often nonlinear, storytelling, acclaimed writer-director Christopher Nolan was born on July 30, 1970 in London, England. Over the course of 15 years of filmmaking, Nolan has gone from low-budget independent films to working on some of the biggest blockbusters ever made.",                                                                                                                                                                              NominatedNum=5, BirthDate=DateTime.Parse("1970-07-30"), Image="/Images/directors/CHRISTOPHER_NOLAN.jpg"},
                new Director {ID=3, Name="Quentin Tarantino", PlaceOfBirth="Knoxville, Tennessee, USA",    Description="Known for his unpredictable, violent films, Quentin Tarantino first earned widespread fame for 'Pulp Fiction,' before going on to direct 'Inglourious Basterds' and 'Django Unchained.",                                                                                                                                                                                                                                                                                                   NominatedNum=6, BirthDate=DateTime.Parse("1970-07-30"), Image="/Images/directors/QUANTIN_TARANTINO.jpg"},
                new Director {ID=4, Name="Martin Scorsese",   PlaceOfBirth="New York City, New York, USA", Description="Director Martin Scorsese has produced some of the most memorable films in cinema history, including the iconic 'Taxi Driver' and Academy Award-winning 'The Departed.",                                                                                                                                                                                                                                                                                                                    NominatedNum=4, BirthDate=DateTime.Parse("1949-07-22"), Image="/Images/directors/MARTIN_SCORSESE.jpg"},
                new Director {ID=5, Name="Edgar Wright",      PlaceOfBirth="Dorset, England, UK",          Description="Edgar Howard Wright (born 18 April 1974) is an English director, screenwriter and producer. He began making independent short films before making his first feature film A Fistful of Fingers (1995). Wright created and directed the comedy series Asylum in 1996, written with David Walliams. After directing several other television shows, Wright directed the sitcom Spaced (1999–2001), which aired for two series and starred frequent collaborators Simon Pegg and Nick Frost.", NominatedNum=0, BirthDate=DateTime.Parse("1974-04-18"), Image="/Images/directors/EDGAR_WRIGHT.jpg"}
            };

            directors.ForEach(b => context.Directors.AddOrUpdate(p => p.ID, b));
            context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie { ID = 1, Title = "The Lion King", Description = "After the murder of his father, a young lion prince flees his kingdom only to learn the true meaning of responsibility and bravery.",
                    Genre = Genre.Adventure, Image = "/Images/movies/THE_LION_KING.jpg",
                    Price =10, Length=118, Rating=7.2, DirectorID=1, ReleaseDate = DateTime.Parse("2019-07-18"),
                    Trailer = "/Trailers/The_Lion_King_Official_Trailer.mp4"},

                new Movie { ID = 2, Title = "The Dark Knight", Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    Genre = Genre.Superhero, Image = "/Images/movies/THE_DARK_KNIGHT.jpg",
                    Price =13, Length=140, Rating=9.0, DirectorID=2, ReleaseDate = DateTime.Parse("2015-10-06"),
                    Trailer = "/Trailers/The_Dark_Knight_Official_Trailer.mp4"},

                new Movie { ID = 3, Title = "Fast And Furious: Hobbs And Shaw", Description = "Lawman Luke Hobbs and outcast Deckard Shaw form an unlikely alliance when a cyber-genetically enhanced villain threatens the future of humanity.",
                    Genre = Genre.Action, Image = "/Images/movies/FAST_AND_FURIOUS.jpg",
                    Price =11, Length=135, Rating=7.3, DirectorID=3, ReleaseDate = DateTime.Parse("2019-08-01"),
                    Trailer = "/Trailers/Fast_And_Furious_Official_Trailer.mp4"},

                new Movie { ID = 4, Title ="Annabelle Comes Home" ,Description = "While babysitting the daughter of Ed and Lorraine Warren, a teenager and her friend unknowingly awaken an evil spirit trapped in a doll.",
                    Genre = Genre.Horror, Image = "/Images/movies/ANNABELLE.jpg",
                    Price =12, Length=110, Rating=6.2, DirectorID = 2 , ReleaseDate = DateTime.Parse("2019-07-11"),
                    Trailer = "/Trailers/Annabelle_Comes_Home_Official_Trailer.mp4"},

                new Movie { ID = 5, Title ="Stuber" ,Description = "A detective recruits his Uber driver into an unexpected night of adventure.",
                    Genre = Genre.Comedy, Image = "/Images/movies/STUBER.jpg",
                    Price =15, Length=105, Rating=6.2, DirectorID = 2 , ReleaseDate = DateTime.Parse("2019-07-11"),
                    Trailer = "Stuber_Official_Trailer.mp4"},
                    
                new Movie { ID = 6, Title ="Escape Plan: The Extractors" ,Description = "After security expert Ray Breslin is hired to rescue the kidnapped daughter of a Hong Kong tech mogul from a formidable Latvian prison, Breslin's girlfriend is also captured.",
                    Genre = Genre.Action, Image = "/Images/movies/ESCAPE_PLAN.jpg",
                    Price =18, Length=97, Rating=4.5, DirectorID = 2 , ReleaseDate = DateTime.Parse("2019-07-04"),
                    Trailer = "/Trailers/Escape_Plan_The_Extractors_Official_Trailer.mp4"},

                new Movie { ID = 7, Title ="Spider-Man Far From Home" ,Description = "Following the events of Avengers: Endgame (2019), Spider-Man must step up to take on new threats in a world that has changed forever.",
                    Genre = Genre.Superhero, Image = "/Images/movies/SPIDER_MAN.jpg",
                    Price =11, Length=130, Rating=7.9, DirectorID = 2 , ReleaseDate = DateTime.Parse("2019-07-04"),
                    Trailer = "/Trailers/Spider_Man_Far_From_Home_Official_Trailer.mp4"},

                new Movie { ID = 8, Title ="Late Night" ,Description = "A late night talk show host suspects that she may soon lose her long-running show.",
                    Genre = Genre.Drama, Image = "/Images/movies/LATE_NIGHT.jpg",
                    Price =18, Length=100, Rating=6.5, DirectorID = 2 , ReleaseDate = DateTime.Parse("2019-06-27"),
                    Trailer = "/Trailers/Late_Night_Official_Trailer.mp4 "},

                new Movie { ID = 9, Title ="Toy Story 4" ,Description = "When a new toy called 'Forky' joins Woody and the gang, a road trip alongside old and new friends reveals how big the world can be for a toy.",
                    Genre = Genre.Animation, Image = "/Images/movies/TOY_STORY.jpg",
                    Price =18, Length=100, Rating=8.2, DirectorID = 2 , ReleaseDate = DateTime.Parse("2019-06-20"),
                    Trailer = "Toy_Story_4 _Official_Trailer.mp4"},
                   
                new Movie { ID = 10, Title ="Aladdin" ,Description = "A kind-hearted street urchin and a power-hungry Grand Vizier vie for a magic lamp that has the power to make their deepest wishes come true.",
                    Genre = Genre.Adventure, Image = "/Images/movies/ALADDIN.jpg",
                    Price =24, Length=129, Rating=7.4, DirectorID = 2 , ReleaseDate = DateTime.Parse("2018-05-23"),
                    Trailer = "/Trailers/Aladdin_Official_Trailer.mp4"},

                new Movie { ID = 11, Title ="Avenger EndGame" ,Description = "After the devastating events of Avengers: Infinity War (2018), the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.",
                    Genre = Genre.Superhero, Image = "/Images/movies/AVENGER.jpg",
                    Price =29, Length=189, Rating=8.7, DirectorID = 3 , ReleaseDate = DateTime.Parse("2018-04-25"),
                    Trailer = "/Trailers/Avengers_Endgame_Official_Trailer.mp4"},

                new Movie { ID = 12, Title ="The Conjuring 2" ,Description = "Ed and Lorraine Warren travel to North London to help a single mother raising 4 children alone in a house plagued by a supernatural spirit.",
                    Genre = Genre.Horror, Image = "/Images/movies/THE_CONJURING.jpg",
                    Price =19, Length=134, Rating=7.7, DirectorID = 3 , ReleaseDate = DateTime.Parse("2016-06-10"),
                    Trailer = "/Trailers/The_Conjuring_2_Official_Trailer.mp4"},

                new Movie { ID = 13, Title ="Let's Be Cops" ,Description = "Two struggling pals dress as police officers for a costume party and become neighborhood sensations. But when these newly-minted 'heroes' get tangled in a real life web of mobsters and dirty detectives, they must put their fake badges on the line.",
                    Genre = Genre.Comedy, Image = "/Images/movies/LETS_BE_COPS.jpg",
                    Price =29, Length=189, Rating=8.7, DirectorID = 3 , ReleaseDate = DateTime.Parse("2018-04-25"),
                    Trailer = "/Trailers/Let's_Be_Cops_Official_Trailer.mp4"},

                new Movie { ID = 14, Title ="Titanic" ,Description = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.",
                    Genre = Genre.Romance, Image = "/Images/movies/TITANIC.jpg",
                    Price =21, Length=194, Rating=7.8, DirectorID = 3 , ReleaseDate = DateTime.Parse("1997-12-19"),
                    Trailer = "/Trailers/Titanic_Official_Trialer.mp4"},

                new Movie { ID = 15, Title ="Get Out" ,Description = "A young African-American visits his white girlfriend's parents for the weekend, where his simmering uneasiness about their reception of him eventually reaches a boiling point.",
                    Genre = Genre.Thriller, Image = "/Images/movies/GET_OUT.jpg",
                    Price =16, Length=104, Rating=7.7, DirectorID = 3 , ReleaseDate = DateTime.Parse("2017-02-24"),
                    Trailer = "/Trailers/Get_Out_Official_Trailer.mp4"},

                new Movie { ID = 16, Title ="The Godfather" ,Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    Genre = Genre.Crime, Image = "/Images/movies/THE_GODFATHER.jpg",
                    Price =23, Length=174, Rating=9.2, DirectorID = 3 , ReleaseDate = DateTime.Parse("1972-03-24"),
                    Trailer = "/Trailers/The_Godfather_Official_Trailer.mp4"},

                new Movie { ID = 17, Title ="Now You See Me 2" ,Description = "The Four Horsemen resurface, and are forcibly recruited by a tech genius to pull off their most impossible heist yet.",
                    Genre = Genre.Mystery, Image = "/Images/movies/NOW_YOU_SEE_ME.jpg",
                    Price =16, Length=129, Rating=6.5, DirectorID = 3 , ReleaseDate = DateTime.Parse("2016-06-10"),
                    Trailer = "/Trailers/Now_You_See_Me_2_Official_Trailer.mp4"},

                new Movie { ID = 18, Title ="Sherlock Holmes" ,Description = "Detective Sherlock Holmes and his stalwart partner Watson engage in a battle of wits and brawn with a nemesis whose plot is a threat to all of England.",
                    Genre = Genre.Mystery, Image = "/Images/movies/SHERLOCK_HOLMES.jpg",
                    Price =12, Length=128, Rating=6.5, DirectorID = 3 , ReleaseDate = DateTime.Parse("2009-09-25"),
                    Trailer = "/Trailers/Sherlock_Holmes_Official_Trailer.mp4"},

                new Movie { ID = 19, Title ="Shutter Island" ,Description = "In 1954, a U.S. Marshal investigates the disappearance of a murderer, who escaped from a hospital for the criminally insane.",
                    Genre = Genre.Mystery, Image = "/Images/movies/SHUTTER_ISLAND.jpg",
                    Price =18, Length=138, Rating=8.1, DirectorID = 4 , ReleaseDate = DateTime.Parse("2010-02-19"),
                    Trailer = "/Trailers/Shutter_Island_Official_Trailer.mp4"},

                new Movie { ID = 20, Title ="Heat" ,Description = "A group of professional bank robbers start to feel the heat from police when they unknowingly leave a clue at their latest heist.",
                    Genre = Genre.Crime, Image = "/Images/movies/HEAT.jpg",
                    Price =22, Length=170, Rating=8.2, DirectorID = 4 , ReleaseDate = DateTime.Parse("1995-12-15"),
                    Trailer = "/Trailers/Heat_Official_Trailer.mp4"}
            };
            movies.ForEach(b => context.Movies.AddOrUpdate(p => p.ID, b));
            context.SaveChanges();

            var orders = new List<Order>
            {
                  new Order { OrderID = 1, CustomerId = customers.Single(s => s.FirstName == "Zach").CustomerID,   OrderDate = DateTime.Parse("2019-01-11"), BranchID = branches.Single(b => b.BranchID == 1).BranchID, Movies = {  movies.Single(b => b.ID == 1), movies.Single(m => m.ID == 2) } },
                  new Order { OrderID = 2, CustomerId = customers.Single(s => s.FirstName == "Zach").CustomerID,   OrderDate = DateTime.Parse("2019-01-06"), BranchID = branches.Single(b => b.BranchID == 1).BranchID, Movies = {  movies.Single(b => b.ID == 2), } },
                  new Order { OrderID = 3, CustomerId = customers.Single(s => s.FirstName == "Zach").CustomerID,   OrderDate = DateTime.Parse("2019-02-06"), BranchID = branches.Single(b => b.BranchID == 1).BranchID, Movies = {  movies.Single(b => b.ID == 2), movies.Single(m => m.ID == 3) } },
                  new Order { OrderID = 4, CustomerId = customers.Single(s => s.FirstName == "Tahel").CustomerID,  OrderDate = DateTime.Parse("2019-04-16"), BranchID = branches.Single(b => b.BranchID == 1).BranchID, Movies = {  movies.Single(b => b.ID == 3), movies.Single(m => m.ID == 2) } },
                  new Order { OrderID = 5, CustomerId = customers.Single(s => s.FirstName == "Yarden").CustomerID, OrderDate = DateTime.Parse("2019-07-01"), BranchID = branches.Single(b => b.BranchID == 2).BranchID, Movies = {  movies.Single(b => b.ID == 5), movies.Single(m => m.ID == 3) } },
                  new Order { OrderID = 6, CustomerId = customers.Single(s => s.FirstName == "Yarden").CustomerID, OrderDate = DateTime.Parse("2019-10-11"), BranchID = branches.Single(b => b.BranchID == 2).BranchID, Movies = {  movies.Single(b => b.ID == 1) } },
                  new Order { OrderID = 7, CustomerId = customers.Single(s => s.FirstName == "Loren").CustomerID,  OrderDate = DateTime.Parse("2019-10-21"), BranchID = branches.Single(b => b.BranchID == 3).BranchID, Movies = {  movies.Single(b => b.ID == 1)} },
                  new Order { OrderID = 8, CustomerId = customers.Single(s => s.FirstName == "Loren").CustomerID,  OrderDate = DateTime.Parse("2019-10-30"), BranchID = branches.Single(b => b.BranchID == 3).BranchID, Movies = {  movies.Single(b => b.ID == 1)} },
                  new Order { OrderID = 9, CustomerId = customers.Single(s => s.FirstName == "Loren").CustomerID,  OrderDate = DateTime.Parse("2019-10-01"), BranchID = branches.Single(b => b.BranchID == 3).BranchID, Movies = {  movies.Single(b => b.ID == 1)} },
            };
            
            orders.ForEach(b => context.Orders.AddOrUpdate(p => p.OrderID, b));
            context.SaveChanges();
        }
    }
}