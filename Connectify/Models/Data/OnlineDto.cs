using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Connectify.Models.Data
{
    [Table("Online")]
    public class OnlineDto
    {
        [Key]
        public int Id { get; set; }
        public string ConnId { get; set; }

        [ForeignKey("Id")]
        public virtual UsersDto Users { get; set; }
    }
}