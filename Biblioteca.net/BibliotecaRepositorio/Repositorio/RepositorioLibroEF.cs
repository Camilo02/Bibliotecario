using System.Linq;
using BibliotecaDominio;
using BibliotecaDominio.IRepositorio;
using BibliotecaRepositorio.Builder;
using BibliotecaRepositorio.Contexto;
using BibliotecaRepositorio.Entidades;

namespace BibliotecaRepositorio.Repositorio
{
    public class RepositorioLibroEF : IRepositorioLibro, IRepositorioLibroEF
    {
        private readonly BibliotecaContexto bibliotecaContexto;

        public RepositorioLibroEF(BibliotecaContexto bibliotecaContexto)
        {
            this.bibliotecaContexto = bibliotecaContexto;
        }
        
        /// <summary>
        /// Permite obtener un libro suando el isbn.
        /// </summary>
        /// <param name="isbn">Codigo del libro.</param>
        /// <returns></returns>
        public Libro ObtenerPorIsbn(string isbn)
        {
            LibroEntidad libroEntidad = ObtenerLibroEntidadPorIsbn(isbn);
            return LibroBuilder.ConvertirADominio(libroEntidad);
        }

        /// <summary>
        /// Metodo para Agregar el Libro al contexto.
        /// </summary>
        /// <param name="libro"></param>
        public void Agregar(Libro libro)
        {
            bibliotecaContexto.Libros.Add(LibroBuilder.ConvertirAEntidad(libro));
            bibliotecaContexto.SaveChanges();
        }

        public LibroEntidad ObtenerLibroEntidadPorIsbn(string isbn)
        {
            var libroEntidad = bibliotecaContexto.Libros.FirstOrDefault(libro => libro.Isbn == isbn);
            return libroEntidad;
        }
    }
}
