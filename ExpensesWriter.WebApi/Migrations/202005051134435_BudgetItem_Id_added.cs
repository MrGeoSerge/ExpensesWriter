namespace ExpensesWriter.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetItem_Id_added : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "BudgetItem_Id", "dbo.BudgetItems");
            DropIndex("dbo.Expenses", new[] { "BudgetItem_Id" });
            AddColumn("dbo.Expenses", "BudgetItem_Id1", c => c.Int());
            CreateIndex("dbo.Expenses", "BudgetItem_Id1");
            AddForeignKey("dbo.Expenses", "BudgetItem_Id1", "dbo.BudgetItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "BudgetItem_Id1", "dbo.BudgetItems");
            DropIndex("dbo.Expenses", new[] { "BudgetItem_Id1" });
            DropColumn("dbo.Expenses", "BudgetItem_Id1");
            CreateIndex("dbo.Expenses", "BudgetItem_Id");
            AddForeignKey("dbo.Expenses", "BudgetItem_Id", "dbo.BudgetItems", "Id");
        }
    }
}
