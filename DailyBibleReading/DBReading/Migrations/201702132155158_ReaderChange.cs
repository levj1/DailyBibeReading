namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReaderChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Readers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Readers", "LastName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Readers", "Email", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Readers", "Email", c => c.String(maxLength: 30));
            AlterColumn("dbo.Readers", "LastName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Readers", "FirstName", c => c.String(maxLength: 30));
        }
    }
}
