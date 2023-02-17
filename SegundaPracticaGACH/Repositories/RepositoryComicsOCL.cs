using System.Data;
using Oracle.ManagedDataAccess.Client;
using SegundaPracticaGACH.Models;

#region PROCEDURES
/*
    CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC
    (P_NOMBRE COMICS.NOMBRE%TYPE, P_IMAGEN COMICS.IMAGEN%TYPE, P_DESCRIPCION COMICS.DESCRIPCION%TYPE)
    AS P_COMIC_COD INT;
    BEGIN
      SELECT MAX(IDCOMIC)+1 INTO P_COMIC_COD FROM COMICS;
      INSERT INTO COMICS VALUES(P_COMIC_COD, P_NOMBRE, P_IMAGEN, P_DESCRIPCION);
      COMMIT;
    END;
 */
#endregion

namespace SegundaPracticaGACH.Repositories {
    public class RepositoryComicsOCL : IRepositoryComics {

        OracleConnection connection;
        OracleCommand command;
        DataTable tabla_comics;

        public RepositoryComicsOCL() {
            string connectionString =
                @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True;User Id=SYSTEM;Password=oracle";

            this.connection = new OracleConnection(connectionString);
            this.command = new OracleCommand();
            this.command.Connection = this.connection;

            // LinQ
            string sql_hospital = "SELECT * FROM COMICS";
            OracleDataAdapter adapter = new OracleDataAdapter(sql_hospital, connectionString);
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

            OracleParameter pamnombre = new OracleParameter("P_NOMBRE", comic.Nombre);
            OracleParameter pamimagen = new OracleParameter("P_IMAGEN", comic.Imagen);
            OracleParameter pamdescri = new OracleParameter("P_DESCRIPCION", comic.Descripcion);

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
