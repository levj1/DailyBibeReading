namespace DBReading.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAddBibleBook : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Genesis', 1, 50, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Exodus', 1, 40, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Leviticus', 1, 27, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Numbers', 1, 36, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Deuteronomy', 1, 34, 'Old Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Joshua', 2, 24, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Judges', 2, 21, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Ruth', 2, 4, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 Samuel', 2, 31, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 Samuel', 2, 24, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 Kings', 2, 22, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 Kings', 2, 25, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 Chronicles', 2, 29, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 Chronicles', 2, 36, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Ezra', 2, 10, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Nehemiah', 2, 13, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Esther', 2, 10, 'Old Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Job', 3, 42, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Psalms', 3, 150, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Proverbs', 3, 31, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Ecclesiastes', 3, 12, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Song of Songs', 3, 8, 'Old Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Isaiah', 4, 66, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Jeremiah', 4, 52, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Lamentations', 4, 5, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Ezekiel', 4, 48, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Daniel', 4, 12, 'Old Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Hosea', 5, 14, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Joel', 5, 3, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Amos', 5, 9, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Obadiah', 5, 1, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Jonah', 5, 4, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Micah', 5, 7, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Nahum', 5, 3, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Habakkuk', 5, 3, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Zephaniah', 5, 3, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Haggai', 5, 2, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Zechariah', 5, 14, 'Old Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Malachi', 5, 4, 'Old Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Matthew', 6, 28, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Mark', 6, 16, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Luke', 6, 24, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('John', 6, 21, 'New Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Acts', 7, 28, 'New Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Romans', 8, 16, 'New Testament')");Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Acts', 7, 28, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 Corinthians', 8, 16, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 Corinthians', 8, 13, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Galatians', 8, 6, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Ephesians', 8, 6, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Philippians', 8, 4, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Colossians', 8, 4, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 Thessalonians', 8, 5, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 Thessalonians', 8, 3, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 Timothy', 8, 6, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 Timothy', 8, 4, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Titus', 8, 3, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Philemon', 8, 1, 'New Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Hebrews', 9, 13, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('James', 9, 5, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 Peter', 9, 5, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 Peter', 9, 3, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('1 John', 9, 5, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('2 John', 9, 1, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('3 John', 9, 1, 'New Testament')");
            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Jude', 9, 1, 'New Testament')");

            Sql("INSERT INTO BibleBooks(name, ReadingGroupBookID, MaxChapter, Testament) values('Revelation', 10, 22, 'New Testament')");
        }
        
        public override void Down()
        {
        }
    }
}
