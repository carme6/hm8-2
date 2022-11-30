using System.Reflection;
using System;
using System.Windows.Forms;
using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;

namespace hm8_2
{
    public partial class Form1 : Form
    {

        Bitmap b;
        Graphics g;
        Random r = new Random();
        Rectangle rect;


        Dictionary<double, double> normal_distribution;


        Dictionary<double, double> chisquared_distribution;


        Dictionary<double, double> cauchy_distribution;

        Dictionary<double, double> fisher_distribution;


        Dictionary<double, double> Tstudent_distribution;

        int numtrial = 3000;


        public Form1()
        {
            InitializeComponent();

            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);

            rect = new Rectangle(20, 20, b.Width - 40, b.Height - 40);
            g.DrawRectangle(Pens.Black, rect);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);


            normal_distribution = new Dictionary<double, double>();
            double X, Y, S;

            for (int j = 0; j < numtrial; j++)
            {

                do
                {
                    X = r.NextDouble() * 2 - 1;
                    Y = r.NextDouble() * 2 - 1;
                    S = X * X + Y * Y;
                } while (S >= 1.0 || S == 0);

                double T = Math.Sqrt(-2 * Math.Log(S) / S);
                double Z1 = T * X;

                double num = Math.Round(Z1, 1);

                if (normal_distribution.ContainsKey(num)) {
                    normal_distribution[num]++;
                }
                else
                {
                    normal_distribution.Add(num, 1);
                }


            }
            int lenght = normal_distribution.Count;
            drawInsto(normal_distribution, lenght);
            this.pictureBox1.Image = b;



        }


        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);


            chisquared_distribution = new Dictionary<double, double>();

            double X, Y, S;

            for (int i = 0; i < numtrial; i++)
            {
                do
                {
                    X = r.NextDouble() * 2 - 1;
                    Y = r.NextDouble() * 2 - 1;
                    S = X * X + Y * Y;
                } while (S >= 1.0 || S == 0);

                double T = Math.Sqrt(-2 * Math.Log(S) / S);
                double Z1 = T * X;

                double num = Math.Round(Z1, 1);
                num = Math.Pow(num, 2);

                if (chisquared_distribution.ContainsKey(num))
                {
                    chisquared_distribution[num]++;
                }
                else
                {
                    chisquared_distribution.Add(num, 1);
                }
            }
            int lenght = chisquared_distribution.Count;

            drawInsto(chisquared_distribution, lenght);
            this.pictureBox1.Image = b;


        }




        private void drawInsto(Dictionary<double, double> distrib, int lenght)
        {
            
                      int j = 0;
                      int step = pictureBox1.Width / 150;
                      foreach (KeyValuePair<double,double> entry in distrib)
                      {
                          double virtualX = fromXRealToXVirtual(entry.Key , 0, lenght, pictureBox1.Left, pictureBox1.Width);
                          Rectangle r = new Rectangle((int)virtualX + 200, pictureBox1.Height -(int) entry.Value, step -2, (int)entry.Value);
                          g.DrawRectangle(Pens.Black, r);
                          g.FillRectangle(new SolidBrush(Color.Red), r);
                          j += step;
                      }

            }
   
        private int fromXRealToXVirtual(double x, double minX, double maxX, int left, int w)
        {
            return left + (int)(w * (x - minX) / (maxX - minX));
        }

        private int fromYRealToYVirtual(double y, double minY, double maxY, int top, int h)
        {
            return top + (int)(h - h * (y - minY) / (maxY - minY));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
           


            Tstudent_distribution = new Dictionary<double, double>();
            double X, Y, S,ris;

            for (int i = 0; i < numtrial; i++)
            {
                do
                {
                    X = r.NextDouble() * 2 - 1;
                    Y = r.NextDouble() * 2 - 1;
                    S = X * X + Y * Y;
                } while (S >= 1.0 || S == 0);

                double T = Math.Sqrt(-2 * Math.Log(S) / S);
                double Z1 = T * X;
                double Z2 = T * Y;

                if (Y != 0)
                    ris = Math.Round(Math.Pow(Z1, 2) / Z2, 0);
                else continue;

                if (Tstudent_distribution.ContainsKey(ris))
                {
                    Tstudent_distribution[ris]++;
                }
                else
                {
                    Tstudent_distribution.Add(ris, 1);
                }
            }
            int lenght = Tstudent_distribution.Count;

            drawInsto(Tstudent_distribution,lenght);
            this.pictureBox1.Image = b;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
          

            fisher_distribution = new Dictionary<double, double>();
            double X, Y, S, ris;

            for (int i = 0; i < numtrial; i++)
            {
                do
                {
                    X = r.NextDouble() * 2 - 1;
                    Y = r.NextDouble() * 2 - 1;
                    S = X * X + Y * Y;
                } while (S >= 1.0 || S == 0);

                double T = Math.Sqrt(-2 * Math.Log(S) / S);
                double Z1 = T * X;
                double Z2 = T * Y;

                if (Y != 0)
                    ris = Math.Round((double)Math.Pow(Z1, 2) / (Math.Pow(Z2, 2)), 0);
                else continue;

                if (fisher_distribution.ContainsKey(ris))
                {
                    fisher_distribution[ris]++;
                }
                else
                {
                    fisher_distribution.Add(ris, 1);
                }
            }

            int lenght = fisher_distribution.Count;
            drawInsto(fisher_distribution, lenght);
            this.pictureBox1.Image = b;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
      

            cauchy_distribution = new Dictionary<double, double>();
            double X, Y, S, ris;

            for (int i = 0; i < numtrial; i++)
            {
                do
                {
                    X = r.NextDouble() * 2 - 1;
                    Y = r.NextDouble() * 2 - 1;
                    S = X * X + Y * Y;
                } while (S >= 1.0 || S == 0);

                double T = Math.Sqrt(-2 * Math.Log(S) / S);
                double Z1 = T * X;
                double Z2 = T * Y;

                if (Y != 0)
                    ris = Math.Round((double)Z1 / Z2, 0);
                else continue;

                if (cauchy_distribution.ContainsKey(ris))
                {
                    cauchy_distribution[ris]++;
                }
                else
                {
                    cauchy_distribution.Add(ris, 1);
                }
            }

            int lenght=cauchy_distribution.Count;   
            drawInsto(cauchy_distribution, lenght);
            this.pictureBox1.Image = b;
        }

    }

}