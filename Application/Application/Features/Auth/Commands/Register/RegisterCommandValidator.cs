﻿using FluentValidation;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Password).MinimumLength(6).NotEmpty();
           
        }
    }
}
