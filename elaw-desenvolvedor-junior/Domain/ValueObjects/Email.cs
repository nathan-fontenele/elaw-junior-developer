using System.Text.RegularExpressions;

namespace elaw_desenvolvedor_junior.Domain.ValueObjects
{
    public class Email
    {
        internal string EmailAddress { get; set; }

        public Email()
        {
            
        }

        public Email(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentNullException(nameof(emailAddress), "O endereço de e-mail não pode ser nulo ou vazio");

            if (!IsValidEmailAddress(emailAddress))
                throw new ArgumentException("O e-mail não está em um formato válido.", nameof(emailAddress));

            EmailAddress = emailAddress;
        }

        private bool IsValidEmailAddress(string emailAddress)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(emailAddress);
                return true; // Se chegou aqui, o e-mail é válido
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
