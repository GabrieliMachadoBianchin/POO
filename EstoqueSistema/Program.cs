using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueSistema.Models;
using EstoqueSistema.Enums;
using EstoqueSistema.DataBase;
using EstoqueSistema.Services;

namespace EstoqueSistema
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            using var db = new EstoqueLojinhaContext();
            var funcionarioService = new Services<Funcionario>(db);
            var produtoService = new Services<Produto>(db);
            var movimentacaoService = new Services<MovimentacaoEstoque>(db);

            Funcionario funcionarioLogado = null;
            while (funcionarioLogado == null)
            {
                funcionarioLogado = RealizarLogin(funcionarioService);
                if (funcionarioLogado == null)
                {
                    Console.WriteLine("Login falhou. Tente novamente.");
                }
            }

            Console.WriteLine($"Bem-vindo, {funcionarioLogado.Nome}!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Estoque ===");
                Console.WriteLine("1. Gerenciar Funcionários");
                Console.WriteLine("2. Gerenciar Produtos");
                Console.WriteLine("3. Gerenciar Movimentações");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        GerenciarFuncionarios(funcionarioService);
                        break;
                    case "2":
                        GerenciarProdutos(produtoService);
                        break;
                    case "3":
                        GerenciarMovimentacoes(movimentacaoService, produtoService, funcionarioService);
                        break;
                    case "0":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static Funcionario RealizarLogin(Services<Funcionario> service)
        {
            Console.Clear();
            Console.WriteLine("=== Login ===");
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            var funcionarios = service.ObterTodos();
            var funcionario = funcionarios.FirstOrDefault(f => f.Cpf == cpf);

            if (funcionario != null && funcionario.ValidarSenha(senha))
            {
                return funcionario;
            }

            return null;
        }

        static string ObterSenhaComConfirmacao()
        {
            string senha = "";
            string confirmacaoSenha = "";
            bool senhasCorrespondem = false;

            while (!senhasCorrespondem)
            {
                Console.Write("Digite a senha: ");
                senha = Console.ReadLine();

                Console.Write("Confirme a senha: ");
                confirmacaoSenha = Console.ReadLine();

                if (senha == confirmacaoSenha)
                {
                    senhasCorrespondem = true;
                }
                else
                {
                    Console.WriteLine("As senhas não correspondem. Por favor, tente novamente.");
                }
            }

            return senha;
        }

        static void GerenciarFuncionarios(Services<Funcionario> service)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== Gerenciar Funcionários ===");
                Console.WriteLine("1. Listar Funcionários");
                Console.WriteLine("2. Adicionar Funcionário");
                Console.WriteLine("3. Atualizar Funcionário");
                Console.WriteLine("4. Excluir Funcionário");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ListarFuncionarios(service);
                        break;
                    case "2":
                        AdicionarFuncionario(service);
                        break;
                    case "3":
                        AtualizarFuncionario(service);
                        break;
                    case "4":
                        ExcluirFuncionario(service);
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void GerenciarProdutos(Services<Produto> service)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== Gerenciar Produtos ===");
                Console.WriteLine("1. Listar Produtos");
                Console.WriteLine("2. Adicionar Produto");
                Console.WriteLine("3. Atualizar Produto");
                Console.WriteLine("4. Excluir Produto");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ListarProdutos(service);
                        break;
                    case "2":
                        AdicionarProduto(service);
                        break;
                    case "3":
                        AtualizarProduto(service);
                        break;
                    case "4":
                        ExcluirProduto(service);
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void GerenciarMovimentacoes(Services<MovimentacaoEstoque> movimentacaoService, Services<Produto> produtoService, Services<Funcionario> funcionarioService)
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== Gerenciar Movimentações de Estoque ===");
                Console.WriteLine("1. Listar Movimentações Todas");
                Console.WriteLine("2. Listar Movimentações Entrada");
                Console.WriteLine("3. Listar Movimentações Saida");
                Console.WriteLine("4. Adicionar Nova Movimentação");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ListarMovimentacoes(movimentacaoService);
                        break;
                    case "2":
                        ListarMovimentacoesEntrada(movimentacaoService);
                        break;
                    case "3":
                        ListarMovimentacoesSaida(movimentacaoService);
                        break;
                    case "4":
                        AdicionarMovimentacao(movimentacaoService, produtoService, funcionarioService);
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        /* Funcionário*/
        
        static void ListarFuncionarios(Services<Funcionario> service)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Funcionários ===");
           // var funcionarios = service.ObterTodos().OrderBy(m => m.Id);
            
            foreach (var funcionario in service.ObterTodos())
            {
                funcionario.ExibirFuncionarios(funcionario);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        static void AdicionarFuncionario(Services<Funcionario> service)
        {
            Console.Clear();
            Console.WriteLine("=== Adicionar Funcionário ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("CPF do funcionário (11 dígitos): ");
            string cpf = Console.ReadLine();
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                Console.WriteLine("CPF inválido. Deve conter 11 dígitos numéricos. Operação cancelada.");
                Console.ReadKey();
                return;
            }
            string senha = ObterSenhaComConfirmacao();
            if (string.IsNullOrWhiteSpace(senha))
            {
                Console.WriteLine("Operação cancelada.");
                Console.ReadKey();
                return;
            }


            var novoFuncionario = new Funcionario(nome, cpf, senha);
            service.Adicionar(novoFuncionario);

            Console.WriteLine("Funcionário adicionado com sucesso. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        static void AtualizarFuncionario(Services<Funcionario> service)
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar Funcionário ===");

            Console.Write("Digite o ID do funcionário que deseja atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            var funcionario = service.ObterPorId(id);
            if (funcionario == null)
            {
                Console.WriteLine("Funcionário não encontrado. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Atualizando funcionário: {funcionario.Nome}");

            Console.Write("Novo nome (deixe em branco para manter o atual): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                funcionario.AtualizarNomeFuncionario(novoNome);
            }

            Console.Write("Novo CPF (deixe em branco para manter o atual): ");
            string novoCpf = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoCpf))
            {
                if (novoCpf.Length == 11 && novoCpf.All(char.IsDigit))
                {
                    funcionario.AtualizarCpfFuncionario(novoCpf);
                }
                else
                {
                    Console.WriteLine("CPF inválido. Este campo não será atualizado.");
                }
            }

            Console.Write("Deseja alterar a senha? (S/N): ");
            if (Console.ReadLine().Trim().ToUpper() == "S")
            {
                string novaSenha = ObterSenhaComConfirmacao();
                if (!string.IsNullOrWhiteSpace(novaSenha))
                {
                    funcionario.AtualizarSenhaFuncionario(novaSenha);
                }
                else
                {
                    Console.WriteLine("Senha não alterada.");
                }
            }

            service.Atualizar(funcionario);

            Console.WriteLine("Funcionário atualizado com sucesso. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        static void ExcluirFuncionario(Services<Funcionario> service)
        {
            Console.Clear();
            Console.WriteLine("=== Excluir Funcionário ===");

            Console.Write("Digite o ID do funcionário que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            var funcionario = service.ObterPorId(id);
            if (funcionario == null)
            {
                Console.WriteLine("Funcionário não encontrado. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Tem certeza que deseja excluir o funcionário: {funcionario.Nome}?");
            Console.Write("Digite 'SIM' para confirmar: ");
            if (Console.ReadLine().ToUpper() != "SIM")
            {
                Console.WriteLine("Operação cancelada.");
                Console.ReadKey();
                return;
            }

            service.Excluir(id);

            Console.WriteLine("Funcionário excluído com sucesso. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        
        /* Movimentações*/
        
        static void ListarMovimentacoes(Services<MovimentacaoEstoque> service)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Movimentações de Estoque ===");
            var movimentacoes = service.ObterTodos().OrderByDescending(m => m.Data);
            foreach (var movimentacao in movimentacoes)
            {
                movimentacao.ExibirMovimentacao(movimentacao);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        static void ListarMovimentacoesEntrada(Services<MovimentacaoEstoque> service)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Movimentações de Estoque ===");
            var movimentacoes = service.ObterTodos().OrderByDescending(m => m.Data);
            foreach (var movimentacao in movimentacoes)
            {
                movimentacao.ExibirMovimentacaoEntrada(movimentacao);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        static void ListarMovimentacoesSaida(Services<MovimentacaoEstoque> service)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Movimentações de Estoque ===");
            var movimentacoes = service.ObterTodos().OrderByDescending(m => m.Data);
            foreach (var movimentacao in movimentacoes)
            {
                movimentacao.ExibirMovimentacaoSaida(movimentacao);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        static void AdicionarMovimentacao(Services<MovimentacaoEstoque> movimentacaoService, Services<Produto> produtoService, Services<Funcionario> funcionarioService)
        {
            Console.Clear();
            Console.WriteLine("=== Adicionar Nova Movimentação de Estoque ===");

            // Selecionar produto
            Console.WriteLine("Produtos disponíveis:");
            var produtos = produtoService.ObterTodos();
            foreach (var produto in produtos)
            {
                Console.WriteLine($"ID: {produto.Id}, Nome: {produto.Nome}, Estoque Atual: {produto.Quantidade}");
            }
            Console.Write("Digite o ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int produtoId))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                return;
            }
            var produtoSelecionado = produtoService.ObterPorId(produtoId);
            if (produtoSelecionado == null)
            {
                Console.WriteLine("Produto não encontrado. Operação cancelada.");
                return;
            }
        
            // Selecionar tipo de movimentação
            
            Console.WriteLine("Tipos de movimentação:");
            Console.WriteLine("1. Entrada");
            Console.WriteLine("2. Saída");
            Console.Write("Escolha o tipo de movimentação (1 ou 2): ");
            string aux = Console.ReadLine();
            int escolha = int.Parse(aux);
            TipoMovimentacao tipoAdd;
            if (aux == null)
            {
                Console.WriteLine("Opção inválida. Operação cancelada.");
                return;
            }
            if(escolha == 1) { tipoAdd = TipoMovimentacao.Entrada; } else { tipoAdd = TipoMovimentacao.Saida; }
            
            Console.Write("Digite a quantidade de unidades: ");
            if (!int.TryParse(Console.ReadLine(), out int unidades) || unidades <= 0)
            {
                Console.WriteLine("Quantidade inválida. Operação cancelada.");
                return;
            }

            // Verificar estoque para saída
            if (tipoAdd == TipoMovimentacao.Saida && unidades > produtoSelecionado.Quantidade)
            {
                Console.WriteLine("Quantidade insuficiente em estoque. Operação cancelada.");
                return;
            }

            // Selecionar funcionário (supondo que o funcionário logado está realizando a operação)
            Console.Write("Digite o ID do funcionário responsável pela movimentação: ");
            if (!int.TryParse(Console.ReadLine(), out int funcionarioId))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                return;
            }
            var funcionarioResponsavel = funcionarioService.ObterPorId(funcionarioId);
            if (funcionarioResponsavel == null)
            {
                Console.WriteLine("Funcionário não encontrado. Operação cancelada.");
                return;
            }

            var novaMovimentacao = new MovimentacaoEstoque(produtoId, unidades, funcionarioId, tipoAdd);
            movimentacaoService.Adicionar(novaMovimentacao);

            // Atualizar o estoque do produto e add novaMovimentacao na lista de produtos e funcionarios
            produtoSelecionado.AtividadeProduto(unidades, tipoAdd);
            produtoSelecionado.AtualizarListaProduto(novaMovimentacao);
            funcionarioResponsavel.AtualizarListaFuncionario(novaMovimentacao);
            produtoService.Atualizar(produtoSelecionado);
            funcionarioService.Atualizar(funcionarioResponsavel);

            Console.WriteLine("Movimentação adicionada com sucesso. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
            
        /* Produto */
        
        static void ListarProdutos(Services<Produto> service)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Produtos ===");
            var produtos = service.ObterTodos();
            foreach (var produto in produtos)
            {
                produto.ExibirProduto(produto);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        static void AdicionarProduto(Services<Produto> service)
        {
            Console.Clear();
            Console.WriteLine("=== Adicionar Novo Produto ===");

            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Descrição do produto: ");
            string descricao = Console.ReadLine();

            Console.Write("Preço do produto: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco) || preco < 0)
            {
                Console.WriteLine("Preço inválido. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            Console.Write("Quantidade inicial em estoque: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade < 0)
            {
                Console.WriteLine("Quantidade inválida. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            var novoProduto = new Produto(nome, descricao, preco, quantidade);
            service.Adicionar(novoProduto);

            Console.WriteLine("Produto adicionado com sucesso. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        static void AtualizarProduto(Services<Produto> service)
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar Produto ===");

            Console.Write("Digite o ID do produto que deseja atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            var produto = service.ObterPorId(id);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Atualizando produto: {produto.Nome}");

            Console.Write("Novo nome (deixe em branco para manter o atual): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                produto.AtualizarNomeProduto(novoNome);
            }

            Console.Write("Nova descrição (deixe em branco para manter a atual): ");
            string novaDescricao = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaDescricao))
            {
                produto.AtualizarDescricaoProduto(novaDescricao);   
            }

            Console.Write($"Novo preço (atual: R${produto.Preco}, deixe em branco para manter): ");
            string novoPrecoStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoPrecoStr))
            {
                if (decimal.TryParse(novoPrecoStr, out decimal novoPreco) && novoPreco >= 0)
                {
                    produto.AtualizarPrecoProduto(novoPreco);
                }
                else
                {
                    Console.WriteLine("Preço inválido. Este campo não será atualizado.");
                }
            }

            service.Atualizar(produto);

            Console.WriteLine("Produto atualizado com sucesso. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }

        static void ExcluirProduto(Services<Produto> service)
        {
            Console.Clear();
            Console.WriteLine("=== Excluir Produto ===");

            Console.Write("Digite o ID do produto que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                Console.ReadKey();
                return;
            }

            var produto = service.ObterPorId(id);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado. Operação cancelada.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine($"Tem certeza que deseja excluir o produto: {produto.Nome}?");
            Console.Write("Digite 'SIM' para confirmar: ");
            if (Console.ReadLine().ToUpper() != "SIM")
            {
                Console.WriteLine("Operação cancelada.");
                Console.ReadKey();
                return;
            }

            service.Excluir(id);

            Console.WriteLine("Produto excluído com sucesso. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        
        /* Funciona caso o menu esteja incompleto*/
        /*
        static void Main(string[] args)
        {
            using var db = new EstoqueLojinhaContext();
            var service = new Services<Funcionario>(db);
            var servicep = new Services<Produto>(db);
            var servicem = new Services<MovimentacaoEstoque>(db);
            var novoFuncionario = new Funcionario( "aaaa", "00000000002", "admin");
            db.Funcionarios.Add(novoFuncionario);
            db.SaveChanges();


            var produto = new Produto("Coca-Cola", "2 Litros", 12.00M, 50);
            db.Produtos.Add(produto);
            db.SaveChanges();


            var movimento = new MovimentacaoEstoque(1, 10, 2, TipoMovimentacao.Entrada);
            var produtoo = servicep.ObterPorId(1);
            var funcionario = service.ObterPorId(1);

            produtoo.AtividadeProduto(movimento.Unidades, movimento.Tipo);
            funcionario.AtualizarListaFuncionario(movimento);
            produto.AtualizarListaProduto(movimento);

            db.MovimentacoesEstoque.Add(movimento);
            db.Produtos.Update(produtoo);
            db.Funcionarios.Update(funcionario);
            db.SaveChanges();

            ExibirFunc(db);
            ExibirProdutos(db);
            ListarMovimentacoes(servicem);
        }
        public static void ExibirFunc(EstoqueLojinhaContext db)
        {
            var funcionarios = db.Funcionarios.ToList();
            foreach (var func in funcionarios)
            {
                Console.WriteLine($"{func.Nome}, {func.Id}");
            }

        }
        public static void ExibirProdutos(EstoqueLojinhaContext db)
        {
            var produtos = db.Produtos.ToList();
            foreach (var p in produtos)
            {
                Console.WriteLine($"{p.Nome}, {p.Id},  {p.Preco},  {p.Quantidade}");
            }

        }

        static void ListarMovimentacoes(Services<MovimentacaoEstoque> service)
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Movimentações de Estoque ===");
            var movimentacoes = service.ObterTodos().OrderByDescending(m => m.Data);
            foreach (var movimentacao in movimentacoes)
            {
                
                  Console.WriteLine($"ID: {movimentacao.Id}, Data: {movimentacao.Data}, " +
                    $"Produto: {movimentacao.ProdutoMovimento.Nome}, " +
                    $"Tipo: {movimentacao.Tipo}, Unidades: {movimentacao.Unidades}, " +
                    $"Funcionário: {movimentacao.FuncionarioMovimento.Nome}");
                  
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
        */
    }

}
