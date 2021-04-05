using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using System;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            InserirDadosEmMassa();
        }

        private static void InserirDadosEmMassa()
        {
            var listaDeClientes = new[] {
              new Cliente
                {
                    Nome = "Rafael Almeida",
                    Telefone = "43998439787",
                    CEP = "99999900",
                    Estado = "PR",
                    Cidade = "Curitiba"
                },
               new Cliente
                {
                    Nome = "Rafael Almeida 2",
                    Telefone = "43998439787",
                    CEP = "99999900",
                    Estado = "PR",
                    Cidade = "Curitiba"
                },
                new Cliente
                {
                    Nome = "Rafael Almeida 3",
                    Telefone = "43998439787",
                    CEP = "99999900",
                    Estado = "PR",
                    Cidade = "Curitiba"
                },
            };

            using var db = new Data.ApplicationContext();
            //db.AddRange(Produto,cliente);
            //db.Clientes.AddRange(listaDeClientes);
            db.Set<Cliente>().AddRange(listaDeClientes);
            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");


        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true,
            };
            using var db = new Data.ApplicationContext();
            // Diferentes formas de persistir os dados
            //db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            db.Add(produto);
            db.SaveChanges();
        }
    }
}
