using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server.Auth
{
    internal static class AuthExtensions
    {
        internal static string GetSha1(this string content)
        {
            byte[] hash;

            using var sha1 = new SHA1Managed();

            hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(content));

            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}
