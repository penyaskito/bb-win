using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BB;
using System.IO;
using System.Xml.Linq;

namespace BBLogic
{
    public class RosterManager
    {
        public API API { get; set; }

        public API.Roster GetCurrentData()
        {
            API.Roster roster = null;
            try
            {
                roster = API.GetRoster(null);
            }
            catch (BB.NotAuthorizedException naexc)
            {
                if (API.Login())
                {
                    roster = API.GetRoster(null);
                }
                else
                {
                    throw naexc;
                }
            }

            if (IsNewer(roster))
            {
                SaveRoster(roster);
            }
            return roster;
        }

        public RosterDiff GetCurrentDataWithDiff()
        {
            API.Roster roster = GetCurrentData();
            API.Roster older = GetPastData(1, roster.TeamID);

            RosterDiff diff = new RosterDiff(roster, older);

            return diff;
        }

        private void SaveRoster(API.Roster roster)
        {
            string dir = GetDirectoryForSavingRoster();
            string filename = Path.Combine(dir, string.Format("{0}.xml", roster.TeamID));
            roster.XML.Save(filename);
        }

        private string GetDirectoryForRoster()
        {
            string userFolder = Path.Combine(Util.GetRootFolder(), API.Username);
            string rosterFolder = Path.Combine(userFolder, "roster");
            Util.CreateIfNotExists(rosterFolder);
            return rosterFolder;
        }

        private string GetDirectoryForSavingRoster()
        {
            string rosterFolder = GetDirectoryForRoster();
            string f = Path.Combine(rosterFolder, Util.GetNewFolderName(rosterFolder));
            Util.CreateIfNotExists(f);
            return f;

        }

        private bool IsNewer(API.Roster roster)
        {
            API.Roster older = GetPastData(0, roster.TeamID);

            if (older == null)
            {
                return true;
            }

            bool isNewer = !(roster.Players.Count == older.Players.Count
                && roster.TeamID == older.TeamID);

            if (isNewer == true)
            {
                return true;
            }

            for (int i = 0; i < roster.Players.Count && !isNewer; i++)
            {
                BB.API.Player p = roster.Players[i];
                if (!p.Equals(roster.getPlayer(p.ID)))
                {
                    isNewer = true;
                }
            }


            return isNewer;
        }


        public API.Roster GetPastData(int periodsPast, int teamID)
        {
            string folder = GetDirectoryForRoster();
            int last = Util.GetMaxNumber(folder);

            int pastFolder = (last - periodsPast > 1) ? last - periodsPast : 1;

            string filename = Path.Combine(folder, pastFolder.ToString());
            filename = Path.Combine(filename, string.Format("{0}.xml", teamID));

            if (!File.Exists(filename))
            {
                return null;
            }

            API.Roster r = API.Roster.ParseXDocument(XDocument.Load(filename));

            return r;
        }

        public class RosterDiff : API.Roster
        {
            public new List<PlayerDiff> Players { get; set; }

            public RosterDiff(API.Roster roster, API.Roster old)
            {
                this.TeamID = roster.TeamID;
                this.XML = roster.XML;
                base.Players = roster.Players;


                this.Players = GetPlayersDiff(roster, old);
            }

            private List<PlayerDiff> GetPlayersDiff(API.Roster roster, API.Roster old)
            {
                List<PlayerDiff> playerdiff = new List<PlayerDiff>();
                for (int i = 0; i < roster.Players.Count; i++)
                {
                    BB.API.Player newP = roster.Players[i];
                    PlayerDiff diff = new PlayerDiff(newP, old.getPlayer(newP.ID));
                    playerdiff.Add(diff);
                }
                return playerdiff;
            }
        }
        public class PlayerDiff : API.Player
        {
            public API.Skills Diff { get; set; }

            public PlayerDiff(API.Player p, API.Player old)
            {
                this.Age = p.Age;
                this.BestPosition = p.BestPosition;
                this.DMI = p.DMI;
                this.FirstName = p.FirstName;
                this.Height = p.Height;
                this.ID = p.ID;
                this.Jersey = p.Jersey;
                this.LastName = p.LastName;
                this.Nationality = p.Nationality;
                this.Salary = p.Salary;
                this.Skills = p.Skills;

                if (old != null)
                {
                    this.Diff = GetDiff(p.Skills, old.Skills);
                }
                else
                {
                    this.Diff = new API.Skills();
                }
            }

            private API.Skills GetDiff(BB.API.Skills newSkills, BB.API.Skills oldSkills)
            {
                API.Skills skills = new API.Skills()
                {
                    Block = newSkills.Block - oldSkills.Block,
                    Driving = newSkills.Driving - oldSkills.Driving,
                    Experience = newSkills.Experience - oldSkills.Experience,
                    FreeThrow = newSkills.FreeThrow - oldSkills.FreeThrow,
                    GameShape = newSkills.GameShape - oldSkills.GameShape,
                    Handling = newSkills.Handling - oldSkills.Handling,
                    InsideDefense = newSkills.InsideDefense - oldSkills.InsideDefense,
                    InsideShot = newSkills.InsideShot - oldSkills.InsideShot,
                    JumpShot = newSkills.JumpShot - oldSkills.JumpShot,
                    OutsideDefense = newSkills.OutsideDefense - oldSkills.OutsideDefense,
                    Passing = newSkills.Passing - oldSkills.Passing,
                    Potential = newSkills.Potential - oldSkills.Potential,
                    Range = newSkills.Range - oldSkills.Range,
                    Rebound = newSkills.Rebound - oldSkills.Rebound,
                    Stamina = newSkills.Stamina - oldSkills.Stamina,
                };

                return skills;
            }
        }

    }

}
