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

namespace Buoi3_1
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
            new Thread(() =>
            {
                while (!isRunning)
                {
                    toolsStrip.Text = $"{DateTime.Now.ToString("dd/MM/yyyy")} - {DateTime.Now.ToString("hh:mm:ss tt")}";
                    Thread.Sleep(1000);
                }
            }).Start();
        }
       
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "video|*.mp4;*.mp3;*.mkv;*.avi";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = openFileDialog.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRunning = true;
            this.Close();
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
