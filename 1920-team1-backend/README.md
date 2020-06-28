# Webapplicatie ITproject
Dit project is een Web Api applicatie opgezet in .net Core.
De applicatie wordt gebruikt om stageaanvragen die door bedrijven ingevuld worden te behandelen en zo voor studenten beschikbaar te maken.
Gebruikte nugget packages:
 -dot net core
 -ef core
 -IText7
 -Sendgrid
 -MSsqlDriver
 
 # Klasse Diagram
Zie screenshot in repo.

# General Setup & Strategy
Zie screenshot in repo.

# Status verklaringen
2.5.1	Company States
New	- Het bedrijf heeft zich aangemeld (als nieuw bedrijf) en de coördinator heeft nog niet beslist of het bedrijf weerhouden wordt.
Active -	Het bedrijf voldoet aan de voorwaarden en is goedgekeurd door de coördinator.
Inactive	- Het bedrijf is afgewezen door de coördinator (kan zijn vlak na het aanmelden, of omdat het niet langer voldoet aan de eisen om 'Active' te zijn).


2.5.2	 Internship States   (De status van een stagevoorstel => deze kan alleen door de coördinator gewijzigd worden.)
New -	Het stagevoorstel is ingediend door het bedrijf, er is nog geen enkele actie op gebeurd door de PXL.
Pending -	De coördinator heeft het stagevoorstel bekeken en aan een (of meerdere) lectoren toegewezen voor review.
To be modified -	Een reviewer (lector) heeft een negatief advies gegeven en heeft vragen en/of opmerkingen geformuleerd over dit stagevoorstel. Het voorstel zal teruggestuurd worden naar het bedrijf voor bijsturing.
Modified -	Het stagevoorstel werd bijgestuurd in het bedrijf en wordt opnieuw voorgelegd voor review.
Approved -	Het stagevoorstel is aanvaard. Deze status zal ook beletten dat het voorstel nog gewijzigd kan worden.
Rejected -	Het stagevoorstel werd definitief afgewezen.


2.5.3	Validation states  (Status van een review door een lector => dient als advies voor de coördinator voor de Internship status.)
New -	De lector krijgt het stagevoorstel binnen om te reviewen.
Approved -	De lector geeft een gunstig advies voor de review (Is niet hetzelfde als de status van het stagevoorstel).
Rejected -	De lector geeft een ongunstig advies voor deze review en kan bijkomende vragen stellen of opmerkingen formuleren.
Questions Or Remarks -	De lector geeft nog geen advies, maar formuleert wel vragen en / of opmerkingen


2.5.4	Roles
Contact -	De contactpersoon bij een bedrijf dat stagevoorstellen indient.
Student -	De PXL studenten die in aanmerking komen om een stageplaats te kiezen.
Lector -	De PXL lectoren die stagevoorstellen zullen reviewen.
Coördinator -	De PXL stagecoördinator. Hij/Zij is het aanspreekpunt vanuit en naar de bedrijven toe. Heeft ook de 'Admin' rechten voor - deze applicatie.

# Applicatie
Deployed url Frontend:
http://itproject1.s3-website.eu-west-3.amazonaws.com/

Deployed url Backend:
http://webapp2-env.eba-srwzz35z.eu-central-1.elasticbeanstalk.com/

Test users

student1@pxl.be - student2@pxl.be - student3@pxl.be - student4@pxl.be

admin@pxl.be
 
lector1@pxl.be - lector2@pxl.be - lector3@pxl.be
 
contact@krustykrab.be - contact@chumbucket.be - contact@boatingschool.be

wachtwoord:paswoord

# SendGrid

De configuratie voor de SendGrid client kan men terugvinden onder de folder EmailService.

Huidig geconfigureerd emailadres:

Rol                     Emailadres
Coördinator/Admin       coördinator@gmail.com

