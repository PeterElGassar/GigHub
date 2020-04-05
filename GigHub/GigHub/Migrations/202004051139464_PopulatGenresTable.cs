namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulatGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(Id,Name) VALUES(1,'Jazz')");
            Sql("INSERT INTO Genres(Id,Name) VALUES(2,'Blues')");
            Sql("INSERT INTO Genres(Id,Name) VALUES(3,'Classic')");
            Sql("INSERT INTO Genres(Id,Name) VALUES(4,'Rock')");
            Sql("INSERT INTO Genres(Id,Name) VALUES(5,'Flamenco')");

        }

        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id In(1,2,3,4,5)");
        }
    }
}
