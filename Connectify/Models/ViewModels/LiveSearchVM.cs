using Connectify.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify.Models.ViewModels
{
    public class LiveSearchVM
    {
        public LiveSearchVM()
        {

        }
        public LiveSearchVM(UsersDto users)
        {
            Id = users.Id;
            FirstName = users.FirstName;
            LastName = users.LastName;
            UserName = users.UserName;
        }
        public int Id { get; set; }
   
        public string FirstName { get; set; }
    
        public string LastName { get; set; }
  
        public string UserName { get; set; }
    }
}