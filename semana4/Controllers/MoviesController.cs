using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using Microsoft.Extensions.Caching.Memory;

namespace semana4.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMemoryCache _cache;

        public MoviesController(IMemoryCache cache)
        {
            _cache = cache;

            // Inicialización de la lista de películas en el caché si no existe
            if (!_cache.TryGetValue("Movies", out List<Movie> movies))
            {
                movies = new List<Movie>
                {
                    new Movie { Id = 1, Titulo = "La noche del terror", Genero = "Terror", Precio = 250, Fecha_Estreno = DateTime.Now },
                    new Movie { Id = 2, Titulo = "La noche del terror II", Genero = "Terror", Precio = 300, Fecha_Estreno = DateTime.Now },
                    new Movie { Id = 3, Titulo = "Sherk", Genero = "Animada", Precio = 350, Fecha_Estreno = DateTime.Now },
                    new Movie { Id = 4, Titulo = "Dune", Genero = "Ciencia Ficción", Precio = 500, Fecha_Estreno = DateTime.Now }
                };
                _cache.Set("Movies", movies);
            }
        }

        // GET: Movies
        public IActionResult Index()
        {
            var movies = _cache.Get<List<Movie>>("Movies");
            return View(movies);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = _cache.Get<List<Movie>>("Movies");
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Titulo,Genero,Precio,Fecha_Estreno")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                var movies = _cache.Get<List<Movie>>("Movies");
                movie.Id = movies.Max(m => m.Id) + 1;
                movies.Add(movie);
                _cache.Set("Movies", movies);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = _cache.Get<List<Movie>>("Movies");
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Titulo,Genero,Precio,Fecha_Estreno")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var movies = _cache.Get<List<Movie>>("Movies");
                var existingMovie = movies.FirstOrDefault(m => m.Id == id);
                if (existingMovie != null)
                {
                    existingMovie.Titulo = movie.Titulo;
                    existingMovie.Genero = movie.Genero;
                    existingMovie.Precio = movie.Precio;
                    existingMovie.Fecha_Estreno = movie.Fecha_Estreno;
                    _cache.Set("Movies", movies);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = _cache.Get<List<Movie>>("Movies");
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movies = _cache.Get<List<Movie>>("Movies");
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                movies.Remove(movie);
                _cache.Set("Movies", movies);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
