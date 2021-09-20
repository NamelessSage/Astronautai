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
        string P1X;
        string P1Y;
        string P1Input;
        string P1Xadded = "0";
        string P1Yadded = "0";
        string P1Inputadded = "No Input!";


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
            InitTimer();
            GetData();
        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 100; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PutData(P1Xadded, P1Yadded, P1Inputadded);
            GetData();
            player1.Location = new Point(int.Parse(P1X), int.Parse(P1Y));
        }

        private async void PutData(string X, string Y, string Input)
        {
            //define the insert sql command, here I insert data into the student table in azure db.
            string cmdText = @"update players set ""input""=@input, x=@X, y=@Y where id=1";

            using (SqlConnection con = new SqlConnection(cnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@X", (int.Parse(X) + int.Parse(P1X)).ToString());
                cmd.Parameters.AddWithValue("@Y", (int.Parse(Y) + int.Parse(P1Y)).ToString());
                cmd.Parameters.AddWithValue("@input", Input);

                cmd.ExecuteNonQuery();

                con.Close();
            }
            Console.WriteLine("Im Getting blasted!!!");
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
                        P1X = dataReader.GetString(1);
                        P1Y = dataReader.GetString(2);
                        P1Input = dataReader.GetString(3);

                        label1.Text = P1Input;

                        
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
                P1Xadded = "0";
                P1Yadded = "-1";
                P1Inputadded = "W";

            }
            if (e.KeyCode == Keys.S)
            {
                P1Xadded = "0";
                P1Yadded = "1";
                P1Inputadded = "S";
            }
            if (e.KeyCode == Keys.A)
            {
                P1Xadded = "-1";
                P1Yadded = "0";
                P1Inputadded = "A";
            }
            if (e.KeyCode == Keys.D)
            {
                P1Xadded = "1";
                P1Yadded = "0";
                P1Inputadded = "D";
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            P1Xadded = "0";
            P1Yadded = "0";
            P1Inputadded = "No Input!";
        }
    }
}
