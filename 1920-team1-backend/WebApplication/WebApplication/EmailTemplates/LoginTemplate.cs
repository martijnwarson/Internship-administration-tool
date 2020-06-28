using System.Text;
using WebApplication.EmailService;
using WebApplication.Models;

namespace WebApplication.EmailTemplates
{
    public class LoginTemplate
    {
        public static string ComposeBody(Person person, string password)
        {
            string url = ConstantValues.LOGIN_PAGE_URL;
            StringBuilder sb = new StringBuilder();

            sb.Append("Beste " + person.FirstName + "," + "<br>" + "<br>")
                .Append(
                    "Uw inloggegevens om toegang te krijgen tot de applicatie voor de stages van de Professionele bacheloropleiding richting Toegepaste Informatica zijn:" +"<br>" + "<br>")
                .Append("Gebruikersnaam: " + person.Email + "<br>")
                .Append("Paswoord: " + password + "<br>" + "<br>")
                .Append("Via <a href=\"" + url + "\">deze link</a> komt u terecht op de loginpagina." + "<br>" + "<br>")
                .Append("Voor bijkomende inlichtingen kunt u mij contacteren via onderstaande gegevens" + "<br>" + "<br>")
                .Append("Met vriendelijke groeten" + "<br>")
                .Append(ConstantValues.MAIL_FROM_NAME + "<br>")
                .Append(ConstantValues.MAIL_FROM + "<br>" + "<br>")
                .Append("Stagecoördinator PXL");

            return sb.ToString();
        }
    }
}