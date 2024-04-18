using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace semana4.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Simulación de creación de un objeto (model)
            //Mas adelante vamos a ver como usar una base de datos
            var movie = new Movie
            {
                Genero = "Terror",
                Id = 1,
                Precio = 1,
                Fecha_Estreno = DateTime.Now,
                Titulo = "La noche del terror"
            };


            return View(movie);
        }



        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var listMovies = new List<Movie>();

            var movie1 = new Movie
            {
                Genero = "Terror",
                Id = 1,
                Precio = 250,
                Fecha_Estreno = DateTime.Now,
                Titulo = "La noche del terror"
            };
            listMovies.Add(movie1);

            var movie2 = new Movie
            {
                Genero = "Terror",
                Id = 1,
                Precio = 300,
                Fecha_Estreno = DateTime.Now,
                Titulo = "La noche del terror II"
            };
            listMovies.Add(movie2);
            var movie3 = new Movie
            {
                Genero = "Animada",
                Id = 1,
                Precio = 350,
                Fecha_Estreno = DateTime.Now,
                Titulo = "Sherk"
            };
            listMovies.Add(movie3);
            var movie4 = new Movie
            {
                Genero = "Ciencia Ficción",
                Id = 1,
                Precio = 500,
                Fecha_Estreno = DateTime.Now,
                Titulo = "Dune"
            };
            listMovies.Add(movie4);

            return View(listMovies);

        }
    }
}
