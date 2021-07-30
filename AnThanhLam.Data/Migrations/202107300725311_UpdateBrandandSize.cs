namespace AnThanhLam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBrandandSize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        DisplayOrder = c.Int(),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "BrandID", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "SizeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "BrandID");
            AddForeignKey("dbo.Products", "BrandID", "dbo.Brands", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropColumn("dbo.Products", "SizeID");
            DropColumn("dbo.Products", "BrandID");
            DropTable("dbo.Sizes");
            DropTable("dbo.Brands");
        }
    }
}
