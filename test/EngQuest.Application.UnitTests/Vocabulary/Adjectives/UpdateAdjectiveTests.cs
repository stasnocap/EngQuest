using FluentAssertions;
using NSubstitute;
using EngQuest.Application.Vocabulary.Adjectives.UpdateAdjective;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Adjectives;
using EngQuest.Application.Abstractions.Repositories;

namespace EngQuest.Application.UnitTests.Vocabulary.Adjectives;

public class UpdateAdjectiveTests
{
    private readonly UpdateAdjectiveCommand _command = new(1, "big");

    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IAdjectiveRepository _adjectiveRepositoryMock;

    private readonly UpdateAdjectiveCommandHandler _handler;

    public UpdateAdjectiveTests()
    {
        _adjectiveRepositoryMock = Substitute.For<IAdjectiveRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new UpdateAdjectiveCommandHandler(_adjectiveRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_IfAdjectiveIsNotFound()
    {
        // Act
        _adjectiveRepositoryMock
            .GetByIdAsync(_command.Id, Arg.Any<CancellationToken>())
            .Returns(null as Adjective);

        // Arrange
        Result result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(AdjectiveErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_IfAdjectiveAlreadyExists()
    {
        // Act
        var adjective = new Adjective(new Text("bad"))
        {
            Id = _command.Id,
        };

        _adjectiveRepositoryMock
            .GetByIdAsync(_command.Id, Arg.Any<CancellationToken>())
            .Returns(adjective);
        
        _adjectiveRepositoryMock
            .ExistsAsync(Arg.Is<Text>(x => x.Value == _command.Text), Arg.Any<CancellationToken>())
            .Returns(true);

        // Arrange
        Result result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(AdjectiveErrors.AlreadyExists);
    }

    [Fact]
    public async Task Handle_Should_UpdateAdjectiveText()
    {
        // Act
        var adjective = new Adjective(new Text("bad"))
        {
            Id = _command.Id,
        };
        
        _adjectiveRepositoryMock
            .GetByIdAsync(_command.Id, Arg.Any<CancellationToken>())
            .Returns(adjective);
        
        _adjectiveRepositoryMock
            .ExistsAsync(Arg.Is<Text>(x => x.Value == _command.Text), Arg.Any<CancellationToken>())
            .Returns(false);

        // Arrange
        Result result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        adjective.Text.Value.Should().Be(_command.Text);
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

}
