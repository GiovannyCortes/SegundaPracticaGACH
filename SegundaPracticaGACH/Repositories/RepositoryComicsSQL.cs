using SegundaPracticaGACH.Models;
using System.Data;
using System.Data.SqlClient;

#region PROCEDURES
/*
    CREATE PROCEDURE SP_INSERT_COMIC (@NOMBRE NVARCHAR(150), @IMAGEN NVARCHAR(600), @DESCRIPCION NVARCHAR(500))
    AS
        DECLARE @IDCOMIC INT;
	    SET @IDCOMIC = (SELECT MAX(IDCOMIC) + 1 FROM COMICS)
	    INSERT INTO COMICS VALUES(@IDCOMIC, @NOMBRE, @IMAGEN, @DESCRIPCION)
    GO
 */
#endregion

namespace SegundaPracticaGACH.Repositories {
    public class RepositoryComicsSQL : IRepositoryComics {

        SqlConnection connection;
        SqlCommand command;
        DataTable tabla_comics;

        public RepositoryComicsSQL() {
            string connectionString =
                @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";

            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;

            // LinQ
            string sql_hospital = "SELECT * FROM COMICS";
            SqlDataAdapter adapter = new SqlDataAdapter(sql_hospital, connectionString);
            this.tabla_comics = new DataTable();
            adapter.Fill(this.tabla_comics);
        }

        public List<Comic> GetComics() {
            var consulta = (from datos in this.tabla_comics.AsEnumerable()
                            select new Comic {
                                IdComic = datos.Field<int>("IDCOMIC"),
                                Nombre = datos.Field<string>("NOMBRE"),
                                Imagen = datos.Field<string>("IMAGEN"),
                                Descripcion = datos.Field<string>("DESCRIPCION"),
                            });
            return consulta.ToList();
        }

        public Comic GetComic(int idcomic) {
            var consulta = (from datos in this.tabla_comics.AsEnumerable()
                            where datos.Field<int>("IDCOMIC") == idcomic
                            select new Comic {
                                IdComic = datos.Field<int>("IDCOMIC"),
                                Nombre = datos.Field<string>("NOMBRE"),
                                Imagen = datos.Field<string>("IMAGEN"),
                                Descripcion = datos.Field<string>("DESCRIPCION"),
                            });
            return consulta.FirstOrDefault();
        }

        public void InsertComic(Comic comic) {
            this.command.CommandText = "SP_INSERT_COMIC";
            this.command.CommandType = CommandType.StoredProcedure;

            SqlParameter pamnombre = new SqlParameter("@NOMBRE", comic.Nombre);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", comic.Imagen);
            SqlParameter pamdescri = new SqlParameter("@DESCRIPCION", comic.Descripcion);

            this.command.Parameters.Add(pamnombre);
            this.command.Parameters.Add(pamimagen);
            this.command.Parameters.Add(pamdescri);

            this.connection.Open();
            this.command.ExecuteNonQuery();
            this.connection.Close();
            this.command.Parameters.Clear();
        }
    }
}
