using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using FluentValidation;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Commands.UploadPicture;

internal class UploadPictureCommandValidator 
    : AbstractValidator<UploadPictureCommand>
{
    readonly IProfileRepository _repository;

    public UploadPictureCommandValidator(IProfileRepository repository)
    {
        _repository = repository;

        RuleFor(p => p.ThumbnailFileName)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MinimumLength(1)
            .MaximumLength(500)
            .WithMessage("{PropertyName} must be between 1 and 500 characters");

        RuleFor(p => p.FileName)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MinimumLength(1)
            .MaximumLength(500)
            .WithMessage("{PropertyName} must be between 1 and 500 characters");

        RuleFor(p => p.ProfileId)
            .MustAsync(ProfileMustExists)
            .WithMessage("Profile must exists");
    }

    private async Task<bool> ProfileMustExists(int id, CancellationToken token)
    {
        var profile = await _repository.GetByIdAsync(id);

        return profile != null;
    }
}
