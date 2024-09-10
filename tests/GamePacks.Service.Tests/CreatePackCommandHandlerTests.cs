using Moq;
using GamePacks.DataAccess;
using GamePacks.DataAccess.Models;
using GamePacks.Service.Models;
using GamePacks.Service.UseCases;

public class CreatePackCommandHandlerUnitTests
{
    private readonly Mock<IPackRepository> _mockPackRepository;
    private readonly CreatePackCommandHandler _commandHandler;

    public CreatePackCommandHandlerUnitTests()
    {
        _mockPackRepository = new Mock<IPackRepository>();
        _commandHandler = new CreatePackCommandHandler(_mockPackRepository.Object);
    }

    [Fact]
    public async Task CreatePack_ShouldReturnPack_WhenValidRequestIsProvided()
    {
        // Arrange
        var createPackRequest = new CreatePackRequest
        {
            PackName = "Test Pack",
            Active = true,
            Price = 100,
            Content = new List<string> { "Item 1", "Item 2" },
            ChildPackIds = new List<Guid>()
        };

        var newPack = new Pack
        {
            Id = Guid.NewGuid(),
            Name = createPackRequest.PackName,
            IsActive = createPackRequest.Active,
            Price = createPackRequest.Price
        };

        _mockPackRepository
            .Setup(repo => repo.AddPackAsync(It.IsAny<Pack>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(newPack);

        // Act
        var result = await _commandHandler.ExecuteAsync(createPackRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsT0, "Expected the result to be a Pack but got a PackError.");
        var createdPack = result.AsT0;
        Assert.Equal(createPackRequest.PackName, createdPack.Name);
        Assert.Equal(2, createdPack.PackItems.Count);
    }

    [Fact]
    public async Task CreatePack_ShouldReturnError_WhenDuplicateContentItemsProvided()
    {
        // Arrange
        var createPackRequest = new CreatePackRequest
        {
            PackName = "Test Pack with Duplicates",
            Active = true,
            Price = 100,
            Content = new List<string> { "Item 1", "Item 1" }, // Duplicate items
            ChildPackIds = new List<Guid>()
        };

        _mockPackRepository
            .Setup(repo => repo.PackItemExistsByNameAsync("Item 1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _commandHandler.ExecuteAsync(createPackRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsT1, "Expected the result to be a PackError but got a Pack.");
        var packError = result.AsT1;
        Assert.IsType<PackValidationError>(packError);
        Assert.Contains("already exists", packError.Message);
    }

    [Fact]
    public async Task CreatePack_ShouldReturnError_WhenChildPackDoesNotExist()
    {
        // Arrange
        var createPackRequest = new CreatePackRequest
        {
            PackName = "Test Pack with Non-existing Child",
            Active = true,
            Price = 150,
            Content = new List<string> { "Item 1" },
            ChildPackIds = new List<Guid> { Guid.NewGuid() }
        };

        _mockPackRepository
            .Setup(repo => repo.GetPackByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await _commandHandler.ExecuteAsync(createPackRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsT1, "Expected the result to be a PackError but got a Pack.");
        var packError = result.AsT1;
        Assert.IsType<PackValidationError>(packError);
        Assert.Contains("not found", packError.Message);
    }

    [Fact]
    public async Task CreatePack_ShouldReturnError_WhenChildPackAlreadyHasParent()
    {
        // Arrange
        var createPackRequest = new CreatePackRequest
        {
            PackName = "Test Pack with Child",
            Active = true,
            Price = 150,
            Content = ["Item 1"],
            ChildPackIds = [Guid.NewGuid()]
        };

        var existingChildPack = new Pack
        {
            Id = createPackRequest.ChildPackIds.First(),
            Name = "Child Pack",
            IsActive = true,
            ParentPack = new Pack { Id = Guid.NewGuid(), Name = "Parent Pack" } // Child already has a parent
        };

        _mockPackRepository
            .Setup(repo => repo.GetPackByIdAsync(existingChildPack.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingChildPack);

        // Act
        var result = await _commandHandler.ExecuteAsync(createPackRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsT1, "Expected the result to be a PackError but got a Pack.");
        var packError = result.AsT1;
        Assert.IsType<PackValidationError>(packError);
        Assert.Contains("already has a parent", packError.Message);
    }
}
