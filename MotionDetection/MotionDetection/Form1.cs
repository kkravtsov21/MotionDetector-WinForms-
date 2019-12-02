using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;


namespace MotionDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private FilterInfoCollection Devices;
        private VideoCaptureDevice VidSource;
        public Bitmap GetCurrentVideoFrame;

        MotionDetector Detector;
        float DetLevel;

        private void button2_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (VidSource.IsRunning == true)
            {
                VidSource.Stop();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Detector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionBorderHighlighting());
            DetLevel = 0;

            Devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo x in Devices)
            {
                comboBox1.Items.Add(x.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VidSource = new VideoCaptureDevice(Devices[comboBox1.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = VidSource;
            videoSourcePlayer1.Start();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {

        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            DetLevel = Detector.ProcessFrame(image);           
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = String.Format("{0:00.0000}", DetLevel);
            if (SaveMode.Checked == true && DetLevel >=00.0500)
            {
                
                    Bitmap bitmap = (Bitmap)videoSourcePlayer1.GetCurrentVideoFrame().Clone();
                    pictureBox1.Image = bitmap;
                    pictureBox1.Image.Save(@"C:\\Detector\MDTest.bmp", ImageFormat.Bmp);                
            }
            else if (SoundMode.Checked == true && DetLevel>=00.0500)
            {
                Console.Beep();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
