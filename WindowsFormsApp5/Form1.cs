using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.ScreenCapture;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        private ScreenCaptureJob gotu;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartRecord();
        }
        void StartRecord()

        {
            string str = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) +
                          (@"\" + textBox1.Text));
            gotu = new ScreenCaptureJob();
            System.Drawing.Size WorkingArea = SystemInformation.WorkingArea.Size;
            Rectangle captureRect = new Rectangle(0, 0, WorkingArea.Width, WorkingArea.Height); 
            gotu.CaptureRectangle = captureRect;
            gotu.ShowFlashingBoundary = true;
            gotu.ShowCountdown = true;
            gotu.CaptureMouseCursor = true;
            gotu.AddAudioDeviceSource(AudioDevices());
            gotu.OutputPath = str;
            this.WindowState = FormWindowState.Minimized;
            System.Threading.Thread.Sleep(1000);
            gotu.Start();
           
            
        }
         EncoderDevice AudioDevices()
        {
            EncoderDevice foundDevice = null;
          Collection<EncoderDevice> audioDevices = EncoderDevices.FindDevices(EncoderDeviceType.Audio);
            try
            {
                foundDevice = audioDevices.First();
            }
            catch (Exception ex)
            { MessageBox.Show("Audio Device Not Found" + audioDevices[0].Name + ex.Message); }
            return foundDevice;
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (gotu.Status == RecordStatus.Running)
            {
                gotu.Stop();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gotu.Status == RecordStatus.Running)
                gotu.Stop();
            gotu.Dispose();
        }
    }
}
