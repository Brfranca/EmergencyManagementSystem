using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Extension
{
    public static class StringExtension
    {
        public static string RemoveMaskCPF(this string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }

        public static string InsertMaskCPF(this string cpf)
        {
            return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
        }


    }
}
