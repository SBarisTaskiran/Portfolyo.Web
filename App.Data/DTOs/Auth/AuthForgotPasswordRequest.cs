using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.DTOs.Auth
{
    public class AuthForgotPasswordRequest
    {
        public string Email { get; set; } = null!;
    }

    public class AuthForgotPasswordRequestValidator
        : AbstractValidator<AuthForgotPasswordRequest>
    {
        public AuthForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
