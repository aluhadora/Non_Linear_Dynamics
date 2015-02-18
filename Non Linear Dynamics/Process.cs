using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Non_Linear_Dynamics
{
  public class Process
  {
    public Action<Image> SetImage { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    private Bitmap _bmp;

    private double XPixelWidth
    {
      get
      {
        return .6d / Width;
      }
    }
    private double YPixelWidth
    {
      get
      {
        return 1d / Height;
      }
    }
    
    public void Go()
    {
      _bmp = new Bitmap(Width, Height);
      for (int i = 0; i < Width; i++)
      {
        var r = XPixelWidth * i + 3.4;
        var x = 0.5d;
        for (int k = 0; k < Height; k++)
        {
          x = r * x * (1d - x);
        }
        for (int k = 0; k < Height * 2; k++)
        {
          x = r * x * (1d - x);
          _bmp.SetPixel(i, (int)(x/YPixelWidth), Color.Black);
        }
        
      }
      SetImage(_bmp);
      _bmp.Save(@"C:\Chaos.jpg", ImageFormat.Jpeg);
    }
  }
}
