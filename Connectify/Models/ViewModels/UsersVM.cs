using Connectify.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Connectify.Models.ViewModels
{
    public class UsersVM
    {
        public UsersVM()
        {

        }
        public UsersVM(UsersDto row) {
            Id = row.Id;
            FirstName = row.FirstName;
            LastName = row.LastName;
            UserName = row.UserName;
            Password = row.Password;
              }
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required][DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}