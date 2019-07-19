using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DominioTest.Unitarias
{
    public class MetedoFecha
    {
        public MetedoFecha()
        {

        }

        public DateTime generarFechaEntregaMaxima(DateTime fechaIngreso, int diasSumar)
        {
            var fehcaEntregaMaxima = sumarDiasSinContarEsDomingos(fechaIngreso, diasSumar);
            return fehcaEntregaMaxima;
        }

        public DateTime sumarDiasSinContarEsDomingos(DateTime fechaAsumar, int diasSumar)
        {
            var diasAoperar = diasSumar - 1;
            var fechaEntrega = new DateTime();
            while (diasAoperar > 0)
            {
                if (EsDomingo(fechaAsumar))
                {
                    fechaAsumar = fechaAsumar.AddDays(1);
                    fechaEntrega = fechaAsumar;
                    diasAoperar--;
                }
                else
                {
                    fechaAsumar = fechaAsumar.AddDays(1);
                    fechaEntrega = fechaAsumar;
                }
            }
            return fechaEntrega;
        }

        public bool EsDomingo(DateTime fechaOperar)
        {
            return fechaOperar.DayOfWeek.ToString() != "Sunday" ?  true :  false;
        }
    }

    //[TestClass]
    //public class testFEcha
    //{
    //    [TestMethod]
    //    public void tetFechaEntrega()
    //    {
    //        // Arragne
    //        var fechasolicitud = DateTime.Now;
    //        var diasSumar = 15;
    //        MetedoFecha mf = new MetedoFecha();

    //        //Act
    //        var resultadoFecha = mf.sumarDiasSinContarEsDomingos(fechasolicitud, diasSumar);

    //        // Assert
    //        Assert.AreEqual("04", resultadoFecha.Day.ToString());
    //        Assert.AreEqual("08", resultadoFecha.Month.ToString());
    //        Assert.AreEqual("2018", resultadoFecha.Year.ToString());

    //    }
    //}
}

