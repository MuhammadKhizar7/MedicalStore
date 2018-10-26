namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSupplierInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Tel = c.String(),
                        Email = c.String(),
                        City = c.String(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Stock", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Stock", "SupplierId");
            AddForeignKey("dbo.Stock", "SupplierId", "dbo.Supplier", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stock", "SupplierId", "dbo.Supplier");
            DropIndex("dbo.Stock", new[] { "SupplierId" });
            DropColumn("dbo.Stock", "SupplierId");
            DropTable("dbo.Supplier");
        }
    }
}
