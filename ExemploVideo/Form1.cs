using System;
using System.Drawing;
using System.Windows.Forms;
//namespace do AForge
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge;

namespace ExemploVideo
{
    public partial class Form1 : Form
    {
////////////nº 1
        //public AVIFileVideoSource source = null;
        public FileVideoSource source = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //para abrir o AVI
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
////////////nº 2
                //source = new AVIFileVideoSource(openFileDialog1.FileName);
                source = new FileVideoSource(openFileDialog1.FileName);
                source.NewFrame += new NewFrameEventHandler(avi_NewFrame);
                source.Start();
            }
        }

//////////nº 3
        private void avi_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap imagem1 = (Bitmap)eventArgs.Frame.Clone();
            Bitmap imagem2 = (Bitmap)eventArgs.Frame.Clone();

            FiltersSequence filter = new FiltersSequence();

            filter.Add(new ColorFiltering(
            new IntRange(102, 156),
            new IntRange(20, 54),
            new IntRange(20, 47)));


            filter.Add(new Grayscale(.3, .59, .11));
            filter.Add(new BlobsFiltering(18, 18, 100, 100));

            filter.Add(new Threshold(10));

            filter.Add(new Invert());

            pictureBox1.Image = imagem1;
            pictureBox2.Image = filter.Apply(imagem2);
        }

//////////nº 4
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!(source == null))
                if (source.IsRunning)
                {
                    source.Stop();
                    source.SignalToStop();
                    source = null;
                } 
        } 
    }
}
