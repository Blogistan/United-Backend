using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.PasswordReset
{
    public class PasswordResetCommandValidator : AbstractValidator<PasswordResetCommand>
    {
        public PasswordResetCommandValidator()
        {
            RuleFor(x => x.NewPassword).Equal(x => x.PasswordConfirm);
        }
    }
}
