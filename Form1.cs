using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astronautai
{
    public partial class Form1 : Form
    {
        //define the connection string of azure database.
        string cnString = "Server=tcp:astronauts.database.windows.net,1433;Initial Catalog=Astro;Persist Security Info=False;User ID=astronautas;Password=Batonas5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        bool access = true;
        int playerId = 1;

        void OnChange(object sender, SqlNotificationEventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Console.WriteLine("hello!");
            //AstroDataSet1 db = new AstroDataSet1();

            //db.Players.AddPlayersRow("0", "1", "Hello!");
            //PutData();

            GetData();
        }

        private async void PutData(string X, string Y, string Input)
        {
            //define the insert sql command, here I insert data into the student table in azure db.
            string cmdText = @"update players set ""input""=@input where id=1";

            using (SqlConnection con = new SqlConnection(cnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@X", X);
                cmd.Parameters.AddWithValue("@Y", Y);
                cmd.Parameters.AddWithValue("@input", Input);

                cmd.ExecuteNonQuery();

                con.Close();
            }

            Console.WriteLine("completed***");
            //GetData();
        }

        private void GetData()
        {
            //define the insert sql command, here I insert data into the student table in azure db.
            string cmdText = @"select * from Players where id=" + playerId;

            using (SqlConnection con = new SqlConnection(cnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, con))
            {
                con.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += OnChange;
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            if (dataReader.IsDBNull(i))
                            {
                                access = false;
                            } 
                        }

                        access = true;
                        string X = dataReader.GetString(1);
                        string Y = dataReader.GetString(2);
                        string Input = dataReader.GetString(3);

                        label1.Text = Input;

                        Console.WriteLine(X + " " + Y + " " + Input);
                    }
                }
                con.Close();
            }

            Console.WriteLine("completed***");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if(e.KeyCode == Keys.W)
            {
                PutData("1", "1", "W");
            }
            if (e.KeyCode == Keys.S)
            {
                PutData("1", "1", "S");
            }
            if (e.KeyCode == Keys.A)
            {
                PutData("1", "1", "A");
            }
            if (e.KeyCode == Keys.D)
            {
                PutData("1", "1", "D");
            }
        }
    }
}
