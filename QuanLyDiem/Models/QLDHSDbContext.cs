using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyDiem.Models
{
    public partial class QLDHSDbContext : DbContext
    {
        public QLDHSDbContext()
            : base("name=QLDHSDbContext")
        {
        }
        public virtual DbSet<QLGiaoVien> GiaoViens { get; set; }
        public virtual DbSet<QLHocSinh> HocSinhs { get; set; }
        public virtual DbSet<DiemHocSinh> DiemHocSinhs { get; set; }
        public virtual DbSet<QLMonHoc> MonHocs { get; set; }
        public virtual DbSet<QLLop> Lops { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
