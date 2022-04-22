using ComplaintService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace ComplaintService.DataContext
{
    public class ComplaintDbContext : DbContext
    {
        public ComplaintDbContext(DbContextOptions<ComplaintDbContext> options): base(options)
        {
        }
        public DbSet<Complaint> Complaint { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            var keysProperties = builder.Model.GetEntityTypes().Select(x => x.FindPrimaryKey()).SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }
            builder.Entity<Complaint>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
