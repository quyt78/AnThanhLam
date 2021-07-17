namespace AnThanhLam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles");
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropPrimaryKey("dbo.ApplicationRoleGroups");
            DropPrimaryKey("dbo.ApplicationRoles");
            AlterColumn("dbo.ApplicationRoleGroups", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ApplicationRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ApplicationRoles", "Description", c => c.String(maxLength: 50));
            AlterColumn("dbo.ApplicationUserRoles", "IdentityRole_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ApplicationRoleGroups", new[] { "GroupId", "RoleId" });
            AddPrimaryKey("dbo.ApplicationRoles", "Id");
            CreateIndex("dbo.ApplicationRoleGroups", "RoleId");
            CreateIndex("dbo.ApplicationUserRoles", "IdentityRole_Id");
            AddForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropPrimaryKey("dbo.ApplicationRoles");
            DropPrimaryKey("dbo.ApplicationRoleGroups");
            AlterColumn("dbo.ApplicationUserRoles", "IdentityRole_Id", c => c.String(maxLength: 50));
            AlterColumn("dbo.ApplicationRoles", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.ApplicationRoles", "Id", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ApplicationRoleGroups", "RoleId", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.ApplicationRoles", "Id");
            AddPrimaryKey("dbo.ApplicationRoleGroups", new[] { "GroupId", "RoleId" });
            CreateIndex("dbo.ApplicationUserRoles", "IdentityRole_Id");
            CreateIndex("dbo.ApplicationRoleGroups", "RoleId");
            AddForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles", "Id");
            AddForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles", "Id", cascadeDelete: true);
        }
    }
}
