using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace AddressBookWebsite.Models
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext()
        : base("ContactsDbContext")
        {
        }

        public virtual DbSet<ContactViewModel> ContactDetails { get; set; }
        public virtual DbSet<LoginViewModel> UserLogin { get; set; }
        public virtual DbSet<CreateUserRoleViewModel> RoleModel { get; set; }
    }
}