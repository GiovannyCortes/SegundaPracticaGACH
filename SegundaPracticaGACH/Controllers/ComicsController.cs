using Microsoft.AspNetCore.Mvc;
using SegundaPracticaGACH.Repositories;
using SegundaPracticaGACH.Models;

namespace SegundaPracticaGACH.Controllers {
    public class ComicsController : Controller {

        IRepositoryComics repo;

        public ComicsController(IRepositoryComics repo) {
            this.repo = repo;
        }

        public IActionResult ShowComics() {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        [HttpGet]
        public IActionResult Insert() {
            return View();
        }        
        
        [HttpPost]
        public IActionResult Insert(Comic comic) {
            this.repo.InsertComic(comic);
            return RedirectToAction("ShowComics");
        }

    }
}
