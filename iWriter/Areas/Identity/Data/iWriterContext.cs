using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWriter.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iWriter.Models
{
    public class iWriterContext : IdentityDbContext<iWriterUser>
    {
        public iWriterContext(DbContextOptions<iWriterContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ProjectTypeFeature> ProjectTypeFeatures { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //  the entity ProjectTypeFeature has composite key made from ProjectTypeId and FeatureId
            builder.Entity<ProjectTypeFeature>().HasKey(sc => new { sc.ProjectTypeId, sc.FeatureId });

            //  Many Projects  <---|-  One Project Type
            builder.Entity<Project>()
                .HasOne(s => s.ProjectType)
                .WithMany(s => s.Projects)
                .HasForeignKey(s => s.ProjectTypeId);

            //  set Cascade Referential Integrity to Cascade
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}