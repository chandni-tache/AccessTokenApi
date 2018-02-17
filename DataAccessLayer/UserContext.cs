using System.Data.Entity;

namespace TachTechnologies.DataAccessLayer
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("UserContext")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // This line will tell entity framework to use stored procedures
            // when inserting, updating and deleting Employees
            modelBuilder.Entity<User>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);
        }
    }
}