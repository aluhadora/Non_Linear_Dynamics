﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Non_Linear_Dynamics
{
  public class Process2
  {
    readonly Color[] oColor = new Color[16];

    public Process2()
    {
      int i;

      for (i = 0; i < 16; i++)
      {
        oColor[i] = new Color();
      }

      oColor[0] = Color.Black;
      oColor[1] = Color.Blue;
      oColor[2] = Color.Brown;
      oColor[3] = Color.Green;
      oColor[4] = Color.Magenta;
      oColor[5] = Color.Orange;
      oColor[6] = Color.Red;
      oColor[7] = Color.DarkGray;
      oColor[8] = Color.Violet;
      oColor[9] = Color.Khaki;
      oColor[10] = Color.Olive;
      oColor[11] = Color.RosyBrown;
      oColor[12] = Color.Tomato;
      oColor[13] = Color.Yellow;
      oColor[14] = Color.Cyan;
      oColor[15] = Color.Lime;

    }

    public void Generate(Graphics g, int iWidth, int iHeight, int iDim)
    {
      double rho, sigma, beta;
      double iterations;
      double x, y, z, d0_x, d0_y, d0_z, d1_x, d1_y, d1_z, d2_x, d2_y, d2_z;
      double d3_x, d3_y, d3_z, xt, yt, zt, dt, dt2, x_angle, y_angle, z_angle;
      double sx, sy, sz, cx, cy, cz, temp_x, temp_y, old_y;
      int i, row, col, old_row, old_col;
      int color = 0;

      iterations = 8000;
      rho = 28;
      sigma = 10;
      beta = 8.0 / 3.0;

      x_angle = 45;
      y_angle = 0;
      z_angle = 90;
      x_angle = DegToRad(x_angle);
      sx = Math.Sin(x_angle);
      cx = Math.Cos(x_angle);
      y_angle = DegToRad(y_angle);
      sy = Math.Sin(y_angle);
      cy = Math.Cos(y_angle);
      z_angle = DegToRad(z_angle);
      sz = Math.Sin(z_angle);
      cz = Math.Cos(z_angle);

      x = 0;
      y = 1;
      z = 0;

      if (iDim == 3)
      {
        old_col = (int)Math.Round(y * 9.0);
        old_row = (int)Math.Round(350.0 - 6.56 * z);
        g.DrawLine(new Pen(oColor[0]), 0, 348, 638, 348);
        g.DrawLine(new Pen(oColor[0]), 320, 2, 320, 348);
        g.DrawLine(new Pen(oColor[0]), 320, 348, 648, 140);
      }
      else
      {
        old_col = (int)Math.Round(y * 9.0 + 320.0);
        old_row = (int)Math.Round(350.0 - 6.56 * z);
        g.DrawLine(new Pen(oColor[0]), 0, 348, 639, 348);
        g.DrawLine(new Pen(oColor[0]), 320, 2, 320, 348);
      }
      dt = 0.01;
      dt2 = dt / 2.0;
      for (i = 0; i <= iterations; i++)
      {
        d0_x = sigma * (y - x) * dt2;
        d0_y = (-x * z + rho * x - y) * dt2;
        d0_z = (x * y - beta * z) * dt2;
        xt = x + d0_x;
        yt = y + d0_y;
        zt = z + d0_z;
        d1_x = (sigma * (yt - xt)) * dt2;
        d1_y = (-xt * zt + rho * xt - yt) * dt2;
        d1_z = (xt * yt - beta * zt) * dt2;
        xt = x + d1_x;
        yt = y + d1_y;
        zt = z + d1_z;
        d2_x = (sigma * (yt - xt)) * dt;
        d2_y = (-xt * zt + rho * xt - yt) * dt;
        d2_z = (xt * yt - beta * zt) * dt;
        xt = x + d2_x;
        yt = y + d2_y;
        zt = z + d2_z;
        d3_x = (sigma * (yt - xt)) * dt2;
        d3_y = (-xt * zt + rho * xt - yt) * dt2;
        d3_z = (xt * yt - beta * zt / 3) * dt2;
        old_y = y;
        x = x + (d0_x + d1_x + d1_x + d2_x + d3_x) * 0.33333333;
        y = y + (d0_y + d1_y + d1_y + d2_y + d3_y) * 0.33333333;
        z = z + (d0_z + d1_z + d1_z + d2_z + d3_z) * 0.33333333;

        if (iDim == 3)
        {
          temp_x = x * cx + y * cy + z * cz;
          temp_y = x * sx + y * sy + z * sz;
          col = (int)Math.Round(temp_x * 8.0 + 320.0);
          row = (int)Math.Round(350.0 - temp_y * 5.0);
        }
        else
        {
          col = (int)Math.Round(y * 9.0 + 320.0);
          row = (int)Math.Round(350 - 6.56 * z);
        }
        if (col < 320)
        {
          if (old_col >= 320)
          {
            color++;
            color = color % 16;
          }
        }
        if (col > 320)
        {
          if (old_col <= 320)
          {
            color++;
            color = color % 16;
          }
        }
        g.DrawLine(new Pen(oColor[color]), old_col, old_row, col, row);
        old_row = row;
        old_col = col;
      }
    }

    private static double DegToRad(double angle)
    {
      return angle * Math.PI / 180d;
    }
  }
}
