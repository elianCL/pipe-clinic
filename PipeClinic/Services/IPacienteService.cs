// PipeClinic\Services\IPacienteService.cs

using PipeClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipeClinic.Services
{
    public interface IPacienteService
    {
        Task<IEnumerable<Paciente>> GetAllPacientesAsync();
        Task<Paciente> GetPacienteByIdAsync(int id);
        Task<Paciente> AddPacienteAsync(Paciente paciente);
        Task UpdatePacienteAsync(int id, Paciente paciente);
        Task DeletePacienteAsync(int id);
        Task<double?> ObterPesoIdealAsync(int id);
        Task<string> ObterSituacaoIMCAsync(int id);
        Task<string> ObterCpfOfuscadoAsync(int id);
        Task<bool> ValidarCpfAsync(int id);
    }
}
