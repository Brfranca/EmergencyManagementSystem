using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Common.Extentions
{
    public static class StringExtentions
    {
        public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

        //verifica se a string é nula ou se só tem espaços em branco
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
    }
}
