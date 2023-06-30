using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public static class Utility
    {
        //Get Algorithm for where we need 
        public static SHA256 GetAlgo()
        {
            SHA256 algorithm = SHA256.Create();

            return algorithm;
        }

        /**
        *  Function to generate Hascode using System.Security.Cryptography
        *  Parameters
        *  Value          - Value to be converted to hash
        *  algorithm      - algorithm to be used to create hash ex. SHA256CryptoServiceProvider ,MD5CryptoServiceProvider
        SHA256 algorithm = SHA256.Create();
             * **/
        public static string GetHash(string Value, HashAlgorithm algorithm)
        {
            string pwdhash;
            pwdhash = Convert.ToBase64String(algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Value)));
            //Encode html url for 
           // pwdhash = WebUtility.UrlEncode(pwdhash);
            return pwdhash;

        }

        //public static string DecodeHash(string Value, HashAlgorithm algorithm)
        //{
        //    string pwdhash;
        //    algorithm.
        //    pwdhash = Convert.ToBase64String(algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Value)));
        //    return pwdhash;
        //}

        // Verify a hash against a string.
        public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(input, hashAlgorithm);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        public static string GenerateHtmlTable(int columnCount, int rowCount)
        {
            return
                "<table>" +
                    Enumerable.Range(1, rowCount).Select(r =>
                    //"<tr>"+
                    //Enumerable.Range(columnCount * (r - 1) + 1, columnCount)
                    //    .Select(n => "<th>" + n.ToString() + "</th>")
                    //    .Aggregate((html, cell) => html + cell) +
                    //"</tr>"+
                    "<tr>" +
                        Enumerable.Range(columnCount * (r - 1) + 1, columnCount)
                        .Select(n => "<td>" + n.ToString() + "</td>")
                        .Aggregate((html, cell) => html + cell) +
                    "</tr>"
                    ).Aggregate((html, row) => html + row) +
                "</table>";
        }

    }
}
