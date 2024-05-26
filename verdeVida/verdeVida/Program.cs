using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace VerdeVida
{
    public class produto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
    }

    public class itemCompra
    {
        public produto produto { get; set; }
        public int quantidade { get; set; }
        public int codCompra { get; set; }
        public string enderecoEntrega { get; set; }
    }

    public static class telaCompra
    {
        static List<produto> produtos = new List<produto>
        {
            new produto { id = 1, nome = "Tomate", preco = 3.5m },
            new produto { id = 3, nome = "Alface", preco = 1.2m },
            new produto { id = 2, nome = "Cenoura", preco = 2.8m },
            new produto { id = 4, nome = "Batata", preco = 4.0m },
            new produto { id = 5, nome = "Pepino", preco = 2.3m },
            new produto { id = 6, nome = "Beterraba", preco = 2.7m },
            new produto { id = 7, nome = "Cebola", preco = 1.8m },
            new produto { id = 8, nome = "Alho", preco = 5.5m},
        };

        public static void efetuarCompra()
        {
            List<itemCompra> itensCompra = new List<itemCompra>();

            Console.WriteLine("\nProdutos disponíveis:");
            foreach (var produto in produtos)
            {
                Console.WriteLine($"{produto.id}. {produto.nome} - R${produto.preco}");
            }

            bool comprando = true;
            while (comprando)
            {
                Console.Write("\nDigite o ID do produto que deseja comprar (0 para finalizar): ");
                int idProduto;
                if (!int.TryParse(Console.ReadLine(), out idProduto))
                {
                    Console.WriteLine("ID inválido. Tente novamente.");
                    continue;
                }

                if (idProduto == 0)
                {
                    comprando = false;
                }
                else
                {
                    var produto = produtos.Find(p => p.id == idProduto);
                    if (produto != null)
                    {
                        Console.Write($"Digite a quantidade de {produto.nome}: ");
                        int quantidade;
                        if (!int.TryParse(Console.ReadLine(), out quantidade))
                        {
                            Console.WriteLine("Quantidade inválida. Tente novamente.");
                            continue;
                        }

                        itensCompra.Add(new itemCompra { produto = produto, quantidade = quantidade });
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado. Tente novamente.");
                    }
                }
            }

            Console.Write("Digite o endereço de entrega: ");
            string enderecoEntrega = Console.ReadLine();
            foreach (var item in itensCompra)
            {
                item.enderecoEntrega = enderecoEntrega;
            }

            gerarNumerosAleatorios(itensCompra);
            string metodoPagamento = escolherMetodoPagamento();
            decimal total = calcularTotal(itensCompra);
            mostrarRecibo(itensCompra, metodoPagamento);
            salvarCompraBancoDados(itensCompra, metodoPagamento, total);

        }

        private static void gerarNumerosAleatorios(List<itemCompra> itensCompra)
        {
            Random random = new Random();
            foreach (var item in itensCompra)
            {
                item.codCompra = random.Next(1000, 9999);
            }
        }

        private static string escolherMetodoPagamento()
        {
            Console.WriteLine("\nEscolha a forma de pagamento:");
            Console.WriteLine("1. Cartão de Crédito/Débito");
            Console.WriteLine("2. PIX");
            Console.WriteLine("3. Boleto");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    return "Cartão de Crédito/Débito";
                case "2":
                    return "PIX";
                case "3":
                    return "Boleto";
                default:
                    Console.WriteLine("Opção inválida. Usando pagamento por Boleto como padrão.");
                    return "Boleto";
            }
        }

        private static string gerarChavePix()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-*";
            return new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static void mostrarRecibo(List<itemCompra> itensCompra, string metodoPagamento)
        {
            Console.WriteLine("\nRecibo da Compra:");
            decimal total = 0;
            foreach (var item in itensCompra)
            {
                decimal subtotal = item.quantidade * item.produto.preco;
                total += subtotal;
                Console.WriteLine($"{item.produto.nome} - Quantidade: {item.quantidade}, Preço Unitário: R${item.produto.preco}, Subtotal: R${subtotal}, Código compra: {item.codCompra}, Endereço de Entrega: {item.enderecoEntrega}");
            }
            Console.WriteLine($"Total da Compra: R${total}");
            Console.WriteLine($"Método de Pagamento: {metodoPagamento}");

            if (metodoPagamento == "PIX")
            {
                string chavePix = gerarChavePix();
                Console.WriteLine($"Chave PIX para pagamento: {chavePix}");
            }
        }
        private static decimal calcularTotal(List<itemCompra> itensCompra)
        {
            decimal total = 0;
            foreach (var item in itensCompra)
            {
                total += item.quantidade * item.produto.preco;
            }
            return total;
        }
        private static void salvarCompraBancoDados(List<itemCompra> itensCompra, string metodoPagamento, decimal total)
        {
            string stringConexao = @"Data Source=DESKTOP-94QHVNI;Initial Catalog=VERDEVIDA;Integrated Security=True;";
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                foreach (var item in itensCompra)
                {
                    string consulta = "INSERT INTO COMPRA (totalCompra, dataCompra, metodoPagamento) VALUES (@totalCompra, @dataCompra, @metodoPagamento)";
                    using (SqlCommand comando = new SqlCommand(consulta, conexao))
                    {
                        comando.Parameters.AddWithValue("@totalCompra", total);
                        comando.Parameters.AddWithValue("@dataCompra", DateTime.Now);
                        comando.Parameters.AddWithValue("@metodoPagamento", metodoPagamento);

                        comando.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    public class usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string dataNascimento { get; set; }
        public string endereco { get; set; }
    }

    public class Program
    {
        private static List<usuario> usuarios = new List<usuario>();

        static void Main(string[] args)

        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("************************************************************************************************************************************************************************************************************************************************");
                Console.WriteLine(" ");
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Cadastro de Usuário");
                Console.WriteLine("0. Sair");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("************************************************************************************************************************************************************************************************************************************************");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        if (login())
                        {
                            telaCompra.efetuarCompra();
                        }
                        break;
                    case "2":
                        if (cadastroUsuario())
                        {
                            Console.WriteLine("Cadastro realizado com sucesso. Faça login para continuar.");
                            if (login())
                            {
                                telaCompra.efetuarCompra();
                            }
                        }
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static bool login()
        {
            Console.Write("Digite seu email: ");
            string email = Console.ReadLine();
            Console.Write("Digite sua senha: ");
            string senha = Console.ReadLine();

            if (validarCredenciais(email, senha))
            {
                Console.WriteLine("Login realizado com sucesso!");
                return true;
            }
            else
            {
                Console.WriteLine("Email ou senha incorretos. Tente novamente.");
                return false;
            }
        }

        static bool validarCredenciais(string email, string senha)
        {
            string stringConexao = @"Data Source=DESKTOP-94QHVNI;Initial Catalog=VERDEVIDA;Integrated Security=True;";
            string consulta = "SELECT COUNT(1) FROM CLIENTE WHERE emailCliente = @Email AND senhaCliente = @Senha";

            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                SqlCommand comando = new SqlCommand(consulta, conexao);
                comando.Parameters.AddWithValue("@Email", email);
                comando.Parameters.AddWithValue("@Senha", senha);

                conexao.Open();
                int count = Convert.ToInt32(comando.ExecuteScalar());

                return count == 1;
            }
        }

        static bool cadastroUsuario()
        {
            Console.WriteLine("Cadastro de Usuário:");
            Console.Write("Nome completo: ");
            string nomeCliente = Console.ReadLine();
            Console.Write("CPF: ");
            string cpfCliente = Console.ReadLine();
            Console.Write("Data de nascimento: ");
            string dataNascimento = Console.ReadLine();
            Console.Write("Telefone: ");
            string telCliente = Console.ReadLine();
            Console.Write("Email: ");
            string emailCliente = Console.ReadLine();
            Console.Write("Senha: ");
            string senhaCliente = Console.ReadLine();
            Console.Write("CEP: ");
            string cepCliente = Console.ReadLine();
            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();

            string stringConexao = @"Data Source=DESKTOP-94QHVNI;Initial Catalog=VERDEVIDA;Integrated Security=True;";
            string consulta = "INSERT INTO CLIENTE (nomeCliente, cpfCliente, dataNascimento,  telCliente, cepCliente, endereco, emailCliente, senhaCliente) VALUES (@nomeCliente, @cpfCliente, @dataNascimento,  @telCliente, @cepCliente, @endereco, @emailCliente, @senhaCliente)";
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                SqlCommand comando = new SqlCommand(consulta, conexao);
                comando.Parameters.AddWithValue("@nomeCliente", nomeCliente);
                comando.Parameters.AddWithValue("@cpfCliente", cpfCliente);
                comando.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                comando.Parameters.AddWithValue("@telCliente", telCliente);
                comando.Parameters.AddWithValue("@cepCliente", cepCliente);
                comando.Parameters.AddWithValue("@endereco", endereco);
                comando.Parameters.AddWithValue("@emailCliente", emailCliente);
                comando.Parameters.AddWithValue("@senhaCliente", senhaCliente);
                conexao.Open();
                int resultado = comando.ExecuteNonQuery();
                if (resultado > 0)
                {
                    Console.WriteLine("Usuário cadastrado com sucesso no banco de dados.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Falha ao cadastrar usuário no banco de dados.");
                    return false;
                }
            }
        }
    }
}
