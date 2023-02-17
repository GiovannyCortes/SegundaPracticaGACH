
namespace SegundaPracticaGACH.Models {
    public class Comic {

        public int IdComic { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }


        public Comic() {
            this.IdComic = 0;
            this.Nombre = "";
            this.Imagen = "";
            this.Descripcion = "";
        }        
        
        public Comic(int idComic, string nombre, string imagen, string descripcion) {
            this.IdComic = idComic;
            this.Nombre = nombre;
            this.Imagen = imagen;
            this.Descripcion = descripcion;
        }
    }
}