using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace QuanLyDiem.Models
{
    public class AutoGenerateKey
    {
        public string GenerateKey(string ID)
        {
            string strKey = "";
            string numPart = "", strPart = "", strPhanSo = "";
            numPart = Regex.Match(ID, @"\d+").Value;
            strPart = Regex.Match(ID, @"\D+").Value;
            int phanso = (Convert.ToInt32(numPart) + 1);
            for (int i = 0; i < numPart.Length - phanso.ToString().Length; i++)
            {
                strPhanSo += "0";
            }
            strPhanSo += phanso;
            //tach phan chu
            strKey = strPart + strPhanSo;
            return strKey;
        }
        public string GetMD5(string strInput)
        {
            string str_md5 = "";
            byte[] arrOut = System.Text.Encoding.UTF8.GetBytes(strInput);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            arrOut = my_md5.ComputeHash(arrOut);
            foreach (byte b in arrOut)
            {
                str_md5 += b.ToString("X2");
            }
            return str_md5;
        }
    }
}