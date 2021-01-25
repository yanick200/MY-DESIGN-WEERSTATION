using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MY_DESIGN_WEERSTATION
{
    public partial class StartScherm : Form
    {
        public StartScherm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                
                frmAanmelden frmLogin = new frmAanmelden(); //maakt nieuwe aanmeld form
                frmLogin.Show(); //toont het aanmeld form 
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnRegistreren_Click(object sender, EventArgs e)
        {
            try
            {
                Registreren frmRegistreren = new Registreren(); // maakt nieuwe registratie from
                frmRegistreren.Show(); // tooont het registratie form
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void StartScherm_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
