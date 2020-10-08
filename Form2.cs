using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            int t = Form1.I;
            chart1.Series[0].Points.Add(5);
            chart1.Series[0].Points.Add(10);
            chart1.Series[0].Points.Add(15);
            chart1.DataBind();

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            int t = Form1.I;
        }
    }
}
