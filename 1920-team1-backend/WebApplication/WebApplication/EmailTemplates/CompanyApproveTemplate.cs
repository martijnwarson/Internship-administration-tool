using System.Text;
using System.Threading;
using WebApplication.EmailService;
using WebApplication.Models;

namespace WebApplication.EmailTemplates
{
    public class CompanyApproveTemplate
    {
        public static string ComposeBody(Company company, CancellationToken cancellationToken)
        {
            string url = ConstantValues.LOGIN_PAGE_URL;
            StringBuilder sb = new StringBuilder();

            sb.Append("Beste " + company.ContactPerson.FirstName + ","  + "<br>" + "<br>")
                .Append("Wij zijn verheugd u te kunnen melden dat uw bedrijf voldoet aan de criteria die wij vooropstellen om stagevoorstellen voor de richting Professionele Bachelor in de Toegepaste Informatica te kunnen indienen." + "<br>" + "<br>")
                .Append("U heeft normaal gezien een mail ontvangen met uw login gegevens of u zal deze mail zeer binnenkort krijgen." + "<br>")
                .Append("Via <a href=\"" + url+ "\"> deze link</a> komt u terecht op de loginpagina." + "<br>" + "<br>")
                .Append("Voor bijkomende inlichtingen kunt u mij contacteren via onderstaande gegevens" + "<br>" + "<br>")
                .Append("Met vriendelijke groeten" + "<br>")
                .Append(ConstantValues.MAIL_FROM_NAME + "<br>")
                .Append(ConstantValues.MAIL_FROM + "<br>" + "<br>")
                .Append("Stagecoördinator PXL");

            return sb.ToString();
        }
    }
}
