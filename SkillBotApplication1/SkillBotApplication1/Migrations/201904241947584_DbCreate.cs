namespace SkillBotApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumPeople = c.Int(nullable: false),
                        PhNum = c.String(),
                        Requests = c.String(),
                        BookingDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BotHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Source = c.String(),
                        UserId = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BotHistories");
            DropTable("dbo.Bookings");
        }
    }
}
