using System.Collections.Generic;
using Cookie_Shop.Models;
using MySqlConnector;

namespace Asp.netComBanco.Models
{
    public class ClienteRepository
    {
        private const string _strConexao = "Server=den1.mysql6.gear.host;Database=cookiesshop;Uid=cookiesshop;Pwd=Qp12_V~2VIoh";

        public void Insert(Cliente novocCliente)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "INSERT INTO Clientes(nome, email, senha, CPF) VALUES (@nome, @email, @senha, @cpf)";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", novocCliente.nome);
            comando.Parameters.AddWithValue("@email", novocCliente.email);
            comando.Parameters.AddWithValue("@senha", novocCliente.senha);
            comando.Parameters.AddWithValue("@cpf", novocCliente.CPF);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public Cliente FindById(int? id)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "SELECT * FROM Clientes WHERE( Clientes.id = @id)";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            Cliente cliente = new Cliente();
            while (reader.Read())
            {
                cliente.id = reader.GetInt32("id");
                cliente.nome = reader.GetString("nome");
                cliente.senha = reader.GetString("senha");
                cliente.email = reader.GetString("email");
                cliente.CPF = reader.GetString("CPF");


            }
            return cliente;
        }

        public void Atualizar(Cliente cliente)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "UPDATE Clientes SET nome = @nome, email = @email, senha = @senha, CPF = @cpf WHERE(Clientes.id = @id)";
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
            string sql = "DELETE FROM Usuario WHERE id = @id;";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = comandoQuery.ExecuteReader();

        }

        public List<Cliente> Listagem()
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "SELECT * FROM Usuario ORDER BY nome";
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
