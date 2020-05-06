namespace ExpensesWriter.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetItems_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Color = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Expenses", "CategoryName", c => c.String());
            AddColumn("dbo.Expenses", "BudgetItem_Id", c => c.Int());
            CreateIndex("dbo.Expenses", "BudgetItem_Id");
            AddForeignKey("dbo.Expenses", "BudgetItem_Id", "dbo.BudgetItems", "Id");
            DropColumn("dbo.Expenses", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "Category", c => c.String());
            DropForeignKey("dbo.Expenses", "BudgetItem_Id", "dbo.BudgetItems");
            DropIndex("dbo.Expenses", new[] { "BudgetItem_Id" });
            DropColumn("dbo.Expenses", "BudgetItem_Id");
            DropColumn("dbo.Expenses", "CategoryName");
            DropTable("dbo.BudgetItems");
        }
    }
}
