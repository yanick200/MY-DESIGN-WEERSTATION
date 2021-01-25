using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //nodig om met access db te werken 
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace MY_DESIGN_WEERSTATION
{
    public partial class NieuwWeerstation : Form
    {
        public NieuwWeerstation()
        {
            InitializeComponent();
          
            textBox1.Text = frmAanmelden.id;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
            
            try
            {

                //  frmAanmelden frmLogin = new frmAanmelden();
                string Gerbuiker_ID;
                string Naam = txtNaamWeer.Text;
                string Land = txtLandWeer.Text;
                string Gemeente = txtGemeenteWeer.Text;
                double Lon = Convert.ToDouble(txtlon.Text);
                double lat = Convert.ToDouble(txtLat.Text);
                Int16 Rating = Convert.ToInt16(txtRatingWeer.Text);
                string Type = comboBox1.Text;
                string Site = txtSiteWeer.Text;
                 Gerbuiker_ID= textBox1.Text ;

                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.ControleStationNaam, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Naam", Naam);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                int count = ds.Tables[0].Rows.Count;

                if (count == 1)
                {
                    MessageBox.Show("De naam bestaat al gelieve een ander in te voeren");
                }
                else
                {
                    if (Naam != "" && Land != "" && Gemeente != "" && Rating >= 0 && Type != "" && Site != "")
                    {
                        //OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                        DataTable dt = new DataTable();

                        OleDbCommand cmd = new OleDbCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SQLScripts.AddWeerstation;
                        cmd.Connection = conn;

                        cmd.Parameters.AddWithValue("@GebruikerID", Gerbuiker_ID);
                        cmd.Parameters.AddWithValue("@Naam", Naam);
                        cmd.Parameters.AddWithValue("@Land", Land);
                        cmd.Parameters.AddWithValue("@Gemeente", Gemeente);
                        cmd.Parameters.AddWithValue("@lat", lat);
                        cmd.Parameters.AddWithValue("@Lon", Lon);
                        cmd.Parameters.AddWithValue("@Rating", Rating);
                        cmd.Parameters.AddWithValue("@Type", Type);
                        cmd.Parameters.AddWithValue("@Site", Site);

                       // conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        this.Close();
                        Weerstations frmWeerstation = new Weerstations();
                        frmWeerstation.Show();

                    }
                    else
                    {
                        MessageBox.Show("niet alle velden zijn in gevuld");
                    }
                }

               
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

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
            Weerstations frmWeerstation = new Weerstations();
            frmWeerstation.Show();
       

           
        }
    }
}
