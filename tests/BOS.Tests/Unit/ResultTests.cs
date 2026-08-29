using BOS.Core.Results;
using FluentAssertions;

namespace BOS.Tests.Unit;

public class ResultTests
{
    [Fact]
    public void Success_ShouldBeSuccessful()
    {
        var result = Result.Success();
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
    }

    [Fact]
    public void Failure_ShouldContainError()
    {
        var result = Result.Failure("Something went wrong");
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Something went wrong");
    }

    [Fact]
    public void GenericSuccess_ShouldContainValue()
    {
        var result = Result.Success(42);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void GenericFailure_ShouldContainError()
    {
        var result = Result.Failure<int>("Error");
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Error");
    }
}
