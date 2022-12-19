using PlaylistWebApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace PlaylistWebApp.Repository
{
    public class SongRepository
    {
        public IList<Song> Listar()
        {
            IList<Song> lista = new List<Song>();

            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("FiapSmartCityConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT IDSONG, NAMESONG, CATEGORIE FROM SONG  ";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    Song tipoSong  = new Song();
                    tipoSong.IdSong = Convert.ToInt32(dataReader["IDSONG"]);
                    tipoSong.Name = dataReader["NAMESONG"].ToString();
                    tipoSong.Categorie = dataReader["CATEGORIE"].ToString();

                    // Adiciona o modelo da lista
                    lista.Add(tipoSong);
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return lista;
        }

        public Song Consultar(int id)
        {

            Song tipoSong = new Song();

            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("FiapSmartCityConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT IDSONG, NAMESONG, CATEGORIE FROM SONG WHERE IDSONG = @IDSONG ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@IDSONG", SqlDbType.Int);
                command.Parameters["@IDSONG"].Value = id;
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    tipoSong.IdSong = Convert.ToInt32(dataReader["IDSONG"]);
                    tipoSong.Name = dataReader["NAMESONG"].ToString();
                    tipoSong.Categorie = dataReader["CATEGORIE"].ToString();
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return tipoSong;
        }

        public void Inserir(Song song)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("FiapSmartCityConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "INSERT INTO SONG ( NAMESONG, CATEGORIE ) VALUES ( @namesong, @categorie ) ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@namesong", SqlDbType.Text);
                command.Parameters["@namesong"].Value = song.Name;
                command.Parameters.Add("@categorie", SqlDbType.Text);
                command.Parameters["@categorie"].Value = song.Categorie;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Alterar(Song tipoSong)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("FiapSmartCityConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "UPDATE SONG SET CATEGORIE= @descr,NAMESONG = @namesong WHERE IDSONG = @id  ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@descr", SqlDbType.Text);
                command.Parameters.Add("@namesong", SqlDbType.Text);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@descr"].Value = tipoSong.Categorie;
                command.Parameters["@namesong"].Value = tipoSong.Name;
                command.Parameters["@id"].Value = tipoSong.IdSong;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Excluir(int id)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("FiapSmartCityConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "DELETE SONG WHERE IDSONG = @id  ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
