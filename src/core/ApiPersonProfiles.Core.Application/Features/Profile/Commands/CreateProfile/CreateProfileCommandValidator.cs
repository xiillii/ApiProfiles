using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.CreateProfile;

public class CreateProfileCommandValidator
    : AbstractValidator<CreateProfileCommand>
{
    private readonly IProfileRepository _repository;

    public CreateProfileCommandValidator(IProfileRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.FirstName)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");
        RuleFor(p => p.Age)
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} is required and greater than Zero")
            .LessThan(110).WithMessage("{PropertyName} is required and less than 110");
        RuleFor(p => p.Nickname)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(20).WithMessage("{PropertyName} must be fewer than 70 characters");
        RuleFor(p => p)
            .MustAsync(ProfileNicknameUnique)
            .WithMessage("Profile Nickname already exists");
    }

    private async Task<bool> ProfileNicknameUnique(CreateProfileCommand command, CancellationToken token) 
        => await _repository.IsNicknameUnique(command.Nickname);
}
