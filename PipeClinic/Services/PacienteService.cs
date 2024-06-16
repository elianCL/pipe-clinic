// PipeClinic\Services\PacienteService.cs

using PipeClinic.Models;
using PipeClinic.Data; // Se necessário, para acesso ao contexto de dados
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipeClinic.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly List<Paciente> _pacientes; // Ou contexto do banco de dados, se aplicável

        public PacienteService()
        {
            _pacientes = new List<Paciente>();
        }

        public async Task<IEnumerable<Paciente>> GetAllPacientesAsync()
        {
            return await Task.FromResult(_pacientes);
        }

        public async Task<Paciente> GetPacienteByIdAsync(int id)
        {
            return await Task.FromResult(_pacientes.FirstOrDefault(p => p.Id == id));
        }

        public async Task<Paciente> AddPacienteAsync(Paciente paciente)
        {
            paciente.Id = _pacientes.Count + 1;
            _pacientes.Add(paciente);
            return await Task.FromResult(paciente);
        }

        public async Task UpdatePacienteAsync(int id, Paciente pacienteAtualizado)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente != null)
            {
                paciente.Nome = pacienteAtualizado.Nome;
                paciente.Sobrenome = pacienteAtualizado.Sobrenome;
                paciente.Sexo = pacienteAtualizado.Sexo;
                paciente.Nascimento = pacienteAtualizado.Nascimento;
                paciente.Altura = pacienteAtualizado.Altura;
                paciente.Peso = pacienteAtualizado.Peso;
                paciente.Cpf = pacienteAtualizado.Cpf;
            }
            await Task.CompletedTask;
        }

        public async Task DeletePacienteAsync(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente != null)
                _pacientes.Remove(paciente);
            await Task.CompletedTask;
        }

        public async Task<double?> ObterPesoIdealAsync(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
                return null;
            return await Task.FromResult(paciente.ObterPesoIdeal());
        }

        public async Task<string> ObterSituacaoIMCAsync(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
                return null;
            return await Task.FromResult(paciente.ObterSituacaoIMC());
        }

        public async Task<string> ObterCpfOfuscadoAsync(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
                return null;
            return await Task.FromResult(paciente.ObterCpfOfuscado());
        }

        public async Task<bool> ValidarCpfAsync(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente == null)
                return false;
            return await Task.FromResult(paciente.ValidarCpf());
        }
    }
}
