using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDiem.Models
{
    [Table("Lops")]
    public class QLLop
    {
        [Key]
        [Required(ErrorMessage = "Mã Lớp không được để trống !!!")]
        [Display(Name = "Mã Lớp")]
        public string MaLop { get; set; }
        [Required(ErrorMessage = "Tên Lớp không được để trống !!!")]
        [Display(Name = "Tên Lớp")]
        public string TenLop { get; set; }
        [Display(Name = "Niên Khóa")]
        public string NienKhoa { get; set; }
        [Display(Name = "Sĩ Số")]
        public string SiSo { get; set; }

        [AllowHtml]
        [Display(Name = "Ghi Chú")]
        public string GhiChu { get; set; }
        public ICollection<QLHocSinh> QLHocSinhs { get; set; }

    }
}