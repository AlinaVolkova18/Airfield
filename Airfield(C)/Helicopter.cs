using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airfield_C_
{
    [Serializable]
    class Helicopter : Aircrafts
    {
        public Bitmap imageHelicopter = new Bitmap("helicopter.png");
        public Helicopter(float x, float y, float dx, float dy, float h, float w, float dh, float dw) : base(x, y, dx, dy, h, w, dw, dh)
        {
            if (dx > 0)
                imageHelicopter = new Bitmap(@"helicopter1.png");
            else
                imageHelicopter = new Bitmap(@"helicopter.png");
        }
        public override void show(Graphics graphic)
        {
            imageHelicopter.MakeTransparent(Color.White);
            graphic.DrawImage(imageHelicopter, x, y, w, h);
        }

        public override void move()
        {
            x += dx;
            y += dy;
            w += dw;
            h += dh;
        }

        public override void Takeoff_for_helicopter(Runways runways, Aircrafts aircrafts)
        {
            if ((dx > 0 && x >= runways_x || dx < 0 && x <= runways_x) && y < runways_y)
            {
                imageHelicopter = new Bitmap(@"helicopter2.png");
                dw = -Convert.ToSingle(0.1);
                dh = -Convert.ToSingle(0.06);
                dx = 0;
                dy = 1;
            }

            if (y >= runways_y)
            {
                dy = 0;
                dw = 0;
                dh = 0;
            }

            if (y == runways_y && !b)
            {
                if (my_timer < 200)
                    my_timer++;
                else
                if (!flew_away_or_not)
                {
                    my_timer = 0;
                    flew_away_or_not = true;
                    runways.change_position();
                    dy = -1;
                    dw = Convert.ToSingle(0.1);
                    dh = Convert.ToSingle(0.06);
                    b = true;
                }
            }
            else
            if (y <= value && b)
            {
                dy = 0;
                dw = 0;
                dh = 0;
                if (c == 0)
                {
                    dx = 2;
                    imageHelicopter = new Bitmap(@"helicopter1.png");
                }
                else
                {
                    dx = -2;
                    imageHelicopter = new Bitmap(@"helicopter.png");
                }
            }
        }
    }
}
