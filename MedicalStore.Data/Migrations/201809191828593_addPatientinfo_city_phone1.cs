namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPatientinfo_city_phone1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patient", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patient", "Address", c => c.Int(nullable: false));
        }
    }
}
