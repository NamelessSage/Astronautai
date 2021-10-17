using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Astronautai;
using Class_diagram;

namespace Astronautai.Classes
{
    class ServerInput
    {
        private readonly IHubProxy server;
        public ServerInput(Form1 form)
        {
            HubConnection hubConnection = new HubConnection("http://localhost:8080");
            server = hubConnection.CreateHubProxy("serveris");

            server.On<Enemy>("destroyEnemy", (enemy) =>
            {
                form.BeginInvoke(new Action(() =>
                {
                    string name = "Enemy" + enemy.Id;
                    var item = form.Controls.Find(name, true)[0];
                    form.Controls.Remove(item);
                }));
            });
            server.On<Projectile>("destroyProjectile", (projectile) =>
            {
                form.BeginInvoke(new Action(() =>
                {
                    string name = "Projectile" + projectile.Id;
                    var item = form.Controls.Find(name, true)[0];
                    form.Controls.Remove(item);
                }));
            });


            hubConnection.Start().Wait();
        }

    }
}
