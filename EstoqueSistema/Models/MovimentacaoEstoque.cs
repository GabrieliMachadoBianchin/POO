using EstoqueSistema.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueSistema.Models
{
    public class MovimentacaoEstoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; private set; }
        public Produto ProdutoMovimento { get; private set; } = null!;
        public DateTime Data { get; private set; }
        public int Unidades { get; private set; }
        public int FuncionarioId { get; private set; }
        public Funcionario FuncionarioMovimento { get; private set; } = null!;
        public TipoMovimentacao Tipo { get; private set; }

        // Construtor sem parâmetros para o EF Core
        public MovimentacaoEstoque() { }

        // Construtor para uso na aplicação
        public MovimentacaoEstoque(int produtoId, int unidades, int funcionarioId, TipoMovimentacao tipo)
        {
            ProdutoId = produtoId;
            Unidades = unidades;
            FuncionarioId = funcionarioId;
            Tipo = tipo;
            Data = DateTime.Now;
        }
        public void ExibirMovimentacao(MovimentacaoEstoque movimentacao)
        {
            if(movimentacao != null)
            {
                Console.WriteLine($"ID: {Id}, Data: {Data}, " +
                    $"Produto: {ProdutoMovimento.Nome}, " +
                    $"Tipo: {Tipo}, Unidades: {Unidades}, " +
                    $"Funcionário: {FuncionarioMovimento.Nome}");
            }
        }
        public void ExibirMovimentacaoEntrada(MovimentacaoEstoque movimentacao)
        {
            if (movimentacao.Tipo == TipoMovimentacao.Entrada)
            {
                Console.WriteLine($"ID: {Id}, Data: {Data}, " +
                    $"Produto: {ProdutoMovimento.Nome}, " +
                    $"Tipo: {Tipo}, Unidades: {Unidades}, " +
                    $"Funcionário: {FuncionarioMovimento.Nome}");
            }
        }
        public void ExibirMovimentacaoSaida(MovimentacaoEstoque movimentacao)
        {
            if (movimentacao.Tipo == TipoMovimentacao.Saida)
            {
                Console.WriteLine($"ID: {Id}, Data: {Data}, " +
                    $"Produto: {ProdutoMovimento.Nome}, " +
                    $"Tipo: {Tipo}, Unidades: {Unidades}, " +
                    $"Funcionário: {FuncionarioMovimento.Nome}");
            }
        }
    }
}
