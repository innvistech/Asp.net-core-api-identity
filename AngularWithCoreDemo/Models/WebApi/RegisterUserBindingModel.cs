using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithCoreDemo.Models.WebApi
{
    public class RegisterUserBindingModel
    {
      
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class RegisterUserValidator : AbstractValidator<RegisterUserBindingModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
