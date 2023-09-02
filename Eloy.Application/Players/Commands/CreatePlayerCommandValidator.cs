﻿using Eloy.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Eloy.Application.Players.Commands;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    private readonly IContext _context;

    public CreatePlayerCommandValidator(IContext context)
    {
        _context = context;

        RuleFor(a => a.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(CreatePlayerCommand.Name)} should be provided");

        RuleFor(a => a.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage($"{nameof(CreatePlayerCommand.Email)} should be provided")
            .EmailAddress()
            .WithMessage($"{nameof(CreatePlayerCommand.Email)} should be a valid email address")
            .MustAsync(BeUniqueInDatabase)
            .WithMessage($"{nameof(CreatePlayerCommand.Email)} should be unique");

        RuleFor(a => a.Password)
            .Equal(b => b.PasswrodConfirmation)
            .WithMessage(
                $"{nameof(CreatePlayerCommand.Password)} and {nameof(CreatePlayerCommand.PasswrodConfirmation)} should be equal")
            .MustAsync(MeetPasswordStrength)
            .WithMessage(
                $"{nameof(CreatePlayerCommand.Password)} should be at least 5 chars consist of alphabet, Number and signs");
    }

    private async Task<bool> BeUniqueInDatabase(CreatePlayerCommand request, string property,
        CancellationToken cancellationToken)
    {
        var count = await _context.Players.CountAsync(a => a.Email == request.Email,
            cancellationToken);

        return count == 0;
    }

    private async Task<bool> MeetPasswordStrength(CreatePlayerCommand request, string property,
        CancellationToken cancellationToken)
    {
        return request.Password.Length > 5 &&
               request.Password.Any(c => char.IsLetter(c)) &&
               request.Password.Any(c => char.IsDigit(c));
    }
}