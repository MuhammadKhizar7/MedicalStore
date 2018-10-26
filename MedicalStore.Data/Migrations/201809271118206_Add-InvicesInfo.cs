namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvicesInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sale", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.Stock", "MedicineId", "dbo.Medicine");
            DropIndex("dbo.Sale", new[] { "MedicineId" });
            DropIndex("dbo.Stock", new[] { "MedicineId" });
            DropPrimaryKey("dbo.Medicine");
            DropPrimaryKey("dbo.Patient");
            DropPrimaryKey("dbo.Sale");
            DropPrimaryKey("dbo.Stock");
            CreateTable(
                "dbo.InvoiceItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Qnt = c.Int(nullable: false),
                        TTC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        THT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MedicineId = c.Long(nullable: false),
                        InvoiceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        TTC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        THT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsValid = c.Boolean(nullable: false),
                        InvoiceType = c.Int(nullable: false),
                        PaidAmmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Left = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PatientId = c.Long(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patient", t => t.PatientId)
                .Index(t => t.PatientId);
            
            AddColumn("dbo.Sale", "Medicine_Id", c => c.Long());
            AddColumn("dbo.Stock", "Medicine_Id", c => c.Long());
            AlterColumn("dbo.Medicine", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Patient", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Sale", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Stock", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Medicine", "Id");
            AddPrimaryKey("dbo.Patient", "Id");
            AddPrimaryKey("dbo.Sale", "Id");
            AddPrimaryKey("dbo.Stock", "Id");
            CreateIndex("dbo.Sale", "Medicine_Id");
            CreateIndex("dbo.Stock", "Medicine_Id");
            AddForeignKey("dbo.Sale", "Medicine_Id", "dbo.Medicine", "Id");
            AddForeignKey("dbo.Stock", "Medicine_Id", "dbo.Medicine", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stock", "Medicine_Id", "dbo.Medicine");
            DropForeignKey("dbo.Sale", "Medicine_Id", "dbo.Medicine");
            DropForeignKey("dbo.InvoiceItem", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.Invoice", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropIndex("dbo.Stock", new[] { "Medicine_Id" });
            DropIndex("dbo.Sale", new[] { "Medicine_Id" });
            DropIndex("dbo.Invoice", new[] { "PatientId" });
            DropIndex("dbo.InvoiceItem", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceItem", new[] { "MedicineId" });
            DropPrimaryKey("dbo.Stock");
            DropPrimaryKey("dbo.Sale");
            DropPrimaryKey("dbo.Patient");
            DropPrimaryKey("dbo.Medicine");
            AlterColumn("dbo.Stock", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Sale", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Patient", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Medicine", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Stock", "Medicine_Id");
            DropColumn("dbo.Sale", "Medicine_Id");
            DropTable("dbo.Invoice");
            DropTable("dbo.InvoiceItem");
            AddPrimaryKey("dbo.Stock", "Id");
            AddPrimaryKey("dbo.Sale", "Id");
            AddPrimaryKey("dbo.Patient", "Id");
            AddPrimaryKey("dbo.Medicine", "Id");
            CreateIndex("dbo.Stock", "MedicineId");
            CreateIndex("dbo.Sale", "MedicineId");
            AddForeignKey("dbo.Stock", "MedicineId", "dbo.Medicine", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sale", "MedicineId", "dbo.Medicine", "Id", cascadeDelete: true);
        }
    }
}
