using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //nodig om met de access DB te werken

namespace MY_DESIGN_WEERSTATION
{
    public partial class HOME : Form 
    {
        public HOME()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void HOME_Load(object sender, EventArgs e)
        {
          
        }

        private void btnWeerstation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Weerstations frmWeerstations = new Weerstations();
                frmWeerstations.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void btnGebruikers_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Gebruiker frmGebruiker = new Gebruiker();
                frmGebruiker.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void btnWereldkaart_Click(object sender, EventArgs e)
        {
            this.Hide();
            WereldKaart frmWereldkaart = new WereldKaart();
            frmWereldkaart.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            frmAanmelden frmLogin = new frmAanmelden();
            frmLogin.Show();
        }
    }
}
