using LoginProgramacion3.Models;
using System.Security.Cryptography;
using System.Text;

namespace LoginProgramacion3.Utilidades
{
    public class Seguridad
    {

        public string EncriptarPassword(string password)
        {
           
             using (SHA256 sha256 = SHA256.Create())
             {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hashedPassword;
            }
        }

        public bool VericarPassword(Usuario usuario, string password)
        {
            string hashedPasswordBaseDatos = usuario.Password;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPasswordToCheck = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                if (hashedPasswordToCheck == hashedPasswordBaseDatos)
                {
                    return true;
                }
                
            }

            return false;
        }
        
    }
}
