using System.Text;
using System.Threading;
using WebApplication.EmailService;
using WebApplication.Models;

namespace WebApplication.EmailTemplates
{
    public class InternshipApproveTemplate
    {
        public static string ComposeBody(Internship internship, CancellationToken cancellationToken)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Beste " + internship.ContactPerson.FirstName + "," + "<br>" + "<br>")
                .Append("We hebben jullie stageopdracht '" + internship.Title + "' voor onze laatstejaarsstudenten Bachelor Toegepaste Informatica nagekeken en goedgekeurd." + "<br>" + "<br>")
                .Append("Onze studenten kunnen uw stagevoorstel consulteren en wij zullen u contacteren voor een verdere opvolging." + "<br>" + "<br>")
                .Append("Voor bijkomende inlichtingen kunt u mij contacteren via onderstaande gegevens" + "<br>" + "<br>")
                .Append("Met vriendelijke groeten" + "<br>")
                .Append(ConstantValues.MAIL_FROM_NAME + "<br>")
                .Append(ConstantValues.MAIL_FROM + "<br>" + "<br>")
                .Append("Stagecoördinator PXL");

            return sb.ToString();
        }
    }
}
