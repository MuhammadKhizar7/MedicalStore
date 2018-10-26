namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_int_to_Int64 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceItem", "MedicineId", "dbo.Medicine");
            DropIndex("dbo.InvoiceItem", new[] { "MedicineId" });
            AlterColumn("dbo.InvoiceItem", "MedicineId", c => c.Long());
            CreateIndex("dbo.InvoiceItem", "MedicineId");
            AddForeignKey("dbo.InvoiceItem", "MedicineId", "dbo.Medicine", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItem", "MedicineId", "dbo.Medicine");
            DropIndex("dbo.InvoiceItem", new[] { "MedicineId" });
            AlterColumn("dbo.InvoiceItem", "MedicineId", c => c.Long(nullable: false));
            CreateIndex("dbo.InvoiceItem", "MedicineId");
            AddForeignKey("dbo.InvoiceItem", "MedicineId", "dbo.Medicine", "Id", cascadeDelete: true);
        }
    }
}
