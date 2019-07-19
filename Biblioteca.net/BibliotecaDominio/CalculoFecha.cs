using System;

namespace BibliotecaDominio
{
    /// <summary>
    /// Clase encargada de sumar los dias que se puede prestar le libro sin contar los domingos.
    /// </summary>
    public class CalculoFecha
    {

        public CalculoFecha()
        {

        }

        public static DateTime sumarDiasSinContarDomingo(DateTime fechaAsuma, int diasAsumar)
        {
            int diasOperar = diasAsumar - 1;
            DateTime fechaCalculada = fechaAsuma;
            while (diasOperar > 0)
            {
                fechaCalculada = fechaAsuma.AddDays(1);
                fechaAsuma = fechaCalculada;
                diasOperar = DisminuirDiasNoEsdomingo(fechaAsuma, diasOperar);
            }
            return fechaAsuma;
        }

        public static int DisminuirDiasNoEsdomingo(DateTime fechaAsumar, int diasOperar)
        {
            if (NoEsDomingo(fechaAsumar))
            {
                diasOperar--;
            }
            return diasOperar;
        }

        public static Boolean NoEsDomingo(DateTime fechaAsumar)
        {
            var diasemana = fechaAsumar.DayOfWeek.ToString();
            if (diasemana != "Sunday")
            {
                return true;
            }
            return false;
        }
    }
}
