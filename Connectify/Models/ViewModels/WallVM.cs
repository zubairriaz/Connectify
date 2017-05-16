using Connectify.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify.Models.ViewModels
{
    public class WallVM
    {
        public WallVM()
        {

        }
        public WallVM(Wall wall)
        {
            Id=wall.Id;
            Message = wall.Message;
            DateEdited = wall.DateEdited;

          

       }
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateEdited { get; set; }


    }
}