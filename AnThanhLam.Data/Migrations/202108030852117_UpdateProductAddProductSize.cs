namespace AnThanhLam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductAddProductSize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        SizeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.SizeID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.SizeID);
            
            AddColumn("dbo.Products", "Sizes", c => c.String());
            AddColumn("dbo.Sizes", "Type", c => c.String());
            DropColumn("dbo.Products", "SizeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SizeID", c => c.Int());
            DropForeignKey("dbo.ProductSizes", "SizeID", "dbo.Sizes");
            DropForeignKey("dbo.ProductSizes", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductSizes", new[] { "SizeID" });
            DropIndex("dbo.ProductSizes", new[] { "ProductID" });
            DropColumn("dbo.Sizes", "Type");
            DropColumn("dbo.Products", "Sizes");
            DropTable("dbo.ProductSizes");
        }
    }
}
