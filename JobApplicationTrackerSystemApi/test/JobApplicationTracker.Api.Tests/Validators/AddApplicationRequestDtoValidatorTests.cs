using JobApplicationTracker.Api.Models.Requests;
using JobApplicationTracker.Api.Models.Shared;

namespace JobApplicationTracker.Api.Tests.Validators;

public class AddApplicationRequestDtoValidatorTests
{
    private readonly AddApplicationRequestDtoValidator _validator = new();

    [Fact]
    public async Task ValidateAsync_EmptyPosition_Fails()
    {
        var request = new AddApplicationRequestDto
        {
            Status = ApplicationStatusDto.Offer,
            Position = string.Empty,
            CompanyName = "PWC",
            DateApplied = DateTime.Today
        };

        var result = await _validator.ValidateAsync(request);
        Assert.NotNull(result);
        Assert.False(result.IsValid);

        var error = Assert.Single(result.Errors);
        Assert.Equal("'Position' must not be empty.", error.ErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_EmptyCompany_Fails()
    {
        var request = new AddApplicationRequestDto
        {
            Status = ApplicationStatusDto.Rejected,
            Position = "Tester",
            CompanyName = string.Empty,
            DateApplied = DateTime.Today
        };

        var result = await _validator.ValidateAsync(request);
        Assert.NotNull(result);
        Assert.False(result.IsValid);

        var error = Assert.Single(result.Errors);
        Assert.Equal("'Company Name' must not be empty.", error.ErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_LargePosition_Fails()
    {
        var request = new AddApplicationRequestDto
        {
            Status = ApplicationStatusDto.Interview,
            Position = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex",
            CompanyName = "PWC",
            DateApplied = DateTime.Today
        };

        var result = await _validator.ValidateAsync(request);
        Assert.NotNull(result);
        Assert.False(result.IsValid);

        var error = Assert.Single(result.Errors);
        Assert.Equal("The length of 'Position' must be 30 characters or fewer. You entered 209 characters.", error.ErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_LargeCompany_Fails()
    {
        var request = new AddApplicationRequestDto
        {
            Status = ApplicationStatusDto.Interview,
            Position = "Tester",
            CompanyName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex",
            DateApplied = DateTime.Today
        };

        var result = await _validator.ValidateAsync(request);
        Assert.NotNull(result);
        Assert.False(result.IsValid);

        var error = Assert.Single(result.Errors);
        Assert.Equal("The length of 'Company Name' must be 30 characters or fewer. You entered 209 characters.", error.ErrorMessage);
    }

    [Fact]
    public async Task ValidateAsync_ValidModel_Passes()
    {
        var request = new AddApplicationRequestDto
        {
            Status = ApplicationStatusDto.Interview,
            Position = "Tester",
            CompanyName = "PWC",
            DateApplied = DateTime.Today
        };

        var result = await _validator.ValidateAsync(request);
        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }
}