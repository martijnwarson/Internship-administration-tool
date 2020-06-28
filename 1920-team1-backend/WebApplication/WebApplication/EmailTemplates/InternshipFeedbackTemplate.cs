using System.Text;
using System.Threading;
using WebApplication.EmailService;
using WebApplication.Models;

namespace WebApplication.EmailTemplates
{
    public class InternshipFeedbackTemplate
    {
        public static string ComposeBody(Internship internship, CancellationToken cancellationToken)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Beste " + internship.ContactPerson.FirstName + "," + "<br>" + "<br>")
                .Append("We hebben jullie stageopdracht '" + internship.Title + "' voor onze laatstejaarsstudenten Bachelor Toegepaste Informatica nagekeken en hebben nog enkele bijkomende vragen of opmerkingen." + "<br>" + "<br>")
                .Append("Graag kregen we meer informatie over het volgende:" + "<br>");
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
