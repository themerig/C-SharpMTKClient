using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MtkClientC
{
    public partial class Form1 : Form
    {

        public static Thread WorkThread;
        public static Thread thr;
        static Form1 instance;
        MtkProcess mtkProcess = new MtkProcess();

        public static Form1 Instance
        {
            get
            {
                return instance;
            }
        }


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            instance = this;
        }


        private void start_Click(object sender, EventArgs e)
        {

            richTextBox1.Clear();

            thr = new Thread(new ThreadStart(readMtk));
            thr.Start();
        }

        public void readMtk()
        {
            mtkProcess.Driver();
            mtkProcess.connect_brom(mtkProcess.FindMtkDevice());

        }

    }
}