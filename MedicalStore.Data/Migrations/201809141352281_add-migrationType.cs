namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medicine", "StockId", "dbo.Stock");
            DropIndex("dbo.Medicine", new[] { "StockId" });
            AlterColumn("dbo.Medicine", "StockId", c => c.Int());
            CreateIndex("dbo.Medicine", "StockId");
            AddForeignKey("dbo.Medicine", "StockId", "dbo.Stock", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicine", "StockId", "dbo.Stock");
            DropIndex("dbo.Medicine", new[] { "StockId" });
            AlterColumn("dbo.Medicine", "StockId", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicine", "StockId");
            AddForeignKey("dbo.Medicine", "StockId", "dbo.Stock", "Id", cascadeDelete: true);
        }
    }
}
