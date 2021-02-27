using System.Collections.Generic;
using Cookie_Shop.Models;
using MySqlConnector;

namespace Asp.netComBanco.Models
{
    public class CookiesRepository
    {
        private const string _strConexao = "Server=den1.mysql6.gear.host;Database=cookiesshop;Uid=cookiesshop;Pwd=Qp12_V~2VIoh";

        public void Insert(Cookie novoCookie)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "INSERT INTO Cookie(nome, preco, quantidade) VALUES (@nome, @preco, @quantidade)";
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", novoCookie.nome);
            comando.Parameters.AddWithValue("@preco", novoCookie.preco);
            comando.Parameters.AddWithValue("@quantidade", novoCookie.quantidade);
            comando.ExecuteNonQuery();
            conexao.Close();
        }


        public Cookie FindById(int? id)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "SELECT * FROM Cookies WHERE( Cookies.id = @id)";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            Cookie cookie = new Cookie();
            while (reader.Read())
            {
                cookie.id = reader.GetInt32("id");
                cookie.nome = reader.GetString("nome");
                cookie.preco = reader.GetInt32("preco");
                cookie.quantidade = reader.GetInt32("quantidade");


            }
            return cookie;
        }
        public void Atualizar(Cookie cookie)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "UPDATE Cookies SET nome = @nome, preco = @preco, quantidade = @quantidade WHERE(Usuario.id = @id)";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", cookie.id);
            comandoQuery.Parameters.AddWithValue("@Nome", cookie.nome);
            comandoQuery.Parameters.AddWithValue("@Login", cookie.preco);
            comandoQuery.Parameters.AddWithValue("@Senha", cookie.quantidade);

            MySqlDataReader reader = comandoQuery.ExecuteReader();

        }

        public void Deletar(int? id)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "DELETE FROM Cookies WHERE id = @id;";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = comandoQuery.ExecuteReader();

        }

        public List<Cookie> Listagem()
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            string sql = "SELECT * FROM Cookies";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            List<Cookie> lista = new List<Cookie>();
            while (reader.Read())
            {
                Cookie cookie = new Cookie();
                cookie.id = reader.GetInt32("id");

                if (!reader.IsDBNull(reader.GetOrdinal("nome")))
                    cookie.nome = reader.GetString("nome");
                if (!reader.IsDBNull(reader.GetOrdinal("preco")))
                    cookie.preco = reader.GetInt32("preco");
                if (!reader.IsDBNull(reader.GetOrdinal("quantidade")))
                    cookie.quantidade = reader.GetInt32("quantidade");
                lista.Add(cookie);
            }
            conexao.Close();
            return lista;
        }

        public List<Cookie> Carrinho(int idUsuario)
        {
            MySqlConnection conexao = new MySqlConnection(_strConexao);
            conexao.Open();
            List<Cookie> lista = new List<Cookie>();
            string sql = "SELECT Cookies.id, Cookies.nome, Cookies.preco, Cookies.quantidade FROM Cookies, Pedido WHERE Pedido.idUsuario = @idUsuario AND Pedido.pago = false AND Cookies.id = Pedido.idCookie;";
            MySqlCommand comandoQuery = new MySqlCommand(sql, conexao);
            comandoQuery.Parameters.AddWithValue("@idUsuario", idUsuario);
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            while (reader.Read())
            {
                Cookie cookie = new Cookie();
                cookie.id = reader.GetInt32("id");

                if (!reader.IsDBNull(reader.GetOrdinal("nome")))
                    cookie.nome = reader.GetString("nome");
                if (!reader.IsDBNull(reader.GetOrdinal("preco")))
                    cookie.preco = reader.GetInt32("preco");
                if (!reader.IsDBNull(reader.GetOrdinal("quantidade")))
                    cookie.quantidade = reader.GetInt32("quantidade");
                lista.Add(cookie);
            }


            conexao.Close();
            return lista;
        }
    }
}
