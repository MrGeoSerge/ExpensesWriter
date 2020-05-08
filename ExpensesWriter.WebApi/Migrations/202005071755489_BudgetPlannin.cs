namespace ExpensesWriter.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetPlannin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetPlanningItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BudgetItemId = c.Int(nullable: false),
                        Money = c.Double(nullable: false),
                        PlanningMonth = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BudgetItems", t => t.BudgetItemId, cascadeDelete: true)
                .Index(t => t.BudgetItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetPlanningItems", "BudgetItemId", "dbo.BudgetItems");
            DropIndex("dbo.BudgetPlanningItems", new[] { "BudgetItemId" });
            DropTable("dbo.BudgetPlanningItems");
        }
    }
}
