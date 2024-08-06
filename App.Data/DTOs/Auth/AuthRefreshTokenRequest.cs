using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.DTOs.Auth
{
    public class AuthRefreshTokenRequest
    {
        public string Token { get; set; } = default!;
    }

    public class AuthRefreshTokenRequestValidator : AbstractValidator<AuthRefreshTokenRequest>
    {
        public AuthRefreshTokenRequestValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
