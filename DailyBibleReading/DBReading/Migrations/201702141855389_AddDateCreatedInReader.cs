namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateCreatedInReader : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Readers", "ReaderDteCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Readers", "ReaderDteCreated");
        }
    }
}
