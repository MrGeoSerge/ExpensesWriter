namespace ExpensesWriter.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Money = c.Double(nullable: false),
                        Name = c.String(nullable: false),
                        Category = c.String(),
                        CreationDateTime = c.DateTime(nullable: false),
                        ModificationDateTime = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Expenses");
        }
    }
}
