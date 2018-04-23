using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSeguidor
{
    public partial class DataWindow : Form
    {

        #region

        private double kpdata;
        private double kidata;
        private double kddata;

        #endregion

        public DataWindow()
        {
            InitializeComponent();
            Kpdata = 0;
            Kidata = 0;
            Kddata = 0;
        }

        public double Kpdata
        {
            get { return kpdata; }
            set { kpdata = value; }
        }

        public double Kidata
        {
            get { return kidata; }
            set { kidata = value; }
        }

        public double Kddata
        {
            get { return kddata; }
            set { kddata = value; }
        }

        private void Kptxb_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Kpdata = Convert.ToDouble(Kptxb.Text);
                    Kptxb.Clear();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Kptxb.Clear();
            }
          
        }

        private void Kitxb_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Kidata = Convert.ToDouble(Kitxb.Text);
                    Kitxb.Clear();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Kitxb.Clear();
            }
        }

        private void Kdtxb_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Kddata = Convert.ToDouble(Kdtxb.Text);
                    Kdtxb.Clear();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Kdtxb.Clear();
            }
        }
    }
}
