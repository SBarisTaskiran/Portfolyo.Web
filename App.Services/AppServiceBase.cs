﻿using Ardalis.Result;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public abstract class AppServiceBase(IServiceProvider serviceProvider)
    {
        protected IConfiguration Configuration => serviceProvider.GetRequiredService<IConfiguration>();
        protected IServiceProvider ServiceProvider => serviceProvider;

        protected T? GetService<T>() => serviceProvider.GetService<T>();

        protected T GetRequiredService<T>() where T : notnull => serviceProvider.GetRequiredService<T>();

        protected virtual async Task<Result> ValidateModelAsync<T>(T model)
        {
            var validator = GetService<IValidator<T>>();
            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Result.Invalid(validationResult.Errors.Select(x => new ValidationError(x.ErrorMessage)));
                }
            }

            return Result.Success();
        }
    }
}
