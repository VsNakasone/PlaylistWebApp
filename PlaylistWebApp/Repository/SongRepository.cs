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
                    "SELECT IDSONG, NAMESONG, ARTIST FROM SONG  ";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    Song tipoSong  = new Song();
                    tipoSong.IdSong = Convert.ToInt32(dataReader["IDSONG"]);
                    tipoSong.NameSong = dataReader["NAMESONG"].ToString();
                    tipoSong.Artist = dataReader["ARTIST"].ToString();

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
                    "SELECT IDSONG, NAMESONG, ARTIST FROM SONG WHERE IDSONG = @IDSONG ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@IDSONG", SqlDbType.Int);
                command.Parameters["@IDSONG"].Value = id;
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    tipoSong.IdSong = Convert.ToInt32(dataReader["IDSONG"]);
                    tipoSong.NameSong = dataReader["NAMESONG"].ToString();
                    tipoSong.Artist = dataReader["ARTIST"].ToString();
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
                    "INSERT INTO SONG ( NAMESONG, ARTIST ) VALUES ( @namesong, @artist ) ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@namesong", SqlDbType.Text);
                command.Parameters["@namesong"].Value = song.NameSong;
                command.Parameters.Add("@artist", SqlDbType.Text);
                command.Parameters["@artist"].Value = song.Artist;

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
                    "UPDATE SONG SET ARTIST= @descr,NAMESONG = @namesong WHERE IDSONG = @id  ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@descr", SqlDbType.Text);
                command.Parameters.Add("@namesong", SqlDbType.Text);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@descr"].Value = tipoSong.Artist;
                command.Parameters["@namesong"].Value = tipoSong.NameSong;
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
