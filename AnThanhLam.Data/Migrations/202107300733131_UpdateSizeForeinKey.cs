namespace AnThanhLam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSizeForeinKey : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "SizeID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "SizeID", c => c.Int(nullable: false));
        }
    }
}
