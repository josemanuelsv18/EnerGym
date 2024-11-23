using System.Security.Cryptography;
using System.Text;

public class Seguridad
{
    public string Encriptar(string textoAEncriptar)
    {
        string clave = "clavePrueba1234";
        string iv = "vectorInicial123";

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(clave), aesAlg.Key, Math.Min(aesAlg.Key.Length, Encoding.UTF8.GetBytes(clave).Length));


            aesAlg.IV = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(iv), aesAlg.IV, Math.Min(aesAlg.IV.Length, Encoding.UTF8.GetBytes(iv).Length));

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            byte[] buffer = Encoding.UTF8.GetBytes(textoAEncriptar);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(buffer, 0, buffer.Length);
                    cs.FlushFinalBlock();
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public string Desencriptar(string textoEncriptado)
    {
        string clave = "clavePrueba1234";
        string iv = "vectorInicial123";

        try {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = new byte[16];
                Array.Copy(Encoding.UTF8.GetBytes(clave), aesAlg.Key, Math.Min(aesAlg.Key.Length, Encoding.UTF8.GetBytes(clave).Length));

                aesAlg.IV = new byte[16];
                Array.Copy(Encoding.UTF8.GetBytes(iv), aesAlg.IV, Math.Min(aesAlg.IV.Length, Encoding.UTF8.GetBytes(iv).Length));

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] buffer;
                try
                {
                    buffer = Convert.FromBase64String(textoEncriptado);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("El texto encriptado no tiene un formato válido de Base64.", nameof(textoEncriptado));
                }

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Agregar manejo de excepciones para ver el origen exacto del error
            throw new InvalidOperationException("Ocurrió un error al desencriptar el texto.", ex);
        }
    }
}