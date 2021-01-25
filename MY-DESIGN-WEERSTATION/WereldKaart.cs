using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
    public partial class WereldKaart : Form
    {
        public WereldKaart()
        {
            InitializeComponent();
        }

        private void btnZoeken_Click(object sender, EventArgs e)
        {
          
            try
            {

                string naam = cmbWeerNaam.Text;

                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open();

                OleDbDataAdapter adapetrLat = new OleDbDataAdapter(SQLScripts.WeerLat, conn);
                adapetrLat.SelectCommand.Parameters.AddWithValue("@Naam",naam);
                OleDbDataAdapter adapetrLon = new OleDbDataAdapter(SQLScripts.Weerlon, conn);
                adapetrLon.SelectCommand.Parameters.AddWithValue("@Naam", naam);

                DataTable dtLat = new DataTable();
                DataTable dtLon = new DataTable();

                adapetrLat.Fill(dtLat);
                adapetrLon.Fill(dtLon);

                DataRow[] drLat = dtLat.Select("WeerstationLatitude=WeerstationLatitude");
                txtLat.Text = Convert.ToString(drLat[0]["WeerstationLatitude"].ToString());

                DataRow[] drLon = dtLon.Select("WeerstationLongitude=WeerstationLongitude");
                txtLong.Text = Convert.ToString(drLon[0]["WeerstationLongitude"].ToString());

                double lat = Convert.ToDouble(txtLat.Text);
                double lon = Convert.ToDouble(txtLong.Text);

                map.DragButton = MouseButtons.Right;
                map.MapProvider = GMapProviders.GoogleMap;
                map.Position = new PointLatLng(lat, lon);
                map.MinZoom = 5; //min zoom 
                map.MaxZoom = 100; //max zoom
                map.Zoom = 10; //current zooming postition 
              

                PointLatLng point = new PointLatLng(lat, lon);
                GMapMarker marker = new GMarkerGoogle(point,GMarkerGoogleType.red_dot);
                marker.ToolTipText = naam;
                //create a overlay
                GMapOverlay markers = new GMapOverlay("markers");

                //add all available markers to that overlay
                markers.Markers.Add(marker);

                //cover map with overlay
                map.Overlays.Add(markers);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void WereldKaart_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQLScripts.AlleWeerstations, conn);

                DataSet ds = new DataSet(); //maakt een nieuwe dataset

                adapter.Fill(ds, "MijnTabel"); //adapter vult de dataset

                DataTable dtWeerNaam = new DataTable();

                adapter.Fill(dtWeerNaam);

                DataRow drWeerNaam = dtWeerNaam.NewRow();

                drWeerNaam["WeerstationNaam"] = "Selecteer een weerstation";
                dtWeerNaam.Rows.InsertAt(drWeerNaam, 0);
                cmbWeerNaam.DisplayMember = "WeerstationNaam";
                cmbWeerNaam.DataSource = dtWeerNaam;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            
           




        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            HOME frmHome = new HOME();
            frmHome.Show();
        }
    }
}
