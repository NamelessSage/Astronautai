using Class_diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GameServer.Classes
{
    public class ObjectDestructor
    {
        Map map = Map.Instance;
        bool IsInRemoveArea(Coordinates coordinates)
        {
            if (coordinates.X < -100 || coordinates.X > 900)
                return true;

            if (coordinates.Y < -100 || coordinates.Y > 700)
                return true;

            return false;
        }

        public bool RemoveEnemy(Enemy enemy)
        {
            if (IsInRemoveArea(enemy.GetCoordinates()))
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
                context.Clients.All.destroyEnemy(enemy);
                map.enemies.Remove(enemy);
                return true;
            }
            else
                return false;
        }
    }
}
