using SegundaPracticaGACH.Models;

namespace SegundaPracticaGACH.Repositories {
    public interface IRepositoryComics {

        List<Comic> GetComics();
        Comic GetComic(int idcomic);
        void InsertComic(Comic comic);

    }
}
