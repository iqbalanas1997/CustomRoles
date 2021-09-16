
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


#nullable disable

namespace CustomRoles.Data
{
    public partial class CustomRolesDbContext : IdentityDbContext<ApplicationUser>
    {
        public CustomRolesDbContext()
        {
        }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {

//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=ASP-NET\\SQLEXPRESS;Initial Catalog=eprocurementWithLibrary;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }
        public CustomRolesDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessRoles> BusinessRoles { get; set; }
        public virtual DbSet<BusinessRolePermissions> BusinessRolePermissions { get; set; }
        public virtual DbSet<BusinessUserRoles> BusinessUserRole { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public DbSet<Business> Business { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // I had removed this
            /// Rest of on model creating here.
        }
    }
}
