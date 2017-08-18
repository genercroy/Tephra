using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TephraCharacter
{
    public partial class ucWeapons : UserControl
    {
        Random random;
        Weapon _weapon;
        Stat _stats;
        List<Specialty> _specialties;
        Skill _skills;
        public ucWeapons(Weapon weapon,Stat stats, List<Specialty> specialties, Skill skills)
        {
            InitializeComponent();
            _weapon = weapon;
            _stats = stats;
            _specialties = specialties;
            _skills = skills;
        }

        private void ucWeapons_Load(object sender, EventArgs e)
        {
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Heavy-Handed"); }) != null)
            {
                chkModifiers.Items.Add("Heavy-Handed");
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Heavy Hitter"); }) != null &&
                _weapon.Name.ToLower().Contains("heavy"))
            {
                chkModifiers.Items.Add("Heavy Hitter");
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Monstrous Attacks"); }) != null &&
                _weapon.Name.ToLower().Contains("super-heavy"))
            {
                chkModifiers.Items.Add("Monstrous Attacks");
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Berserker"); }) != null)
            {
                chkModifiers.Items.Add("Berserker");
                nCount.Enabled = true;
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Titanic Strength"); }) != null &&
                _weapon.Name.ToLower().Contains("super-heavy"))
            {
                _weapon.Readying = 0;
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Fisticuffs"); }) != null)
            {
                chkModifiers.Items.Add("Fisticuffs");
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Fray Fighter"); }) != null)
            {
                chkModifiers.Items.Add("Fray Fighter");
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Hundred Strikes"); }) != null)
            {
                chkModifiers.Items.Add("Hundred Strikes");
            }
            if (_specialties.Find(delegate(Specialty q) { return q.Name.Equals("Solid Assault"); }) != null)
            {
                chkModifiers.Items.Add("Solid Assault");
            }
            lblName.Text = _weapon.Name;
            lblAcuracy.Text += String.Format(" {0}", _stats.Accuracy);
            lblAP.Text += String.Format(" {0}", _weapon.AP);
            lblReady.Text += String.Format(" {0}", _weapon.Readying);
            lblStrike.Text += String.Format(" {0}", _stats.Strike+_weapon.Mod.Strike);
            lblDamageClass.Text += String.Format(" {0} | {1} | {2} | {3}",
                _weapon.DamageClass,
                _weapon.DamageClass * 2,
                _weapon.DamageClass * 3,
                _weapon.DamageClass * 4);
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            random = new Random(Guid.NewGuid().GetHashCode());
            StringBuilder sb = new StringBuilder();
            int accuracy = random.Next(1, 12);
            while (accuracy % 12 == 0)
            {
                accuracy += random.Next(1, 12);
            }
            int damageclass = (int)_weapon.DamageClass;
            int HeavyHitter = chkModifiers.FindStringExact("Heavy Hitter");
            int HeavyHanded = chkModifiers.FindStringExact("Heavy-Handed");
            int MonstrousAttack = chkModifiers.FindStringExact("Monstrous Attacks");
            int Berserker = chkModifiers.FindStringExact("Berserker");
            int BloodLust = chkModifiers.FindStringExact("Bloodlust");
            int UnquenchableThirst = chkModifiers.FindStringExact("Unquenchable Thirst");
            int Fisticuffs = chkModifiers.FindStringExact("Fisticuffs");
            int FrayFighter = chkModifiers.FindStringExact("Fray Fighter");
            int HundredStrikes = chkModifiers.FindStringExact("Hundred Strikes");
            int SolidAssault = chkModifiers.FindStringExact("Solid Assault");

            if (HeavyHitter != -1)
            {
                if (chkModifiers.GetItemCheckState(HeavyHitter) == CheckState.Checked)
                {
                    if (_weapon.Name.ToLower().Contains("heavy"))
                    {
                        damageclass += (int)Math.Floor((float)_skills.Overpower / 4);
                    }
                } 
            }
            if (Berserker != -1)
            {
                if (chkModifiers.GetItemCheckState(Berserker) == CheckState.Checked)
                {
                    damageclass += (1 + _skills.Frenzy / 6);
                    sb.AppendLine(string.Format("New Damage Class: {0}", damageclass));
                }
            }
            if (BloodLust != -1)
            {
                if (chkModifiers.GetItemCheckState(BloodLust) == CheckState.Checked)
                {
                    damageclass += (1 + (int)nCount.Value);
                    sb.AppendLine(string.Format("New Damage Class: {0}", damageclass));
                }
            }
            if (UnquenchableThirst != -1)
            {
                if (chkModifiers.GetItemCheckState(UnquenchableThirst) == CheckState.Checked)
                {
                    damageclass += (1 + (int)nCount.Value);
                    sb.AppendLine(string.Format("New Damage Class: {0}", damageclass));
                }
            }
            sb.AppendLine(string.Format("Accuracy: {0}", accuracy + ((accuracy != 1)?_stats.Accuracy:0)));
            if (HeavyHanded != -1)
            {
                if (chkModifiers.GetItemCheckState(HeavyHanded) == CheckState.Checked)
                {
                    random = new Random(Guid.NewGuid().GetHashCode());
                    int hh = random.Next(1, 12) + _skills.Brawl;
                    sb.AppendLine(string.Format("Heavy-Handed Bonus: +{0}", Calc.GetTier(hh) + 2));
                    damageclass += Calc.GetTier(hh) + 2;
                } 
            }
            if (MonstrousAttack != -1)
            {
                if (chkModifiers.GetItemCheckState(MonstrousAttack) == CheckState.Checked)
                {
                    damageclass += (2 + _skills.Overpower / 6);
                    sb.AppendLine(string.Format("New Damage Class: {0}", damageclass));
                } 
            }
            
            if (Fisticuffs != -1)
            {
                if (chkModifiers.GetItemCheckState(Fisticuffs) == CheckState.Checked)
                {
                    damageclass += 1 + (int)Math.Floor((float)_skills.Brawl / 12);
                    sb.AppendLine(string.Format("New Damage Class: {0}", damageclass));
                } 
            }
            random = new Random(Guid.NewGuid().GetHashCode());
            int strike = random.Next(1, 12) ;
            while (strike % 12 == 0)
            {
                strike += random.Next(1, 12);
            }
            strike += _stats.Strike + _weapon.Mod.Strike;
            if (_weapon.Name.ToLower().Contains("firearm") || _weapon.Name.ToLower().Contains("bow"))
            {
                strike = accuracy+_stats.Accuracy;
            }
            int strikeTier = Calc.GetTier(strike);
            if (SolidAssault != -1)
            {
                if (chkModifiers.GetItemCheckState(SolidAssault) == CheckState.Checked)
                {
                    strikeTier += 1;
                    
                }
            }
            if (FrayFighter != -1)
            {
                if (chkModifiers.GetItemCheckState(FrayFighter) == CheckState.Checked)
                {
                    sb.AppendLine(string.Format("damage {0} adjacent opponents",(strikeTier < 4) ? (1 + strikeTier).ToString() : "all"));
                }
            }
            sb.AppendLine(string.Format("Strike: {0}, Damage Tier: {1}", strike, strikeTier));
            sb.AppendLine(string.Format("Damage: {0}", damageclass * strikeTier));
            if (HundredStrikes != -1)
            {
                for (int i = 0; i < _stats.AP-1; i++)
                {
                    strike = random.Next(1, 12) ;
                    while (strike % 12 == 0)
                    {
                        strike += random.Next(1, 12);
                    }
                    strike += _stats.Strike;
                    strikeTier = Calc.GetTier(strike);
                    sb.AppendLine(string.Format("Strike: {0}, Damage Tier: {1}", strike, strikeTier));
                    sb.AppendLine(string.Format("Damage: {0}", damageclass * strikeTier));
                }
            }
            
            txtDamage.Text = sb.ToString();
        }
    }
}
