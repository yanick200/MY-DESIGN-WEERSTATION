using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //nodig om te werken met een acces DB

namespace MY_DESIGN_WEERSTATION
{
    public partial class GebruikerWijzigen : Form
    {
        public GebruikerWijzigen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int ID =Convert.ToInt32( txtGebruikerID.Text);
                string Naam =Convert.ToString( txbNaam.Text);
                string Voornaam = Convert.ToString(txtVoornaam.Text);
                string Straat = Convert.ToString(txbStraat.Text);
                string Gemeente = Convert.ToString(txbGemeente.Text);
                string HuisNR = Convert.ToString(txbHuisnummer.Text);
                string Postcode = Convert.ToString(txbPostcode.Text);
                string TelefoonNummer = Convert.ToString(txbTelefoonnummer.Text);
                string Email = Convert.ToString(txbEmail.Text);
                string Wachtwoord = Convert.ToString(txbWachtwoord.Text);
                string GebruikersNaam = Convert.ToString(txbGebruikersnaam.Text);
                string GebruikerType = Convert.ToString(cmbType.Text);

                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
               

                OleDbCommand cmd = new OleDbCommand(SQLScripts.UpdateGebruiker,conn);
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = SQLScripts.UpdateGebruiker;
                //cmd.Connection = conn;

               
                cmd.Parameters.AddWithValue("@Naam", Naam);
                cmd.Parameters.AddWithValue("@Voornaam", Voornaam);
                cmd.Parameters.AddWithValue("@Straat", Straat);
                cmd.Parameters.AddWithValue("@Gemeente", Gemeente);
                cmd.Parameters.AddWithValue("@HuisNR", HuisNR);
                cmd.Parameters.AddWithValue("@Postcode", Postcode);
                cmd.Parameters.AddWithValue("@TelefoonNummer", TelefoonNummer);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@wachtwoord", Wachtwoord);
                cmd.Parameters.AddWithValue("@gebruikersnaam", GebruikersNaam);
                cmd.Parameters.AddWithValue("@gebruikertype", GebruikerType);
                cmd.Parameters.AddWithValue("@ID", ID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                this.Close();
                Gebruiker frmGebruiker = new Gebruiker();
                frmGebruiker.Show();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
          
            
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Close();
            Gebruiker frmGebruiker = new Gebruiker();
            frmGebruiker.Show();
        }
    }
}
