﻿using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Update
{
    public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken = default)
        {
            var user = new UserRequestDto()
            {
                Id = request.Id,
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role,
                Status = request.Status,
            };
            var newUser = user.ToModel();

            if(!string.IsNullOrEmpty(request.Password))
            {
                byte[] passwordHash, passwordSalt;
                _passwordService.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;
            }

            _userRepository.Update(newUser, cancellationToken);

            return Result.Success();
        }
    }
}
