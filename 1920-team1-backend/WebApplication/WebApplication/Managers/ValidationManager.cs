using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Enums;
using WebApplication.Managers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Managers
{
    /// <inheritdoc cref="IValidationManager"/>
    public class ValidationManager
        : ModelManager<Validation>,
            IValidationManager
    {
        public ValidationManager(IValidationRepository repository)
            : base(repository)
        {
        }

        public async Task CreateValidationsByInternshipAndLectorsAsync(Internship internship, IEnumerable<Lector> lectors, CancellationToken cancellationToken)
        {
            foreach (Lector lector in lectors)
            {
                await AddAsync(new Validation { Date = DateTime.Now, Internship = internship, Lector = lector, State = ValidationStateEnum.NEW }, cancellationToken);
            }
        }
    }
}