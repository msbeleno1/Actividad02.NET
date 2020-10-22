using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad02.NET
{
    class Operaciones
    {
        public static double resultado;

        public static int factorial(int n1, bool positivo)
        {
            int result = 1;
            if(positivo == true)
            {
                for (int i = 1; i <= n1; i++)
                {
                    result = result * i;
                }
            }
            else
            {
                for (int i = -1; i >= n1; i--)
                {
                    result = result * i;
                }
            }
            

            return result;
        }

        public static double suma(double n1)
        {
            resultado = resultado + n1;
            return resultado;
        }

        public static double resta(double n1)
        {
            if (resultado == 0)
            {
                resultado = n1;
            }
            else
            {
                resultado = resultado - n1;
            }

            return resultado;
        }

        public static double multiplicacion(double n1)
        {
            if (resultado == 0)
            {
                resultado = n1;
            }
            else
            {
                resultado = resultado * n1;
            }

            return resultado;
        }

        public static double division(double n1)
        {
            if (resultado == 0)
            {
                resultado = n1;
            }
            else
            {
                resultado = resultado / n1;
            }

            return resultado;
        }
    }
}
