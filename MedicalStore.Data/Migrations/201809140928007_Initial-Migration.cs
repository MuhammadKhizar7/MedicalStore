namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Company = c.String(),
                        Formula = c.String(),
                        Strength = c.String(),
                        PackSize = c.Int(nullable: false),
                        QuentityInPack = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        StockId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Stock", t => t.StockId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Quentity = c.Int(nullable: false),
                        OrderLevel = c.Int(nullable: false),
                        BatchNo = c.Int(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        CostPerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmployeeType = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Disease = c.String(),
                        PhoneNo = c.Int(nullable: false),
                        Address = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Quentiy = c.Int(nullable: false),
                        Tex = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MedicineId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sale", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.Medicine", "StockId", "dbo.Stock");
            DropForeignKey("dbo.Medicine", "CategoryId", "dbo.Category");
            DropIndex("dbo.Sale", new[] { "MedicineId" });
            DropIndex("dbo.Medicine", new[] { "StockId" });
            DropIndex("dbo.Medicine", new[] { "CategoryId" });
            DropTable("dbo.Sale");
            DropTable("dbo.Patient");
            DropTable("dbo.Employee");
            DropTable("dbo.Stock");
            DropTable("dbo.Medicine");
            DropTable("dbo.Category");
        }
    }
}
