using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Enums;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class Mockupcontroller : ControllerBase
    {
        public ICourseManager CourseManager { get; }
        public IPersonManager<Person> PersonManager { get; }
        public ICompanyManager CompanyManager { get; }
        public ITechnologyManager TechnologyManager { get; }
        public IPeriodManager PeriodManager { get; }
        public IInternshipManager InternshipManager { get; }

        public Mockupcontroller(
            ICourseManager courseManager,
            IPersonManager<Person> personManager,
            ICompanyManager companyManager,
            ITechnologyManager technologyManager,
            IPeriodManager periodManager,
            IInternshipManager internshipManager)
        {
            CourseManager = courseManager;
            PersonManager = personManager;
            CompanyManager = companyManager;
            TechnologyManager = technologyManager;
            PeriodManager = periodManager;
            InternshipManager = internshipManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> CreateUserMock(CancellationToken cancellationToken)
        {
            string password = "paswoord";
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                // COURSES
                // add Course
                Course course_TI_AO = new Course{ Name = "TI - Applicatie-ontwikkeling"};
                await CourseManager.AddAsync(course_TI_AO, cancellationToken);
                Course course_TI_SN = new Course{ Name = "TI - Systemen en netwerkbeheer"};
                await CourseManager.AddAsync(course_TI_SN, cancellationToken);
                Course course_TI_SM = new Course {Name = "TI - Software-management"};
                await CourseManager.AddAsync(course_TI_SM, cancellationToken);
                Course course_EICT = new Course {Name = "Elektronica-ICT"};
                await CourseManager.AddAsync(course_EICT, cancellationToken);
                 
                 
                // TECHNOLOGIES
                // add technology
                Technology tech_JAVA = new Technology{ Name = "JAVA"};
                await TechnologyManager.AddAsync(tech_JAVA, cancellationToken);
                Technology tech_NET = new Technology{ Name = ".NET"};
                await TechnologyManager.AddAsync(tech_NET, cancellationToken);
                Technology tech_HTML = new Technology{ Name = "HTML"};
                await TechnologyManager.AddAsync(tech_HTML, cancellationToken);
                Technology tech_CSS = new Technology{ Name = "CSS"};
                await TechnologyManager.AddAsync(tech_CSS, cancellationToken);
                Technology tech_JS = new Technology{ Name = "JavaScript"};
                await TechnologyManager.AddAsync(tech_JS, cancellationToken);
                Technology tech_Angular = new Technology{ Name = "Angular"};
                await TechnologyManager.AddAsync(tech_Angular, cancellationToken);
                Technology tech_React = new Technology{ Name = "React"};
                await TechnologyManager.AddAsync(tech_React, cancellationToken);
                Technology tech_CPlusPlus = new Technology{ Name = "C++"};
                await TechnologyManager.AddAsync(tech_CPlusPlus, cancellationToken);
                Technology tech_CSharp = new Technology{ Name = "C#"};
                await TechnologyManager.AddAsync(tech_CSharp, cancellationToken);
                 
                 
                // PERIODS
                // add Periods
                Period periodSpring = 
                    new Period(
                        "semester 1",
                        new DateTime(2020,1, 1),
                        new DateTime(2020,7, 1));
                await PeriodManager.AddAsync(periodSpring, cancellationToken);
                Period periodAutumn = 
                    new Period(
                        "semester 2",
                        new DateTime(2020,9, 1),
                        new DateTime(2020,12, 1));
                await PeriodManager.AddAsync(periodAutumn, cancellationToken);    
                 
                 
                // USERS
                // add students
                Student student_spongebob = new Student
                {
                    Name = "SquarePants",
                    FirstName = "SpongeBob",
                    Title = "Student",
                    Address =
                        new Address
                        {
                            Street = "Mainstreet",
                            Number = "5",
                            ZipCode = "2090",
                            City = "BikiniBottom",
                            Country = "Ocean"
                        },
                    Course = course_TI_AO,
                    Email = "student1@pxl.be",
                    Role = RoleEnum.STUDENT,
                    TelephoneNumber = "012/34.56.78",
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(student_spongebob, cancellationToken);
                
                Student student_patrick = new Student
                {
                    Name = "Star",
                    FirstName = "Patrick",
                    Title = "Student",
                    Address =
                        new Address
                        {
                            Street = "Mainstreet",
                            Number = "3",
                            ZipCode = "2090",
                            City = "BikiniBottom",
                            Country = "Ocean"
                        },
                    Course = course_TI_AO,
                    Email = "student2@pxl.be",
                    Role = RoleEnum.STUDENT,
                    TelephoneNumber = "012/34.56.79",
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(student_patrick, cancellationToken);
                
                Student student_squidward = new Student
                {
                    Name = "Tentacles",
                    FirstName = "Squidward",
                    Title = "Student",
                    Address =
                        new Address
                        {
                            Street = "Mainstreet",
                            Number = "4",
                            ZipCode = "2090",
                            City = "BikiniBottom",
                            Country = "Ocean"
                        },
                    Course = course_TI_SM,
                    Email = "student3@pxl.be",
                    Role = RoleEnum.STUDENT,
                    TelephoneNumber = "012/34.56.80",
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(student_squidward, cancellationToken);
                
                Student student_sandy = new Student
                {
                    Name = "Cheeks",
                    FirstName = "Sandy",
                    Title = "Student",
                    Address =
                        new Address
                        {
                            Street = "Glassdome",
                            Number = "1",
                            ZipCode = "2090",
                            City = "BikiniBottom",
                            Country = "Ocean"
                        },
                    Course = course_EICT,
                    Email = "student4@pxl.be",
                    Role = RoleEnum.STUDENT,
                    TelephoneNumber = "012/34.56.90",
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(student_sandy, cancellationToken);
                
                // add coördinator
                Coördinator coördinator_gary = new Coördinator
                {
                    Email = "admin@pxl.be",
                    Name = "the Snail",
                    FirstName = "Gary",
                    Title = "Coördinator",
                    Role = RoleEnum.COÖRDINATOR,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(coördinator_gary, cancellationToken);
                
                // add lectors
                Lector lector_patchy = new Lector
                {
                    Email = "lector1@pxl.be",
                    Name = "the Pirate",
                    FirstName = "Patchy",
                    Title = "Lector",
                    Role = RoleEnum.LECTOR,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(lector_patchy, cancellationToken);
                
                Lector lector_potty = new Lector
                {
                    Email = "lector2@pxl.be",
                    Name = "the Parrot",
                    FirstName = "Potty",
                    Title = "Lector",
                    Role = RoleEnum.LECTOR,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(lector_potty, cancellationToken);
                
                Lector lector_narrator = new Lector
                {
                    Email = "lector3@pxl.be",
                    Name = "Narrator",
                    FirstName = "French",
                    Title = "Lector",
                    Role = RoleEnum.LECTOR,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(lector_narrator, cancellationToken);
                
                // add contact persons
                Person contact_eugene = new Person
                {
                    Email = "contact@krustykrab.be",
                    Name = "Krabs",
                    FirstName = "Eugene",
                    Title = "Contactperson",
                    Role = RoleEnum.CONTACT,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(contact_eugene, cancellationToken);
                
                Person contact_plankton = new Person
                {
                    Email = "contact@chumbucket.be",
                    Name = "Plankton",
                    FirstName = "Plankton",
                    Title = "Contactperson",
                    Role = RoleEnum.CONTACT,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(contact_plankton, cancellationToken);
                
                Person contact_puff = new Person
                {
                    Email = "contact@boatingschool.be",
                    Name = "Puff",
                    FirstName = "Mrs.",
                    Title = "Contactperson",
                    Role = RoleEnum.CONTACT,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(contact_puff, cancellationToken);

                Person contact_davy_jones = new Person
                {
                    Email = "contact@flyingdutchman.be",
                    Name = "Jones",
                    FirstName = "Davy",
                    Title = "Contactperson",
                    Role = RoleEnum.CONTACT,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };
                await PersonManager.AddAsync(contact_davy_jones, cancellationToken);


                // COMPANIES
                // add companies
                Company company_krustykrab =
                    new Company
                    {
                        Name = "Krusty Krab",
                        AmountOfEmployees = 50,
                        AmountOfEmployeesIt = 5,
                        Address =
                            new Address
                            {
                                Street = "Mainstreet",
                                Number = "1",
                                ZipCode = "2090",
                                City = "BikiniBottom",
                                Country = "Ocean"
                            },
                        ContactPerson = contact_eugene,
                        State = CompanyStateEnum.ACTIVE
                    };
                await CompanyManager.AddAsync(company_krustykrab, cancellationToken);
                
                Company company_chumbucket =
                    new Company
                    {
                        Name = "Chum Bucket",
                        AmountOfEmployees = 20,
                        AmountOfEmployeesIt = 3,
                        Address =
                            new Address
                            {
                                Street = "Mainstreet",
                                Number = "2",
                                ZipCode = "2090",
                                City = "BikiniBottom",
                                Country = "Ocean"
                            },
                        ContactPerson = contact_plankton,
                        State = CompanyStateEnum.ACTIVE
                    };
                await CompanyManager.AddAsync(company_chumbucket, cancellationToken);
                
                Company company_boatingschool =
                    new Company
                    {
                        Name = "BikiniBottom Boating School",
                        AmountOfEmployees = 10,
                        AmountOfEmployeesIt = 6,
                        Address =
                            new Address
                            {
                                Street = "Lighthouse",
                                Number = "6",
                                ZipCode = "2090",
                                City = "BikiniBottom",
                                Country = "Ocean"
                            },
                        ContactPerson = contact_puff,
                        State = CompanyStateEnum.ACTIVE
                    };
                await CompanyManager.AddAsync(company_boatingschool, cancellationToken);

                Company company_flyingdutchman =
                    new Company
                    {
                        Name = "Flying Dutchman Diner",
                        AmountOfEmployees = 15,
                        AmountOfEmployeesIt = 1,
                        Address =
                            new Address
                            {
                                Street = "Fishermanslane",
                                Number = "26",
                                ZipCode = "2090",
                                City = "BikiniBottom",
                                Country = "Ocean"
                            },
                        ContactPerson = contact_davy_jones,
                        State = CompanyStateEnum.INACTIVE
                    };
                await CompanyManager.AddAsync(company_flyingdutchman, cancellationToken);

                // INTERNSHIPS
                // create internships
                // Krusty Krab
                Internship internship_ao_kk_spring = new Internship
                {
                    Company = company_krustykrab,
                    Title =  "Application Development",
                    Description = "Say cheese cheesy grin blue castello. Brie feta cut the cheese cheesy grin cheesecake cheesy grin jarlsberg cheese and wine. Hard cheese cheeseburger bocconcini fondue pepper jack st. agur blue cheese jarlsberg cheese triangles. Macaroni cheese melted cheese emmental ricotta cheese and biscuits.", 
                    ContactPerson = contact_eugene,
                    Address = company_krustykrab.Address,
                    TechDescription = "Danish fontina rubber cheese bavarian bergkase",
                    ResearchTopic = "Port-salut cheese strings cauliflower cheese stilton pepper jack cut the cheese fromage frais caerphilly. ",
                    Application = true,
                    Résumée = true,
                    Reimbursement = true,
                    StudentAmount = 1,
                    NrOfSupportEmployees = 2,
                    Remarks = "",
                    State = InternshipStateEnum.APPROVED
                };
                internship_ao_kk_spring.Courses.Add(new InternshipCourse(internship_ao_kk_spring, course_TI_AO));
                internship_ao_kk_spring.Technologies.Add(new InternshipTechnology(internship_ao_kk_spring, tech_CSharp));
                internship_ao_kk_spring.Technologies.Add(new InternshipTechnology(internship_ao_kk_spring, tech_NET));
                internship_ao_kk_spring.Promotors.Add(new InternshipPerson(internship_ao_kk_spring, contact_eugene));
                internship_ao_kk_spring.Periods.Add(new InternshipPeriod(internship_ao_kk_spring, periodSpring));
                
                Internship internship_ao_kk_autumn = new Internship
                {
                    Company = company_krustykrab,
                    Title =  "Application Development",
                    Description = "Say cheese cheesy grin blue castello. Brie feta cut the cheese cheesy grin cheesecake cheesy grin jarlsberg cheese and wine. Hard cheese cheeseburger bocconcini fondue pepper jack st. agur blue cheese jarlsberg cheese triangles. Macaroni cheese melted cheese emmental ricotta cheese and biscuits.", 
                    ContactPerson = contact_eugene,
                    Address = company_krustykrab.Address,
                    TechDescription = "Danish fontina rubber cheese bavarian bergkase",
                    ResearchTopic = "Port-salut cheese strings cauliflower cheese stilton pepper jack cut the cheese fromage frais caerphilly. ",
                    Application = true,
                    Résumée = true,
                    Reimbursement = true,
                    StudentAmount = 1,
                    NrOfSupportEmployees = 2,
                    Remarks = "",
                    State = InternshipStateEnum.APPROVED
                };
                internship_ao_kk_autumn.Courses.Add(new InternshipCourse(internship_ao_kk_autumn, course_TI_AO));
                internship_ao_kk_autumn.Technologies.Add(new InternshipTechnology(internship_ao_kk_autumn, tech_CSharp));
                internship_ao_kk_autumn.Technologies.Add(new InternshipTechnology(internship_ao_kk_autumn, tech_NET));
                internship_ao_kk_autumn.Promotors.Add(new InternshipPerson(internship_ao_kk_autumn, contact_eugene));
                internship_ao_kk_autumn.Periods.Add(new InternshipPeriod(internship_ao_kk_autumn, periodAutumn));
                
                Internship internship_sm_kk_spring = new Internship
                {
                    Company = company_krustykrab,
                    Title =  "Software Management",
                    Description = "Cheese and wine mascarpone cheese and biscuits. Caerphilly cheddar parmesan feta smelly cheese the big cheese st. agur blue cheese squirty cheese. Melted cheese halloumi airedale cheese and wine feta when the cheese comes out everybody's happy cheese and wine croque monsieur. Port-salut queso brie ricotta pepper jack cheesy grin fromage cottage cheese. Roquefort paneer boursin pepper jack.", 
                    ContactPerson = contact_eugene,
                    Address = company_krustykrab.Address,
                    TechDescription = "The big cheese blue castello cut the cheese caerphilly roquefort.",
                    ResearchTopic = "Hard cheese danish fontina say cheese caerphilly fromage frais cheeseburger.",
                    Application = true,
                    Résumée = true,
                    Reimbursement = true,
                    StudentAmount = 1,
                    NrOfSupportEmployees = 2,
                    Remarks = "",
                    State = InternshipStateEnum.PENDING
                };
                internship_sm_kk_spring.Courses.Add(new InternshipCourse(internship_sm_kk_spring, course_TI_SM));
                internship_sm_kk_spring.Technologies.Add(new InternshipTechnology(internship_sm_kk_spring, tech_NET));
                internship_sm_kk_spring.Promotors.Add(new InternshipPerson(internship_sm_kk_spring, contact_eugene));
                internship_sm_kk_spring.Periods.Add(new InternshipPeriod(internship_sm_kk_spring, periodSpring));

                // Chum Bucket
                Internship internship_ao_cb_spring = new Internship
                {
                    Company = company_chumbucket,
                    Title =  "Application Development",
                    Description = "Edam cheeseburger babybel. Stilton cheddar stinking bishop queso fromage camembert de normandie jarlsberg lancashire. Brie cheese and wine manchego pecorino melted cheese cheese and wine squirty cheese edam. Rubber cheese edam melted cheese halloumi.", 
                    ContactPerson = contact_plankton,
                    Address = company_chumbucket.Address,
                    TechDescription = "Taleggio cut the cheese the big cheese.",
                    ResearchTopic = "Fondue smelly cheese who moved my cheese stinking bishop cauliflower cheese cheddar boursin cow.",
                    Application = true,
                    Résumée = true,
                    Reimbursement = true,
                    StudentAmount = 1,
                    NrOfSupportEmployees = 2,
                    Remarks = "",
                    State = InternshipStateEnum.NEW
                };
                internship_ao_cb_spring.Courses.Add(new InternshipCourse(internship_ao_cb_spring, course_TI_AO));
                internship_ao_cb_spring.Technologies.Add(new InternshipTechnology(internship_ao_cb_spring, tech_JAVA));
                internship_ao_cb_spring.Technologies.Add(new InternshipTechnology(internship_ao_cb_spring, tech_HTML));
                internship_ao_cb_spring.Technologies.Add(new InternshipTechnology(internship_ao_cb_spring, tech_CSS));
                internship_ao_cb_spring.Promotors.Add(new InternshipPerson(internship_ao_cb_spring, contact_plankton));
                internship_ao_cb_spring.Periods.Add(new InternshipPeriod(internship_ao_cb_spring, periodSpring));
                
                Internship internship_ao_cb_autumn = new Internship
                {
                    Company = company_chumbucket,
                    Title =  "Application Development",
                    Description = "Edam cheeseburger babybel. Stilton cheddar stinking bishop queso fromage camembert de normandie jarlsberg lancashire. Brie cheese and wine manchego pecorino melted cheese cheese and wine squirty cheese edam. Rubber cheese edam melted cheese halloumi.", 
                    ContactPerson = contact_plankton,
                    Address = company_chumbucket.Address,
                    TechDescription = "Taleggio cut the cheese the big cheese.",
                    ResearchTopic = "Fondue smelly cheese who moved my cheese stinking bishop cauliflower cheese cheddar boursin cow.",
                    Application = true,
                    Résumée = true,
                    Reimbursement = true,
                    StudentAmount = 1,
                    NrOfSupportEmployees = 2,
                    Remarks = "",
                    State = InternshipStateEnum.MODIFIED
                };
                internship_ao_cb_autumn.Courses.Add(new InternshipCourse(internship_ao_cb_autumn, course_TI_AO));
                internship_ao_cb_autumn.Technologies.Add(new InternshipTechnology(internship_ao_cb_autumn, tech_JAVA));
                internship_ao_cb_autumn.Technologies.Add(new InternshipTechnology(internship_ao_cb_autumn, tech_HTML));
                internship_ao_cb_autumn.Technologies.Add(new InternshipTechnology(internship_ao_cb_autumn, tech_CSS));
                internship_ao_cb_autumn.Promotors.Add(new InternshipPerson(internship_ao_cb_autumn, contact_plankton));
                internship_ao_cb_autumn.Periods.Add(new InternshipPeriod(internship_ao_cb_autumn, periodAutumn));
                
                // Boating School
                Internship internship_eict_bs_spring = new Internship
                {
                    Company = company_boatingschool,
                    Title =  "ICT Support",
                    Description = "Croque monsieur stinking bishop hard cheese. Goat lancashire smelly cheese edam monterey jack when the cheese comes out everybody's happy airedale lancashire. Who moved my cheese cheesy grin blue castello cow feta ricotta chalk and cheese dolcelatte. Everyone loves fromage frais swiss bavarian bergkase cheese strings swiss feta goat. Stinking bishop goat squirty cheese.", 
                    ContactPerson = contact_puff,
                    Address = company_boatingschool.Address,
                    TechDescription = "Mascarpone edam cheese and wine.",
                    ResearchTopic = "Ricotta fondue manchego ricotta cottage cheese macaroni cheese ricotta fondue. ",
                    Application = true,
                    Résumée = true,
                    Reimbursement = true,
                    StudentAmount = 1,
                    NrOfSupportEmployees = 2,
                    Remarks = "",
                    State = InternshipStateEnum.TO_BE_MODIFIED
                };
                internship_eict_bs_spring.Courses.Add(new InternshipCourse(internship_eict_bs_spring, course_EICT));
                internship_eict_bs_spring.Technologies.Add(new InternshipTechnology(internship_eict_bs_spring, tech_JAVA));
                internship_eict_bs_spring.Technologies.Add(new InternshipTechnology(internship_eict_bs_spring, tech_HTML));
                internship_eict_bs_spring.Technologies.Add(new InternshipTechnology(internship_eict_bs_spring, tech_CSS));
                internship_eict_bs_spring.Promotors.Add(new InternshipPerson(internship_eict_bs_spring, contact_puff));
                internship_eict_bs_spring.Periods.Add(new InternshipPeriod(internship_eict_bs_spring, periodSpring));
                
                // create validations
                Validation validation_kk_spring_approved = new Validation
                {
                    Internship = internship_ao_kk_spring,
                    Lector = lector_patchy,
                    State = ValidationStateEnum.APPROVED,
                    Date = DateTime.Now,
                    FeedBack =  new FeedBack
                    {
                        CreatedAt = DateTime.Now,
                        Value = " jsdhffgrfgmkrkgkgkerg"
                    }
                };
                Validation validation_kk_spring_rejected = new Validation
                {
                    Internship = internship_ao_kk_spring,
                    Lector = lector_patchy,
                    State = ValidationStateEnum.REJECTED,
                    Date = DateTime.Now
                };
                internship_ao_kk_spring.Validations.Add(validation_kk_spring_approved);
                internship_ao_kk_spring.Validations.Add(validation_kk_spring_rejected);
                
                Validation validation_kk_autumn_approved_1 = new Validation
                {
                    Internship = internship_ao_kk_autumn,
                    Lector = lector_patchy,
                    State = ValidationStateEnum.APPROVED,
                    Date = DateTime.Now
                };
                Validation validation_kk_autumn_approved_2 = new Validation
                {
                    Internship = internship_ao_kk_autumn,
                    Lector = lector_potty,
                    State = ValidationStateEnum.APPROVED,
                    Date = DateTime.Now
                };
                internship_ao_kk_autumn.Validations.Add(validation_kk_autumn_approved_1);
                internship_ao_kk_autumn.Validations.Add(validation_kk_autumn_approved_2);

                // add internships
                await InternshipManager.AddAsync(internship_ao_kk_spring, cancellationToken);
                await InternshipManager.AddAsync(internship_ao_kk_autumn, cancellationToken);
                await InternshipManager.AddAsync(internship_sm_kk_spring, cancellationToken);
                await InternshipManager.AddAsync(internship_ao_cb_spring, cancellationToken);
                await InternshipManager.AddAsync(internship_ao_cb_autumn, cancellationToken);
                await InternshipManager.AddAsync(internship_eict_bs_spring, cancellationToken);
            }
            return Ok("Mockup created");
        }
    }
}