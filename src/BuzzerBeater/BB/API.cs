using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace BB
{
    public class API
    {

        public string Username { get; private set; }
        public string Key { get; private set; }
        private CookieCollection Cookies { get; set; }

        /// <summary>
        /// Creates the API with the given username and the key.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="key"></param>
        public API(string username, string key)
        {
            Username = username;
            Key = key;
        }


        /// <summary>
        /// Logs in the user into the API.
        /// </summary>
        public bool Login()
        {
            string LOGIN_URL = String.Format("http://www2.buzzerbeater.org/BBAPI/login.aspx?login={0}&code={1}",
                Username, Key);

            try
            {
                GetXDocumentResponse(LOGIN_URL, true);
            }
            catch (NotAuthorizedException nae)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Logs out the user, invalidating the cookie.
        /// </summary>
        public void Logout()
        {
            string LOGOUT_URL = String.Format("http://www2.buzzerbeater.org/BBAPI/logout.aspx",
    Username, Key);

            // we don't need the result
            GetXDocumentResponse(LOGOUT_URL);

        }


        /// <summary>
        /// Retrieves the info from the team based on the given ID. If the given ID is null,
        /// retrieves the data from the current user team.
        /// </summary>
        /// <param name="TeamID"></param>
        /// <returns></returns>
        public Team GetTeam(int? TeamID)
        {
            string TEAM_INFO_URL = string.Empty;

            if (!TeamID.HasValue)
            {
                TEAM_INFO_URL = "http://www2.buzzerbeater.org/BBAPI/teaminfo.aspx";
            }
            else
            {
                TEAM_INFO_URL = String.Format("http://www2.buzzerbeater.org/BBAPI/teaminfo.aspx?teamid={0}",
                    TeamID.Value);
            }

            XDocument doc = GetXDocumentResponse(TEAM_INFO_URL);
            Team t = Team.ParseXDocument(doc);
            return t;
        }

        /// <summary>
        /// Retrieves the info from the team arena based on the given ID. If the given ID is null,
        /// retrieves the data from the current user team arena.
        /// </summary>
        /// <param name="TeamID"></param>
        /// <returns></returns>
        public Arena GetArena(int? TeamID)
        {
            string ARENA_URL = string.Empty;

            if (!TeamID.HasValue)
            {
                ARENA_URL = "http://www2.buzzerbeater.org/BBAPI/arena.aspx";
            }
            else
            {
                ARENA_URL = String.Format("http://www2.buzzerbeater.org/BBAPI/arena.aspx?teamid={0}",
                    TeamID.Value);
            }

            XDocument doc = GetXDocumentResponse(ARENA_URL);
            Arena a = Arena.ParseXDocument(doc);
            return a;
        }

        /// <summary>
        /// Retrieves the info from the team roster based on the given ID. If the given ID is null,
        /// retrieves the data from the current user team roster.
        /// </summary>
        /// <param name="TeamID"></param>
        /// <returns></returns>
        public Roster GetRoster(int? TeamID)
        {
            string ROSTER_URL = string.Empty;

            if (!TeamID.HasValue)
            {
                ROSTER_URL = "http://www2.buzzerbeater.org/BBAPI/roster.aspx";
            }
            else
            {
                ROSTER_URL = String.Format("http://www2.buzzerbeater.org/BBAPI/roster.aspx?teamid={0}",
                    TeamID.Value);
            }

            XDocument doc = GetXDocumentResponse(ROSTER_URL);
            Roster a = Roster.ParseXDocument(doc);
            return a;
        }

        /// <summary>
        /// Retrieves the economic info from the current user team.
        /// </summary>
        /// <returns></returns>
        public Arena GetEconomy()
        {
            //TODO: This is incomplete
            string ECONOMY_URL = "http://www2.buzzerbeater.org/BBAPI/economy.aspx";

            XDocument doc = GetXDocumentResponse(ECONOMY_URL);
            Arena a = Arena.ParseXDocument(doc);
            return a;
        }

        private XDocument GetXDocumentResponse(string url)
        {
            return GetXDocumentResponse(url, false);
        }

        private XDocument GetXDocumentResponse(string url, bool saveCookies)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            if (Cookies != null)
            {
                request.CookieContainer.Add(Cookies);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (saveCookies)
            {
                this.Cookies = response.Cookies;
            }

            XmlReader xmlreader = XmlReader.Create(response.GetResponseStream());
            XDocument doc = XDocument.Load(xmlreader);
            CheckForError(doc);
            return doc;
        }

        private void CheckForError(XDocument doc)
        {
            var x = from nodes in doc.Descendants("error")
                    select new
                    {
                        Message = nodes.Attribute("message").Value
                    };
            if (x.Count() > 0)
            {
                string m = x.Single().Message;
                if ("NotAuthorized".Equals(m))
                {
                    throw new NotAuthorizedException();
                }
            }
        }

        public class TeamData
        {
            public string UserName { get; set; }
            public int TeamID { get; set; }
            public string TeamName { get; set; }
        }

        public class Team : TeamData
        {
            public XDocument XML { get; set; }


            public string ShortName { get; set; }
            public League League { get; set; }
            public Country Country { get; set; }

            internal static Team ParseXDocument(XDocument doc)
            {

                var x = from team in doc.Descendants("team")
                        select new Team()
                        {
                            TeamID = Convert.ToInt32(team.Attribute("id").Value),
                            TeamName = team.Element("teamName").Value,
                            UserName = team.Element("owner").Value,
                            ShortName = team.Element("shortName").Value,
                            League = new League()
                            {
                                ID = Convert.ToInt32(team.Element("league").Attribute("id").Value),
                                Level = Convert.ToInt32(team.Element("league").Attribute("level").Value),
                                Name = team.Element("league").Value
                            },
                            Country = new Country()
                            {
                                ID = Convert.ToInt32(team.Element("country").Attribute("id").Value),
                                Name = team.Element("country").Value
                            }
                        };
                Team t = x.Single<Team>();
                t.XML = doc;
                return t;
            }
        }

        public class League
        {
            public int ID { get; set; }
            public int Level { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return string.Format("Liga {0} (división {1})", Name, Level);
            }
        }

        public class Country
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public class Arena
        {
            public XDocument XML { get; set; }


            public string Name { get; set; }
            public Seat Bleachers { get; set; }
            public Seat LowerTier { get; set; }
            public Seat CourtSide { get; set; }
            public Seat Luxury { get; set; }

            internal static Arena ParseXDocument(XDocument doc)
            {
                var x = from arena in doc.Descendants("arena")
                        select new Arena()
                        {
                            Name = arena.Element("name").Value,
                            Bleachers = Seat.ParseXElement(arena.Element("seats").Element("bleachers")),
                            LowerTier = Seat.ParseXElement(arena.Element("seats").Element("lowerTier")),
                            CourtSide = Seat.ParseXElement(arena.Element("seats").Element("courtside")),
                            Luxury = Seat.ParseXElement(arena.Element("seats").Element("luxury"))
                        };
                Arena a = x.Single<Arena>();

                a.Bleachers.Type = Seat.SeatType.Bleachers;
                a.LowerTier.Type = Seat.SeatType.LowerTier;
                a.Luxury.Type = Seat.SeatType.Luxury;
                a.CourtSide.Type = Seat.SeatType.CourtSide;

                a.XML = doc;

                return a;
            }
        }

        public class Seat
        {
            public string Type { get; set; }
            public int Prize { get; set; }
            public int NextPrize { get; set; }
            public int Capacity { get; set; }

            internal static Seat ParseXElement(XElement element)
            {

                Seat s = new Seat()
                {
                    Prize = Convert.ToInt32(element.Attribute("price").Value),
                    NextPrize = Convert.ToInt32(element.Attribute("nextPrice").Value),
                    Capacity = Convert.ToInt32(element.Value)
                };
                return s;
            }

            public static class SeatType
            {
                public static string Bleachers = "General";
                public static string LowerTier = "Preferente";
                public static string CourtSide = "Tribuna";
                public static string Luxury = "Palcos";
            }
        }

        public class Roster
        {
            public XDocument XML { get; set; }

            public int TeamID { get; set; }
            public List<Player> Players { get; set; }

            public static Roster ParseXDocument(XDocument doc)
            {
                var x = from players in doc.Descendants("player")
                        select new Player()
                        {
                            ID = Convert.ToInt32(players.Attribute("id").Value),
                            FirstName = players.Element("firstName").Value,
                            LastName = players.Element("lastName").Value,
                            Nationality = players.Element("nationality").Value,
                            Age = Convert.ToInt32(players.Element("age").Value),
                            Height = Convert.ToInt32(players.Element("height").Value),
                            DMI = Convert.ToInt32(players.Element("dmi").Value),
                            Jersey = (players.Element("jersey") != null) ? new Jersey() { N = ParseJersey(players.Element("jersey").Value) } : null,
                            Salary = Convert.ToInt32(players.Element("salary").Value),
                            BestPosition = (Position)Enum.Parse(typeof(Position), players.Element("bestPosition").Value),
                            Skills = Skills.ParseXElement(players.Element("skills"))
                        };

                Roster r = new Roster();
                r.TeamID = (from roster in doc.Descendants("roster")
                            select Convert.ToInt32(roster.Attribute("teamid").Value)).Single<int>();
                r.Players = new List<Player>();
                r.Players.AddRange(x);

                r.XML = doc;

                return r;
            }

            private static string ParseJersey(string number)
            {
                if (number.Length > 2)
                {
                    return number.Substring(1, 2);
                }
                return number;
            }

            public Player getPlayer(int playerId)
            {
                foreach (var p in Players)
                {
                    if (p.ID == playerId)
                    {
                        return p;
                    }
                }
                return null;
            }
        }
        public class Jersey : IComparable<Jersey>, IComparable
        {
            public string N { get; set; }

            #region IComparable<Jersey> Members

            public int CompareTo(Jersey other)
            {
                if (string.IsNullOrEmpty(N) == null && string.IsNullOrEmpty(other.N))
                {
                    return 0;
                }
                if (string.IsNullOrEmpty(N) && !string.IsNullOrEmpty(other.N))
                {
                    return 1;
                }
                if (!string.IsNullOrEmpty(N) && string.IsNullOrEmpty(other.N))
                {
                    return -1;
                }
                return Convert.ToInt32(N).CompareTo(Convert.ToInt32(other.N));
            }

            #endregion

            public override string ToString()
            {
                return N;
            }

            #region IComparable Members

            public int CompareTo(object other)
            {
                return CompareTo((Jersey)other);
            }

            #endregion
        }
        public class Player
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Name { get { return string.Format("{0} {1}", FirstName, LastName); } }
            public string Nationality { get; set; }
            public int Age { get; set; }
            public int Height { get; set; }
            public int DMI { get; set; }

            public Jersey Jersey { get; set; }
            public int Salary { get; set; }

            public Position BestPosition { get; set; }
            public Skills Skills { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is Player))
                {
                    return false;
                }
                Player other = (Player)obj;
                return (ID == other.ID && Age == other.Age && DMI == other.DMI && Skills.Equals(other.Skills));
            }
        }

        public enum Position
        {
            PG,
            SG,
            SF,
            PF,
            C
        }

        public class Skills
        {
            public int GameShape { get; set; }
            public int Potential { get; set; }

            public int JumpShot { get; set; }
            public int Range { get; set; }
            public int OutsideDefense { get; set; }
            public int Handling { get; set; }
            public int Driving { get; set; }
            public int Passing { get; set; }
            public int InsideShot { get; set; }
            public int InsideDefense { get; set; }
            public int Rebound { get; set; }
            public int Block { get; set; }
            public int Stamina { get; set; }
            public int FreeThrow { get; set; }
            public int Experience { get; set; }

            internal static Skills ParseXElement(XElement element)
            {
                Skills s = new Skills()
                {
                    GameShape = Convert.ToInt32(element.Element("gameShape").Value),
                    Potential = Convert.ToInt32(element.Element("potential").Value),
                    JumpShot = Convert.ToInt32(element.Element("jumpShot").Value),
                    Range = Convert.ToInt32(element.Element("range").Value),
                    OutsideDefense = Convert.ToInt32(element.Element("outsideDef").Value),
                    Handling = Convert.ToInt32(element.Element("handling").Value),
                    Driving = Convert.ToInt32(element.Element("driving").Value),
                    Passing = Convert.ToInt32(element.Element("passing").Value),
                    InsideShot = Convert.ToInt32(element.Element("insideShot").Value),
                    InsideDefense = Convert.ToInt32(element.Element("insideDef").Value),
                    Rebound = Convert.ToInt32(element.Element("rebound").Value),
                    Block = Convert.ToInt32(element.Element("block").Value),
                    Stamina = Convert.ToInt32(element.Element("stamina").Value),
                    FreeThrow = Convert.ToInt32(element.Element("freeThrow").Value),
                    Experience = Convert.ToInt32(element.Element("experience").Value)
                };
                return s;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Skills))
                {
                    return false;
                }
                Skills other = (Skills)obj;
                return (GameShape == other.GameShape && Potential == other.Potential && JumpShot == other.JumpShot
                    && Range == other.Range && OutsideDefense == other.OutsideDefense && Handling == other.Handling
                    && Driving == other.Driving && Passing == other.Passing && InsideShot == other.InsideShot
                    && InsideDefense == other.InsideDefense && Rebound == other.Rebound && Block == other.Block
                    && Stamina == other.Stamina && FreeThrow == other.FreeThrow && Experience == other.Experience);
            }
        }
    }

    [global::System.Serializable]
    public class ServerErrorException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ServerErrorException() { }
        public ServerErrorException(string message) : base(message) { }
        public ServerErrorException(string message, Exception inner) : base(message, inner) { }
        protected ServerErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [global::System.Serializable]
    public class NotAuthorizedException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public NotAuthorizedException() { }
        public NotAuthorizedException(string message) : base(message) { }
        public NotAuthorizedException(string message, Exception inner) : base(message, inner) { }
        protected NotAuthorizedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
