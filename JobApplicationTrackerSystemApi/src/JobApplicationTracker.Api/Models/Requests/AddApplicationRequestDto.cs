using FluentValidation;
using JobApplicationTracker.Api.Models.Shared;

namespace JobApplicationTracker.Api.Models.Requests;

public class AddApplicationRequestDto
{
    public string CompanyName { get; set; }
    
    public string Position { get; set; }

    public ApplicationStatusDto Status { get; set; }

    public DateTime DateApplied { get; set; }
}

public class AddApplicationRequestDtoValidator : AbstractValidator<AddApplicationRequestDto>
{
    public AddApplicationRequestDtoValidator()
    {
        RuleFor(e => e.Position)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(e => e.CompanyName)
            .NotEmpty()
            .MaximumLength(30);
    }
}