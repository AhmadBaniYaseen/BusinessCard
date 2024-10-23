using BusinessCard.Core.DTO;
using BusinessCard.Core.Repository;
using BusinessCard.Core.Service;
using BusinessCardAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text;

namespace BusinessCardAPIUnitTest;

public class BusinessCardControllerUnitTest
{
    private readonly Mock<IBusinessCardService> _mockService;
    private readonly BusinessCardController _controller;
    private readonly Mock<IBusinessCardRepository> _mockRepository;
    public BusinessCardControllerUnitTest()
    {

        _mockRepository = new Mock<IBusinessCardRepository>();
        _mockService = new Mock<IBusinessCardService>(); 
        _controller = new BusinessCardController(_mockService.Object); 

    }

    [Fact]
    public async Task GetAllBusinessCard_ReturnsOkResult_WithListOfBusinessCards()
    {

        // Arrange
        var mockBusinessCards = new List<BusinessCard.Core.Data.BusinessCard>
        {
            new BusinessCard.Core.Data.BusinessCard
            {
                Name = "Test Name",
                Gender = "Male",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "test@example.com",
                Phone = "1234567890",
                Address = "123 Test St",
                Photo = "testphoto.png"
            },
            new BusinessCard.Core.Data.BusinessCard
            {
                Name = "Test Name 2",
                Gender = "Male",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "test2@example.com",
                Phone = "1234567890",
                Address = "333 Test St",
                Photo = "testphoto323.png"
            }
        };

        _mockService.Setup(service => service.GetAllBusinessCard()).ReturnsAsync(mockBusinessCards);

        // Act
        var result = await _controller.GetAllBusinessCard();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<List<BusinessCard.Core.Data.BusinessCard>>(okResult.Value);
        Assert.Equal(mockBusinessCards.Count, returnValue.Count);
    }

    [Fact]
    public async Task CreateBusinessCard_CallsServiceCreateMethod()
    {
        // Arrange
        var input = new CreateBusinessCardInput ( "", "", DateTime.UtcNow, "", "", "", "" );

        // Act
        await _controller.CreateBusinessCard(input);

        // Assert
        _mockService.Verify(service => service.CreateBusinessCard(input), Times.Once);
    }

    [Fact]
    public async Task DeleteBusinessCard_CallsServiceDeleteMethod()
    {
        // Arrange
        var input = new DeleteBusinessCard(1);

        // Act
        await _controller.DeleteBusinessCard(input);

        // Assert
        _mockService.Verify(service => service.DeleteBusinessCard(input), Times.Once);
    }

    [Fact]
    public async Task UpdateBusinessCard_CallsServiceUpdateMethod()
    {
        // Arrange
        var input = new UpdateBusinessCard(1, "", "", DateTime.UtcNow, "", "", "", "");

        // Act
        await _controller.UpdateBusinessCard(input);

        // Assert
        _mockService.Verify(service => service.UpdateBusinessCard(input), Times.Once);
    }

    [Fact]
    public async Task GetByBusinessCardId_ReturnsBusinessCard()
    {
        // Arrange
        var input = new GetBusinessCardById(1);
        var businessCard = new BusinessCard.Core.Data.BusinessCard { /* Set properties */ };
        _mockService.Setup(service => service.GetByBusinessCardId(input)).ReturnsAsync(businessCard);

        // Act
        var result = await _controller.GetByBusinessCardId(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(businessCard, result);
    }

    [Fact]
    public async Task UploadBusinessCardFile_CallsUploadMethod()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        // Setup fileMock properties as needed

        // Act
        await _controller.UploadBusinessCardFile(fileMock.Object);

        // Assert
        _mockService.Verify(service => service.UploadBusinessCardFile(fileMock.Object), Times.Once);
    }

    [Fact]
    public async Task ExportCsv_CallsExportToCsvMethod()
    {
        // Arrange
        var businessCard = new BusinessCard.Core.Data.BusinessCard
        {
            Name = "Test Name",
            Gender = "Male",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "test@example.com",
            Phone = "1234567890",
            Address = "123 Test St",
            Photo = "testphoto.png"
        };

        var fileResult = new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes("Sample CSV Content")), "text/csv")
        {
            FileDownloadName = "businesscards.csv"
        };

        _mockService.Setup(service => service.ExportToCsv(businessCard)).ReturnsAsync(fileResult);

        // Act
        var result = await _controller.ExportCsv(businessCard);

        // Assert
        var fileResultFromController = Assert.IsType<FileStreamResult>(result);
        Assert.NotNull(fileResultFromController); // Ensure it's not null
        Assert.Equal("businesscards.csv", fileResultFromController.FileDownloadName);
    }

    [Fact]
    public async Task ExportXml_CallsExportToXmlMethod()
    {
        // Arrange
        var businessCard = new BusinessCard.Core.Data.BusinessCard
        {
            Name = "Test Name",
            Gender = "Male",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "test@example.com",
            Phone = "1234567890",
            Address = "123 Test St",
            Photo = "testphoto.png"
        };

        var fileResult = new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes("<BusinessCard>...</BusinessCard>")), "application/xml")
        {
            FileDownloadName = "businesscard.xml"
        };

        _mockService.Setup(service => service.ExportToXml(businessCard)).ReturnsAsync(fileResult);

        // Act
        var result = await _controller.ExportXml(businessCard);

        // Assert
        var fileResultFromController = Assert.IsType<FileStreamResult>(result);
        Assert.NotNull(fileResultFromController); // Ensure it's not null
        Assert.Equal("businesscard.xml", fileResultFromController.FileDownloadName);
    }
}