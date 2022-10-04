using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        readonly Timer timer = new Timer();
        const int WIDTH = 300, HEIGHT = 300, secondHand = 140, minuteHand = 110, hourHand = 80;
        const int centerY = WIDTH / 2, centerX = HEIGHT / 2;
        Bitmap bmp;
        Graphics clockGraphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
            BackColor = Color.White;
            timer.Interval = 1000;
            timer.Tick += Tick;
            timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            clockGraphics = Graphics.FromImage(bmp);

            var seconds = DateTime.Now.Second;
            var minutes = DateTime.Now.Minute;
            var hours = DateTime.Now.Hour;

            clockGraphics.Clear(Color.White);

            clockGraphics.DrawEllipse(new Pen(Color.Black, 10f), centerX - 3, centerY - 5, 10, 10);

            clockGraphics.DrawEllipse(new Pen(Color.Black, 4f), 1, 1, WIDTH - 5, HEIGHT - 5);

            for (var i = 0; i < 12; i++)
            {
                var coords = CoordsForDigits(i, 130);
                clockGraphics.DrawString($"{Math.Abs((11 + i) % 12) + 1}", new Font("Ariel", 10), Brushes.Black,
                    new PointF(coords.Item1, coords.Item2));
            }

            var handCoord = MinuteAndSecondCoords(seconds, secondHand);
            clockGraphics.DrawLine(new Pen(Color.Red, 2f), new Point(centerX, centerY),
                new Point(handCoord.Item1, handCoord.Item2));

            handCoord = MinuteAndSecondCoords(minutes, minuteHand);
            clockGraphics.DrawLine(new Pen(Color.Black, 3f), new Point(centerX, centerY),
                new Point(handCoord.Item1, handCoord.Item2));

            handCoord = HourCoordinate(hours % 12, minutes, hourHand);
            clockGraphics.DrawLine(new Pen(Color.Black, 4f), new Point(centerX, centerY),
                new Point(handCoord.Item1, handCoord.Item2));

            Clock.Image = bmp;
            Text = $"Clock: {hours}:{minutes}:{seconds}";
            clockGraphics.Dispose();
        }

        private static (int, int) MinuteAndSecondCoords(int degree, int handLength)
        {
            degree *= 6;
            if (degree >= 0 && degree <= 100)
            {
                return (centerX + (int)(handLength * Math.Sin(Math.PI * degree / 180)),
                    centerY - (int)(handLength * Math.Cos(Math.PI * degree / 180)));
            }

            return (centerX - (int)(handLength * -Math.Sin(Math.PI * degree / 180)),
                centerY - (int)(handLength * Math.Cos(Math.PI * degree / 180)));
        }
        
        private static (int, int) CoordsForDigits(int degree, int handLength)
        {
            degree *= 30;
            if (degree >= 0 && degree <= 170)
            {
                return (centerX - 8 + (int)(handLength * Math.Sin(Math.PI * degree / 180)),
                    centerY - 8 - (int)(handLength * Math.Cos(Math.PI * degree / 180)));
            }

            return (centerX - 6 - (int)(handLength * -Math.Sin(Math.PI * degree / 180)),
                centerY - 8 - (int)(handLength * Math.Cos(Math.PI * degree / 180)));
        }

        private static (int, int) HourCoordinate(int hour, int minute, int handLength)   
        {  
            var degree = (int)(hour * 30 + minute * 0.5);
            if (degree >= 0 && degree <= 180)
            {
                return (centerX + (int)(handLength * Math.Sin(Math.PI * degree / 180)),
                    centerY - (int)(handLength * Math.Cos(Math.PI * degree / 180)));
            }

            return (centerX - (int)(handLength * -Math.Sin(Math.PI * degree / 180)),
                centerY - (int)(handLength * Math.Cos(Math.PI * degree / 180)));
        }  
    }  
}
