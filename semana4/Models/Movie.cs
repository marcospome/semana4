using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha_Estreno { get; set; }
        public string Genero { get; set; }
        public decimal Precio { get; set; }
    }
}