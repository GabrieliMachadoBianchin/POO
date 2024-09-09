using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueSistema.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; private set; } = null!;
        [MaxLength(11)]
        public string Cpf { get; private set; } = null!;
        public string Senha { get; private set; } = null!; // o get era pra ser protected KKKKKKK
        public List<MovimentacaoEstoque>? AtividadeEstoque { get; set; }

        public Funcionario(string nome, string cpf, string senha)
        {
            Nome = nome;
            Cpf = cpf;
            Senha = senha;
            AtividadeEstoque = [];
        }

        public void AtualizarListaFuncionario(MovimentacaoEstoque movimento)
        {
            if (movimento == null)
            {
                Console.WriteLine("Movimento nulo");
            }
            
            AtividadeEstoque.Add(movimento);
        }

        public bool ValidarSenha(string senha)
        {
            if (string.Equals(senha, Senha))
            {
                return true;
            }
            return false;
        }

        public void AtualizarNomeFuncionario(string novoNome)
        {
            Nome = novoNome; return;
        }
        public void AtualizarCpfFuncionario(string novoCpf)
        {
            Cpf = novoCpf; return;
        }

        public void AtualizarSenhaFuncionario(string novaSenha)
        {
            Senha = novaSenha; return; 
        }

        public void ExibirFuncionarios(Funcionario funcionario)
        {
            if (funcionario != null)
            {
                Console.WriteLine($"ID: {Id}, Nome: {Nome}, CPF: {Cpf}");
            }
        }
    }

}