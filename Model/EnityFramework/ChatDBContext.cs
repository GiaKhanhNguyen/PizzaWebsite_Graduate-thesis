using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EnityFramework
{
    public class ChatDBContext : DbContext
    {
        public ChatDBContext()
            : base("name=ChatDBContext")
        {
        }
        public static ChatDBContext Create()
        {
            return new ChatDBContext();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}
