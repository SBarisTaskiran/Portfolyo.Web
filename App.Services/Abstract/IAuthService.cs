using App.Data.DTOs.Auth;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Abstract
{
    public interface IAuthService
    {
        Task<Result<AuthLoginResult>> LoginAsync(AuthLoginRequest loginRequest);
        Task<Result<AuthRefreshTokenResult>> RefreshTokenAsync(AuthRefreshTokenRequest refreshTokenRequest);
        Task<Result> RegisterAsync(AuthRegisterRequest registerRequest);
        Task<Result> ForgotPasswordAsync(AuthForgotPasswordRequest forgotPasswordRequest);
        Task<Result> ResetPasswordAsync(AuthResetPasswordRequest resetPasswordRequest);
    }
}
