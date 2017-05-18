using Connectify.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify.Models.ViewModels
{
    public class OnlineVM
    {
        public OnlineVM()
        {

        }
        public OnlineVM(OnlineDto online)
        {
            Id = online.Id;
            ConnId = online.ConnId;
        }
        public int Id { get; set; }
        public string ConnId { get; set; }
    }
}