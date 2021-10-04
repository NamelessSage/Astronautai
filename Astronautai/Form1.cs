using Class_diagram;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections;
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
        private readonly IHubProxy server;
        //define the connection string of azure database.
        string cnString = "Server=tcp:astronauts.database.windows.net,1433;Initial Catalog=Astro;Persist Security Info=False;User ID=astronautas;Password=Batonas5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        bool access = true;
        Player player;


        void OnChange(object sender, SqlNotificationEventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            HubConnection hubConnection = new HubConnection("http://localhost:8080");
            server = hubConnection.CreateHubProxy("serveris");
            server.On<int>("createPlayer", (pid) =>
            {
                button1.Visible = false;
                player = new Player(pid, 3, 100);
                player.X = 100;
                player.Y = 100;
                if (pid == 0)
                {
                    player1.Location = new Point(player.X, player.Y);
                }
                else if (pid == 1)
                {

                    player2.Location = new Point(player.X, player.Y);
                }
            });

            server.On<int, int, int>("movePlayer", (pid, x, y) =>
            {
                if (pid == 0)
                {
                    player1.Location = new Point(x, y);
                }
                else if(pid == 1)
                {
                    player2.Location = new Point(x, y);
                }
            });

            hubConnection.Start().Wait();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.Invoke("CreatePlayer").Wait();

            //player = new Player(1, 100, 100);
            //server.Invoke("PlayerMovement", player.Id, player.X, player.Y).Wait();
        }


        //private Timer timer1;
        //public void InitTimer()
        //{
        //    timer1 = new Timer();
        //    timer1.Tick += new EventHandler(timer1_Tick);
        //    timer1.Interval = 100; // in miliseconds
        //    timer1.Start();
        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    PutData(P1Xadded, P1Yadded, P1Inputadded);
        //    GetData();

        //}

        //private async void PutData(string X, string Y, string Input)
        //{
        //    //define the insert sql command, here I insert data into the student table in azure db.
        //    string cmdText = @"update players set ""input""=@input, x=@X, y=@Y where id=@id";

        //    using (SqlConnection con = new SqlConnection(cnString))
        //    using (SqlCommand cmd = new SqlCommand(cmdText, con))
        //    {
        //        con.Open();
        //        cmd.Parameters.AddWithValue("@id", playerId);
        //        cmd.Parameters.AddWithValue("@X", (int.Parse(X) + int.Parse(P1X)).ToString());
        //        cmd.Parameters.AddWithValue("@Y", (int.Parse(Y) + int.Parse(P1Y)).ToString());
        //        cmd.Parameters.AddWithValue("@input", Input);

        //        cmd.ExecuteNonQuery();

        //        con.Close();
        //    }
        //    Console.WriteLine("Im Getting blasted!!!");
        //    //GetData();
        //}

        //private void GetData()
        //{
        //    //define the insert sql command, here I insert data into the student table in azure db.
        //    string cmdText = @"select * from Players";

        //    using (SqlConnection con = new SqlConnection(cnString))
        //    using (SqlCommand cmd = new SqlCommand(cmdText, con))
        //    {
        //        con.Open();
        //        using (SqlDataReader dataReader = cmd.ExecuteReader())
        //        {
        //            SqlDependency dependency = new SqlDependency(cmd);
        //            dependency.OnChange += OnChange;
        //            while (dataReader.Read())
        //            {
        //                for (int i = 0; i < dataReader.FieldCount; i++)
        //                {
        //                    if (dataReader.IsDBNull(i))
        //                    {
        //                        access = false;
        //                    } 
        //                }

        //                access = true;


        //                label1.Text = P1Input;

        //                if (dataReader.GetInt32(0) == 1)
        //                {
        //                    player1.Location = new Point(int.Parse(dataReader.GetString(1)), int.Parse(dataReader.GetString(2)));
        //                    if(playerId == 1)
        //                    {
        //                        P1X = dataReader.GetString(1);
        //                        P1Y = dataReader.GetString(2);
        //                        P1Input = dataReader.GetString(3);
        //                    }
        //                }
        //                else if (dataReader.GetInt32(0) == 2)
        //                {
        //                    player2.Location = new Point(int.Parse(dataReader.GetString(1)), int.Parse(dataReader.GetString(2)));
        //                    if (playerId == 2)
        //                    {
        //                        P1X = dataReader.GetString(1);
        //                        P1Y = dataReader.GetString(2);
        //                        P1Input = dataReader.GetString(3);
        //                    }
        //                }
        //            }
        //        }
        //        con.Close();
        //    }

        //    Console.WriteLine("completed***");
        //}

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == Keys.W)
            {
                player.Y -= 1;
                server.Invoke("PlayerMovement", player.Id, player.X, player.Y).Wait();
            }
            if (e.KeyCode == Keys.S)
            {
                player.Y += 1;
                server.Invoke("PlayerMovement", player.Id, player.X, player.Y).Wait();
            }
            if (e.KeyCode == Keys.A)
            {
                player.X -= 1;
                server.Invoke("PlayerMovement", player.Id, player.X, player.Y).Wait();
            }
            if (e.KeyCode == Keys.D)
            {
                player.X += 1;
                server.Invoke("PlayerMovement", player.Id, player.X, player.Y).Wait();
            }

        }

    }
}
