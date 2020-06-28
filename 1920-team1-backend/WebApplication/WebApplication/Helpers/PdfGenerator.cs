using System.Collections;
using System.Collections.Generic;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public class PdfGenerator
        : IPdfGenerator
    {
        
        public FileStreamResult CreateCompanyPdf(Company company)
        {
            // Base PDF Settings
            var workStream = new MemoryStream();
            using (var pdfWriter = new PdfWriter(workStream))
            {
                pdfWriter.SetCloseStream(false);
                var pdf = new PdfDocument(pdfWriter);
                var document = new Document(pdf);
                
                // Set margins and header image
                SetPdfLayout(document);

                // Add paragraphs
                CreateStringParagraph(document, "Naam", company.Name);
                CreateAddressParagraph(document, company.Address);
                CreatePersonParagraph(document, "Contactpersoon", company.ContactPerson);
                CreateStringParagraph(document, "Aantal Medewerkers", company.AmountOfEmployees.ToString());
                CreateStringParagraph(document, "Aantal Medewerkers in IT", company.AmountOfEmployeesIt.ToString());
                
                // Close and save document
                document.Close();
            }
            workStream.Position = 0;
            
            // Return stream with PDF file
            return new FileStreamResult(workStream, "application/pdf");
        }
        
        public FileStreamResult CreateInternshipPdf(Internship internship)
        {
            // Base PDF Settings
            var workStream = new MemoryStream();
            using (var pdfWriter = new PdfWriter(workStream))
            {
                pdfWriter.SetCloseStream(false);
                var pdf = new PdfDocument(pdfWriter);
                var document = new Document(pdf);
                
                // Set margins and header image
                SetPdfLayout(document);
                
                // Add paragraphs
                // Bedrijfsinfo
                CreateStringTitle(document, "Bedrijfsinfo");
                CreateStringParagraph(document, "Naam", internship.Company.Name);
                CreateAddressParagraph(document, internship.Company.Address);
                CreatePersonParagraph(document, "Contactpersoon", internship.ContactPerson);
                
                // Stage omschrijving
                CreateStringTitle(document, "Stageomschrijving");
                CreateStringParagraph(document, "Titel", internship.Title);
                CreateStringParagraph(document, "Omschrijving", internship.Description);
                CreateStringParagraph(document, "Research Topic", internship.ResearchTopic);
                CreateTechnologyList(document, "Gebruikte techonologiën", internship.Technologies);
                CreateStringParagraph(document, "Technologie Omchrijving", internship.TechDescription);
                
                // Stage Details
                CreateStringTitle(document, "Stagedetails");
                CreatePeriodList(document, "Stageperiode", internship.Periods);
                CreateCourseList(document, "Voor afstudeerrichtingen", internship.Courses);
                if (internship.Application != null)
                {
                    CreateBoolParagraph(document, "Aanvraag open", internship.Application.Value);
                }
                if (internship.Résumée != null)
                {
                    CreateBoolParagraph(document, "CV verplicht", internship.Résumée.Value);
                }
                if (internship.Reimbursement != null)
                {
                    CreateBoolParagraph(document, "Vergoeding", internship.Reimbursement.Value);

                }
                CreateStringParagraph(document, "Aantal Studenten", internship.StudentAmount.ToString());
                foreach (var promotor in internship.Promotors)
                {
                    CreatePersonParagraph(document, "Promotor", promotor.Person);
                }
                CreateAddressParagraph(document, internship.Address);
                
                
                // Extra Info
                CreateStringTitle(document, "Extra Info");
                if (internship.Remarks != null)
                {
                    CreateStringParagraph(document, "Opmerkingen", internship.Remarks);
                }
                if (internship.Conditions != null)
                {
                    CreateStringParagraph(document, "Condities", internship.Conditions);
                }

                document.Close();
                
            }
            workStream.Position = 0;
            
            // Return stream with PDF file
            return new FileStreamResult(workStream, "application/pdf");
        }
        
        private void SetPdfLayout(Document document)
        {
            document.SetMargins(36, 28, 14, 56);
            document.Add(
                new Image(ImageDataFactory.Create("https://www.pxl.be/img/logo.png"))
                    .Scale(10, 10)
                    .SetHorizontalAlignment(HorizontalAlignment.RIGHT));
            document.SetHorizontalAlignment(HorizontalAlignment.LEFT);
        }
        
        private void CreateStringTitle(Document document, string title)
        {
            document.Add(new Paragraph(title.ToUpper())
                .SetBold()
                .SetFontSize(16)
                .SetMarginBottom(10));
        }
        private void CreateStringParagraph(Document document, string title, string content)
        {
            document.Add(new Paragraph(title.ToUpper())
                            .SetBold()
                            .SetFontSize(10)
                            .SetMarginBottom(0));
            document.Add(new Paragraph(content)
                            .SetMarginTop(0)
                            .SetMarginBottom(10));
        }
        
        private void CreateBoolParagraph(Document document, string title, bool content)
        {
            document.Add(new Paragraph(title.ToUpper())
                .SetBold()
                .SetFontSize(10)
                .SetMarginBottom(0));
            document.Add(new Paragraph(content ? "Ja" : "Nee")
                .SetMarginTop(0)
                .SetMarginBottom(10));
        }

        private void CreateCourseList(Document document, string title, IEnumerable<InternshipCourse> courses)
        {
            var list = new List();
            
            foreach (var course in courses)
            {
                list.Add(course.Course.Name);
            }
            
            document.Add(new Paragraph(title.ToUpper())
                .SetBold()
                .SetFontSize(10)
                .SetMarginBottom(0));
            document.Add(list);
        }
        
        private void CreateTechnologyList(Document document, string title, IEnumerable<InternshipTechnology> technologies)
        {
            var list = new List();
            
            foreach (var technology in technologies)
            {
                list.Add(technology.Technology.Name);
            }
            
            document.Add(new Paragraph(title.ToUpper())
                .SetBold()
                .SetFontSize(10)
                .SetMarginBottom(0));
            document.Add(list);
        }

        private void CreatePeriodList(Document document, string title, IEnumerable<InternshipPeriod> periods)
        {
            var list = new List();

            foreach (var period in periods)
            {
                list.Add($"{period.Period.Name}: {period.Period.From} - {period.Period.To}");
            }
            
            document.Add(new Paragraph(title.ToUpper())
                .SetBold()
                .SetFontSize(10)
                .SetMarginBottom(0));
            document.Add(list);
        }
        
        private void CreateAddressParagraph(Document document, Address address)
        {
            document.Add(new Paragraph("Adres".ToUpper())
                .SetBold()
                .SetFontSize(10)
                .SetMarginBottom(0));
            
            document.Add(new Paragraph($"{address.Street} {address.Number}")
                .SetMarginTop(0)
                .SetMarginBottom(0));
            
            if (address.Box != null)
            {
                document.Add(new Paragraph($"{address.Box}")
                    .SetMarginTop(0)
                    .SetMarginBottom(0));
            }
            
            document.Add(new Paragraph($"{address.ZipCode} {address.City}")
                .SetMarginTop(0)
                .SetMarginBottom(0));
            
            document.Add(new Paragraph($"{address.Country.ToUpper()}")
                .SetMarginTop(0)
                .SetMarginBottom(10));
        }
        
        private void CreatePersonParagraph(Document document, string type, Person person)
        {
            document.Add(new Paragraph(type.ToUpper())
                .SetBold()
                .SetFontSize(10)
                .SetMarginBottom(0));
            
            document.Add(new Paragraph($"{person.Title} {person.Name} {person.FirstName}")
                .SetMarginTop(0)
                .SetMarginBottom(0));

            document.Add(new Paragraph($"Email: {person.Email}")
                .SetMarginTop(0)
                .SetMarginBottom(0));
            
            document.Add(new Paragraph($"Telefoonnr.: {person.TelephoneNumber}")
                .SetMarginTop(0)
                .SetMarginBottom(10));
        }
    }
}