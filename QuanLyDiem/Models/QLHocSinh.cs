using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDiem.Models
{
    [Table("HocSinhs")]
    public class QLHocSinh
    {
        [Key]
        [Required(ErrorMessage = "Mã học sinh không được để trống !!!")]
        [Display(Name = "Mã học sinh")]
        public string MaHS { get; set; }
        [Required(ErrorMessage = "Tên học sinh không được để trống !!!")]
        [Display(Name = "Tên học sinh")]
        public string TenHS { get; set; }
        [Display(Name = "Giới Tính")]
        public string GioiTinh { get; set; }
        [Display(Name = "Ngày Sinh")]
        public string NgaySinh { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [AllowHtml]
        [Display(Name = "Ảnh học sinh")]
        public string AnhHS { get; set; }

        [Display(Name = "Mã Lớp")]
        public string MaLop { get; set; }
        public QLLop QLLop { get; set; }
        /*public object QLMonHoc { get; internal set; }*/
    }
}