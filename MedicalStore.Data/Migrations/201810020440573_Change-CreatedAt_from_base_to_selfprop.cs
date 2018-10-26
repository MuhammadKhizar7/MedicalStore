namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCreatedAt_from_base_to_selfprop : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoice", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Invoice", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoice", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Invoice", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
