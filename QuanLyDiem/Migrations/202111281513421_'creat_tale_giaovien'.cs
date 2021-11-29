namespace QuanLyDiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creat_tale_giaovien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HocSinhs",
                c => new
                    {
                        MaHS = c.String(nullable: false, maxLength: 128),
                        TenHS = c.String(nullable: false),
                        GioiTinh = c.String(),
                        NgaySinh = c.String(),
                        SoDienThoai = c.String(),
                        DiaChi = c.String(),
                        AnhHS = c.String(),
                        MaLop = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaHS)
                .ForeignKey("dbo.Lops", t => t.MaLop)
                .Index(t => t.MaLop);
            
            CreateTable(
                "dbo.Lops",
                c => new
                    {
                        MaLop = c.String(nullable: false, maxLength: 128),
                        TenLop = c.String(nullable: false),
                        NienKhoa = c.String(),
                        SiSo = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.MaLop);
            
            CreateTable(
                "dbo.MonHocs",
                c => new
                    {
                        MaMH = c.String(nullable: false, maxLength: 128),
                        TenMH = c.String(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.MaMH);
            
            CreateTable(
                "dbo.Giaoviens",
                c => new
                    {
                        MaGV = c.String(nullable: false, maxLength: 128),
                        TenGV = c.String(nullable: false),
                        GioiTinh = c.String(),
                        NgaySinh = c.String(),
                        SoDienThoai = c.String(),
                        DiaChi = c.String(),
                        AnhGV = c.String(),
                    })
                .PrimaryKey(t => t.MaGV);
            
            CreateTable(
                "dbo.DiemHocSinhs",
                c => new
                    {
                        MaHS = c.String(nullable: false, maxLength: 128),
                        DiemMieng = c.String(),
                        Diem15Phut = c.String(),
                        Diem1Tiet = c.String(),
                        DiemHK = c.String(),
                        DiemTBHK = c.String(),
                        GhiChu = c.String(),
                        MaMH = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaHS)
                .ForeignKey("dbo.HocSinhs", t => t.MaHS)
                .ForeignKey("dbo.MonHocs", t => t.MaMH)
                .Index(t => t.MaHS)
                .Index(t => t.MaMH);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DiemHocSinhs", "MaMH", "dbo.MonHocs");
            DropForeignKey("dbo.DiemHocSinhs", "MaHS", "dbo.HocSinhs");
            DropForeignKey("dbo.HocSinhs", "MaLop", "dbo.Lops");
            DropIndex("dbo.DiemHocSinhs", new[] { "MaMH" });
            DropIndex("dbo.DiemHocSinhs", new[] { "MaHS" });
            DropIndex("dbo.HocSinhs", new[] { "MaLop" });
            DropTable("dbo.DiemHocSinhs");
            DropTable("dbo.Giaoviens");
            DropTable("dbo.MonHocs");
            DropTable("dbo.Lops");
            DropTable("dbo.HocSinhs");
        }
    }
}
