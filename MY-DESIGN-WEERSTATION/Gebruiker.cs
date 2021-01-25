using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //nodig om met access DB te werken
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms.VisualStyles;

namespace MY_DESIGN_WEERSTATION
{
    public partial class Gebruiker : Form 
    {
        public Gebruiker()
        {
            InitializeComponent();
        }

        private void Gebruiker_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.AlleGebruikers, conn);

                DataSet ds = new DataSet(); //maakt een nieuwe dataset

                adapter.Fill(ds, "MijnTabel"); //adapter vult de dataset

                dataGridView1.DataSource = ds.Tables["MijnTabel"]; //zet da datgridview source naar de tables mijntabel


                //maakt nieuwe datatbels
                DataTable dt = new DataTable();
                DataTable dtStraat = new DataTable();
                DataTable dtPostcode = new DataTable();
                DataTable dtHuisnummer = new DataTable();
                DataTable dtEmail = new DataTable();
                DataTable dtGebruikerType = new DataTable();

                //vul de datattabels
                adapter.Fill(dt);
                adapter.Fill(dtStraat);
                adapter.Fill(dtPostcode);
                adapter.Fill(dtHuisnummer);
                adapter.Fill(dtEmail);
                adapter.Fill(dtGebruikerType);

                //maakt datarow 
                DataRow dr = dt.NewRow();
                DataRow drStraat = dtStraat.NewRow();
                DataRow drPostcode = dtPostcode.NewRow();
                DataRow drHuisnummer = dtHuisnummer.NewRow();
                DataRow drEmail = dtEmail.NewRow();
                DataRow drGebruikerType = dtGebruikerType.NewRow();

                //vul de datarows met de juiste informatie 
                                
                dr["GebruikerGemeente"] = "Selecteer een gemeente";
                dt.Rows.InsertAt(dr, 0);
                comboBox3.DisplayMember = "GebruikerGemeente";
                comboBox3.DataSource = dt;
                               
                drStraat["GebruikerStraat"] = "Selecteer een straat";
                dtStraat.Rows.InsertAt(drStraat, 0);
                comboBox4.DisplayMember = "GebruikerStraat";
                comboBox4.DataSource = dtStraat;

                drPostcode["GebruikerPostcode"] = "Selecteer een postcode";
                dtPostcode.Rows.InsertAt(drPostcode, 0);
                comboBox5.DisplayMember = "GebruikerPostcode";
                comboBox5.DataSource = dtPostcode;

                drHuisnummer["GebruikerHuisNR"] = "Selecteer een huisnummer";
                dtHuisnummer.Rows.InsertAt(drHuisnummer, 0);
                comboBox6.DisplayMember = "GebruikerHuisNR";
                comboBox6.DataSource = dtHuisnummer;

                drEmail["GebruikerEmail"] = "Selecteer een Email";
                dtEmail.Rows.InsertAt(drEmail, 0);
                comboBox7.DisplayMember = "GebruikerEmail";
                comboBox7.DataSource = dtEmail;

                drGebruikerType["GebruikerType"] = "Selecteer een type";
                dtGebruikerType.Rows.InsertAt(drGebruikerType, 0);
                comboBox8.DisplayMember = "GebruikerType";
                comboBox8.DataSource = dtGebruikerType;
                
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
            
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open();

                OleDbDataAdapter adapterFilter = new OleDbDataAdapter(SQLScripts.FilterVoornaam, conn);
                OleDbDataAdapter adapterFilterNaam = new OleDbDataAdapter(SQLScripts.FilterNaam, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.AlleGebruikers, conn);
                OleDbDataAdapter adapterFIlterNaamVoornaam = new OleDbDataAdapter(SQLScripts.FilterNaamEnVoornaam, conn);
                OleDbDataAdapter adapterFilterGemeente = new OleDbDataAdapter(SQLScripts.FilterGemeentes, conn);
                OleDbDataAdapter adapterFilterStraat = new OleDbDataAdapter(SQLScripts.FilterStaart, conn);
                OleDbDataAdapter adapterFilterPostcode = new OleDbDataAdapter(SQLScripts.FilterPostcode, conn);
                OleDbDataAdapter adapterFilterHuisnummer = new OleDbDataAdapter(SQLScripts.FilterHuisnummer, conn);
                OleDbDataAdapter adapterFilterEmail = new OleDbDataAdapter(SQLScripts.FilterEmail, conn);
                OleDbDataAdapter adapterFilterGebruikerType = new OleDbDataAdapter(SQLScripts.FilterGebruikerType, conn);
                OleDbDataAdapter adapterFilterAlles = new OleDbDataAdapter(SQLScripts.FilterAlles, conn);

                DataSet ds = new DataSet();



                if (textBox2.Text == "Voornaam" && textBox1.Text == "Naam" && comboBox3.SelectedIndex == 0 && comboBox4.SelectedIndex == 0 && comboBox5.SelectedIndex == 0 && comboBox6.SelectedIndex == 0 && comboBox7.SelectedIndex ==0 && comboBox8.SelectedIndex ==0)
                {

                    adapter.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();

                }
                else if (textBox2.Text != "Voornaam" && textBox1.Text != "Naam" && comboBox3.SelectedIndex != 0 && comboBox4.SelectedIndex != 0 && comboBox5.SelectedIndex != 0 && comboBox6.SelectedIndex != 0 && comboBox7.SelectedIndex != 0 && comboBox8.SelectedIndex != 0)
                {
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Voornaam", textBox2.Text);
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Naam", textBox1.Text);
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Gemeente", Convert.ToString(comboBox3.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Straat", Convert.ToString(comboBox4.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Postcode", Convert.ToString(comboBox5.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Huisnummer", Convert.ToString(comboBox6.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Email", Convert.ToString(comboBox7.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@GebruikerType", Convert.ToString(comboBox8.Text));
                    adapterFilterAlles.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }
                else if (textBox2.Text != "Voornaam" && textBox1.Text != "Naam")
                {
                    adapterFIlterNaamVoornaam.SelectCommand.Parameters.AddWithValue("@Voornaam", textBox2.Text);
                    adapterFIlterNaamVoornaam.SelectCommand.Parameters.AddWithValue("@Naam", textBox1.Text);
                    adapterFIlterNaamVoornaam.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                   

                }
                else if (textBox2.Text != "Voornaam")
                {

                    adapterFilter.SelectCommand.Parameters.AddWithValue("@Voornaam", textBox2.Text);
                    adapterFilter.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                  
                }
                else if (textBox1.Text != "Naam")
                {

                    adapterFilterNaam.SelectCommand.Parameters.AddWithValue("@Naam", textBox1.Text);
                    adapterFilterNaam.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                   

                }

                if (comboBox3.SelectedIndex != 0)
                {
                    adapterFilterGemeente.SelectCommand.Parameters.AddWithValue("@Gemeente", Convert.ToString(comboBox3.Text));
                    adapterFilterGemeente.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                    comboBox3.SelectedIndex = 0;
                }

                if (comboBox4.SelectedIndex != 0)
                {
                    comboBox3.SelectedIndex = 0;
                    adapterFilterStraat.SelectCommand.Parameters.AddWithValue("@Straat", Convert.ToString(comboBox4.Text));
                    adapterFilterStraat.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                   
                }

                if (comboBox5.SelectedIndex != 0)
                {
                    adapterFilterPostcode.SelectCommand.Parameters.AddWithValue("@Postcode", Convert.ToString(comboBox5.Text));
                    adapterFilterPostcode.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                   
                }

                if (comboBox6.SelectedIndex != 0)
                {
                    adapterFilterHuisnummer.SelectCommand.Parameters.AddWithValue("@Huisnummer", Convert.ToString(comboBox6.Text));
                    adapterFilterHuisnummer.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                   
                }

                if (comboBox7.SelectedIndex != 0)
                {
                    adapterFilterEmail.SelectCommand.Parameters.AddWithValue("@Email", Convert.ToString(comboBox7.Text));
                    adapterFilterEmail.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                  
                }

                if (comboBox8.SelectedIndex != 0)
                {
                    adapterFilterGebruikerType.SelectCommand.Parameters.AddWithValue("@GebruikerType", Convert.ToString(comboBox8.Text));
                    adapterFilterGebruikerType.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
            
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                GebruikerToevoegen frmGebruikerToevoegen = new GebruikerToevoegen();
                frmGebruikerToevoegen.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                GebruikerWijzigen frmGebruikerWijzigen = new GebruikerWijzigen(); //roept de form GebruikerWijzigen 

                //vult de de textboxen van de from GebruikerWWijzigen 
                //met de overeenkomende data van het datagridview
                frmGebruikerWijzigen.txtGebruikerID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frmGebruikerWijzigen.txbNaam.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frmGebruikerWijzigen.txtVoornaam.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                frmGebruikerWijzigen.txbStraat.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                frmGebruikerWijzigen.txbGemeente.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                frmGebruikerWijzigen.txbHuisnummer.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                frmGebruikerWijzigen.txbPostcode.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                frmGebruikerWijzigen.txbTelefoonnummer.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                frmGebruikerWijzigen.txbEmail.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                frmGebruikerWijzigen.txbWachtwoord.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                frmGebruikerWijzigen.txbGebruikersnaam.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frmGebruikerWijzigen.cmbType.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                frmGebruikerWijzigen.Show();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //gaat over ellke geslecteerd rij in de data grid 
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {

                    OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQLScripts.DeleteGebruiker;
                    cmd.Connection = conn;

                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); //haalt de id van gebruikerID
                    cmd.Parameters.AddWithValue("@ID", id);

                    dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index); //verwijderd de rij uit de DataGridView

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
           
           

            

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //zorgt er voor dat waarde weergegeven word als * 

                if (e.ColumnIndex == 4 && e.Value != null) //als de collomindex = 4 (wachtwoord) en het is nul 
                {
                    e.Value = new string('*', e.Value.ToString().Length); //dan zet de waarde om in * voor de lengte van de string 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.AlleGebruikers, conn);

                DataSet ds = new DataSet(); //maakt een nieuwe dataset

                adapter.Fill(ds, "MijnTabel"); //adapter vult de dataset

                dataGridView1.DataSource = ds.Tables["MijnTabel"]; //zet da datgridview source naar de tables mijntabel

                textBox1.Text = "Naam";
                textBox2.Text = "Voornaam";
                comboBox3.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                comboBox6.SelectedIndex = 0;
                comboBox7.SelectedIndex = 0;
                comboBox8.SelectedIndex = 0;
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            HOME frmHome = new HOME();
            frmHome.Show();
        }
    }
}
