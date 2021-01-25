using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace MY_DESIGN_WEERSTATION
{
    public partial class Weerstations : Form
    {
        public static string id;
        public Weerstations()
        {
            InitializeComponent();
           
          

        }
       
        private void button1_Click(object sender, EventArgs e) 
        {
            try
            {
                this.Hide();
                NieuwWeerstation frmNiewWeerstation = new NieuwWeerstation();
                frmNiewWeerstation.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
          
        }

        private void Weerstations_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.AlleWeerstations, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "MijnTabel");
                dataGridView1.DataSource = ds.Tables["MijnTabel"];

                DataTable dtLand = new DataTable();
                DataTable dtGemeente = new DataTable();
                DataTable dtType = new DataTable();
                DataTable dtSite = new DataTable();

                adapter.Fill(dtLand);
                adapter.Fill(dtGemeente);
                adapter.Fill(dtType);
                adapter.Fill(dtSite);

                DataRow drLand = dtLand.NewRow();
                DataRow drGemeente = dtGemeente.NewRow();
                DataRow drType = dtType.NewRow();
                DataRow drSite = dtSite.NewRow();

                drLand["WeerstationLand"] = "Selecteer een land";
                dtLand.Rows.InsertAt(drLand, 0);
                comboBox1.DisplayMember = "WeerstationLand";
                comboBox1.DataSource = dtLand;

                drGemeente["WeerstationGemeente"] = "Selecteer een geemente/stad";
                dtGemeente.Rows.InsertAt(drGemeente, 0);
                comboBox2.DisplayMember = "WeerstationGemeente";
                comboBox2.DataSource = dtGemeente;

                drType["WeerstationTypeNetwerk"] = "Selecteer het type netwerk";
                dtType.Rows.InsertAt(drType, 0);
                comboBox3.DisplayMember = "WeerstationTypeNetwerk";
                comboBox3.DataSource = dtType;

                drSite["WeerstationWebsite"] = "Selecteer een website";
                dtSite.Rows.InsertAt(drSite, 0);
                comboBox4.DisplayMember = "WeerstationWebsite";
                comboBox4.DataSource = dtSite;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGegevensToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                GegevensToevoegen frmGegevensToevoegen = new GegevensToevoegen();
                frmGegevensToevoegen.txtWeerstationID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frmGegevensToevoegen.txtWeerstationNaam.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frmGegevensToevoegen.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
         
        }

        private void btnGegevensBekijken_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                GegevensBekijken frmGegevensBekijken = new GegevensBekijken();
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frmGegevensBekijken.Show();
                
                
                /*
                GegevensBekijken frmGegevensBekijken = new GegevensBekijken();

                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.Gegevens,conn);

                adapter.SelectCommand.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value.ToString());

                DataSet ds = new DataSet();
                adapter.Fill(ds,"MijnTabel");
                frmGegevensBekijken.dgvGegevens.DataSource = ds.Tables["MijnTabel"];
                frmGegevensBekijken.Show();
                conn.Close();*/
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }

        private void btnZoeken_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
                conn.Open();

                OleDbDataAdapter adapterFilterLand = new OleDbDataAdapter(SQLScripts.FilterLand, conn);
                OleDbDataAdapter adapaterFilterGemeente = new OleDbDataAdapter(SQLScripts.FilterGemeente, conn);
                OleDbDataAdapter adapterFilterType = new OleDbDataAdapter(SQLScripts.FilterType, conn);
                OleDbDataAdapter adapterFilterSite = new OleDbDataAdapter(SQLScripts.FilterSite, conn);
                OleDbDataAdapter adapterFilterAlles = new OleDbDataAdapter(SQLScripts.FilterAllesWeer, conn);
                DataSet ds = new DataSet();

                /*
                if (comboBox1.SelectedIndex != 0 || comboBox2.SelectedIndex !=0 || comboBox3.SelectedIndex != 0 || comboBox4.SelectedIndex != 0)
                {
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Land", Convert.ToString(comboBox1.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Gemeente", Convert.ToString(comboBox2.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Type", Convert.ToString(comboBox3.Text));
                    adapterFilterAlles.SelectCommand.Parameters.AddWithValue("@Site", Convert.ToString(comboBox4.Text));

                    adapterFilterAlles.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }
                */
                if (comboBox1.SelectedIndex !=0)
                {
                    adapterFilterLand.SelectCommand.Parameters.AddWithValue("@Land", Convert.ToString(comboBox1.Text));
                    adapterFilterLand.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }
               
                if(comboBox2.SelectedIndex != 0)
                {
                    adapaterFilterGemeente.SelectCommand.Parameters.AddWithValue("@Gemeente", Convert.ToString(comboBox2.Text));
                    adapaterFilterGemeente.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }

                if(comboBox3.SelectedIndex != 0)
                {
                    adapterFilterType.SelectCommand.Parameters.AddWithValue("@Type", Convert.ToString(comboBox3.Text));
                    adapterFilterType.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }

                if (comboBox4.SelectedIndex !=0)
                {
                    adapterFilterSite.SelectCommand.Parameters.AddWithValue("@Site", Convert.ToString(comboBox4.Text));
                    adapterFilterSite.Fill(ds, "MijnTabel");
                    dataGridView1.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnResetFiltet_Click(object sender, EventArgs e)
        {

            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.AlleWeerstations, conn);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "MijnTabel");
                dataGridView1.DataSource = ds.Tables["MijnTabel"];

                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnVerwijderen_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
           
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = SQLScripts.DeleteWeerstation;
            cmd.Connection = conn;

            OleDbCommand cmdGegevens = new OleDbCommand();
            cmdGegevens.CommandType = CommandType.Text;
            cmdGegevens.CommandText = SQLScripts.DeleteGegevens;
            cmdGegevens.Connection = conn;

            cmd.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            cmdGegevens.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value.ToString());

            conn.Open();
            cmd.ExecuteNonQuery();
            cmdGegevens.ExecuteNonQuery();
            conn.Close();
            

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
          
            
                this.Close(); //sluit het vorige form 
                HOME frmHome = new HOME();
              
                frmHome.Show(); //opent frmHome 

            


           
        }
    }
}
