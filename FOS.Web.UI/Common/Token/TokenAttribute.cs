using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FOS.Web.UI.Common.Token
{
    public class TokenAttribute
    {

        private const string _alg = "HmacSHA256";
        private const string _salt = "rz8LuOtFBXphj9WQfvFh"; //
        private const int _expirationMinutes = 10;


        public static string GenerateToken(string username, string password)
        {
            string hash = string.Join(":", new string[] { username.ToLower(), password.ToLower() });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {

                hashLeft = Base64Encode(password.ToLower());
                hashRight = Base64Encode(username.ToLower());
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }


        public static bool IsTokenValid(string token, bool isRegionalHead = false)
        {
            bool result = false;
            try
            {
                // Base64 decode the string, obtaining the token:username:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                // Split the parts.
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 2)
                {
                    // Get the hash message, username, and timestamp.
                    string hash = Base64Decode(parts[0]);
                    string username = Base64Decode(parts[1]);
                    FOSDataModel db = new FOSDataModel();

                    if (isRegionalHead)
                    {
                        var UserName = db.Users.Where(s => s.UserName == username && s.Password == hash).Select(s => s.UserName).FirstOrDefault();
                        var Password = db.Users.Where(s => s.UserName == username && s.Password == hash).Select(s => s.Password).FirstOrDefault();

                        if (username == UserName.ToLower())
                        {
                            if (hash == Password.ToLower())
                            {
                                // Hash the message with the key to generate a token.
                                string computedToken = GenerateToken(username, hash);
                                // Compare the computed token with the one supplied and ensure they match.
                                result = (token == computedToken);
                            }
                            else
                            {
                                //Wrong Password 
                            }
                        }
                        else
                        {
                            //Wrong UserName 
                        }
                    }
                    else
                    {
                        var UserName = db.SaleOfficers.Where(s => s.UserName == username && s.Password == hash).Select(s => s.UserName).FirstOrDefault();
                        var Password = db.SaleOfficers.Where(s => s.UserName == username && s.Password == hash).Select(s => s.Password).FirstOrDefault();

                        if (username == UserName.Trim().ToLower())
                        {
                            if (hash == Password.ToLower())
                                {
                                // Hash the message with the key to generate a token.
                                string computedToken = GenerateToken(username, hash);
                                // Compare the computed token with the one supplied and ensure they match.
                                result = (token == computedToken);
                            }
                            else
                            {
                                //Wrong Password 
                            }
                        }
                        else
                        {
                            //Wrong UserName 
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
}