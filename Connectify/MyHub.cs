using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace Connectify
{
    [HubName("echo")]
    public class MyHub : Hub
    {
        public void Hello(string message)
        {
            Trace.WriteLine(message);
            Clients.All.test("this is test");
        }
    }
}