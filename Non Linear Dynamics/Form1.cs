using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace Non_Linear_Dynamics
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void PictureBox_Click(object sender, EventArgs e)
    {
      var modifier = 1;
      pictureBox.Image = new Bitmap(pictureBox.Width * modifier, pictureBox.Height * modifier);
      var process = new Process3
        {
          Width = 2880,
          //Width = pictureBox.Width * modifier,
          //Height = pictureBox.Height * modifier,
          Height = 1440,
          SetImage = SetImage,
          Invalidate = InvalidatePic,
          Graphics = pictureBox.CreateGraphics() };
      var thread = new Thread(process.Go);
      thread.Start();
      //process.Go();

      //pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
      //var process = new Process2();
      //process.Generate(Graphics.FromImage(pictureBox.Image), pictureBox.Width, pictureBox.Height, 2);
    }

    private void InvalidatePic()
    {
      if (pictureBox.InvokeRequired)
        pictureBox.Invoke(new Action(Invalidate));
      else
      {
        pictureBox.Invalidate();
      }
    }

    private void SetImage(Image image)
    {
      if (pictureBox.InvokeRequired)
        pictureBox.Invoke(new Action<Image>(SetImage), image);
      else
      {
        pictureBox.BackgroundImage = image;
      }
        
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      int borderWidth = (this.Width - this.ClientSize.Width) /2;
      int titlebarHeight = this.Height - this.ClientSize.Height - 2 * borderWidth;

      Width = 1252 + borderWidth * 2;
      Height = 626 + titlebarHeight + borderWidth * 2;
    }
  }
}
