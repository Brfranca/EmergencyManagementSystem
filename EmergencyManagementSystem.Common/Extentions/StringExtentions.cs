using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EmergencyManagementSystem.Common.Extentions
{
    public static class StringExtentions
    {
        //verifica se o cpf é válido
        public static bool IsValidCPF(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        //veririca se a string é somente números
        public static bool IsNumber(this string number)
        {
            return Regex.IsMatch(number, "^[0-9]+$");
        }

        //verifica se o email é válido
        public static bool IsValidEmail(this string email)
        {
            return Regex.IsMatch(email, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        }

        //verifica se o telefone é válido

        public static bool IsValidPhone(this string phone)
        {
            return Regex.IsMatch(phone, @"^\(?(?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$");
        }
        //verifica se o nome interéiro é válido
        public static bool IsValidFullName(this string fullName)
        {
            var names = fullName.Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).ToList();
            if (names.Count <= 1)
            {
                return false;
            }
            return true;
        }

        //vefifica se o nome é válido
        public static bool IsValidName(this string fullName)
        {
            if (!Regex.IsMatch(fullName, @"^[\p{L} \.\-]+$"))
            {
                return false;
            }
            return true;
        }

        //remove a máscara do cpf
        public static string RemoveMaskCPF(this string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }


        //insere a mascara do cpf
        public static string InsertMaskCPF(this string cpf)
        {
            return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
        }

        //gera o hash da senha
        public static string GenerateHash(this string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] vec = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < vec.Length; i++)
            {
                stringBuilder.Append(vec[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        //verifica se o cep é válido
        public static bool IsValidCEP(this string cep)
        {
            return Regex.IsMatch(cep, @"^\d{5}-d\{3}$");
        }
        public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

        //verifica se a string é nula ou se só tem espaços em branco
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
    }
}
