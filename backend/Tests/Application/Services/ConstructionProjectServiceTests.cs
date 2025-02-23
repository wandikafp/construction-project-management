using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Tests.Application.Services
{
    public class ConstructionProjectServiceTests
    {
        private readonly Mock<IConstructionProjectRepository> _repositoryMock;
        private readonly Mock<IConstructionProjectMapper> _mapperMock;
        private readonly IConstructionProjectService _service;

        public ConstructionProjectServiceTests()
        {
            _repositoryMock = new Mock<IConstructionProjectRepository>();
            _mapperMock = new Mock<IConstructionProjectMapper>();
            _service = new ConstructionProjectService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllProjectsAsync_ReturnsPaginatedList()
        {
            // Arrange
            var projects = new List<ConstructionProject> { new ConstructionProject { ProjectId = 1, ProjectName = "Test Project" } };
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(projects);
            _repositoryMock.Setup(repo => repo.GetTotalAsync()).ReturnsAsync(1);
            _mapperMock.Setup(mapper => mapper.MapToResponse(It.IsAny<ConstructionProject>())).Returns(new ConstructionProjectResponse { ProjectId = 1, ProjectName = "Test Project" });

            // Act
            var result = await _service.GetAllProjectsAsync(1, 10, "");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetProjectByIdAsync_ReturnsProjectDetail()
        {
            // Arrange
            var project = new ConstructionProject { ProjectId = 1, ProjectName = "Test Project" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _mapperMock.Setup(mapper => mapper.MapToDetailResponse(It.IsAny<ConstructionProject>())).Returns(new ConstructionProjectDetailResponse { ProjectId = 1, ProjectName = "Test Project" });

            // Act
            var result = await _service.GetProjectByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProjectId);
        }

        [Fact]
        public async Task CreateProjectAsync_CreatesAndReturnsProjectDetail()
        {
            // Arrange
            var projectRequest = new ConstructionProjectRequest { ProjectName = "New Project" };
            var project = new ConstructionProject { ProjectId = 1, ProjectName = "New Project" };
            _mapperMock.Setup(mapper => mapper.MapToRequest(It.IsAny<ConstructionProjectRequest>())).Returns(project);
            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ConstructionProject>())).ReturnsAsync(project);
            _mapperMock.Setup(mapper => mapper.MapToDetailResponse(It.IsAny<ConstructionProject>())).Returns(new ConstructionProjectDetailResponse { ProjectId = 1, ProjectName = "New Project" });

            // Act
            var result = await _service.CreateProjectAsync(projectRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProjectId);
        }

        [Fact]
        public async Task UpdateProjectAsync_UpdatesProject()
        {
            // Arrange
            var projectRequest = new ConstructionProjectRequest { ProjectId = 1, ProjectName = "Updated Project" };
            var project = new ConstructionProject { ProjectId = 1, ProjectName = "Updated Project" };
            _mapperMock.Setup(mapper => mapper.MapToRequest(It.IsAny<ConstructionProjectRequest>())).Returns(project);
            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<ConstructionProject>())).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateProjectAsync(projectRequest);

            // Assert
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<ConstructionProject>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProjectAsync_DeletesProject()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteProjectAsync(1);

            // Assert
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
