using FluentAssertions;
using NSubstitute;
using EngQuest.Application.Vocabulary.Adjectives.DeleteAdjective;
using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Shared;
using EngQuest.Domain.Vocabulary.Adjectives;
using EngQuest.Application.Abstractions.Repositories;

namespace EngQuest.Application.UnitTests.Vocabulary.Adjectives;

public class DeleteAdjectiveTests
{
    private readonly DeleteAdjectiveCommand _command = new(1);

    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IAdjectiveRepository _adjectiveRepositoryMock;

    private readonly DeleteAdjectiveCommandHandler _handler;

    public DeleteAdjectiveTests()
    {
        _adjectiveRepositoryMock = Substitute.For<IAdjectiveRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new DeleteAdjectiveCommandHandler(_adjectiveRepositoryMock, _unitOfWorkMock);
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
    public async Task Handle_Should_DeleteAdjective()
    {
        // Act
        var adjective = new Adjective(new Text("bad"))
        {
            Id = _command.Id,
        };
        
        _adjectiveRepositoryMock
            .GetByIdAsync(_command.Id, Arg.Any<CancellationToken>())
            .Returns(adjective);

        // Arrange
        Result result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _adjectiveRepositoryMock.Received(1).Delete(adjective);
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
