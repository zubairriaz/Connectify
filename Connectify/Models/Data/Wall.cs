using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Connectify.Models.Data
{
       [Table("Wall")]
    public class Wall
    {
     
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateEdited { get; set; }

        [ForeignKey("Id")]
        public virtual UsersDto Users  { get; set; }
    }
}