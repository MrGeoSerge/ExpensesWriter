namespace ExpensesWriter.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetItem : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Expenses", name: "BudgetItem_Id1", newName: "BudgetItemId");
            RenameIndex(table: "dbo.Expenses", name: "IX_BudgetItem_Id1", newName: "IX_BudgetItemId");
            DropColumn("dbo.Expenses", "BudgetItem_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "BudgetItem_Id", c => c.Int());
            RenameIndex(table: "dbo.Expenses", name: "IX_BudgetItemId", newName: "IX_BudgetItem_Id1");
            RenameColumn(table: "dbo.Expenses", name: "BudgetItemId", newName: "BudgetItem_Id1");
        }
    }
}
