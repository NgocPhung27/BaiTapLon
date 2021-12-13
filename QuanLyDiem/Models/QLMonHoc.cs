using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDiem.Models
{
    [Table("MonHocs")]
    public class QLMonHoc
    {
        [Key]
        [Required(ErrorMessage = "Mã Môn không được để trống !!!")]
        [Display(Name = "Mã môn học")]
        public string MaMH { get; set; }
        [Required(ErrorMessage = "Tên Môn không được để trống !!!")]
        [Display(Name = "Tên môn học")]
        public string TenMH { get; set; }

        [AllowHtml]
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
        public ICollection<DiemHocSinh> DiemHocSinhs { get; set; }
    }
}