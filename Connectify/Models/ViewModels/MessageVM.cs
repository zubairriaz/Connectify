using Connectify.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectify.Models.ViewModels
{
    public class MessageVM
    {
        public MessageVM()
        {

        }
        public MessageVM(MessageDto row)
        {
            Id = row.Id;
            From = row.From;
            To = row.To;
            Read = row.Read;
            Message = row.Message;
            FromId = row.FromUsers.Id;
            FromUserName = row.FromUsers.UserName;
            FromFirstName = row.FromUsers.FirstName;
            FromLastName = row.FromUsers.LastName;

        }
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public bool Read { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public int FromId { get; set; }
        public string FromUserName { get; set; }
        public string FromFirstName { get; set; }
        public string FromLastName { get; set; }
    }
}