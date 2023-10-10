using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMIbasik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //El único botón habilitado al momento de ejecutar el formulario
            //Es el de Open
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            btnOn.Enabled = false;
            btnOff.Enabled = false;

            //Se establecen valores por defecto para el combobox
            //EL progressbar comienza en 0 y el picturebox con la
            //imagen del Led Apagado
            cmbBaud.Text = "9600";
            pgbConnection.Value = 0;
            pbLedImages.Image = Properties.Resources.green_led_off;

            //Se crea una lista de los puertos disponibles
            string[] portList = SerialPort.GetPortNames();
            cmbPort.Items.AddRange(portList);

        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            //Estructura try/ catch para el manejo de excepciones
            try
            {
                //Obteniendo el nombre del puerto del combobox y //convirtiendo a dato numérico el valor del baud rate
                //Abiendo el puerto serial
                serialPort1.PortName = cmbPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cmbBaud.Text);
                serialPort1.Open();

                //Se deshabilita el botón open
                //Se carga el progress bar al 100%
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                btnOn.Enabled = true;
                btnOff.Enabled = true;
                pgbConnection.Value = 100;
                //velocidad de transmisión de datos en una comunicación serie.
                cmbBaud.Enabled = false;
                cmbPort.Enabled = false;

            }
            catch (Exception error)
            {
                //Si sucede algun error se despliega un mensaje 
                MessageBox.Show(error.Message);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Condicional para determinar si el puerto está abierto
            if (serialPort1.IsOpen)
            {
                try
                {
                    //Escribir en el puerto serial  Off y colocar la imagen
                    //De led apagado
                    serialPort1.WriteLine("$Off");
                    pbLedImages.Image = Properties.Resources.green_led_off;

                    //Cerrar el puerto
                    serialPort1.Close();

                    //Habilitar el botón de open y deshabilitar los demás
                    btnOpen.Enabled = true;
                    btnClose.Enabled = false;
                    btnOn.Enabled = false;
                    btnOff.Enabled = false;
                    pgbConnection.Value = 0;
                    cmbBaud.Enabled = true;
                    cmbPort.Enabled = true;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            //Si se da clic en el boton On:
            if (serialPort1.IsOpen)
            {
                try
                {
                    //Colocar la imagen del LED encendido y escribir en el
                    //Puerto serial On
                    pbLedImages.Image = Properties.Resources.green_led_on;

                    serialPort1.WriteLine("$On");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }


        }

        private void btnOff_Click(object sender, EventArgs e)
        {


            //Si se da clic en el boton apagar:
            if (serialPort1.IsOpen)
            {
                try
                {
                    //Cargar la imagen de led apagago y escribir en el puerto
                    //serial Off
                    pbLedImages.Image = Properties.Resources.green_led_off;

                    serialPort1.WriteLine("$Off");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }


        }
    }
}
