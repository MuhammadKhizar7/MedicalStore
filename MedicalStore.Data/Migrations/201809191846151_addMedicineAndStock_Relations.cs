namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMedicineAndStock_Relations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medicine", "StockId", "dbo.Stock");
            DropIndex("dbo.Medicine", new[] { "StockId" });
            AddColumn("dbo.Stock", "PricePerUnit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stock", "MedicineId", c => c.Int(nullable: false));
            CreateIndex("dbo.Stock", "MedicineId");
            AddForeignKey("dbo.Stock", "MedicineId", "dbo.Medicine", "Id", cascadeDelete: true);
            DropColumn("dbo.Medicine", "StockId");
            DropColumn("dbo.Stock", "OrderLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stock", "OrderLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Medicine", "StockId", c => c.Int());
            DropForeignKey("dbo.Stock", "MedicineId", "dbo.Medicine");
            DropIndex("dbo.Stock", new[] { "MedicineId" });
            DropColumn("dbo.Stock", "MedicineId");
            DropColumn("dbo.Stock", "PricePerUnit");
            CreateIndex("dbo.Medicine", "StockId");
            AddForeignKey("dbo.Medicine", "StockId", "dbo.Stock", "Id");
        }
    }
}
