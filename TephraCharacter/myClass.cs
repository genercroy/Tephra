using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TephraCharacter
{
    class myClass
    {
    }

    public class Character
    {
        public Character() { }
        public Character(string characterID)
        {
            this.CharacterID = characterID;
        }

        public string CharacterID { get; set; }
        public string Name { get; set; }
        public string Player { get; set; }
        public Race race { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public Skill Skills = new Skill();
        public Stat Stats = new Stat();
        public List<Specialty> Specialties = new List<Specialty>();
        public Armor armor = new Armor();
        public List<Weapon> Wepons = new List<Weapon>();
    }

    public class Skill
    {
        public Skill() { }
        public string ExternalID { get; set; }
        public int Espionage { get; set; }
        public int Expertise { get; set; }
        public int Showmanship { get; set; }
        public int Tactical { get; set; }
        
        public int Brawl { get; set; }
        public int Frenzy { get; set; }
        public int Overpower { get; set; }
        public int Resilience { get; set; }

        public int Alchemy { get; set; }
        public int Armsmith { get; set; }
        public int Automata { get; set; }
        public int BioFlux { get; set; }
        public int Engineer { get; set; }
        public int Gadgetry { get; set; }

        public int Ace { get; set; }
        public int Agility { get; set; }
        public int Marksmanship { get; set; }
        public int Swashbuckling { get; set; }

        public int Faith { get; set; }
        public int Grace { get; set; }
        public int Luck { get; set; }
        public int Shamanism { get; set; }
    }

    public class Race
    {
        public Race() { }

        public Race(string raceid)
        {
            this.RaceID = raceid;
        }
        public string RaceID { get; set; }
        public string Name { get; set; }
        public Skill Skills = new Skill();
        public Stat Stats = new Stat();
    }

    public class Specialty
    {
        public Specialty() { }

        public string Name { get; set; }
        public Stat Stats = new Stat();
    }

    public class Stat
    {
        public Stat() { }

        public string externalID { get; set; }
        public int AP { get; set; }
        public int Swim { get; set; }
        public int Climb { get; set; }
        public int Fly { get; set; }
        public int HP { get; set; }
        public int Wounds { get; set; }
        public int Accuracy { get; set; }
        public int Evade { get; set; }
        public int Strike { get; set; }
        public int Defense { get; set; }
        public int Priority { get; set; }
        public int Speed { get; set; }
        public int Augments { get; set; }
        public int DIY { get; set; }
        public int Brute { get; set; }
        public int Cunning { get; set; }
        public int Science { get; set; }
        public int Dexterity { get; set; }
        public int Spirit { get; set; }
    }

    public class Armor
    {
        public Armor() { }

        public string Type { get; set; }
        public int SoakClass { get; set; }
        public Stat Stats = new Stat();
    }

    public class Weapon
    {
        public Weapon() { }

        public int ID { get; set; }
        public String Name { get; set; }
        public int AP { get; set; }
        public int DamageClass { get; set; }
        public String Wielded { get; set; }
        public int Readying { get; set; }
        public int Range { get; set; }
        public WeaponMod Mod = new WeaponMod();
    }

    public class WeaponMod
    {
        public WeaponMod() { }

        public int Strike { get; set; }
        public int Evade { get; set; }
    }

    public class Calc
    {
        public static int GetTier(int number)
        {
            if (number < 10)
            {
                return 1;
            }
            else if (number < 20)
            {
                return 2;
            }
            else if (number < 30)
	        {
                return 3;
	        }
            else if (number > 29)
            {
                return 4;
            }
            return 1;
        }
    }
}
