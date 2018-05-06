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
using System.IO;
using System.Runtime.CompilerServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace RobotSeguidor
{

    public partial class Form1 : Form
    {
        #region Variables, Objetos, Propiedades y Delegados
        //delegados
        private delegate void ChangeContentLabelDelegate(string data);
        private delegate void UpdateGraph(double data1, double data2);

        //Forms
        private DataWindow dataWindow;
        private Graphic graphicDistance;

        //Propiedades
        public double Ed { get; private set; }
        public double Ep { get; private set; }
        public double Ei { get; private set; }
        public double Kp { get; private set; }
        public double Kd { get; private set; }
        public double Ki { get; private set; }
        public double DisiredDistance { get; set; }
        public Excel.Application Posiciones1 { get; set; } = default(Excel.Application);
        public Excel.Workbook LibroPosiciones { get; set; } = default(Excel.Workbook);
        public Excel.Worksheet HojaPosiciones { get; set; } = default(Excel.Worksheet);
        public List<double> Errores { get; set; }
        public List<double> Posiciones { get; set; }

        //Variables
        private string _bufferint;
        private int _contador;
        private double _data1;
        private double _distanceHcsr04;
        private double _dataHcsr04;
        private double _dataoldHcsr04;
        private double _kpaux;
        private double _kiaux;
        private double _kdaux;
        private double _olddataInt;
        private double _promHcsr04;
        private double _promTime;
        private double _pwmPid;
        private double _sumHcsr04;
        private double _sumEint;
        private double _sumTime;
        private double _time;
        private int i = 0;
        private int _j1 = 3;
        private int _j2 = 3;
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Inicializando variables
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            dataWindow = new DataWindow();
            graphicDistance = new Graphic();
            btnConnect.Enabled = false;
            cbxPorts.Enabled = false;
            cbxrangeBaud.Enabled = false;
            DataToolStripMenuItem.Enabled = false;
            graficarToolStripMenuItem.Enabled = false;
            generarExcelToolStripMenuItem.Enabled = false;
            iniciarToolStripMenuItem.Enabled = true;
            salirToolStripMenuItem.Enabled = false;
            _contador = 0;
            _distanceHcsr04 = 0;
            DisiredDistance = 20;
            _dataHcsr04 = 0;
            _dataoldHcsr04 = 0;
            Errores = new List<double>();
            Ep = 0;
            Ei = 0;
            Ki = 0;
            Kp = 0;
            Posiciones = new List<double>();
            _promHcsr04 = 0;
            _promTime = 0;
            _pwmPid = 0;
            _sumHcsr04 = 0;
            _sumTime = 0;
            _time = 0;

            //Iniciando archivo de excel a generar
            Posiciones1 = new Excel.Application();
            LibroPosiciones = Posiciones1.Workbooks.Add(); //Creando instancia del Workbooks de excel
            HojaPosiciones = LibroPosiciones.Worksheets[1]; //Creando una instancia de la primer hoja de trabajo de excel
            HojaPosiciones.Visible = Excel.XlSheetVisibility.xlSheetVisible;
            HojaPosiciones.Activate();
            HojaPosiciones.Range["A1:B1"].Merge(); //Creamos celda para Encabezado
            HojaPosiciones.Range["A1:B1"].Value = "Posiciones obtenidas"; //Asignando nombre al encabezado
            HojaPosiciones.Range["A1:B1"].Font.Bold = true; //asignando negrita al encabezado
            HojaPosiciones.Range["A1:B1"].Font.Size = 15; //Ajustando tamaño
            var objCelda1 = HojaPosiciones.Range["A2", Type.Missing];
            objCelda1.Value = "Distancia Medida";
            objCelda1.Columns.AutoFit();
            var objCelda2 = HojaPosiciones.Range["B2", Type.Missing];
            objCelda2.Value = "Error medido";
            objCelda2.Columns.AutoFit();
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
                        cbxPorts.Enabled = false;
                        cbxrangeBaud.Enabled = false;
                        DataToolStripMenuItem.Enabled = true;
                        graficarToolStripMenuItem.Enabled = true;
                        generarExcelToolStripMenuItem.Enabled = false;
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
                        cbxPorts.Enabled = true;
                        cbxrangeBaud.Enabled = true;
                        dataWindow.Visible = false;
                        graphicDistance.Visible = false;
                        DataToolStripMenuItem.Enabled = false;
                        graficarToolStripMenuItem.Enabled = false;
                        generarExcelToolStripMenuItem.Enabled = true;
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
            _bufferint = serialPort1.ReadLine();

            //-------------Convirtiendo valores recibidos a datos tipo double------------------

            bool valnum1 = double.TryParse(_bufferint, out _data1);

            //-------------------Muestreo de datos para filtrar errores de medición-------------

            if (valnum1)  // Validando que el dato recibido sea un número
            {
                _dataHcsr04 = _data1;
                ErrorIntegral(_dataHcsr04 * 0.017);
                _sumHcsr04 = _sumHcsr04 + _dataHcsr04;
                _contador++;
            }

            //--------------Promediando datos del muestreo y calculando distancias------------------

            if (_contador == 29)
            {
                _promHcsr04 = _sumHcsr04 / 30;
                _contador = 0;
                _sumHcsr04 = 0;
                _sumTime = 0;

                _distanceHcsr04 = _promHcsr04 * 0.017;
                ErrorProporcional(_distanceHcsr04);
                ErrorDerivativo(_distanceHcsr04);

                //-------------Mandando distancia sensada por el sensor al monitor------------------

                BufferContentHcsr04(Math.Round(_distanceHcsr04, 1).ToString());
                BufferOutEp(Ep.ToString());
                BufferOutEint(Ei.ToString());
                BufferOutEderiv(Ed.ToString());
                BufferOutPwm(_pwmPid.ToString());

                //-----------------Envio de datos al arduino y graficación---------------------------
                serialPort1_SenddataProportional(_distanceHcsr04);
                Updategraph(i++, _distanceHcsr04);
                serialPort1.DiscardInBuffer();

                //-----------------Añadiendo datos a lista para su ingreso al excel-------------------
                Posiciones.Add(_distanceHcsr04);
                Errores.Add(Ep);

                if (i > 100)
                {
                    i = 0;
                }
            }
        }

        /// <summary>
        /// Mandando datos de constantes al microcontrolador
        /// </summary>
        /// <param name="distance"></param>
        private void serialPort1_SenddataProportional(double distance)
        {
            _pwmPid = Kp * Ep + Ki * Ei + Kd * Ed;

            if (_pwmPid > 15 && _pwmPid < 30)
            {
                _pwmPid = 41; //Saturando el pwm enviado al controlador 
                serialPort1.WriteLine(_pwmPid.ToString());
            }
            else if (_pwmPid < -15 && _pwmPid > -30)
            {
                _pwmPid = -41; //Saturando el pwm enviado al controlador
                serialPort1.WriteLine(_pwmPid.ToString());
            }
            else
            {
                serialPort1.WriteLine(_pwmPid.ToString());
            }

            serialPort1.DiscardOutBuffer();
        }

        /// <summary>
        /// Llamada segura para desplegar distancia sensador por el HCSR04
        /// </summary>
        /// <param name="data"></param>
        private void BufferContentHcsr04(string data)
        {
            if (this.dataWindow.lbDistanceHCSR04.InvokeRequired)
            {
                var currentContentHcsr04 = new ChangeContentLabelDelegate(BufferContentHcsr04);
                this.Invoke(currentContentHcsr04, new object[] { data });
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
                var currentContentEp = new ChangeContentLabelDelegate(BufferOutEp);
                this.Invoke(currentContentEp, new object[] { data });
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
                var currentContentEint = new ChangeContentLabelDelegate(BufferOutEint);
                this.Invoke(currentContentEint, new object[] { data });
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
                var currentContentEderiv = new ChangeContentLabelDelegate(BufferOutEderiv);
                this.Invoke(currentContentEderiv, new object[] { data });
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
        private void BufferOutPwm(string data)
        {
            if (this.dataWindow.PWMlb.InvokeRequired)
            {
                var currentContentPwm = new ChangeContentLabelDelegate(BufferOutPwm);
                this.Invoke(currentContentPwm, new object[] { data });
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
            Ep = await Task.Run(() => data - DisiredDistance);
            Kp = dataWindow.Kpdata;

            if (Math.Abs(Kp - _kpaux) > 0)
            {
                Ep = 0;
            }

            _kpaux = Kp;
        }

        /// <summary>
        /// Cálculo del error integral
        /// </summary>
        /// <param name="data"></param>
        private async void ErrorIntegral(double data)
        {
            Ei = await Task.Run(() => PromedioIntegral(_dataHcsr04));
            Ki = dataWindow.Kidata;

            if (Math.Abs(Ki - _kiaux) > 0)
            {
                Ei = 0;
            }

            _kiaux = Ki;
        }

        /// <summary>
        /// Calculo del error derivativo
        /// </summary>
        /// <param name="data"></param>
        private async void ErrorDerivativo(double data)
        {
            Ed = await Task.Run(() => CalculoDerivativo(_dataHcsr04));
            Kd = dataWindow.Kddata;
        }

        /// <summary>
        /// Cálcula el promedio de los datos recibidos por el microcontrolador
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private double PromedioIntegral(double distance)
        {
            var sum = Math.Round((_dataHcsr04 - _olddataInt) * 0.012, 1);
            _olddataInt = distance;
            if (Math.Abs(distance) < 0)
            {
                _sumEint = 0;
            }

            return _sumEint += sum;
        }

        /// <summary>
        /// Cálculo derivativo
        /// </summary>
        /// <param name="distances"></param>
        /// <returns></returns>
        private double CalculoDerivativo(double distances)
        {
            var diferencia = Math.Round((distances - _dataoldHcsr04) / 0.012, 1);
            _dataoldHcsr04 = distances;
            return diferencia;
        }

        /// <summary>
        /// Muestra la gráfica al hacer click en la pestaña Graficar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graficarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphicDistance.Visible = true;
        }

        /// <summary>
        /// Llamada segura a control para modificar la grafica desde el form principal
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        private void Updategraph(double data1, double data2)
        {
            if (this.graphicDistance.graphicdatas.InvokeRequired)
            {
                var updatedata = new UpdateGraph(Updategraph);
                this.Invoke(updatedata, new object[] { data1, data2 });
            }
            else
            {
                graphicDistance.graphicdatas.Series[0].Points.AddXY(data1, data2);
                graphicDistance.graphicdatas.Series[1].Points.AddXY(data1, 20);

                if (data1 == 100)
                {
                    graphicDistance.graphicdatas.Series[0].Points.Clear();
                    graphicDistance.graphicdatas.Series[1].Points.Clear();
                }
            }
        }

        /// <summary>
        /// Añadiendo los datos sensados al excel generado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Posiciones1.Visible = true;

            foreach (var row in Posiciones)
            {
                HojaPosiciones.Cells[_j1, "A"] = row;

                _j1++;
            }
            foreach (var row in Errores)
            {
                HojaPosiciones.Cells[_j2, "B"] = row;

                _j2++;
            }
        }
    }
}