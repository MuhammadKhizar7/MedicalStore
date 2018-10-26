namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPatientinfo_city_phone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "Tel", c => c.String());
            AddColumn("dbo.Patient", "City", c => c.String());
            DropColumn("dbo.Patient", "PhoneNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patient", "PhoneNo", c => c.Int(nullable: false));
            DropColumn("dbo.Patient", "City");
            DropColumn("dbo.Patient", "Tel");
        }
    }
}
