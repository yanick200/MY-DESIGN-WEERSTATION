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
    public partial class GegevensBekijken : Form
    {
        public GegevensBekijken()
        {
            InitializeComponent();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                GegevensDetails frmGegevensDetails = new GegevensDetails();

                frmGegevensDetails.txtGegevenID.Text = dgvGegevens.CurrentRow.Cells[0].Value.ToString();
                frmGegevensDetails.txtDatum.Text = dgvGegevens.CurrentRow.Cells[1].Value.ToString();
                frmGegevensDetails.txtTemperatuur.Text = dgvGegevens.CurrentRow.Cells[2].Value.ToString();
                frmGegevensDetails.txtWind.Text = dgvGegevens.CurrentRow.Cells[3].Value.ToString();
                frmGegevensDetails.txtNeerslag.Text = dgvGegevens.CurrentRow.Cells[4].Value.ToString();
                frmGegevensDetails.txtLuchtdruk.Text = dgvGegevens.CurrentRow.Cells[5].Value.ToString();
                frmGegevensDetails.txtLuchtvochtigheid.Text = dgvGegevens.CurrentRow.Cells[6].Value.ToString();

                frmGegevensDetails.Show();
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

                OleDbDataAdapter adapeterFilter = new OleDbDataAdapter(SQLScripts.FilterDatum, conn);
                OleDbDataAdapter adapterFilterTemp = new OleDbDataAdapter(SQLScripts.FilterTemp, conn);
                OleDbDataAdapter adapterFilterWind = new OleDbDataAdapter(SQLScripts.FilterWind, conn);
                OleDbDataAdapter adapterFilterNeerslag = new OleDbDataAdapter(SQLScripts.FilterNeerslag, conn);
                OleDbDataAdapter adapterFilterLuchtdruk = new OleDbDataAdapter(SQLScripts.FilterLuchtdruk, conn);
                OleDbDataAdapter adapterFilerLuchtvochtigheid = new OleDbDataAdapter(SQLScripts.FilterLuchtvochtigheid, conn);

                DataSet ds = new DataSet();

                if (cmbDatum.SelectedIndex != 0)
                {
                    adapeterFilter.SelectCommand.Parameters.AddWithValue("@Datum", Convert.ToString(cmbDatum.Text));
                    adapeterFilter.Fill(ds, "MijnTabel");
                    dgvGegevens.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }

                if (cmbTemperatuur.SelectedIndex != 0)
                {
                    adapterFilterTemp.SelectCommand.Parameters.AddWithValue("@Temp", Convert.ToString(cmbTemperatuur.Text));
                    adapterFilterTemp.Fill(ds, "MijnTabel");
                    dgvGegevens.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }

                if (cmbWind.SelectedIndex != 0)
                {
                    adapterFilterWind.SelectCommand.Parameters.AddWithValue("@Wind", Convert.ToString(cmbWind.Text));
                    adapterFilterWind.Fill(ds, "MijnTabel");
                    dgvGegevens.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }

                if (cmbNeerlag.SelectedIndex != 0)
                {
                    adapterFilterNeerslag.SelectCommand.Parameters.AddWithValue("@Neerslag", Convert.ToString(cmbNeerlag.Text));
                    adapterFilterNeerslag.Fill(ds, "MijnTabel");
                    dgvGegevens.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }

                if (cmbLuchtdruk.SelectedIndex != 0)
                {
                    adapterFilterLuchtdruk.SelectCommand.Parameters.AddWithValue("@Luchtdruk", Convert.ToString(cmbLuchtdruk.Text));
                    adapterFilterLuchtdruk.Fill(ds, "MijnTabel");
                    dgvGegevens.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }
                if (cmbLuchtVochtigheid.SelectedIndex != 0)
                {
                    adapterFilerLuchtvochtigheid.SelectCommand.Parameters.AddWithValue("@Luchtvochtigheid", Convert.ToString(cmbLuchtVochtigheid.Text));
                    adapterFilerLuchtvochtigheid.Fill(ds, "MijnTabel");
                    dgvGegevens.DataSource = ds.Tables["MijnTabel"];
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
          
        }

        private void GegevensBekijken_Load(object sender, EventArgs e)
        {
            try
            {
                Weerstations frmweerstations = new Weerstations();

                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.Gegevens, conn);

                adapter.SelectCommand.Parameters.AddWithValue("@ID",Weerstations.id);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "MijnTabel");
                dgvGegevens.DataSource = ds.Tables["MijnTabel"];

                DataTable dtDatum = new DataTable();
                DataTable dtTemperatuur = new DataTable();
                DataTable dtWind = new DataTable();
                DataTable dtNeerslag = new DataTable();
                DataTable dtLuchtdruk = new DataTable();
                DataTable dtRelLuchtvochtigheid = new DataTable();

                adapter.Fill(dtDatum);
                adapter.Fill(dtTemperatuur);
                adapter.Fill(dtWind);
                adapter.Fill(dtNeerslag);
                adapter.Fill(dtLuchtdruk);
                adapter.Fill(dtRelLuchtvochtigheid);

                DataRow drDatum = dtDatum.NewRow();
                DataRow drTemperatuur = dtTemperatuur.NewRow();
                DataRow drWind = dtWind.NewRow();
                DataRow drNeerslag = dtNeerslag.NewRow();
                DataRow drLuchtdruk = dtLuchtdruk.NewRow();
                DataRow drRelLuchtVochtigheid = dtRelLuchtvochtigheid.NewRow();

                drDatum["GegDatum"] = "Selecteer een datum";
                dtDatum.Rows.InsertAt(drDatum, 0);
                cmbDatum.DisplayMember = "GegDatum";
                cmbDatum.DataSource = dtDatum;

                drTemperatuur["GegTemperatuur"] = "Selecteer de temperatuur";
                dtTemperatuur.Rows.InsertAt(drTemperatuur, 0);
                cmbTemperatuur.DisplayMember = "GegTemperatuur";
                cmbTemperatuur.DataSource = dtTemperatuur;

                drWind["GegWind"] = "Selecteer wind sterkte";
                dtWind.Rows.InsertAt(drWind, 0);
                cmbWind.DisplayMember = "GegWind";
                cmbWind.DataSource = dtWind;

                drNeerslag["GegNeerslag"] = "Selecteer de hoeveelheid neerslag";
                dtNeerslag.Rows.InsertAt(drNeerslag, 0);
                cmbNeerlag.DisplayMember = "GegNeerslag";
                cmbNeerlag.DataSource = dtNeerslag;

                drLuchtdruk["GegLuchtdruk"] = "Selecteer de hoeveelheid lucht";
                dtLuchtdruk.Rows.InsertAt(drLuchtdruk, 0);
                cmbLuchtdruk.DisplayMember = "GegLuchtdruk";
                cmbLuchtdruk.DataSource = dtLuchtdruk;

                drRelLuchtVochtigheid["GegRelativeLuchtvochtigheid"] = "Selecteer de hoeveelheid luchtvochtigheid";
                dtRelLuchtvochtigheid.Rows.InsertAt(drRelLuchtVochtigheid, 0);
                cmbLuchtVochtigheid.DisplayMember = "GegRelativeLuchtvochtigheid";
                cmbLuchtVochtigheid.DataSource = dtRelLuchtvochtigheid;

                conn.Close();
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
                Weerstations frmweerstations = new Weerstations();

                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);
                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.Gegevens, conn);

                adapter.SelectCommand.Parameters.AddWithValue("@ID", Weerstations.id);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "MijnTabel");
                dgvGegevens.DataSource = ds.Tables["MijnTabel"];

                cmbDatum.SelectedIndex = 0;
                cmbTemperatuur.SelectedIndex = 0;
                cmbWind.SelectedIndex = 0;
                cmbNeerlag.SelectedIndex = 0;
                cmbLuchtdruk.SelectedIndex = 0;
                cmbLuchtVochtigheid.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnVerwijderen_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQLScripts.DeleteEnkelGegevens;
                cmd.Connection = conn;

                string id = dgvGegevens.CurrentRow.Cells[0].Value.ToString();

                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            Weerstations frmWeerstations = new Weerstations();
            frmWeerstations.Show();
        }
    }
}
