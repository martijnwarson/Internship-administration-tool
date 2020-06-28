using System.Text;
using System.Threading;
using WebApplication.EmailService;
using WebApplication.Models;

namespace WebApplication.EmailTemplates
{
    public class InternshipDenyTemplate
    {
        public static string ComposeBody(Internship internship, CancellationToken cancellationToken)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Beste " + internship.ContactPerson.FirstName + "," + "<br>" + "<br>")
                .Append("Het spijt ons te moeten melden  dat het stagevoorstel '" + internship.Title + "' niet voldoet aan al de criteria die wij vooropstellen voor de richting Professionele Bachelor in de Toegepaste Informatica." + "<br>" + "<br>")
                .Append("De criteria waaraan het voorstel niet voldoet zijn (of is):" + "<br>");
            if (internship.FeedBack?.Value != null && internship.FeedBack?.Value.Trim() != "")
            {
                sb.Append(internship.FeedBack.Value);
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
