using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyDiem.Models
{
    public class Account
    {
        [Key]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Tên tài khoản không được để trống !")]
        public string UseName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Mật Khẩu không được để trống !")]
        //Dùng để biến kí tự thành "".""
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [StringLength(10)]
        [Display(Name = "Phân Quyền")]
        [Required(ErrorMessage = "Phân Quyền không được để trống !!!")]
        public string RoleID { get; set; }
    }
}