using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithCoreDemo.Models.WebApi
{
    public class CredentialsBindingModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class CredentialsBindingValidator : AbstractValidator<CredentialsBindingModel>
    {
        public CredentialsBindingValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
           }
    }
}
