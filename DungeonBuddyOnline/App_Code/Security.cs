using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public static class Security
{

    //New Bcrypt encrypt code fo sho
    public static Byte[] encrypt(string unencryptedString, string salt)
    {
        Byte[] hashedDataBytes = null;
        UTF8Encoding encoder = new UTF8Encoding();

        hashedDataBytes = encoder.GetBytes(BCrypt.Net.BCrypt.HashPassword(unencryptedString, salt));

        return hashedDataBytes;
    }

    //Generates a salt value
    public static string getSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }
}