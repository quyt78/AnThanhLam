namespace AnThanhLam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeVisitoer : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.VisitorStatistics");
            AlterColumn("dbo.VisitorStatistics", "ID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.VisitorStatistics", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.VisitorStatistics");
            AlterColumn("dbo.VisitorStatistics", "ID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.VisitorStatistics", "ID");
        }
    }
}
