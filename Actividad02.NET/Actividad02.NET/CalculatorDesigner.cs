using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad02.NET
{
    public partial class frmCalculator : Form
    {
        private static int clicCE = 0;
        private static int clicSigno = 0;
        private static int clicNumero = 0;
        private static int clicHistorial = 0;
        private static string mostrar = "";
        private static string errorDiv = "No se puede divir entre cero";

        public frmCalculator()
        {
            InitializeComponent();
        }

        private void limpiar()
        {
            lblOperacion.Text = "";
            lblNumero.Text = "0";
            clicCE = 0;
            clicSigno = 0;
            clicNumero = 0;
            mostrar = "";
            Operaciones.resultado = 0;
        }

        private void primerNumero(string num)
        { 
            clicNumero = clicNumero + 1;
            if (lblNumero.Text == "0")
            {
                lblNumero.Text = num;
            }
            else if (lblOperacion.Text != "" && clicNumero == 1)
            {
                lblNumero.Text = num;
            }
            else
            {
                lblNumero.Text = lblNumero.Text + num;
            }
        }

        private string operador(string cadena)
        {
            string respuesta = "";
            string temporal = cadena.TrimEnd();
            string[] operadores = { "+", "-", "÷", "x"};
            string[] nombres = { "suma", "rest", "divi", "mult"};

            for(int i = 0; i < operadores.Length; i++)
            {
                if (temporal.Substring(temporal.Length - 1, 1) == operadores[i])
                {
                    respuesta = nombres[i];
                }
            }
            return respuesta;
        }

        private void operar(string operadroChar)
        {
            switch (operador(lblOperacion.Text))
            {
                case "suma":
                    lblOperacion.Text = lblOperacion.Text + separarNegativos(lblNumero.Text) + operadroChar;
                    lblNumero.Text = (Operaciones.suma(Convert.ToDouble(lblNumero.Text))).ToString();
                    break;

                case "rest":
                    lblOperacion.Text = lblOperacion.Text + separarNegativos(lblNumero.Text) + operadroChar;
                    lblNumero.Text = (Operaciones.resta(Convert.ToDouble(lblNumero.Text))).ToString();
                    break;

                case "mult":
                    lblOperacion.Text = lblOperacion.Text + separarNegativos(lblNumero.Text) + operadroChar;
                    lblNumero.Text = (Operaciones.multiplicacion(Convert.ToDouble(lblNumero.Text))).ToString();
                    break;

                case "divi":
                    if (lblNumero.Text != "0")
                    {
                        lblOperacion.Text = lblOperacion.Text + separarNegativos(lblNumero.Text) + operadroChar;
                        lblNumero.Text = (Operaciones.division(Convert.ToDouble(lblNumero.Text))).ToString();
                    }
                    else
                    {
                        if(lblOperacion.Text == "0 ÷ ")
                        {
                            lblOperacion.Text = lblOperacion.Text + lblNumero.Text + operadroChar;
                            lblNumero.Text = (Operaciones.division(Convert.ToDouble(lblNumero.Text))).ToString();
                        }
                        else
                        {
                            lblNumero.Text = errorDiv;
                        }
                    }
                    break;
            }
            mostrar = lblOperacion.Text;
        }

        private string separarNegativos(string numeroIn)
        {
            if (numeroIn.Substring(0, 1) == "-")
                numeroIn = "(" + numeroIn + ")";

            return numeroIn;
        }

        private void agregarHistorial(string resul, string opera)
        {
            pnlMostrar.AutoScroll = true;
            pnlMostrar.AutoScrollMargin = new Size(0, 3);
            foreach (Control control in pnlMostrar.Controls)
            {
                if(control is Panel)
                {
                    int y = control.Location.Y;
                    int x = control.Location.X;
                    control.Location = new Point(x, y + 60);
                }
            }
            
            int contador = 1;

            // Creo el panel
            Panel Nuevopanel = new Panel();

            // Diseño del panel en tiempo de ejecución
            Nuevopanel.AutoSize = true;
            Nuevopanel.Dock = DockStyle.Top;
            Nuevopanel.BackColor = Color.FromArgb(31, 31, 31);
            Nuevopanel.Location = new Point(0, 0);
            Nuevopanel.Name = "pnlResultado" + contador;
            Nuevopanel.Size = new Size(242, 68);

            // Creacion del label resultado
            Label lblHistorialRe = new Label();
            lblHistorialRe.BackColor = Color.FromArgb(31, 31, 31);
            lblHistorialRe.Dock = DockStyle.Top;
            lblHistorialRe.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHistorialRe.ForeColor = Color.White;
            lblHistorialRe.Location = new Point(0, 30);
            lblHistorialRe.Name = "lblHistorialRe";
            lblHistorialRe.Size = new Size(242, 33);
            lblHistorialRe.Text = resul;
            lblHistorialRe.TextAlign = ContentAlignment.MiddleRight;

            // Creacion del label operaciones
            Label lblHistorialOp = new Label();
            lblHistorialOp.BackColor = Color.FromArgb(31, 31, 31);
            lblHistorialOp.Dock = DockStyle.Top;
            lblHistorialOp.Font = new Font("Microsoft Sans Serif", 8F,FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistorialOp.ForeColor = Color.FromArgb(140, 140, 140);
            lblHistorialOp.Location = new Point(0, 0);
            lblHistorialOp.Name = "lblHistorialOp";
            lblHistorialOp.Size = new Size(242, 30);
            lblHistorialOp.Text = opera;
            lblHistorialOp.TextAlign = ContentAlignment.MiddleRight;

            // Creacion de la linea separadora
            Label lblSeparador = new Label();
            lblSeparador.BackColor = Color.FromArgb(75, 75, 75);
            lblSeparador.Dock = DockStyle.Top;
            lblSeparador.Location = new Point(0, 63);
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(242, 1);

            // Agregamos los labels al panel creado
            Nuevopanel.Controls.Add(lblSeparador);
            Nuevopanel.Controls.Add(lblHistorialRe);
            Nuevopanel.Controls.Add(lblHistorialOp);
            contador++;

            // Agregamos el panel creado
            pnlMostrar.Controls.Add(Nuevopanel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Calculadora";
            pnlMostrar.Visible = false;
            limpiar();
        }

        private void btnCero_Click(object sender, EventArgs e)
        {
            primerNumero("0");
        }

        private void btnUno_Click(object sender, EventArgs e)
        {
            primerNumero("1");
        }

        private void btnDos_Click(object sender, EventArgs e)
        {
            primerNumero("2");
        }

        private void btnTres_Click(object sender, EventArgs e)
        {
            primerNumero("3");
        }

        private void btnCuatro_Click(object sender, EventArgs e)
        {
            primerNumero("4");
        }

        private void btnCinco_Click(object sender, EventArgs e)
        {
            primerNumero("5");
        }

        private void btnSeis_Click(object sender, EventArgs e)
        {
            primerNumero("6");
        }

        private void btnSiete_Click(object sender, EventArgs e)
        {
            primerNumero("7");
        }

        private void btnOcho_Click(object sender, EventArgs e)
        {
            primerNumero("8");
        }

        private void btnNueve_Click(object sender, EventArgs e)
        {
            primerNumero("9");
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            clicCE = clicCE + 1;
            if (lblNumero.Text == errorDiv)
            {
                limpiar();
            }
            else if (clicCE == 1)
            {
                lblNumero.Text = "0";
            }
            else if(clicCE > 1 && lblNumero.Text != "0")
            {
                lblNumero.Text = "0";
            }
            else
            {
                limpiar();
            }
        }

        private void btnBorrarChar_Click(object sender, EventArgs e)
        {
            int longitud = 0;
            longitud = ((lblNumero.Text).Replace("-", "")).Length;

            if(lblNumero.Text == errorDiv)
            {
                limpiar();
            }
            else if(longitud > 1 && lblNumero.Text.Contains("-") == true)
            {
                lblNumero.Text = (lblNumero.Text).Substring(0,longitud);
            }
            else if(longitud > 1 && lblNumero.Text.Contains("-") == false)
            {
                lblNumero.Text = (lblNumero.Text).Substring(0, longitud - 1);
            }
            else
            {
                lblNumero.Text = "0";
            }
        }

        private void btnSigno_Click(object sender, EventArgs e)
        {
            clicSigno = clicSigno + 1;
            if(lblNumero.Text != "0")
            {
                if(clicSigno == 1)
                {
                    lblNumero.Text = "-" + lblNumero.Text;
                }
                else
                {
                    lblNumero.Text = (lblNumero.Text).Replace("-","");
                    clicSigno = 0;
                }
            }
        }

        private void btnComa_Click(object sender, EventArgs e)
        {
            if(lblNumero.Text == (Operaciones.resultado).ToString())
            {
                lblNumero.Text = "0,";
            }
            else if((lblNumero.Text).Contains(",") == false)
            {
                primerNumero(",");
            }
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            if (lblOperacion.Text == "" || (lblOperacion.Text).Count(char.IsWhiteSpace) <= 2)
            {
                lblOperacion.Text = lblNumero.Text + " = ";
                mostrar = lblOperacion.Text;
                Operaciones.resultado = Convert.ToDouble(lblNumero.Text);
                clicNumero = 0;
            }
            else
            {
                operar(" = ");
            }

            agregarHistorial(lblNumero.Text, lblOperacion.Text);
        }

        private void btnSuma_Click(object sender, EventArgs e)
        {
            clicNumero = 0;
            if (lblOperacion.Text == "")
            {
                mostrar = mostrar + lblNumero.Text + " + ";
                lblNumero.Text = (Operaciones.suma(Convert.ToDouble(lblNumero.Text))).ToString();
            }
            else if(operador(mostrar) != "suma" && lblNumero.Text == (Operaciones.resultado).ToString())
            {
                mostrar = mostrar.Substring(0, mostrar.Length - 3) + " + ";
            }
            else
            {
                operar(" + ");
            }
            lblOperacion.Text = mostrar;
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            clicNumero = 0;
            if (lblOperacion.Text == "")
            {
                mostrar = mostrar + lblNumero.Text + " - ";
                lblNumero.Text = (Operaciones.resta(Convert.ToDouble(lblNumero.Text))).ToString();
            }
            else if (operador(mostrar) != "rest" && lblNumero.Text == (Operaciones.resultado).ToString())
            {
                mostrar = mostrar.Substring(0, mostrar.Length - 3) + " - ";
            }
            else
            {
                operar(" - ");
            }
            lblOperacion.Text = mostrar;
        }

        private void btnMultiplicacion_Click(object sender, EventArgs e)
        {
            clicNumero = 0;
            if (lblOperacion.Text == "")
            {
                mostrar = mostrar + lblNumero.Text + " x ";
                lblNumero.Text = (Operaciones.multiplicacion(Convert.ToDouble(lblNumero.Text))).ToString();
            }
            else if (operador(mostrar) != "mult" && lblNumero.Text == (Operaciones.resultado).ToString())
            {
                mostrar = mostrar.Substring(0, mostrar.Length - 3) + " x ";
            }
            else
            {
                operar(" x ");
            }
            lblOperacion.Text = mostrar;
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            clicNumero = 0;
            if (lblOperacion.Text == "")
            {
                mostrar = mostrar + lblNumero.Text + " ÷ ";
                lblNumero.Text = (Operaciones.division(Convert.ToDouble(lblNumero.Text))).ToString();
            }
            else if (operador(mostrar) != "divi" && lblNumero.Text == (Operaciones.resultado).ToString())
            {
                mostrar = mostrar.Substring(0, mostrar.Length - 3) + " ÷ ";
            }
            else
            {
                operar(" ÷ ");
            }
            lblOperacion.Text = mostrar;
        }

        private void btnFactorial_Click(object sender, EventArgs e)
        {
            if(lblNumero.Text != "0")
            {
                bool signo = false;
                if ((lblNumero.Text).Contains("-") == true)
                {
                    signo = false;
                }
                else
                {
                    signo = true;
                }

                lblNumero.Text = (Operaciones.factorial(Convert.ToInt32(lblNumero.Text),signo)).ToString();
            }

        }

        private void picHistorial_MouseMove(object sender, MouseEventArgs e)
        {
            pnlMostrar.Visible = true;
            picHistorial.BackColor = Color.FromArgb(3, 190, 206);
        }

        private void picHistorial_MouseLeave(object sender, EventArgs e)
        {
            if(clicHistorial != 1)
            {
                pnlMostrar.Visible = false;
                picHistorial.BackColor = Color.FromArgb(31, 31, 31);
            }
            else
            {
                pnlMostrar.Visible = true;
                picHistorial.BackColor = Color.FromArgb(3, 190, 206);
            }
        }

        private void picHistorial_Click(object sender, EventArgs e)
        {
            clicHistorial++;
            if(clicHistorial == 1)
            {
                pnlMostrar.Visible = true;
                picHistorial.BackColor = Color.FromArgb(3, 190, 206);
            }
            else
            {
                pnlMostrar.Visible = false;
                picHistorial.BackColor = Color.FromArgb(31, 31, 31);
                clicHistorial = 0;
            }
        }
    }
}
