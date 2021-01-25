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
using System.IO; //nodig om input van files te krijgen
using System.Text.RegularExpressions; //nodig om met regex te kunnen werken 

namespace MY_DESIGN_WEERSTATION
{
    public partial class GegevensToevoegen : Form
    {
        public GegevensToevoegen()
        {
            InitializeComponent();
        }

        private void btnOpenXLSXBestand_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog2 = new OpenFileDialog
                {


                    InitialDirectory = @"C:\",
                    DefaultExt = "csv",
                    Filter = "csv files (*.csv) | *.csv",

                };
                if (openFileDialog2.ShowDialog()== DialogResult.OK)
                {
                    //geeft het path van de file 
                    string path = openFileDialog2.FileName;

                    //een gegevensleze aanmaken om de tekst uit het CSV-bestand te halen
                    StreamReader srReaderData = new StreamReader(path);

                    //eerste lijn lezen en gerbruiken als cel hoofding
                    string[] csvHeader = srReaderData.ReadLine().Split(',');

                    //een datatabel aanmken om gegevens op te slaan
                    DataTable dtCsvData = new DataTable();

                    //celhoofding toekennen aan nieuwe kolom in datatabel
                    foreach (string header in csvHeader)
                    {
                        dtCsvData.Columns.Add(header);
                    }

                    while (!srReaderData.EndOfStream)
                    {
                        string[] csvRows = Regex.Split(srReaderData.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                        DataRow drcsvDataRow = dtCsvData.NewRow();

                        for (int counter = 0; counter < csvHeader.Length; counter++)
                        {
                            drcsvDataRow[counter] = csvRows[counter];
                        }
                        dtCsvData.Rows.Add(drcsvDataRow);
                    }

                    dataGridView1.DataSource = dtCsvData;
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string datum = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string Temperatuur = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Wind = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string Neerslag = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string Luchtdruk = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string RelativeLuchtvochtigheid = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                string WeerstationID = txtWeerstationID.Text;


                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.DB_MyDesign);

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQLScripts.InsertWeerGegevens;
                cmd.Connection = conn;  

                cmd.Parameters.AddWithValue("@Datum", datum);
                cmd.Parameters.AddWithValue("@Temperatuur", Temperatuur);
                cmd.Parameters.AddWithValue("@Wind", Wind);
                cmd.Parameters.AddWithValue("@Neerslag", Neerslag);
                cmd.Parameters.AddWithValue("@Luchtdruk", Luchtdruk);
                cmd.Parameters.AddWithValue("@RelLuchtvochtigheid", RelativeLuchtvochtigheid);
                cmd.Parameters.AddWithValue("@WeerstationID", WeerstationID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                this.Close();
                Weerstations frmWeerstations = new Weerstations();
                frmWeerstations.Show();


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
            Weerstations frmWeerstation = new Weerstations();
            frmWeerstation.Show();

        }
    }
}
