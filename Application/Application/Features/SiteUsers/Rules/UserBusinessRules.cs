﻿using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.SiteUsers.Rules
{
    public class UserBusinessRules : BaseBusinessRules
    {
        private readonly ISiteUserRepository siteUserRepository;
        private readonly IUserRepository userRepository;
        public UserBusinessRules(ISiteUserRepository siteUserRepository, IUserRepository userRepository)
        {
            this.siteUserRepository = siteUserRepository;
            this.userRepository = userRepository;
        }
        public async Task UserShouldBeExistsWhenSelected(User? user)
        {
            if (user == null)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "User", Errors = new List<string> { AuthBusinessMessage.UserNotFound } } });
            }
        }

        public async Task UserIdShouldBeExistsWhenSelected(int id)
        {
            bool doesExist = await siteUserRepository.AnyAsync(predicate: u => u.UserId == id);

            if (!doesExist)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "User", Errors = new List<string> { AuthBusinessMessage.UserNotFound } } });
            }
        }

        public async Task UserPasswordShouldBeMatched(User user, string password)
        {
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AuthorizationException(AuthBusinessMessage.InvlaidPassword);
        }
        public async Task UserPasswordShouldBeMatchBeforeUpdate(User user, string password)
        {
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new AuthorizationException("Invlaid password.");
        }

        public async Task UserEmailShouldNotExistsWhenInsert(string email)
        {
            bool doesExists = await userRepository.AnyAsync(predicate: u => u.Email == email);

            if (doesExists)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "User Email", Errors = new List<string> { AuthBusinessMessage.UserEmailAlreadyExists } } });
            }

        }

        public async Task UserEmailShouldNotExistsWhenUpdate(int id, string email)
        {
            bool doesExists = await userRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email);
            if (doesExists)
                throw new NotFoundException(AuthBusinessMessage.UserNotFound);
        }
    }
}
