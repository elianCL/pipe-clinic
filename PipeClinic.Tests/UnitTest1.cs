using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PipeClinic.Controllers;
using PipeClinic.Models;
using PipeClinic.Services;
using Xunit;

namespace PipeClinic.Tests.Controllers
{
    public class PacientesControllerTests
    {
        private readonly Mock<IPacienteService> _mockPacienteService;
        private readonly PacientesController _controller;

        public PacientesControllerTests()
        {
            _mockPacienteService = new Mock<IPacienteService>();
            _controller = new PacientesController(_mockPacienteService.Object);
        }

        [Fact]
        public async Task GetAllPacientes_ReturnsOkResult()
        {
            var expectedPacientes = new List<Paciente>
            {
                new Paciente { Id = 1, Nome = "João" },
                new Paciente { Id = 2, Nome = "Maria" }
            };
            _mockPacienteService.Setup(service => service.GetAllPacientesAsync()).ReturnsAsync(expectedPacientes);

            var result = await _controller.GetAllPacientes();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var pacientes = Assert.IsAssignableFrom<IEnumerable<Paciente>>(okResult.Value);
            Assert.Equal(expectedPacientes.Count, pacientes.Count());
        }

        [Fact]
        public async Task GetPaciente_WithValidId_ReturnsOkResult()
        {
            var id = 1;
            var paciente = new Paciente { Id = id, Nome = "João" };
            _mockPacienteService.Setup(service => service.GetPacienteByIdAsync(id)).ReturnsAsync(paciente);

            var result = await _controller.GetPaciente(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPaciente = Assert.IsType<Paciente>(okResult.Value);
            Assert.Equal(id, returnedPaciente.Id);
        }

        [Fact]
        public async Task GetPaciente_WithInvalidId_ReturnsNotFoundResult()
        {
            var id = 999; 
            _mockPacienteService.Setup(service => service.GetPacienteByIdAsync(id)).ReturnsAsync((Paciente)null);

            var result = await _controller.GetPaciente(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddPaciente_ReturnsCreatedAtActionResult()
        {
            var newPaciente = new Paciente { Nome = "Carlos" };
            _mockPacienteService.Setup(service => service.AddPacienteAsync(newPaciente)).ReturnsAsync(newPaciente);

            var result = await _controller.AddPaciente(newPaciente);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedPaciente = Assert.IsType<Paciente>(createdAtActionResult.Value);
            Assert.Equal(newPaciente.Nome, returnedPaciente.Nome);
        }

        [Fact]
        public async Task UpdatePaciente_ReturnsNoContentResult()
        {
            var id = 1;
            var pacienteAtualizado = new Paciente { Id = id, Nome = "José" };
            
            var result = await _controller.UpdatePaciente(id, pacienteAtualizado);

            Assert.IsType<NoContentResult>(result);
            _mockPacienteService.Verify(service => service.UpdatePacienteAsync(id, pacienteAtualizado), Times.Once);
        }

        [Fact]
        public async Task DeletePaciente_ReturnsNoContentResult()
        {
            var id = 1;

            var result = await _controller.DeletePaciente(id);
            
            Assert.IsType<NoContentResult>(result);
            _mockPacienteService.Verify(service => service.DeletePacienteAsync(id), Times.Once);
        }

        [Fact]
        public async Task ObterPesoIdeal_WithValidId_ReturnsOkResult()
        {
            var id = 1;
            var pesoIdeal = 70.5;
            _mockPacienteService.Setup(service => service.ObterPesoIdealAsync(id)).ReturnsAsync(pesoIdeal);
            
            var result = await _controller.ObterPesoIdeal(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPesoIdeal = Assert.IsType<double>(okResult.Value);
            Assert.Equal(pesoIdeal, returnedPesoIdeal);
        }

        [Fact]
        public async Task ObterPesoIdeal_WithInvalidId_ReturnsNotFoundResult()
        {
            var id = 999; 
            _mockPacienteService.Setup(service => service.ObterPesoIdealAsync(id)).ReturnsAsync((double?)null);

            var result = await _controller.ObterPesoIdeal(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ObterSituacaoIMC_WithValidId_ReturnsOkResult()
        {
            var id = 1;
            var situacaoIMC = "Sobrepeso";
            _mockPacienteService.Setup(service => service.ObterSituacaoIMCAsync(id)).ReturnsAsync(situacaoIMC);

            var result = await _controller.ObterSituacaoIMC(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSituacaoIMC = Assert.IsType<string>(okResult.Value);
            Assert.Equal(situacaoIMC, returnedSituacaoIMC);
        }

        [Fact]
        public async Task ObterSituacaoIMC_WithInvalidId_ReturnsNotFoundResult()
        {
            var id = 999; 
            _mockPacienteService.Setup(service => service.ObterSituacaoIMCAsync(id)).ReturnsAsync((string)null);
            
            var result = await _controller.ObterSituacaoIMC(id);
            
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ObterCpfOfuscado_WithValidId_ReturnsOkResult()
        {
            
            var id = 1;
            var cpfOfuscado = "123.xxx.456-78";
            _mockPacienteService.Setup(service => service.ObterCpfOfuscadoAsync(id)).ReturnsAsync(cpfOfuscado);

            var result = await _controller.ObterCpfOfuscado(id);
            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCpfOfuscado = Assert.IsType<string>(okResult.Value);
            Assert.Equal(cpfOfuscado, returnedCpfOfuscado);
        }

        [Fact]
        public async Task ObterCpfOfuscado_WithInvalidId_ReturnsNotFoundResult()
        {
            var id = 999; 
            _mockPacienteService.Setup(service => service.ObterCpfOfuscadoAsync(id)).ReturnsAsync((string)null);

            var result = await _controller.ObterCpfOfuscado(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ValidarCpf_WithValidId_ReturnsOkResult()
        {
            var id = 1;
            _mockPacienteService.Setup(service => service.ValidarCpfAsync(id)).ReturnsAsync(true);

            var result = await _controller.ValidarCpf(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var isValid = Assert.IsType<bool>(okResult.Value);
            Assert.True(isValid);
        }

        [Fact]
        public async Task ValidarCpf_WithInvalidId_ReturnsNotFoundResult()
        {
            var id = 999; 
            _mockPacienteService.Setup(service => service.ValidarCpfAsync(id)).ReturnsAsync(false);

            var result = await _controller.ValidarCpf(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
