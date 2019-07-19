namespace BibliotecaDominio
{
    /// <summary>
    /// Clase Libro
    /// </summary>
    public class Libro
    {
        public string Isbn { get; }
        public string Titulo { get; }        
        public int Anio { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isbn">Codigo del libro.</param>
        /// <param name="titulo">Titulo del Libro</param>
        /// <param name="anio">Año del Libro</param>
        public Libro(string isbn, string titulo, int anio)
        {
            this.Isbn = isbn;
            this.Titulo = titulo;
            this.Anio = anio;
        }
    }
}
