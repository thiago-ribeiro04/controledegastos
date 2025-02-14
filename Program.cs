using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeGastos
{
    // Classe que representa uma pessoa
    public class Pessoa
    {
        public int Id { get; set; } // Identificador único da pessoa
        public string Nome { get; set; } // Nome da pessoa
        public int Idade { get; set; } // Idade da pessoa

        public Pessoa(int id, string nome, int idade)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
        }
    }

    // Classe que representa uma transação financeira
    public class Transacao
    {
        public int Id { get; set; } // Identificador único da transação
        public string Descricao { get; set; } // Descrição da transação
        public decimal Valor { get; set; } // Valor da transação
        public string Tipo { get; set; } // Tipo da transação (receita/despesa)
        public int PessoaId { get; set; } // Identificador da pessoa associada

        public Transacao(int id, string descricao, decimal valor, string tipo, int pessoaId)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
            PessoaId = pessoaId;
        }
    }

    // Classe que gerencia as operações do sistema
    public class SistemaControleDeGastos
    {
        private List<Pessoa> pessoas = new List<Pessoa>(); // Lista de pessoas cadastradas
        private List<Transacao> transacoes = new List<Transacao>(); // Lista de transações realizadas
        private int proximoIdPessoa = 1; // Contador para os IDs das pessoas
        private int proximoIdTransacao = 1; // Contador para os IDs das transações

        // Método para cadastrar uma pessoa
        public void CadastrarPessoa(string nome, int idade)
        {
            var pessoa = new Pessoa(proximoIdPessoa++, nome, idade);
            pessoas.Add(pessoa);
            Console.WriteLine($"Pessoa {nome} cadastrada com sucesso!");
        }

        // Método para deletar uma pessoa e suas transações
        public void DeletarPessoa(int id)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa != null)
            {
                // Remove todas as transações associadas à pessoa
                transacoes.RemoveAll(t => t.PessoaId == id);
                pessoas.Remove(pessoa);
                Console.WriteLine($"Pessoa com ID {id} e suas transações foram deletadas.");
            }
            else
            {
                Console.WriteLine("Pessoa não encontrada!");
            }
        }

        // Método para listar todas as pessoas
        public void ListarPessoas()
        {
            if (pessoas.Count == 0)
            {
                Console.WriteLine("Nenhuma pessoa cadastrada.");
            }
            else
            {
                Console.WriteLine("Pessoas cadastradas:");
                foreach (var pessoa in pessoas)
                {
                    Console.WriteLine($"ID: {pessoa.Id}, Nome: {pessoa.Nome}, Idade: {pessoa.Idade}");
                }
            }
        }

        // Método para cadastrar uma transação
        public void CadastrarTransacao(string descricao, decimal valor, string tipo, int pessoaId)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Id == pessoaId);
            if (pessoa == null)
            {
                Console.WriteLine("Pessoa não encontrada!");
                return;
            }

            // Se a pessoa for menor de idade, só pode cadastrar despesas
            if (pessoa.Idade < 18 && tipo != "despesa")
            {
                Console.WriteLine("Menores de 18 anos só podem cadastrar despesas.");
                return;
            }

            var transacao = new Transacao(proximoIdTransacao++, descricao, valor, tipo, pessoaId);
            transacoes.Add(transacao);
            Console.WriteLine($"Transação {descricao} cadastrada com sucesso!");
        }

        // Método para listar transações de uma pessoa
        public void ListarTransacoes(int pessoaId)
        {
            var transacoesPessoa = transacoes.Where(t => t.PessoaId == pessoaId).ToList();
            if (transacoesPessoa.Count == 0)
            {
                Console.WriteLine("Nenhuma transação encontrada para essa pessoa.");
            }
            else
            {
                Console.WriteLine($"Transações de {pessoas.First(p => p.Id == pessoaId).Nome}:");
                foreach (var transacao in transacoesPessoa)
                {
                    Console.WriteLine($"ID: {transacao.Id}, Descrição: {transacao.Descricao}, Valor: {transacao.Valor}, Tipo: {transacao.Tipo}");
                }
            }
        }

        // Método para consultar totais de receitas, despesas e saldo de cada pessoa
        public void ConsultarTotais()
        {
            decimal totalReceitas = 0;
            decimal totalDespesas = 0;
            decimal saldoGeral = 0;

            Console.WriteLine("Totais de cada pessoa:");

            foreach (var pessoa in pessoas)
            {
                var receitas = transacoes.Where(t => t.PessoaId == pessoa.Id && t.Tipo == "receita").Sum(t => t.Valor);
                var despesas = transacoes.Where(t => t.PessoaId == pessoa.Id && t.Tipo == "despesa").Sum(t => t.Valor);
                var saldo = receitas - despesas;

                Console.WriteLine($"Pessoa: {pessoa.Nome}, Receitas: {receitas}, Despesas: {despesas}, Saldo: {saldo}");

                totalReceitas += receitas;
                totalDespesas += despesas;
                saldoGeral += saldo;
            }

            Console.WriteLine($"Total Geral de Receitas: {totalReceitas}, Total Geral de Despesas: {totalDespesas}, Saldo Geral: {saldoGeral}");
        }
    }

    // Classe de execução do programa
    class Program
    {
        static void Main(string[] args)
        {
            var sistema = new SistemaControleDeGastos();

            // Cadastrando pessoas
            sistema.CadastrarPessoa("João", 30);
            sistema.CadastrarPessoa("Maria", 16);

            // Listando pessoas cadastradas
            sistema.ListarPessoas();

            // Cadastrando transações
            sistema.CadastrarTransacao("Salário", 3000, "receita", 1); // João
            sistema.CadastrarTransacao("Compra de supermercado", 200, "despesa", 1); // João
            sistema.CadastrarTransacao("Mesada", 50, "receita", 2); // Maria, mas como ela é menor de idade, apenas despesa pode ser registrada
            sistema.CadastrarTransacao("Compra de roupa", 100, "despesa", 2); // Maria

            // Listando transações de uma pessoa
            sistema.ListarTransacoes(1);

            // Consultando totais
            sistema.ConsultarTotais();

            // Deletando uma pessoa
            sistema.DeletarPessoa(1); // Deleta João e suas transações

            // Listando pessoas após a exclusão
            sistema.ListarPessoas();

            // Consultando totais após exclusão
            sistema.ConsultarTotais();
        }
    }
}
