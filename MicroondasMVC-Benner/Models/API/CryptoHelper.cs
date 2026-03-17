using DotNetEnv;
using System.Security.Cryptography;
using System.Text;

namespace MicroondasMVC_Benner.Models.API
{
    public class CryptoHelper
    {
        private static readonly string Key = Env.GetString("ChaveConnection", "");

        // Método auxiliar para garantir que a chave tenha exatamente 32 bytes (256 bits)
        // Ajusta/normaliza a chave vinda do .env pra um tamanho que o AES aceite sem drama.
        private static byte[] GetValidAesKey(string keyString)
        {
            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("A chave de criptografia não foi encontrada no .env ou está vazia.");

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(keyString));
        }

        // Criptografa o texto (ex.: connection string) pra guardar de um jeito menos “na cara”.
        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = GetValidAesKey(Key);
            aes.IV = new byte[16]; // *Nota de segurança abaixo

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs)) sw.Write(plainText);

            return Convert.ToBase64String(ms.ToArray());
        }

        // Descriptografa o texto e devolve o valor original pra usar em runtime.
        public static string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = GetValidAesKey(Key);
            aes.IV = new byte[16];

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}