namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBibleBookData1 : DbMigration
    {
        public override void Up()
        {
            Sql("Delete From ReadingGroupBooks");
            Sql("INSERT INTO ReadingGroupBooks(name) values('Pentateuch')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('History')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('Poetic')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('Major Prophets')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('Minor Prophets')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('The Gospels')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('Acts')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('Letters From Paul')");
            Sql("INSERT INTO ReadingGroupBooks(name) values('General Letters')");

        }
        
        public override void Down()
        {
        }
    }
}
