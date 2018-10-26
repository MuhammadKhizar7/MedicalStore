namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMedicine_CreatedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stock", "ExpireDate", c => c.DateTime());
            AlterColumn("dbo.Stock", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stock", "CreationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stock", "ExpireDate", c => c.DateTime(nullable: false));
        }
    }
}
