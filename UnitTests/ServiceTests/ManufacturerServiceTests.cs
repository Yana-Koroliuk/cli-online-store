using Moq;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.ServiceTests
{
    /// <summary>
    /// Unit tests for the <see cref="ManufacturerService"/> class.
    /// </summary>
    public class ManufacturerServiceTests
    {
        private readonly Mock<IManufacturerRepository> mockRepository;
        private readonly ManufacturerService manufacturerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerServiceTests"/> class.
        /// </summary>
        public ManufacturerServiceTests()
        {
            mockRepository = new Mock<IManufacturerRepository>();
            manufacturerService = new ManufacturerService(mockRepository.Object);
        }

        /// <summary>
        /// Tests the Add method of <see cref="ManufacturerService"/> class.
        /// </summary>
        [Fact]
        public void Add_ShouldAddManufacturer()
        {
            var manufacturerModel = new ManufacturerModel(0, "Test Manufacturer");

            manufacturerService.Add(manufacturerModel);

            mockRepository.Verify(r => r.Add(It.Is<Manufacturer>(m => m.Name == manufacturerModel.Name)), Times.Once);
        }

        /// <summary>
        /// Tests the Delete method of <see cref="ManufacturerService"/> class.
        /// </summary>
        [Fact]
        public void Delete_ShouldDeleteManufacturer()
        {
            var manufacturerId = 1;

            manufacturerService.Delete(manufacturerId);

            mockRepository.Verify(r => r.DeleteById(manufacturerId), Times.Once);
        }

        /// <summary>
        /// Tests the GetAll method of <see cref="ManufacturerService"/> class.
        /// </summary>
        [Fact]
        public void GetAll_ShouldReturnAllManufacturers()
        {
            var manufacturers = new List<Manufacturer>
            {
                new Manufacturer(1, "Manufacturer 1"),
                new Manufacturer(2, "Manufacturer 2")
            };
            mockRepository.Setup(r => r.GetAll()).Returns(manufacturers);

            var result = manufacturerService.GetAll();

            Assert.Equal(2, result.Count());
            Assert.Equal("Manufacturer 1", ((ManufacturerModel)result.ElementAt(0)).Name);
            Assert.Equal("Manufacturer 2", ((ManufacturerModel)result.ElementAt(1)).Name);
        }

        /// <summary>
        /// Tests the GetById method of <see cref="ManufacturerService"/> class.
        /// </summary>
        [Fact]
        public void GetById_ShouldReturnManufacturer()
        {
            var manufacturer = new Manufacturer(1, "Test Manufacturer");
            mockRepository.Setup(r => r.GetById(1)).Returns(manufacturer);

            var result = manufacturerService.GetById(1);

            Assert.Equal(1, ((ManufacturerModel)result).Id);
            Assert.Equal("Test Manufacturer", ((ManufacturerModel)result).Name);
        }

        /// <summary>
        /// Tests the GetByName method of <see cref="ManufacturerService"/> class.
        /// </summary>
        [Fact]
        public void GetByName_ShouldReturnManufacturer()
        {
            var manufacturer = new Manufacturer(1, "Test Manufacturer");
            mockRepository.Setup(r => r.GetAll()).Returns(new List<Manufacturer> { manufacturer });

            var result = manufacturerService.GetByName("Test Manufacturer");

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Manufacturer", ((ManufacturerModel)result).Name);
        }

        /// <summary>
        /// Tests the Create method of <see cref="ManufacturerService"/> class.
        /// </summary>
        [Fact]
        public void Create_ShouldCreateManufacturer()
        {
            var manufacturerName = "New Manufacturer";
            var newManufacturer = new Manufacturer(1, manufacturerName);
            var manufacturers = new List<Manufacturer> { newManufacturer };

            mockRepository.Setup(r => r.GetAll()).Returns(manufacturers);

            var result = manufacturerService.Create(manufacturerName);

            mockRepository.Verify(r => r.Add(It.Is<Manufacturer>(m => m.Name == manufacturerName)), Times.Once);
            Assert.Equal(manufacturerName, ((ManufacturerModel)result).Name);
        }

        /// <summary>
        /// Tests the Update method of <see cref="ManufacturerService"/> class.
        /// </summary>
        [Fact]
        public void Update_ShouldUpdateManufacturer()
        {
            var manufacturerModel = new ManufacturerModel(1, "Updated Manufacturer");
            var existingManufacturer = new Manufacturer(1, "Old Manufacturer");

            mockRepository.Setup(r => r.GetById(manufacturerModel.Id)).Returns(existingManufacturer);

            manufacturerService.Update(manufacturerModel);

            mockRepository.Verify(r => r.Update(It.Is<Manufacturer>(m => m.Id == manufacturerModel.Id && m.Name == manufacturerModel.Name)), Times.Once);
        }
    }
}
