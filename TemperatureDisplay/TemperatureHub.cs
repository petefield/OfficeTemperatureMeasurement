using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Temperature_Display
{
    public class TemperatureHub : Hub
    {
        public void Send(string value) {
            Clients.All.addNewTemperature(value);
        }
    }
}