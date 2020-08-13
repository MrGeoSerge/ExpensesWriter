namespace ExpensesWriter.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Expense_IsDeleted_FieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "IsDeleted");
        }
    }
}
