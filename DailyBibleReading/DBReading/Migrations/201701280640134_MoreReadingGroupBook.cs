namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreReadingGroupBook : DbMigration
    {
        public override void Up()
        {            
            Sql("INSERT INTO ReadingGroupBooks(name) values('Whole Bible')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('Other')");
        }
        
        public override void Down()
        {
        }
    }
}
