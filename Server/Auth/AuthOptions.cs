using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Server.Auth
{
    public class AuthOptions
    {
        internal const int Lifetime = 1;
        internal const string Issuer = "MyIssuer";
        internal const string Audience = "MyServer";

        private const string PublicKeyString = "MIIBCgKCAQEA1d6OiSPbTddNXYeD20bHw0FYJm5ik3W797ABK9fupdlnAoHlLSGhpDRMI\u002BrQTu14LjXDA9zSIpr8l0tZ9gebFVmmPz0KRQvZDzhE4s/URjtw1Z0FXzm4xYRfW6aCY9CVZi1lNdsVPyptpdb\u002BqZwl1qxFS6MEkubWY0j4uEWPAlIOU\u002BNwqnGGimfU9C\u002ByCCsODtv7UfrwLOW97T7sMxoSqIIExSRzB\u002BJ0SlKD5PMuRjUFIq7wTQZISR3nFpqWv\u002BnTeRDb7CMBhp3kbZalcI\u002BUydiaKhbQqhSs7TOB5IzBmpup7HyRS4WYXXgv5YufkXjlo54kySxOAdjD5t/aXOiYWQIDAQAB";
        private const string PrivateKeyString = "MIIEpAIBAAKCAQEA1d6OiSPbTddNXYeD20bHw0FYJm5ik3W797ABK9fupdlnAoHlLSGhpDRMI\u002BrQTu14LjXDA9zSIpr8l0tZ9gebFVmmPz0KRQvZDzhE4s/URjtw1Z0FXzm4xYRfW6aCY9CVZi1lNdsVPyptpdb\u002BqZwl1qxFS6MEkubWY0j4uEWPAlIOU\u002BNwqnGGimfU9C\u002ByCCsODtv7UfrwLOW97T7sMxoSqIIExSRzB\u002BJ0SlKD5PMuRjUFIq7wTQZISR3nFpqWv\u002BnTeRDb7CMBhp3kbZalcI\u002BUydiaKhbQqhSs7TOB5IzBmpup7HyRS4WYXXgv5YufkXjlo54kySxOAdjD5t/aXOiYWQIDAQABAoIBADWnDIj49tZTUfRJv9ltq0WfifayMIafvZUIkXKnTBZNMNtfLNginMNt30APc0yppEGreQ3qAsltqjpmS9490DkN8Xfh30atDzoTOqzPyIgJ92ePab6W43Shna6HWiSiOiiU8NQO7wiWo0U8Z0yzgIaXVxb/gXE8\u002BcGUl9jakmLZL3dmCwLXk2UGb0QPEXfFffUHoZgbirkOH6gnMA1KrEjg6QYqsx4yASIW1GA1We91RL02TYeJ9PqB1FQBBW8d\u002BPL8BmpQv/buRjsjC5VF0ICQOAe3jjSiWIk\u002BQSJjmpgeUa8WK\u002BVycHx/QP0LSPdZS/A6hH/i173LuUTk/02awIUCgYEA3vw8C7NHp\u002BTvKuqovQ7nTuzMNav2s2xqyMr\u002Bre9Khmok6y1qoIC1\u002B\u002BRa07xSg4A9xXJd9ZndBM9XnbW/xqP4SGJwzHNXuJm6UBRbw8tRjuDGcGQOQe0JqF/QUKXQhjCmR2p78PQlhax1vBee\u002BFtpJzbKHyn6Rw8nSU7R/m5y4ssCgYEA9YjNlVCP5tPIGh\u002B52YpWs1Z6u\u002BPAtZJ8iOGOe8MfQvFw1GkIg04mxCodJPB3xp3uFw6U20fG9PXYjcSckcrkGsrPr3TyDUdKHGJtlZuw8HapLOeAaE6DQMlimERoOkwQW0bR6gNJnvRXhhhxMSCxuQOL9uK0iDUEjFnERK7COOsCgYEAzT64ekCLrTNhHUyfAl4MdK4rjR961U3uwGFwqGLAYxaiYsIMCi5O08st\u002BwS1oorQ0MPjTmRQdtPunqCjI9DbgKzzjIyCas09G\u002BaRTJgBuxSUMvE12mK3Z/7BPOpTFKEE/Gk8Kd5gBzllqTrIvWtOT6TzyFmYH/4HuEdPrPS21J8CgYEApREqfNZjV\u002BBK510q2ZgKP5jE/GnaFXd8p51T59o\u002BuweMPhC6u2Of5kMVws6zB2EHPpL7\u002BgtgMzDQPEbQp6cKAQUV6LQrQkgKB9kYKPs5Uf/ELj8YcYdO8BAza4CZvY\u002BDc4nB95vsM6O675ihCFEdie9bekfg7K0P\u002BLcknrQKOK8CgYAZbzmPKCj3SXs/4ROkRaQifEA1uhSJsRbJOOpYm1AqPg3c6CuqeDbJ\u002Brcn8abTbz1orr7Q4IxsfQBHleXR9s83cNkr3O9XHyLRX\u002BuiJPKdUXkVtABQYg0UbwUrAdxcQ/DmzDpudhB9/c\u002BmuajFwjCYwJTWnPWcgNLVwcCDIxRGRQ==";

        internal static SecurityKey PublicKey = GetPublicKey();
        internal static SecurityKey PrivateKey = GetPrivateKey();

        private static SecurityKey GetPublicKey()
        {
            var key = RSA.Create();
            key.ImportRSAPublicKey(source: Convert.FromBase64String(PublicKeyString), bytesRead: out var _);
            return new RsaSecurityKey(key);
        }

        private static SecurityKey GetPrivateKey()
        {
            var key = RSA.Create();
            key.ImportRSAPrivateKey(source: Convert.FromBase64String(PrivateKeyString), bytesRead: out var _);
            return new RsaSecurityKey(key);
        }
    }
}
