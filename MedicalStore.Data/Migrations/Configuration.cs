using MedicalStore.Model.Entities;

namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MedicalStore.Data.ObjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MedicalStore.Data.ObjectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (!context.Categories.Any())
            {
              context.Categories.AddOrUpdate(c=>c.Name, new Category{Name = "ABC",Description = "abc Category", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow});

            }
        }
    }
}
