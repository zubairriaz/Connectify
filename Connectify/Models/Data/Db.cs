using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Connectify.Models.Data
{
    public class Db:DbContext
    {
        public DbSet<UsersDto> Users { get; set; }
        public DbSet<FriendsDto> Friends { get; set; }
        public DbSet<MessageDto> Messages { get; set; }
        public DbSet<Wall> Wall { get; set; }
    }
}