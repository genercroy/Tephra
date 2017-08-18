using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TephraCharacter
{
    public partial class Form1 : Form
    {
        string _characterID = "2";
        
        Character _character;
        
        Skill totalSkill;
        Stat totalStat;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //_characterID = "6D2817B3-72F9-4F99-AA2D-761103D95259";
            if (!string.IsNullOrEmpty(_characterID))
            {
                Data.GetCharacterInfo(_characterID, out _character);
            }

            cboRace.SelectedIndexChanged -= cboRace_SelectedIndexChanged;
            cboRace.DataSource = Data.GetRaceList();
            cboRace.DisplayMember = "Name";
            cboRace.ValueMember = "RaceID";
            cboRace.SelectedIndexChanged += cboRace_SelectedIndexChanged;

            if (_character != null)
            {
                DisplayCharacterInformation();
            }
        }

        internal void DisplayCharacterInformation()
        {
            txtName.Text = _character.Name;
            cboRace.SelectedIndexChanged -= cboRace_SelectedIndexChanged;
            cboRace.SelectedValue = _character.race.RaceID;
            cboRace.SelectedIndexChanged += cboRace_SelectedIndexChanged;
            txtNationality.Text = _character.Nationality;
            txtReligion.Text = _character.Religion;

            txtAge.Text = string.Format("{0}", _character.Age);
            txtHeight.Text = string.Format("{0}", _character.Height);
            txtWeight.Text = string.Format("{0}", _character.Weight);

            totalStat = null;
            totalSkill = null;
            CombineSkills((Skill)_character.Skills,out totalSkill);
            CombineSkills((Skill)_character.race.Skills, out totalSkill);
            

            CombineStats((Stat)_character.Stats,out totalStat);
            CombineStats((Stat)_character.race.Stats, out totalStat);
            foreach (Specialty s in _character.Specialties)
            {
                CombineStats((Stat)s.Stats, out totalStat);
            }
            CombineStats((Stat)_character.armor.Stats, out totalStat);

            if (_character.Specialties.Find(delegate(Specialty q) { return q.Name.Equals("Tough Stuff"); }) != null)
            {
                totalStat.HP += _character.Specialties.Count + (totalSkill.Resilience/8)-3;
            }
            if (_character.Specialties.Find(delegate(Specialty q) { return q.Name.Equals("Relentless"); }) != null)
            {
                totalStat.HP += _character.Specialties.Count;
            }
            txtMaxHP.Text = string.Format("{0}", totalStat.HP);
            txtMaxWounds.Text = string.Format("{0}", totalStat.Wounds);
            txtEvade.Text = string.Format("{0}", totalStat.Evade);
            if (_character.Specialties.Find(delegate(Specialty q) { return q.Name.Equals("Walking Fortress"); }) != null)
            {
                totalStat.Defense += _character.Specialties.Count + (totalSkill.Resilience / 3);
            }
            txtDefense.Text = string.Format("{0}", totalStat.Defense);
            txtSoak.Text = string.Format("{0}", _character.armor.SoakClass);

            txtAP.Text = string.Format("{0}", totalStat.AP + _character.Level/4);
            txtPriority.Text = string.Format("{0}", totalStat.Priority);
            txtSpeed.Text = string.Format("{0}", totalStat.Speed);
            txtClimb.Text = string.Format("{0}", totalStat.Climb);
            txtSwim.Text = string.Format("{0}", totalStat.Swim);
            txtFly.Text = string.Format("{0}", totalStat.Fly);

            lblBrute.Text = totalStat.Brute.ToString("#");
            lblCunning.Text = totalStat.Cunning.ToString("#");
            lblScience.Text = totalStat.Science.ToString("#");
            lblDexterity.Text = totalStat.Dexterity.ToString("#");
            lblSpirit.Text = totalStat.Spirit.ToString("#");

            nAce.Value = totalSkill.Ace;
            nAgility.Value = totalSkill.Agility;
            nAlchemy.Value = totalSkill.Alchemy;
            nArmsmith.Value = totalSkill.Armsmith;
            nAutomata.Value = totalSkill.Automata;
            nBioflux.Value = totalSkill.BioFlux;
            nBrawl.Value = totalSkill.Brawl;
            nEngineer.Value = totalSkill.Engineer;
            nEspionage.Value = totalSkill.Espionage;
            nExpertise.Value = totalSkill.Expertise;
            nFaith.Value = totalSkill.Faith;
            nFrenzy.Value = totalSkill.Frenzy;
            nGadgetry.Value = totalSkill.Gadgetry;
            nGrace.Value = totalSkill.Grace;
            nLuck.Value = totalSkill.Luck;
            nMarksmanship.Value = totalSkill.Marksmanship;
            nOverpower.Value = totalSkill.Overpower;
            nResilience.Value = totalSkill.Resilience;
            nShamanism.Value = totalSkill.Shamanism;
            nShowmanship.Value = totalSkill.Showmanship;
            nSwashbuckling.Value = totalSkill.Swashbuckling;
            nTatical.Value = totalSkill.Tactical;

            CalcCunning();
            CalcBrute();
            CalcScience();
            CalcDexterity();
            CalcSpirit();

            flpSpecialties.Controls.Clear();
            foreach (Specialty s in _character.Specialties)
            {
                flpSpecialties.Controls.Add(new ucSpecialties((Specialty)s));
            }

            Weapon wUnarmed = new Weapon();
            wUnarmed.Name = "Unarmed";
            Data.GetWeaponStats(wUnarmed, out wUnarmed);
            if (_character.race.Name.Equals("Elf"))
            {
                wUnarmed.DamageClass += 2;
            }

            flpWeapons.Controls.Clear();
            flpWeapons.Controls.Add(new ucWeapons(wUnarmed, (Stat)totalStat, _character.Specialties, (Skill)totalSkill));

            foreach (Weapon w in _character.Wepons)
            {
                Weapon weap = (Weapon)w;
                if (_character.race.Name.Equals("Elf"))
                {
                    w.DamageClass += 1;
                }
                flpWeapons.Controls.Add(new ucWeapons(weap, (Stat)totalStat,_character.Specialties, (Skill)totalSkill));
            }
        }

        private void CombineStats(Stat inStat,out Stat ts)
        {
            if (totalStat == null)
            {
                ts = (Stat)inStat;
                return;
            }
            else
            {
                ts = totalStat;
            }

            
            ts.Accuracy += inStat.Accuracy;
            ts.AP += inStat.AP;
            ts.Augments += inStat.Augments;
            ts.Brute += inStat.Brute;
            ts.Climb += inStat.Climb;
            ts.Cunning += inStat.Cunning;
            ts.Defense += inStat.Defense;
            ts.Dexterity += inStat.Dexterity;
            ts.DIY += inStat.DIY;
            ts.Evade += inStat.Evade;
            ts.Fly += inStat.Fly;
            ts.HP += inStat.HP;
            ts.Priority += inStat.Priority;
            ts.Science += inStat.Science;
            ts.Speed += inStat.Speed;
            ts.Spirit += inStat.Spirit;
            ts.Strike += inStat.Strike;
            ts.Swim += inStat.Swim;
            ts.Wounds += inStat.Wounds;
        }

        internal object GetPropertyValue(object item, string name)
        {
            return item.GetType().GetProperties()
                .Single(pi => pi.Name == name)
                .GetValue(item, null);
        }

        private void CombineSkills(Skill inSkill, out Skill ts)
        {
            if (totalSkill==null)
            {
                ts = (Skill)inSkill;
                return;
            }
            else
            {
                ts = totalSkill;
            }
            
            
            ts.Ace += inSkill.Ace;
            ts.Agility += inSkill.Agility;
            ts.Alchemy += inSkill.Alchemy;
            ts.Armsmith += inSkill.Armsmith;
            ts.Automata += inSkill.Automata;
            ts.BioFlux += inSkill.BioFlux;
            ts.Brawl += inSkill.Brawl;
            ts.Engineer += inSkill.Engineer;
            ts.Espionage += inSkill.Espionage;
            ts.Expertise += inSkill.Expertise;
            ts.Faith += inSkill.Faith;
            ts.Frenzy += inSkill.Frenzy;
            ts.Gadgetry += inSkill.Gadgetry;
            ts.Grace += inSkill.Grace;
            ts.Luck += inSkill.Luck;
            ts.Marksmanship += inSkill.Marksmanship;
            ts.Overpower += inSkill.Overpower;
            ts.Resilience += inSkill.Resilience;
            ts.Shamanism += inSkill.Shamanism;
            ts.Showmanship += inSkill.Showmanship;
            ts.Swashbuckling += inSkill.Swashbuckling;
            ts.Tactical += inSkill.Tactical;
        }

        private void txtX_Keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CalcCunning()
        {
            lblCunning.Text = (nEspionage.Value + nExpertise.Value + nShowmanship.Value + nTatical.Value + totalStat.Cunning).ToString("#");

        }
        private void cunnning_ValueChanged(object sender, EventArgs e)
        {
            CalcCunning();
        }

        private void brute_ValueChanged(object sender, EventArgs e)
        {
            CalcBrute();
        }

        private void CalcBrute()
        {
            lblBrute.Text = (nBrawl.Value + nFrenzy.Value + nOverpower.Value + nResilience.Value + totalStat.Brute).ToString("#");
        }

        private void science_ValueChanged(object sender, EventArgs e)
        {
            CalcScience();
        }

        private void CalcScience()
        {
            lblScience.Text = (nAlchemy.Value + nArmsmith.Value + nAutomata.Value + nBioflux.Value + nEngineer.Value + nGadgetry.Value + totalStat.Science).ToString("#");
        }

        private void dexterity_ValueChanged(object sender, EventArgs e)
        {
            CalcDexterity();
        }

        private void CalcDexterity()
        {
            lblDexterity.Text = (nAce.Value + nAgility.Value + nMarksmanship.Value + nSwashbuckling.Value).ToString("#");
        }

        private void spirit_ValueChanged(object sender, EventArgs e)
        {
            CalcSpirit();
        }

        private void CalcSpirit()
        {
            lblSpirit.Text = (nFaith.Value + nGrace.Value + nLuck.Value + nShamanism.Value + totalStat.Spirit).ToString("#");
        }

        private void cboRace_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.GetCharacterInfo(_character.CharacterID, out _character);
            _character.race=
            Data.GetRaceInfo(cboRace.SelectedValue.ToString());

            DisplayCharacterInformation();
        }

        
    }
}
