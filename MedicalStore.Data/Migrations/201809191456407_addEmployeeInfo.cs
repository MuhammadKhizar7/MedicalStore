namespace MedicalStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmployeeInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "FullName", c => c.String());
            AddColumn("dbo.Employee", "Tel", c => c.String());
            AddColumn("dbo.Employee", "City", c => c.String());
            AddColumn("dbo.Employee", "Street", c => c.String());
            DropColumn("dbo.Employee", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "Name", c => c.String());
            DropColumn("dbo.Employee", "Street");
            DropColumn("dbo.Employee", "City");
            DropColumn("dbo.Employee", "Tel");
            DropColumn("dbo.Employee", "FullName");
        }
    }
}
