using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Connectify.Models.Data
{
    [Table("Messages")]
    public class MessageDto
    {
        [Key]
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public bool Read { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }

        [ForeignKey("From")]
        public virtual UsersDto FromUsers { get; set; }
        [ForeignKey("To")]
        public virtual UsersDto ToUsers { get; set; }
    }
}