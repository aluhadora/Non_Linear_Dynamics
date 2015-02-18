using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace Non_Linear_Dynamics
{
  public class Process3
  {
    public Action<Image> SetImage { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    private Bitmap _bmp;
    private readonly Color _color = Color.Blue;
    private double Dt
    {
      get
      {
        return .005d;
      }
    }
    private const double Sigma = 10;
    private const double Rho = 28;
    private const double Beta = 8 / 3d;
    public Graphics Graphics;
    public Action Invalidate;

    private double XPixelWidth
    {
      get
      {
        return 60d / Width;
      }
    }
    private double YPixelWidth
    {
      get
      {
        return 60d / Height;
      }
    }
    
    public void Go()
    {
      _bmp = new Bitmap(Width, Height);
      //SetImage(_bmp);
      Graphics = Graphics.FromImage(_bmp);
      Graphics.Clear(Color.Black);
      Graphics.Clear(Color.Black);
      Graphics.Clear(Color.Black);
      Graphics.Clear(Color.Black);

      double x = .5;
      double y = .5;
      double z = .5;
      double minx = int.MaxValue;
      double maxX = int.MinValue;
      double miny = minx;
      double maxy = maxX;
      double minz = minx;
      double maxz = maxX;

      for (int i = 0; i < 64000; i++)
      {
        var oldX = x;
        var oldY = y;
        var oldZ = z;

        x += Sigma * Dt * (oldY - oldX);
        y += Dt *(oldX * (Rho - oldZ) - oldY);
        z += Dt * (oldX * oldY - Beta * oldZ);
        Draw(oldX, oldY, x, y, z);

        if (x < minx) minx = x;
        if (x > maxX) maxX = x;
        if (y < miny) miny = y;
        if (y > maxy) maxy = y;
        if (z < minz) minz = z;
        if (z > maxz) maxz = z;
      }
      _bmp.Save(@"C:\Chaos.bmp", ImageFormat.Bmp);

      var image = Image.FromFile(@"C:\ChaosMod.bmp");
      ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

      // Create an Encoder object based on the GUID
      // for the Quality parameter category.
      System.Drawing.Imaging.Encoder myEncoder =
          System.Drawing.Imaging.Encoder.Quality;

      // Create an EncoderParameters object.
      // An EncoderParameters object has an array of EncoderParameter
      // objects. In this case, there is only one
      // EncoderParameter object in the array.
      EncoderParameters myEncoderParameters = new EncoderParameters(1);

      EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,
          50L);
      myEncoderParameters.Param[0] = myEncoderParameter;
      image.Save(@"c:\TestPhotoQualityFifty.jpg", jgpEncoder,
          myEncoderParameters);

      myEncoderParameter = new EncoderParameter(myEncoder, 100L);
      myEncoderParameters.Param[0] = myEncoderParameter;
      image.Save(@"c:\TestPhotoQualityHundred.jpg", jgpEncoder,
          myEncoderParameters);
      
      // Save the bitmap as a JPG file with zero quality level compression.
      myEncoderParameter = new EncoderParameter(myEncoder, 0L);
      myEncoderParameters.Param[0] = myEncoderParameter;
      image.Save(@"c:\TestPhotoQualityZero.jpg", jgpEncoder,
          myEncoderParameters);



      SetImage(_bmp);
    }

    private ImageCodecInfo GetEncoder(ImageFormat format)
    {
      ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
      foreach (ImageCodecInfo codec in codecs)
      {
        if (codec.FormatID == format.Guid)
        {
          return codec;
        }
      }
      return null;
    }

    private void Draw(double oldX, double oldY, double x, double y, double z)
    {
      var x1 = (int)(oldX / XPixelWidth + 30 / XPixelWidth);
      var y1 = (int)(oldY / YPixelWidth + 30 / YPixelWidth);
      var x2 = (int)(x / XPixelWidth + 30 / XPixelWidth);
      var y2 = (int)(y / YPixelWidth + 30 / YPixelWidth);
      //_g.DrawLine(new Pen(Color.FromArgb((int)(z * 255d / 60d), _color)), x1, y1, x2, y2);
      Graphics.DrawLine(new Pen(Color.FromArgb((int)(z * 255d / 60d), _color), (float)z / 24f), x1, y1, x2, y2);
      //Graphics.DrawLine(new Pen(Color.FromArgb((int)(z * 255d / 60d), _color), (float)z / 6f), x1, y1, x2, y2);
      //SetImage(_bmp);
      //Thread.Sleep(1);
    }
  }
}
