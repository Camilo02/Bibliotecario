using System;
using BibliotecaDominio.IRepositorio;

namespace BibliotecaDominio
{
    /// <summary>
    /// Clase del bibliotecario
    /// </summary>
    public class Bibliotecario
    {
        public const string EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE = "El libro no se encuentra disponible";
        public const string EL_LIBRO_ES_PALINDROMO = "Los libros palíndromos solo se pueden utilizar en la biblioteca";
        public const int DIAS_A_SUMAR = 15;
        private  IRepositorioLibro libroRepositorio;
        private  IRepositorioPrestamo prestamoRepositorio;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="libroRepositorio">Repositorio del Libro.</param>
        /// <param name="prestamoRepositorio">Repositorio del prestamo</param>
        public Bibliotecario(IRepositorioLibro libroRepositorio, IRepositorioPrestamo prestamoRepositorio)
        {
            this.libroRepositorio = libroRepositorio;
            this.prestamoRepositorio = prestamoRepositorio;
        }

        /// <summary>
        /// Metodo para prestar Libro en caso de que cumpla las validaciones.
        /// </summary>
        /// <param name="isbn">Codigo del libro.</param>
        /// <param name="nombreUsuario">Nombre de la presona pidiendo el libro.</param>
        public void Prestar(string isbn, string nombreUsuario)
        {
            if (this.EsPrestado(isbn))
            {
                throw new Exception(EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE);
            }

            if (EsPalindromo(isbn))
            {
                throw new Exception(EL_LIBRO_ES_PALINDROMO);
            }

            Libro libro = this.libroRepositorio.ObtenerPorIsbn(isbn);
            
            DateTime? fechaEntrega = null;
            if (this.SumaNumerosIsbnMayor30(isbn))
            {
                fechaEntrega = CalculoFecha.sumarDiasSinContarDomingo(DateTime.Now, DIAS_A_SUMAR);
            }
            
            Prestamo nuevoPrestamo = new Prestamo(DateTime.Now, libro, fechaEntrega, nombreUsuario);
            this.prestamoRepositorio.Agregar(nuevoPrestamo);
        }

        /// <summary>
        /// Metodo que verifica si el libro ya fue prestado.
        /// </summary>
        /// <param name="isbn">Codigo del libro</param>
        /// <returns>Booleano</returns>
        public bool EsPrestado(string isbn)
        {
            Libro libro = this.prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn);

            if (libro == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Metodo que valida si el ISBN se lee igual al derecho que al reves.
        /// </summary>
        /// <param name="isbn">Codigo del libro</param>
        /// <returns>Booleano</returns>
        public bool EsPalindromo(string isbn)
        {
            char[] caracteres = isbn.ToCharArray();
            Array.Reverse(caracteres);
            string isbnInvertido = new string(caracteres);
            if (isbn == isbnInvertido)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Metodo que valida si los numeros en el ISBN suman mas de 30.
        /// </summary>
        /// <param name="isbn">Codigo del libro.</param>
        /// <returns>booleano</returns>
        public bool SumaNumerosIsbnMayor30(string isbn)
        {
            char[] caracteres = isbn.ToCharArray();
            int sumaIsbn = 0;

            foreach  (char caracter in caracteres)
            {
                if (Char.IsDigit(caracter))
                {
                    sumaIsbn = sumaIsbn +  (int)Char.GetNumericValue(caracter);
                }
            }

            if (sumaIsbn > 30)
            {
                return true;
            }

            return false;
        }
    }
}
