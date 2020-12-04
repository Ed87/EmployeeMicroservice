using EmployeeAPI.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Validators
{
    public class CreateEmployeeModelValidator : AbstractValidator<Employee>
    {
        public CreateEmployeeModelValidator()
        {
            RuleFor(x => x.Name)
               .NotNull()
               .WithMessage("The first name must be at least 2 character long");
            RuleFor(x => x.Name)
                .MinimumLength(2).
                WithMessage("The first name must be at least 2 character long");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("The last name must be at least 2 character long");
            RuleFor(x => x.LastName)
                .MinimumLength(2)
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(x => x.DateOfBirth)
                .InclusiveBetween(DateTime.Now.AddYears(-150).Date, DateTime.Now)
                .WithMessage("The birthday must not be longer ago than 150 years and can not be in the future");

            RuleFor(x => x.DateOfBirth)
                .NotNull()
               .WithMessage("The birthday cannot be null");



            RuleFor(x => x.EmploymentTypeId)
                 .NotNull()
                 .WithMessage("The Employment Type must be provided");
        }
    }
}
