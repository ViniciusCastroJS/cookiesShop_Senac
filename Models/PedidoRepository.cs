using System.Collections.Generic;
using Cookie_Shop.Models;
using MySqlConnector;

namespace Asp.netComBanco.Models
{
    public class PedidoRepository
    {
        private const string _strConexao = "Server=den1.mysql6.gear.host;Database=cookiesshop;Uid=cookiesshop;Pwd=Qp12_V~2VIoh";

        public void Insert(Cliente novocCliente)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "INSERT INTO Pedi(nome, email, senha, CPF) VALUES (@nome, @email, @senha, @cpf)";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", novocCliente.nome);
            comando.Parameters.AddWithValue("@email", novocCliente.email);
            comando.Parameters.AddWithValue("@senha", novocCliente.senha);
            comando.Parameters.AddWithValue("@cpf", novocCliente.CPF);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void FazerPedido(int idCookie, int idUsuario)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "INSERT INTO Pedido (idUsuario, idCookie, quantidade, pago) VALUES (@idUsuario, @idCookie, 1, false);";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);
            comando.Parameters.AddWithValue("@idCookie", idCookie);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void Atualizar(Cliente cliente)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "UPDATE Pedido SET nome = @nome, email = @email, senha = @senha, CPF = @cpf WHERE(Clientes.id = @id)";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", cliente.id);
            comandoQuery.Parameters.AddWithValue("@nome", cliente.nome);
            comandoQuery.Parameters.AddWithValue("@email", cliente.email);
            comandoQuery.Parameters.AddWithValue("@senha", cliente.senha);
            comandoQuery.Parameters.AddWithValue("@cpf", cliente.CPF);

            MySqlDataReader reader = comandoQuery.ExecuteReader();

        }

        public void Deletar(int? id)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "DELETE FROM Pedido WHERE id = @id;";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = comandoQuery.ExecuteReader();

        }

        public List<Pedido> Listar(int id)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "SELECT * FROM Pedido WHERE idUsuario = @id and pago = false";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            List<Pedido> lista = new List<Pedido>();
            while (reader.Read())
            {
                Pedido pedido = new Pedido();
                pedido.id = reader.GetInt32("id");

                if (!reader.IsDBNull(reader.GetOrdinal("idUsuario")))
                    pedido.idUsuario = reader.GetInt32("idUsuario");
                if (!reader.IsDBNull(reader.GetOrdinal("idCookie")))
                    pedido.idCookie = reader.GetInt32("idCookie");
                if (!reader.IsDBNull(reader.GetOrdinal("pago")))
                    pedido.pago = reader.GetBoolean("pago");
                if (!reader.IsDBNull(reader.GetOrdinal("quantidade")))
                    pedido.quantidade = reader.GetInt32("quantidade");

                lista.Add(pedido);
            }
            conexao.Close();
            return lista;
        }

        public List<Cliente> Listagem()
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "SELECT * FROM Pedido ORDER BY nome";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            List<Cliente> lista = new List<Cliente>();
            while (reader.Read())
            {
                Cliente usr = new Cliente();
                usr.id = reader.GetInt32("id");

                if (!reader.IsDBNull(reader.GetOrdinal("nome")))
                    usr.nome = reader.GetString("nome");
                if (!reader.IsDBNull(reader.GetOrdinal("email")))
                    usr.email = reader.GetString("email");
                if (!reader.IsDBNull(reader.GetOrdinal("senha")))
                    usr.senha = reader.GetString("senha");
                if (!reader.IsDBNull(reader.GetOrdinal("CPF")))
                    usr.CPF = reader.GetString("CPF");
                lista.Add(usr);
            }
            conexao.Close();
            return lista;
        }
    }
}
