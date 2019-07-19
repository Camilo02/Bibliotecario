using System;
using BibliotecaDominio;
using BibliotecaRepositorio.Contexto;
using BibliotecaRepositorio.Repositorio;
using DominioTest.TestDataBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DominioTest.Integracion
{

    [TestClass]
    public class BibliotecarioTest
    {
        public const String CRONICA_UNA_MUERTE_ANUNCIADA = "Cronica de una muerte anunciada";
        public const String PALINDROMO = "1991";
        public const String ISBN_CON_FECHA_ENTREGA = "95Q98R4";
        private  BibliotecaContexto contexto;
        private  RepositorioLibroEF repositorioLibro;
        private RepositorioPrestamoEF repositorioPrestamo;
        
        [TestInitialize]
        public void setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContexto>();
            contexto = new BibliotecaContexto(optionsBuilder.Options);
            repositorioLibro  = new RepositorioLibroEF(contexto);
            repositorioPrestamo = new RepositorioPrestamoEF(contexto, repositorioLibro);
        }

        [TestMethod]
        public void PrestarLibroTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn, "Juan");
            
            // Assert
            Assert.AreEqual(bibliotecario.EsPrestado(libro.Isbn), true);
            Assert.IsNotNull(repositorioPrestamo.ObtenerLibroPrestadoPorIsbn(libro.Isbn));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PrestarLibroNoDisponibleTest()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConTitulo(CRONICA_UNA_MUERTE_ANUNCIADA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            Prestamo prestamo = new Prestamo(DateTime.Now, libro, null, "Juan");
            repositorioPrestamo.Agregar(prestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn,"Juan");
            try
            {
                bibliotecario.Prestar(libro.Isbn, "Juan");
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual("El libro no se encuentra disponible", err.Message);
            }
        
        }

        [TestMethod]
        public void LibroEsPalindromo()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConIsbn(PALINDROMO).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);
            
            try
            {
                bibliotecario.Prestar(libro.Isbn, "Jorge");
                Assert.Fail();
            }
            catch (Exception err)
            {
                // Assert
                Assert.AreEqual("Los libros palíndromos solo se pueden utilizar en la biblioteca", err.Message);
            }
        }
        
        [TestMethod]
        public void FechaEntregaDiferenteNull()
        {
            // Arrange
            Libro libro = new LibroTestDataBuilder().ConIsbn(ISBN_CON_FECHA_ENTREGA).Build();
            repositorioLibro.Agregar(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro, repositorioPrestamo);

            // Act
            bibliotecario.Prestar(libro.Isbn, "Javier");

            // Assert
            Prestamo prestamo = repositorioPrestamo.Obtener(ISBN_CON_FECHA_ENTREGA);
            DateTime fechaConDomingosSumados = DateTime.Now.AddDays(15);

            Assert.IsNotNull(prestamo.FechaEntregaMaxima);
            Assert.AreNotEqual(fechaConDomingosSumados, prestamo.FechaEntregaMaxima);
        }
    }
}
