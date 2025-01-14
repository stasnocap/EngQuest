using FluentAssertions;
using NSubstitute;
using EngQuest.Application.Vocabulary.Adjectives.CreateAdjective;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Adjectives;
using EngQuest.Application.Abstractions.Repositories;

namespace EngQuest.Application.UnitTests.Vocabulary.Adjectives;

public class CreateAdjectiveTests
{
    private readonly CreateAdjectiveCommand _command = new("big");

    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IAdjectiveRepository _adjectiveRepositoryMock;

    private readonly CreateAdjectiveCommandHandler _handler;

    public CreateAdjectiveTests()
    {
        _adjectiveRepositoryMock = Substitute.For<IAdjectiveRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new CreateAdjectiveCommandHandler( _adjectiveRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_IfAdjectiveAlreadyExists()
    {
        // Act
        _adjectiveRepositoryMock
            .ExistsAsync(Arg.Is<Text>(x => x.Value == _command.Text), Arg.Any<CancellationToken>())
            .Returns(true);

        // Arrange
        Result<int> result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(AdjectiveErrors.AlreadyExists);
    }

    [Fact]
    public async Task Handle_Should_CreateAdjective_IfNotExists()
    {
        // Act
        _adjectiveRepositoryMock
            .ExistsAsync(Arg.Is<Text>(x => x.Value == _command.Text), Arg.Any<CancellationToken>())
            .Returns(false);

        // Arrange
        Result<int> result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _adjectiveRepositoryMock.Received(1).Add(Arg.Is<Adjective>(x => x.Text.Value == _command.Text));
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
