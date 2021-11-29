namespace QuanLyDiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qldhs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonHocs", "QLMonHoc_MaMH", c => c.String(maxLength: 128));
            AddColumn("dbo.Giaoviens", "MaMH", c => c.String(maxLength: 128));
            CreateIndex("dbo.MonHocs", "QLMonHoc_MaMH");
            CreateIndex("dbo.Giaoviens", "MaMH");
            AddForeignKey("dbo.MonHocs", "QLMonHoc_MaMH", "dbo.MonHocs", "MaMH");
            AddForeignKey("dbo.Giaoviens", "MaMH", "dbo.MonHocs", "MaMH");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Giaoviens", "MaMH", "dbo.MonHocs");
            DropForeignKey("dbo.MonHocs", "QLMonHoc_MaMH", "dbo.MonHocs");
            DropIndex("dbo.Giaoviens", new[] { "MaMH" });
            DropIndex("dbo.MonHocs", new[] { "QLMonHoc_MaMH" });
            DropColumn("dbo.Giaoviens", "MaMH");
            DropColumn("dbo.MonHocs", "QLMonHoc_MaMH");
        }
    }
}
