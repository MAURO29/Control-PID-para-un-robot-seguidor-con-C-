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
using System.Windows.Forms.DataVisualization.Charting;
using rtChart;

namespace RobotSeguidor
{

    public partial class Form1 : Form
    {
        #region Variables, Objetos, Propiedades y Delegados
        private Random num = new Random();
        delegate void ChangeContentLabelDelegate(string data);
        private DataWindow dataWindow;
        private Grafica graphicDistance;
        private string Bufferint;
        private int contador;
        private double distanceHCSR04;
        private double disiredDistance;
        private double dataHCSR04;
        private double dataoldHCSR04;
        private double eD;
        private double eP;
        private double eI;
        private double kP;
        private double kD;
        private double kI;
        private double olddataInt;
        private double PromHCSR04;
        private double PromTime;
        private double pwmPID;
        private double sumHCSR04;
        private double sumEint;
        private double sumTime;
        private double time;
        public double KI { get => kI; set => kI = value; }
        public double KP { get => kP; set => kP = value; }
        public double KD { get => kD; set => kD = value; }
        #endregion

        /// <summary>
        /// Inicializando variables
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            dataWindow = new DataWindow();
            graphicDistance = new Grafica();
            btnConnect.Enabled = false;
            cbxPorts.Enabled = false;
            cbxrangeBaud.Enabled = false;
            DataToolStripMenuItem.Enabled = false;
            iniciarToolStripMenuItem.Enabled = true;
            salirToolStripMenuItem.Enabled = false;
            Bufferint = "";
            contador = 0;
            distanceHCSR04 = 0;
            disiredDistance = 10;
            dataHCSR04 = 0;
            dataoldHCSR04 = 0;
            eP = 0;
            eI = 0;
            KI = 0;
            KP = 0;
            PromHCSR04 = 0;
            PromTime = 0;
            pwmPID = 0;
            sumHCSR04 = 0;
            sumTime = 0;
            time = 0;
        }

        /// <summary>
        /// Buscador de puertos seriales disponibles y activando controles del menú gráfico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Botón que permite la conexión y desconexión del puerto serial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    serialPort1.PortName = cbxPorts.Text;

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
                        DataToolStripMenuItem.Enabled = false;
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

        /// <summary>
        /// Muestra la ventada de datos al hacer click en la pestaña desplegar datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataWindow.Visible = true;   
        }

        /// <summary>
        /// Finaliza la aplicación en curso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                MessageBox.Show("La conexión serial sigue activa.");  //Revisa que la conexión este cerrada para cerrar la aplicación
            }
            else
            {
                this.Dispose();  //Finaliza la aplicación
            }
        }

        /// <summary>
        /// Recepción de datos del puerto serial enviados por arduino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //-------------------------Recepción de datos del arduino------------------------------
            Bufferint = serialPort1.ReadLine();
            string[] datasReceived = Bufferint.Split(',');

            if (datasReceived.Length == 2)
            {
                double data1 = 0;
                double data2 = 0;
                bool valnum1 = double.TryParse(datasReceived[0], out data1);
                bool valnum2 = double.TryParse(datasReceived[1], out data2);

                //-------------------Muestreo de datos para filtrar errores de medición----------------
                if (valnum1 && valnum2)  // Validando que el dato recibido sea un número
                {
                    dataHCSR04 = data1;
                    time = data2;
                    ErrorIntegral(dataHCSR04);
                    sumHCSR04 = sumHCSR04 + dataHCSR04;
                    sumTime = sumTime + time;
                    contador++;
                }

                //--------------Promediando datos del muestreo y calculando distancias------------------
                if (contador == 9)
                {
                    PromHCSR04 = sumHCSR04 / 10;
                    PromTime = sumTime / 10;
                    contador = 0;
                    sumHCSR04 = 0;
                    sumTime = 0;

                    distanceHCSR04 = PromHCSR04 * 0.017;
                    ErrorProporcional(distanceHCSR04);
                    ErrorDerivativo(distanceHCSR04);

                    //-------------Mandando distancia sensada por el sensor al monitor------------------
                    if (distanceHCSR04 >= 2)
                    {
                        BufferContentHCSR04(Math.Round(distanceHCSR04, 1).ToString());
                        BufferOutEp(eP.ToString());
                        BufferOutEint(eI.ToString());
                        BufferOutEderiv(eD.ToString());
                        BufferOutPWM(pwmPID.ToString());
                    }
                    else
                    {
                        BufferContentHCSR04("");
                        BufferOutEp("");
                        BufferOutEint("");
                        BufferOutEderiv("");
                        BufferOutPWM("");
                    }

                    //-----------------Envio de datos al arduino y graficación---------------------------
                    serialPort1_SenddataProportional();
                    //kayGraphic.TriggeredUpdate(distanceHCSR04);
                    //graphicDistance.GraphicDistance.Series[1].Points.AddY(5);
                    //graphicDistance.GraphicDistance.Refresh();
                    //graphicDistance.GraphicDistance.Series[1].Points.AddXY(PromTime, 10);
                }
            }
           

            
        }

        /// <summary>
        /// Mandando datos de constantes al microcontrolador
        /// </summary>
        /// <param name="distance"></param>
        private void serialPort1_SenddataProportional()
        {
            

            pwmPID = KP * eP + KI * eI + KD * eD;
            serialPort1.WriteLine(pwmPID.ToString());

            //if (pwmPID > 28 && pwmPID < 60)
            //{
            //    pwmPID = 40; //Saturando el pwm enviado al controlador 
            //    serialPort1.WriteLine(pwmPID.ToString());
            //}
            //else if (pwmPID < -28 && pwmPID > -60)
            //{
            //    pwmPID = 40; //Saturando el pwm enviado al controlador
            //    serialPort1.WriteLine(pwmPID.ToString());
            //}
            //else
            //{
            //    serialPort1.WriteLine(pwmPID.ToString());
            //}
        }
    
        /// <summary>
        /// Llamada segura para desplegar distancia sensador por el HCSR04
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


        /// <summary>
        /// Cálculo del error proporcional
        /// </summary>
        /// <param name="data"></param>
        private async void ErrorProporcional(double data)
        {
            eP = await Task.Run(() => data - disiredDistance);
            KP = dataWindow.Kpdata;
        }

        /// <summary>
        /// Cálculo del error integral
        /// </summary>
        /// <param name="data"></param>
        private async void ErrorIntegral(double data)
        {
            eI = await Task.Run(() => PromedioIntegral(dataHCSR04));
            KI = dataWindow.Kidata;  
        }

        /// <summary>
        /// Calculo del error derivativo
        /// </summary>
        /// <param name="data"></param>
        private async void ErrorDerivativo(double data)
        {
            eD = await Task.Run(() => calculoDerivativo(dataHCSR04));
            KD = dataWindow.Kddata;
        }

        /// <summary>
        /// Cálcula el promedio de los datos recibidos por el microcontrolador
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private double PromedioIntegral(double distance)
        {
            double TiempoMuestreo = 0.012;
            double sum =Math.Round((dataHCSR04 - olddataInt) * TiempoMuestreo, 1) ;
            olddataInt = distance;
            if (distance == 0)
            {
                sumEint = 0;
            }
           
            return sumEint += sum;
        }

        /// <summary>
        /// Cálculo derivativo
        /// </summary>
        /// <param name="distances"></param>
        /// <returns></returns>
        private double calculoDerivativo(double distances)
        {
            double TiempoMuestreo = 0.012;
            double diferencia = Math.Round((distances - dataoldHCSR04) / TiempoMuestreo, 1) ;
            dataoldHCSR04 = distances;
            return diferencia;
        }

        private void graficarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphicDistance.Visible = true;
        }

      
    }
}
