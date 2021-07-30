namespace AnThanhLam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBrand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sizes", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Sizes", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Sizes", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Sizes", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Sizes", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Sizes", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Sizes", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sizes", "Status");
            DropColumn("dbo.Sizes", "MetaDescription");
            DropColumn("dbo.Sizes", "MetaKeyword");
            DropColumn("dbo.Sizes", "UpdatedBy");
            DropColumn("dbo.Sizes", "UpdatedDate");
            DropColumn("dbo.Sizes", "CreatedBy");
            DropColumn("dbo.Sizes", "CreatedDate");
        }
    }
}
