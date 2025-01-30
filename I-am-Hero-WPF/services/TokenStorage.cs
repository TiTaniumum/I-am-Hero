using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class TokenStorage
{
    private static readonly string filePath = "token.dat";

    public static void SaveToken(string token)
    {
        try
        {
            byte[] encryptedData = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(token),
                null,
                DataProtectionScope.CurrentUser
            );

            File.WriteAllBytes(filePath, encryptedData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении токена: {ex.Message}");
        }
    }

    public static string LoadToken()
    {
        try
        {
            if (!File.Exists(filePath))
                return null;

            byte[] encryptedData = File.ReadAllBytes(filePath);
            byte[] decryptedData = ProtectedData.Unprotect(
                encryptedData,
                null,
                DataProtectionScope.CurrentUser
            );

            return Encoding.UTF8.GetString(decryptedData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке токена: {ex.Message}");
            return null;
        }
    }

    public static void DeleteToken()
    {
        try
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении токена: {ex.Message}");
        }
    }
}
