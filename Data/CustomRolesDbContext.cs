
using CustomRoles.Data.SPEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


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

        protected CustomRolesDbContext(DbContextOptions<CustomRolesDbContext> options)
           : base(options)
        {
        }


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

        //Fake Entities for Strored Proceudures
        public DbSet<DefaultRole> DefaultRole { get; set; }
        public DbSet<GetBusinessId> GetBusinessId { get; set; }

        public override int SaveChanges()
        {
            Handle();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            Handle();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private void Handle()
        {
            var entities = ChangeTracker.Entries()
                                .Where(e => e.State == EntityState.Deleted);
            foreach (var entity in entities)
            {
                if (entity.Entity is Actions)
                {
                    entity.State = EntityState.Modified;
                    var m = entity.Entity as Actions;
                    m.IsDeleted = true;
                }

                if (entity.Entity is ApplicationUser)
                {
                    entity.State = EntityState.Modified;
                    var m = entity.Entity as ApplicationUser;
                    m.IsDeleted = true;
                }

                if (entity.Entity is Business)
                {
                    entity.State = EntityState.Modified;
                    var m = entity.Entity as Business;
                    m.IsDeleted = true;
                }

                if (entity.Entity is BusinessRolePermissions)
                {
                    entity.State = EntityState.Modified;
                    var m = entity.Entity as BusinessRolePermissions;
                    m.IsDeleted = true;
                }

                if (entity.Entity is BusinessRoles)
                {
                    entity.State = EntityState.Modified;
                    var m = entity.Entity as BusinessRoles;
                    m.IsDeleted = true;
                }

                if (entity.Entity is BusinessUserRoles)
                {
                    entity.State = EntityState.Modified;
                    var m = entity.Entity as BusinessUserRoles;
                    m.IsDeleted = true;
                }

                if (entity.Entity is Page)
                {
                    entity.State = EntityState.Modified;
                    var m = entity.Entity as Page;
                    m.IsDeleted = true;
                }




            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // I had removed this
            /// Rest of on model creating here.
            /// 

            modelBuilder.Entity<DefaultRole>(entity => {
                // Migration will not be generated for this table
                entity.ToView("DefaultRole", "dbo");
            });
            modelBuilder.Entity<GetBusinessId>(entity => {
                // Migration will not be generated for this table
                entity.ToView("GetBusinessId", "dbo");
            });

            modelBuilder.Entity<Actions>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Business>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<BusinessRolePermissions>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<BusinessRoles>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<BusinessUserRoles>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Page>().HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
