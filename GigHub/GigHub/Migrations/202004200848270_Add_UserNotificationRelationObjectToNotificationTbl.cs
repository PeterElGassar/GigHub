namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserNotificationRelationObjectToNotificationTbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Notification", "NotificationId", "dbo.Notifications");
            AddForeignKey("dbo.User_Notification", "NotificationId", "dbo.Notifications", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Notification", "NotificationId", "dbo.Notifications");
            AddForeignKey("dbo.User_Notification", "NotificationId", "dbo.Notifications", "Id", cascadeDelete: true);
        }
    }
}
