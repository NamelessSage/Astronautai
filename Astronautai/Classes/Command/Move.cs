using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Astronautai.Classes
{

    class Move
    {
        public Player Player { get; set; }
        public char Direction { get; set; }

        private readonly IHubProxy server;

        public Move(Player player, char direction)
        {
            Player = player;
            Direction = direction;
            Player.Rotation = direction;
            HubConnection hubConnection = new HubConnection("http://localhost:8080");
            server = hubConnection.CreateHubProxy("serveris");
            hubConnection.Start().Wait();
        }

        public void UpdatePlayer(Player player)
        {
            Player = player;
            Direction = player.Rotation;
        }

        public void MoveW()
        {
            Player.Rotation = 'W';
            Direction = 'W';
            server.Invoke("MovePlayer", Player);
        }

        
        public void MoveA()
        {
            Player.Rotation = 'A';
            Direction = 'A';
            server.Invoke("MovePlayer", Player);
        }


        public void MoveS()
        {
            Player.Rotation = 'S';
            Direction = 'S';
            server.Invoke("MovePlayer", Player);
        }


        public void MoveD()
        {
            Player.Rotation = 'D';
            Direction = 'D';
            server.Invoke("MovePlayer", Player);
        }

        public void UndoW()
        {
            Player.Rotation = 'W';
            Direction = 'W';
            Player.Y += Player.Speed;
            server.Invoke("UndoMovePlayer", Player);
        }


        public void UndoA()
        {
            Player.Rotation = 'A';
            Direction = 'A';
            Player.X += Player.Speed;
            server.Invoke("UndoMovePlayer", Player);
        }


        public void UndoS()
        {
            Player.Rotation = 'S';
            Direction = 'S';
            Player.Y -= Player.Speed;
            server.Invoke("UndoMovePlayer", Player);
        }


        public void UndoD()
        {
            Player.Rotation = 'D';
            Direction = 'D';
            Player.X -= Player.Speed;
            server.Invoke("UndoMovePlayer", Player);
        }
        

    }
}
