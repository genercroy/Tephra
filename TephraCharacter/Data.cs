using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace TephraCharacter
{
    class Data
    {
        static string data = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0}\Data.mdf;Integrated Security=True", Environment.CurrentDirectory);
        

        internal static void GetCharacterInfo(string _characterID, out Character value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from character where characterid=@id";
                cmd.Parameters.AddWithValue("@id", _characterID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = null;
                foreach (DataRow r in dt.Rows)
                {
                    value = new Character(_characterID);
                    value.Name = String.Format("{0}", r["name"]);
                    value.Player = String.Format("{0}", r["player"]);
                    value.race = GetRaceInfo(String.Format("{0}", r["Race"]));
                    value.Nationality = String.Format("{0}", r["nationality"]);
                    value.Religion = String.Format("{0}", r["religion"]);
                    value.Age = Convert.ToInt32(String.Format("{0}", r["age"]));
                    value.Height = Convert.ToInt32(String.Format("{0}", r["height"]));
                    value.Weight = Convert.ToInt32(String.Format("{0}", r["weight"]));
                    value.Level = Convert.ToInt32(String.Format("{0}", r["level"]));
                    value.Experience = Convert.ToInt32(String.Format("{0}", r["experience"]));
                    GetStats(_characterID, out value.Stats);
                    GetSkills(_characterID, out value.Skills);
                    GetSpecialties(_characterID, out value.Specialties);
                    GetArmor(_characterID, out value.armor);
                    if (value.Specialties.Find(delegate(Specialty q) { return q.Name.Equals("Armored Ease"); }) != null)
                    {
                        value.armor.Stats.Evade = Math.Min(value.armor.Stats.Evade+1, 0);
                        if (value.armor.Stats.Speed <= -5) { value.armor.Stats.Speed = Math.Min(value.armor.Stats.Speed + 5, 0); }
                        value.armor.Stats.Climb = Math.Min(value.armor.Stats.Climb + 5, 0);
                        value.armor.Stats.Swim = Math.Min(value.armor.Stats.Swim + 5, 0);
                    }
                    if (value.Specialties.Find(delegate(Specialty q) { return q.Name.Equals("Armored Freedom"); }) != null)
                    {
                        value.armor.Stats.Evade = Math.Min(value.armor.Stats.Evade + 2, 0);
                        if (value.armor.Stats.Speed <= -5) { value.armor.Stats.Speed = Math.Min(value.armor.Stats.Speed + 5, 0); }
                        value.armor.Stats.Climb = Math.Min(value.armor.Stats.Climb + 10, 0);
                        value.armor.Stats.Swim = Math.Min(value.armor.Stats.Swim + 10, 0);
                    }
                    GetWeapons(_characterID, out value.Wepons);
                    return;
                }
            }
        }

        private static void GetWeapons(string _characterID, out List<Weapon> value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from character_weapons where characterid=@id";
                cmd.Parameters.AddWithValue("@id", _characterID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = new List<Weapon>();
                foreach (DataRow r in dt.Rows)
                {
                    Weapon w = new Weapon();
                    w.Name = string.Format("{0}",r["weapon"]);
                    w.ID = Convert.ToInt32(string.Format("{0}", r["id"]));
                    GetWeaponStats(w, out w);
                    GetWeaponMod(w, out w);
                    value.Add(w);
                }


            }
        }

        private static void GetWeaponMod(Weapon inValue, out Weapon value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from weaponmods where weaponid=@id";
                cmd.Parameters.AddWithValue("@id", inValue.ID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = inValue;
                foreach (DataRow r in dt.Rows)
                {
                    value.Mod.Strike = Convert.ToInt32(string.Format("{0}", r["strike"]));
                    value.Mod.Evade = Convert.ToInt32(string.Format("{0}", r["evade"]));
                }
            }
        }

        public static void GetWeaponStats(Weapon inValue, out Weapon value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from weapons where weapon=@weapon";
                cmd.Parameters.AddWithValue("@weapon", inValue.Name);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = inValue;
                foreach (DataRow r in dt.Rows)
                {
                    value.AP = Convert.ToInt32(string.Format("{0}", r["ap"]));
                    value.DamageClass = Convert.ToInt32(string.Format("{0}", r["damageclass"]));                    
                    value.Wielded =string.Format("{0}", r["ap"]);
                    value.Readying = Convert.ToInt32(string.Format("{0}", r["readying"]));
                    value.Range = Convert.ToInt32(string.Format("{0}", r["range"]));
                }


            }
        }

        private static void GetArmor(string _characterID, out Armor value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from character_armor where characterid=@id";
                cmd.Parameters.AddWithValue("@id", _characterID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = new Armor();
                foreach (DataRow r in dt.Rows)
                {
                    value.Type = String.Format("{0}",r["type"]);
                    GetArmorStats(value, out value);
                }


            }
        }

        private static void GetArmorStats(Armor inValue, out Armor value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from armor where type=@type";
                cmd.Parameters.AddWithValue("@type", inValue.Type);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = inValue;
                foreach (DataRow r in dt.Rows)
                {
                    value.SoakClass = Convert.ToInt32(String.Format("{0}", r["soakclass"]));
                    value.Stats.Evade = Convert.ToInt32(String.Format("{0}", r["evade"]));
                    value.Stats.Speed = Convert.ToInt32(String.Format("{0}", r["speed"]));
                    value.Stats.Climb = Convert.ToInt32(String.Format("{0}", r["climb"]));
                    value.Stats.Swim = value.Stats.Climb;
                }


            }
        }

        private static void GetSpecialties(string _characterID, out List<Specialty> list)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from character_specialties where characterid=@id";
                cmd.Parameters.AddWithValue("@id", _characterID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                list = new List<Specialty>();
                foreach (DataRow r in dt.Rows)
                {
                    Specialty s = new Specialty();

                    s.Name = String.Format("{0}", r["specialty"]);
                    GetSpecialtyInfo(s.Name, out s.Stats);
                    list.Add(s);

                }

            }
        }

        private static void GetSpecialtyInfo(string name, out Stat value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from specialties where name=@name";
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = new Stat();
                foreach (DataRow r in dt.Rows)
                {
                    value.AP = Convert.ToInt32(String.Format("{0}", r["ap"]));
                    value.Swim = Convert.ToInt32(String.Format("{0}", r["swim"]));
                    value.Climb = Convert.ToInt32(String.Format("{0}", r["climb"]));
                    value.Fly = Convert.ToInt32(String.Format("{0}", r["fly"]));
                    value.HP = Convert.ToInt32(String.Format("{0}", r["hp"]));
                    value.Wounds = Convert.ToInt32(String.Format("{0}", r["wounds"]));
                    value.Accuracy = Convert.ToInt32(String.Format("{0}", r["accuracy"]));
                    value.Evade = Convert.ToInt32(String.Format("{0}", r["evade"]));
                    value.Strike = Convert.ToInt32(String.Format("{0}", r["strike"]));
                    value.Defense = Convert.ToInt32(String.Format("{0}", r["defense"]));
                    value.Priority = Convert.ToInt32(String.Format("{0}", r["priority"]));
                    value.Speed = Convert.ToInt32(String.Format("{0}", r["speed"]));
                    value.Augments = Convert.ToInt32(String.Format("{0}", r["augments"]));
                    value.DIY = Convert.ToInt32(String.Format("{0}", r["DIY"]));
                    value.Brute = Convert.ToInt32(String.Format("{0}", r["brute"]));
                    value.Cunning = Convert.ToInt32(String.Format("{0}", r["cunning"]));
                    value.Science = Convert.ToInt32(String.Format("{0}", r["science"]));
                    value.Dexterity = Convert.ToInt32(String.Format("{0}", r["dexterity"]));
                    value.Spirit = Convert.ToInt32(String.Format("{0}", r["spirit"]));
                }


            }
        }

        public static Race GetRaceInfo(string raceid)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from race where raceid=@id";
                cmd.Parameters.AddWithValue("@id", raceid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Race value = new Race(raceid);
                foreach (DataRow r in dt.Rows)
                {
                    value.Name = String.Format("{0}", r["race"]);
                    GetSkills(raceid, out value.Skills);
                    GetStats(raceid, out value.Stats);
                    
                }

                return value;
            }
        }

        public static List<Race> GetRaceList()
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from race";
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                List<Race> value = new List<Race>();
                foreach (DataRow r in dt.Rows)
                {
                    Race race = new Race();
                    race.Name = String.Format("{0}", r["race"]);
                    race.RaceID = String.Format("{0}", r["raceid"]);
                    value.Add(race);
                }

                return value;
            }
        }

        private static void GetStats(string externalid, out Stat value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from stats where externalid=@id";
                cmd.Parameters.AddWithValue("@id", externalid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = new Stat();
                foreach (DataRow r in dt.Rows)
                {
                    value.AP = Convert.ToInt32(String.Format("{0}", r["ap"]));
                    value.Swim = Convert.ToInt32(String.Format("{0}", r["swim"]));
                    value.Climb = Convert.ToInt32(String.Format("{0}", r["climb"]));
                    value.Fly = Convert.ToInt32(String.Format("{0}", r["fly"]));
                    value.HP = Convert.ToInt32(String.Format("{0}", r["hp"]));
                    value.Wounds = Convert.ToInt32(String.Format("{0}", r["wounds"]));
                    value.Accuracy = Convert.ToInt32(String.Format("{0}", r["accuracy"]));
                    value.Evade = Convert.ToInt32(String.Format("{0}", r["evade"]));
                    value.Strike = Convert.ToInt32(String.Format("{0}", r["strike"]));
                    value.Defense = Convert.ToInt32(String.Format("{0}", r["defense"]));
                    value.Priority = Convert.ToInt32(String.Format("{0}", r["priority"]));
                    value.Speed = Convert.ToInt32(String.Format("{0}", r["speed"]));
                    value.Augments = Convert.ToInt32(String.Format("{0}", r["augments"]));
                    value.DIY = Convert.ToInt32(String.Format("{0}", r["DIY"]));
                    value.Brute = Convert.ToInt32(String.Format("{0}", r["brute"]));
                    value.Cunning = Convert.ToInt32(String.Format("{0}", r["cunning"]));
                    value.Science = Convert.ToInt32(String.Format("{0}", r["science"]));
                    value.Dexterity = Convert.ToInt32(String.Format("{0}", r["dexterity"]));
                    value.Spirit = Convert.ToInt32(String.Format("{0}", r["spirit"]));
                }

                
            }
        }

        private static void GetSkills(string externalid, out Skill value)
        {
            using (SqlConnection conn = new SqlConnection(data))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "Select * from skills where externalid=@id";
                cmd.Parameters.AddWithValue("@id", externalid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                value = new Skill();
                foreach (DataRow r in dt.Rows)
                {
                    value.Espionage = Convert.ToInt32(String.Format("{0}", r["Espionage"]));
                    value.Expertise = Convert.ToInt32(String.Format("{0}", r["Expertise"]));
                    value.Showmanship = Convert.ToInt32(String.Format("{0}", r["Showmanship"]));
                    value.Tactical = Convert.ToInt32(String.Format("{0}", r["Tactical"]));
                    value.Brawl = Convert.ToInt32(String.Format("{0}", r["Brawl"]));
                    value.Frenzy = Convert.ToInt32(String.Format("{0}", r["Frenzy"]));
                    value.Overpower = Convert.ToInt32(String.Format("{0}", r["Overpower"]));
                    value.Resilience = Convert.ToInt32(String.Format("{0}", r["Resilience"]));
                    value.Alchemy = Convert.ToInt32(String.Format("{0}", r["Alchemy"]));
                    value.Armsmith = Convert.ToInt32(String.Format("{0}", r["Armsmith"]));
                    value.Automata = Convert.ToInt32(String.Format("{0}", r["Automata"]));
                    value.BioFlux = Convert.ToInt32(String.Format("{0}", r["Bio-Flux"]));
                    value.Engineer = Convert.ToInt32(String.Format("{0}", r["Engineer"]));
                    value.Gadgetry = Convert.ToInt32(String.Format("{0}", r["Gadgetry"]));
                    value.Ace = Convert.ToInt32(String.Format("{0}", r["Ace"]));
                    value.Agility = Convert.ToInt32(String.Format("{0}", r["Agility"]));
                    value.Marksmanship = Convert.ToInt32(String.Format("{0}", r["Marksmanship"]));
                    value.Swashbuckling = Convert.ToInt32(String.Format("{0}", r["Swashbuckling"]));
                    value.Faith = Convert.ToInt32(String.Format("{0}", r["faith"]));
                    value.Grace = Convert.ToInt32(String.Format("{0}", r["Grace"]));
                    value.Luck = Convert.ToInt32(String.Format("{0}", r["Luck"]));
                    value.Shamanism = Convert.ToInt32(String.Format("{0}", r["Shamanism"]));
                }


            }
        }
    }
}
