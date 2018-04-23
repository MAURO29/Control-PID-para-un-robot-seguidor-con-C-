using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RobotSeguidor
{

    public partial class Form1 : Form
    {
        #region Variables, Objetos, Propiedades y Delegados
        private string Bufferint;
        private int contador;
        private DataWindow dataWindow;
        private double distanceHCSR04;
        private double disiredDistance;
        private double dataHCSR04;
        private double eP;
        private double eI;
        private double kP;
        private double kI;
        private double kproporcional;
        private double olddateInt;
        private double PromHCSR04;
        private double pwmPID;
        private double sumHCSR04;
        private double sumEint;
        delegate void ChangeContentLabelDelegate(string data);
        public double KI { get => kI; set => kI = value; }
        public double KP { get => kP; set => kP = value; }
        #endregion

        public Form1()
        {
            InitializeComponent();
            Bufferint = "";
            distanceHCSR04 = 0;
            disiredDistance = 20;
            dataHCSR04 = 0;
            eP = 0;
            eI = 0;
            KI = 0;
            KP = 0;
            kproporcional = 0;
            sumHCSR04 = 0;
            PromHCSR04 = 0;
            pwmPID = 0;
            contador = 0;
            dataWindow = new DataWindow();
            DataToolStripMenuItem.Enabled = false;
            btnConnect.Enabled = false;
            cbxPorts.Enabled = false;
            cbxrangeBaud.Enabled = false;
            iniciarToolStripMenuItem.Enabled = true;
            salirToolStripMenuItem.Enabled = false;
        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] puertos = SerialPort.GetPortNames();
            cbxPorts.Items.Clear();

            foreach (string puerto in puertos)
            {
                cbxPorts.Items.Add(puerto);
            }

            btnConnect.Enabled = true;
            cbxPorts.Enabled = true;
            cbxPorts.Text = cbxPorts.Items[0].ToString();
            cbxrangeBaud.Enabled = true;
            cbxrangeBaud.Text = 9600.ToString();
            salirToolStripMenuItem.Enabled = true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnConnect.Text == "Conectar")
                {
                    //--------------inicializando valores de puerto serial----------
                    serialPort1.BaudRate = Convert.ToInt32(cbxrangeBaud.Text);
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Parity = Parity.None;
                    serialPort1.Handshake = Handshake.None;
                    serialPort1.PortName = cbxPorts.Items[0].ToString();

                    //---------------abriendo puerto serial-------------------------

                    try
                    {
                        serialPort1.Open();
                        btnConnect.Text = "Desconectar";
                        btnConnect.BackColor = Color.Red;
                        menuStrip1.BackColor = Color.Red;
                        lbStateConections.Text = "Conexión Exitosa";
                        DataToolStripMenuItem.Enabled = true;
                        cbxPorts.Enabled = false;
                        cbxrangeBaud.Enabled = false;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        lbStateConections.Text = "Conexión fallida.";
                    }
                   
                    

                }
                else if (btnConnect.Text == "Desconectar")
                {
                    btnConnect.Text = "Conectar";
                    btnConnect.BackColor = Color.Aquamarine;
                    menuStrip1.BackColor = Color.Aquamarine;

                    //-----------------Cerrando Puerto serial-------------------
                    try
                    {
                        serialPort1.Dispose();
                        serialPort1.Close();
                        lbStateConections.Text = "Conección Terminada.";
                        dataWindow.Visible = false;
                        cbxPorts.Enabled = true;
                        cbxrangeBaud.Enabled = true;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataWindow.Visible = true;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                MessageBox.Show("La conexión serial sigue activa.");
            }
            else
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Recepción de datos del arduino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //-------------------------Recepción de datos del arduino------------------------------
            Bufferint = serialPort1.ReadLine();
            int data = 0;
            bool valnum = int.TryParse(Bufferint, out data);

            //-------------------Muestreo de datos para filtrar errores de medición----------------
                if (valnum)
                {
                    dataHCSR04 = Convert.ToDouble(Bufferint);
                    ErrorIntegral(dataHCSR04);
                    sumHCSR04 = sumHCSR04 + dataHCSR04;
                    contador++;
                }

            //--------------Promediando datos del muestreo y calculando distancias------------------
            if (contador == 9)
            {            
                PromHCSR04 = sumHCSR04 / 10;
                contador = 0;
                sumHCSR04 = 0;

                distanceHCSR04 = PromHCSR04 * 0.017;

                //-------------Mandando distancia sensada por el sensor al monitor------------------
                if (distanceHCSR04 >= 2)
                {
                    BufferContentHCSR04(Math.Round(distanceHCSR04, 1).ToString());
                    BufferOutEp(eP.ToString());
                    BufferOutEint(eI.ToString());
                    BufferOutPWM(pwmPID.ToString());
                }
                else
                {
                    BufferContentHCSR04("");
                    BufferOutEp("");
                    BufferOutEint("");
                }

                //-----------------Envio de datos al arduino-----------------------------------------
                serialPort1_SenddataProportional(distanceHCSR04);
            }
        }

        /// <summary>
        /// Mandando datos de constantes al microcontrolador
        /// </summary>
        /// <param name="distance"></param>
        private void serialPort1_SenddataProportional(double distance)
        {
            KP = dataWindow.Kpdata;
            eP = distance - disiredDistance;
            pwmPID = KP * eP + eI*KI;

            serialPort1.WriteLine(pwmPID.ToString());
        }
    
        /// <summary>
        /// Ejecutando llamadas seguras a subprocesos para modificar elementos del 
        /// formulario SensoresFrontales.
        /// </summary>
        /// <param name="data"></param>
        private void BufferContentHCSR04(string data)
        {
            if (this.dataWindow.lbDistanceHCSR04.InvokeRequired)
            {
                ChangeContentLabelDelegate CurrentContentHCSR04 = new ChangeContentLabelDelegate(BufferContentHCSR04);
                this.Invoke(CurrentContentHCSR04, new object[] { data });
            }
            else
            {
                dataWindow.lbDistanceHCSR04.Text = data;
            }
        }

        /// <summary>
        /// Llamada segura para desplegar Error proporcional en pantalla
        /// </summary>
        /// <param name="data"></param>
        private void BufferOutEp(string data)
        {
            if (this.dataWindow.Eplb.InvokeRequired)
            {
                ChangeContentLabelDelegate CurrentContentEp = new ChangeContentLabelDelegate(BufferOutEp);
                this.Invoke(CurrentContentEp, new object[] {data});
            }
            else
            {
                dataWindow.Eplb.Text = data;
            }
        }

        /// <summary>
        /// Llamada segura para desplegar Error integral en pantalla
        /// </summary>
        /// <param name="data"></param>
        private void BufferOutEint(string data)
        {
            if (this.dataWindow.Eilb.InvokeRequired)
            {
                ChangeContentLabelDelegate CurrentContentEint = new ChangeContentLabelDelegate(BufferOutEint);
                this.Invoke(CurrentContentEint, new object[] { data });
            }
            else
            {
                dataWindow.Eilb.Text = data;
            }
        }

        /// <summary>
        /// Llamada segura para desplegar Error derivativo en pantalla
        /// </summary>
        /// <param name="data"></param>
        private void BufferOutEderiv(string data)
        {
            if (this.dataWindow.Edlb.InvokeRequired)
            {
                ChangeContentLabelDelegate CurrentContentEderiv = new ChangeContentLabelDelegate(BufferOutEderiv);
                this.Invoke(CurrentContentEderiv, new object[] { data });
            }
            else
            {
                dataWindow.Edlb.Text = data;
            }
        }

        /// <summary>
        /// Llamada segura para desplegar el PWM que se enviará al motor en función
        /// de los errores calculados
        /// </summary>
        /// <param name="data"></param>
        private void BufferOutPWM(string data)
        {
            if (this.dataWindow.PWMlb.InvokeRequired)
            {
                ChangeContentLabelDelegate CurrentContentPWM = new ChangeContentLabelDelegate(BufferOutPWM);
                this.Invoke(CurrentContentPWM, new object[] {data});
            }
            else
            {
                dataWindow.PWMlb.Text = data;
            }
        }

        private async void ErrorIntegral(double data)
        {
            eI = await Task.Run(() => calculoIntegral(dataHCSR04));
            KI = dataWindow.Kidata;
            
        }

        private double calculoIntegral(double distance)
        {
            double TiempoMuestreo = 0.012;
            double sum = (dataHCSR04-olddateInt) * TiempoMuestreo;
            olddateInt = distance;
            return sumEint += sum;
        }
        
    }
}
