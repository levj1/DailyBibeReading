namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupDateCreatedprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "GroupDateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "GroupDateCreated");
        }
    }
}
