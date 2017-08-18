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
    public partial class ucSpecialties : UserControl
    {
        Specialty _specialty;
        public ucSpecialties(Specialty specialty)
        {
            InitializeComponent();
            _specialty = specialty;
        }

        private void ucSpecialties_Load(object sender, EventArgs e)
        {
            lblSpecialty.Text = _specialty.Name;
        }
    }
}
