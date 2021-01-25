using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //nodig om te werken met een access DB

namespace MY_DESIGN_WEERSTATION
{
    public partial class GebruikerToevoegen : Form
    {
        public GebruikerToevoegen()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //zet inhoud van de textboxen in variablen 
                string Naam = txbNaam.Text;
                string Voornaam = txtVoornaam.Text;
                string Straat = txbStraat.Text;
                string Gemeente = txbGemeente.Text;
                string HuisNR = txbHuisnummer.Text;
                Int16 Postcode = Convert.ToInt16(txbPostcode.Text);
                Int32 Telefoonnummer = Convert.ToInt32(txbTelefoonnummer.Text);
                string Email = txbEmail.Text;
                string Wachtwoord = txbWachtwoord.Text;
                string Gebruikersnaam = txbGebruikersnaam.Text;
                string Type = cmbType.Text;
               
             
                
                
                    if (Naam != "" && Voornaam != "" && Straat != "" && Gemeente != "" && HuisNR != "" && Postcode != 0 && Telefoonnummer != 0 && Email != "" && Wachtwoord != "" && Gebruikersnaam != "" && Type != "")
                    {

                        OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
                        
                        /*
                        OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.ControleInlognaam, conn);
                        adapter.SelectCommand.Parameters.AddWithValue("@inlognaam", conn);

                        DataSet ds = new DataSet();
                        adapter.Fill(ds,"MijnTabel");

                        int count = ds.Tables["MijnTabel"].Rows.Count;

                        if (count == 1)
                        {
                            MessageBox.Show("deze inlognaam is al in gebruik");
                        }
                        */
                        //maakt een nieuwe dataTable
                        DataTable dt = new DataTable();

                        //maakt een nieuwe command
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.CommandType = CommandType.Text; //zet het command type als text
                        cmd.CommandText = SQLScripts.Registreren; //zet de command text gelijk aan SQL instructie Registreren uit de klasse SQLScripts
                        cmd.Connection = conn; //zet de command connectie gelijk met de DB connectie 

                        //de command vullen met de nodige parameters en hun waarden 
                        cmd.Parameters.AddWithValue("@GebruikersNaam", Naam);
                        cmd.Parameters.AddWithValue("@GebruikersVoornaam", Voornaam);
                        cmd.Parameters.AddWithValue("@straat", Straat);
                        cmd.Parameters.AddWithValue("@Gemeente", Gemeente);
                        cmd.Parameters.AddWithValue("@HuisNR", HuisNR);
                        cmd.Parameters.AddWithValue("@Postcode", Convert.ToString(Postcode));
                        cmd.Parameters.AddWithValue("@Telefoonnummer",Convert.ToString(Telefoonnummer));
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Wachtwoord", Wachtwoord);
                        cmd.Parameters.AddWithValue("@LoginNaam", Gebruikersnaam);
                        cmd.Parameters.AddWithValue("@GebruikerType", Type);

                        conn.Open();//connectie met DB openen
                        cmd.ExecuteNonQuery(); //het commando uitvoeren

                        conn.Close();//connectie met DB sluiten
                        this.Close();
                        Gebruiker frmGebruikers = new Gebruiker();
                        frmGebruikers.Show();
                    }
                    else
                    {
                        MessageBox.Show("Niet alle velden zijn in gevuld");
                    }
                
               

               
            }catch(FormatException)
            {
                MessageBox.Show("U heeft ergens foutief gegegven ingevoerd controleer alles nog eens");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
            Gebruiker frmGebruiker = new Gebruiker();
            frmGebruiker.Show();
        }
    }
}
