using System.Text;
using System.Threading;
using WebApplication.EmailService;
using WebApplication.Models;

namespace WebApplication.EmailTemplates
{
    public class CompanyDenyTemplate
    {
        public static string ComposeBody(Company company, CancellationToken cancellationToken)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Beste " + company.ContactPerson.FirstName + "," + "<br>" + "<br>")
                .Append("Het spijt ons te moeten melden  dat uw bedrijf niet voldoet aan al de criteria die wij vooropstellen om stagevoorstellen voor de richting Professionele Bachelor in de Toegepaste Informatica te kunnen indienen." + "<br>" + "<br>")
                .Append("De criteria waaraan uw bedrijf niet voldoet zijn (of is):" + "<br>");
            if (company.ReasonOfInactive != null && company.ReasonOfInactive.Trim() != "")
            {
                sb.Append(company.ReasonOfInactive);
            }
            if (company.FeedBack != null && company.FeedBack.Value.Trim() != "")
            {
                sb.Append(company.FeedBack.Value);
            }
            sb.Append("<br>" + "Voor bijkomende inlichtingen kunt u mij contacteren via onderstaande gegevens" + "<br>" + "<br>")
                .Append("Met vriendelijke groeten" + "<br>")
                .Append(ConstantValues.MAIL_FROM_NAME + "<br>")
                .Append(ConstantValues.MAIL_FROM + "<br>" + "<br>")
                .Append("Stagecoördinator PXL");

            return sb.ToString();
        }
    }
}
