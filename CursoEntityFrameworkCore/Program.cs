using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoverDados();
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
            //Diferentes formas de persistir os dados
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

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //Diferentes formas de persistir os dados
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodos = db.Clientes
                .Where(prop => prop.Id > 0)
                .OrderBy(prop => prop.Id)
                .ToList();
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>{
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };
            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultaPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            List<Pedido> pedidos = db.Pedidos
                .Include(prop => prop.Itens)
                .ThenInclude(prop => prop.Produto)
                .ToList();
            Console.WriteLine(pedidos.Count);
        }
        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            //Diferentes formas de persistir os dados
            //var cliente = db.Clientes.FirstOrDefault(prop => prop.Id == 1);
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Cliente alterado 2";
            //Diferentes formas de persistir os dados
            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void RemoverDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(3);
            //Diferentes formas de persistir os dados
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }

}
