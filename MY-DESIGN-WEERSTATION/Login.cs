using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //nodig voor te werken met een acces DB = database
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

namespace MY_DESIGN_WEERSTATION
{
    public partial class frmAanmelden : Form
    {
        public static string id;

        public frmAanmelden()
        {
            InitializeComponent();
        }

        private void btnAanmelden_Click(object sender, EventArgs e)
        {
            
            try
            {
               
                string username = txtGebruikersNaam.Text;
                string Password = txtWachtwoord.Text;
                
                //connectie maken met de DB 
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open(); //connectie openen met de DB

                //haalt de SQL instructie uit de klasse SQLScripts
                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.Login, conn);

                //haalt de waarde van de textbox en zet in de SQL instructie 
                adapter.SelectCommand.Parameters.AddWithValue("@inlognaam", username);
                adapter.SelectCommand.Parameters.AddWithValue("@password", Password);

                //maakt een nieuwe dataset
                DataSet ds = new DataSet();
                adapter.Fill(ds);//fult de data set met de info van de adapter

                int count = ds.Tables[0].Rows.Count;

                if (count == 1)
                {
                    
                    OleDbDataAdapter CheckType = new OleDbDataAdapter(SQLScripts.CheckType, conn);
                    CheckType.SelectCommand.Parameters.AddWithValue("@inlognaam", username);
                    DataTable Type = new DataTable();
                    CheckType.Fill(Type);

               
                    

                    OleDbDataAdapter GetID = new OleDbDataAdapter(SQLScripts.GetID, conn);
                    GetID.SelectCommand.Parameters.AddWithValue("@inlognaam", username);
                    DataTable ID = new DataTable();
                    GetID.Fill(ID);

                    DataRow[] drID = ID.Select("GebruikerID = GebruikerID");
                    id = Convert.ToString(drID[0]["GebruikerID"]);
                    NieuwWeerstation frmNieuwWeerstation = new NieuwWeerstation();
                    frmNieuwWeerstation.textBox1.Text = id;
                  



                    DataRow[] dr = Type.Select("GebruikerType = gebruikerType");
                    

                    if (dr[0]["GebruikerType"].ToString()== "admin")
                    {
                        
                        this.Close(); //sluit het vorige form 
                        HOME frmHome = new HOME();
                        frmHome.btnGebruikers.Enabled = true;
                        frmHome.btnGebruikers.Visible = true;
                        frmHome.Show(); //opent frmHome 
                  
                    }
                    else
                    {
                        
                        this.Close(); //sluit het vorige form 
                        HOME frmHome = new HOME();
                        frmHome.btnGebruikers.Enabled = false; //schakelt de knop gebruikers uit
                        frmHome.btnGebruikers.Visible = false;//maakt de knop niet zichtbaar
                        frmHome.Show(); //opent frmHome 
                        
                    }
                   
                }
                else
                {
                    MessageBox.Show("Inlognaam of wachtwoord is incorrect");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
         


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void txtWachtwoord_Click(object sender, EventArgs e)
        {
            try
            {
                //zorgt er voor dat de tekst weg gaat zodra je in de textbox klikt
                if (txtWachtwoord.Text == "Wachtwoord")
                {
                    txtWachtwoord.Text = "";
                    txtWachtwoord.PasswordChar = '*';
                    txtWachtwoord.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
        }

        private void txtGebruikersNaam_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            StartScherm frmstartscherm = new StartScherm();
            frmstartscherm.Show();
        }
    }
}
