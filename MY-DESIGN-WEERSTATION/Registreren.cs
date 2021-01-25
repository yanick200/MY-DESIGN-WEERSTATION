using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //nodig om met access DB te kunnen werken
using System.Data.SqlClient;

namespace MY_DESIGN_WEERSTATION
{
    public partial class Registreren : Form
    {
        public Registreren()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                Int32 Telefoonnummer =Convert.ToInt32(txbTelefoonnummer.Text);
                string Email = txbEmail.Text;
                string Wachtwoord = txbWachtwoord.Text;
                string Gebruikersnaam = txbGebruikersnaam.Text;
                string Type = cmbType.Text;

                if (Wachtwoord.Length < 8)
                {
                    MessageBox.Show("wachtwoord is te kort"+"\n" +"het moet 8 karakters lang zijn of langer");
                }
                else
                {
                    //controle of elk veld wel ingevuld is 
                    if (Naam != "" && Voornaam != "" && Straat != "" && Gemeente != "" && HuisNR != "" && Postcode != 0 && Telefoonnummer != 0 && Email != "" && Wachtwoord != "" && Gebruikersnaam != "" && Type != "")
                    {
                        //maakt connectie met de DB
                        OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                        //haalt de SQL instructie uit de klasse SQLScripts
                        OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.ControleInlognaam, conn);

                        //haalt de waarde van de textbox en zet in de SQL instructie 
                        adapter.SelectCommand.Parameters.AddWithValue("@inlognaam", txbGebruikersnaam.Text);

                        //maakt een nieuwe dataset
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);//fult de data set met de info van de adapter

                        int count = ds.Tables[0].Rows.Count; //checkt of de rij bestaat

                        if (count == 1) //als de count 1 wilt zegggen rij bestat dus zo ook de inlognaam
                        {
                            MessageBox.Show("De GebruikersNaam Bestaat al"); //toont een MessageBox weer met de gepaste boodschap
                        }
                        else
                        {
                            // maakt een nieuwe dataTable
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
                            cmd.Parameters.AddWithValue("@Telefoonnummer", Convert.ToString(Telefoonnummer));
                            cmd.Parameters.AddWithValue("@Email", Email);
                            cmd.Parameters.AddWithValue("@Wachtwoord", Wachtwoord);
                            cmd.Parameters.AddWithValue("@LoginNaam", Gebruikersnaam);
                            cmd.Parameters.AddWithValue("@GebruikerType", Type);

                            conn.Open();//connectie met DB openen
                            cmd.ExecuteNonQuery(); //het commando uitvoeren

                            conn.Close();//connectie met DB sluiten
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Niet alle velden zijn in gevuld");
                    }
                }

               


                

            }catch (FormatException) //gaat over foutieve ingevoerde waarden 
            {
                MessageBox.Show("U heeft ergens foutief gegegven ingevoerd controleer alles nog eens"); 
            }
            catch (Exception ex) //geeft elke andere soort error weer 
            {

                MessageBox.Show(ex.ToString()); //geeft de error weer 
            }
            
            
        }

        private void txbPostcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtVoornaam_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbGebruikersnaam_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbWachtwoord_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txbHuisnummer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbTelefoonnummer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbGemeente_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbStraat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbNaam_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblStraat_Click(object sender, EventArgs e)
        {

        }

        private void lblVoornaam_Click(object sender, EventArgs e)
        {

        }

        private void lblNaam_Click(object sender, EventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
