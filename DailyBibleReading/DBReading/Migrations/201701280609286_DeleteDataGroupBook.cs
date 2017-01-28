namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDataGroupBook : DbMigration
    {
        public override void Up()
        {
            Sql("delete from ReadingGroupBooks");
        }
        
        public override void Down()
        {
        }
    }
}
