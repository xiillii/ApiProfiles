using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Features.Profile.Commands.CreateProfile;
using FluentValidation;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.UpdateProfile;

public class UpdateProfileCommandValidator
    : AbstractValidator<UpdateProfileCommand>
{
    private readonly IProfileRepository _repository;

    public UpdateProfileCommandValidator(IProfileRepository repository)
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

        RuleFor(p => p.Id)
            .MustAsync(ProfileMustExists)
            .WithMessage("Profile must exists");
    }

    private async Task<bool> ProfileMustExists(int id, CancellationToken token)
    {
        var profile = await _repository.GetByIdAsync(id);

        return profile != null;
    }

    private async Task<bool> ProfileNicknameUnique(UpdateProfileCommand command, CancellationToken token)
        => await _repository.IsNicknameUnique(command.Nickname, command.Id);
}
