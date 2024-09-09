using EstoqueSistema.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueSistema.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; private set; } = null!;
        public string? Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; set; }

        public List<MovimentacaoEstoque>? AtividadeEstoque { get; set; }

        public Produto( string nome, string descricao, decimal preco, int quantidade)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            AtividadeEstoque = [];
            Quantidade = quantidade;
        }

        public void AtividadeProduto(int unidades, TipoMovimentacao tipo)
        {
            if(tipo == TipoMovimentacao.Entrada) { Quantidade += unidades; }else { Quantidade -= unidades; }
        }

        public void AtualizarListaProduto(MovimentacaoEstoque movimento)
        {
            if (movimento == null)
            {
                Console.WriteLine("Movimento nulo");
            }

            AtividadeEstoque.Add(movimento);
        }

        public void AtualizarNomeProduto(string nome)
        {
            Nome = nome; return;
        }
        public void AtualizarDescricaoProduto(string novaDescricao)
        {
            Descricao = novaDescricao; return;
        }

        public void AtualizarPrecoProduto(decimal novoPreco)
        {
            Preco = novoPreco; return;
        }

        public void ExibirProduto(Produto produto)
        {
            if(produto != null)
            {
                Console.WriteLine($"ID: {Id}, Nome: {Nome}, Descrição: {Descricao}, Preço: R${Preco}, Quantidade: {Quantidade}");
            }
        }
    }
}
