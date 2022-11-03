using SnowFallCord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowfalls
{
    public partial class Form1 : Form
    {
        private IList<SnowCord> snowfall;
        private readonly Timer timer;
        private Graphics reset;
        Bitmap backfon, snow, stage, stage2;
        public Form1()
        {
            InitializeComponent();
            snowfall = new List<SnowCord>();
            backfon = (Bitmap)Properties.Resources.Font;
            snow = (Bitmap)Properties.Resources.Snow1;
            stage = new Bitmap(backfon, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            stage2 = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            reset = Graphics.FromImage(stage2);
            AddSnow();
            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            snow.MakeTransparent();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach (var snowFall in snowfall)
            {
                snowFall.Y += snowFall.Size;
                if (snowFall.Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowFall.Y = -snowFall.Y / 2;
                }
                snowFall.X += snowFall.Size;
                if (snowFall.X > Screen.PrimaryScreen.WorkingArea.Width)
                {
                    snowFall.X = -snowFall.X / 2;
                }
            }
            DrawStage();
            timer.Start();
        }
        private void AddSnow()
        {
            var rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                snowfall.Add(new SnowCord
                {
                    X = rnd.Next(-Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Width / 2),
                    Y = -rnd.Next(0, Screen.PrimaryScreen.WorkingArea.Height / 2),
                    Size = rnd.Next(5, 35)
                });
            }
        }
        private void DrawStage()
        {
            reset.DrawImage(stage, new Rectangle(0, 0,
                Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height));
            foreach (var snowFall in snowfall)
            {
                if (snowFall.Y > 0)
                {
                    reset.DrawImage(snow, new Rectangle(
                    snowFall.X,
                    snowFall.Y,
                    snowFall.Size,
                    snowFall.Size));
                }
            }
            var n = CreateGraphics();
            n.DrawImage(stage2, 0, 0);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawStage();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
    }
}
