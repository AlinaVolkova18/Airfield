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
    class Plane : Aircrafts
    {
        public Bitmap imagePlane = new Bitmap("plane.png");
        public bool p = false;
        public bool o = false;
        public bool k = false;
        public int which_side;
        public int runways_xp, runways_yp;
        public Plane(float x, float y, float dx, float dy, float h, float w, float dh, float dw) : base(x, y, dx, dy, h, w, dh, dw)
        {
            if (dx > 0)
            {
                imagePlane = new Bitmap(@"plane.png");
                runways_x = 860;
            }
            else
            {
                imagePlane = new Bitmap(@"plane1.png");
                runways_x = -60;
            }
        }
        public override void show(Graphics graphic)
        {
            imagePlane.MakeTransparent(Color.White);
            graphic.DrawImage(imagePlane, x, y, w, h);
        }

        public override void move()
        {
            x += dx;
            y += dy;
            w += dw;
            h += dh;
        }

        
        public override void Landing(Runways []runways, Aircrafts aircrafts, int stand_number)
        {
            if (x == runways_x)
            {
                if (dx > 0)
                {
                    if (y < runways_y && x > 700 && !d)
                    {
                            imagePlane = new Bitmap(@"plane1.png");
                            d = true;
                            //runways[aircrafts.runways_p].change_position();
                            dw = -Convert.ToSingle(0.05);
                            dh = -Convert.ToSingle(0.05);
                            dx = -1;
                            dy = 75 / (150 / Math.Abs(dx));
                    }
                }

                if (dx < 0)
                {
                    if (y < runways_y && x < 40 && !d)
                    {
                            imagePlane = new Bitmap(@"plane.png");
                            d = true;
                            //runways[aircrafts.runways_p].change_position();
                            dw = -Convert.ToSingle(0.05);
                            dh = -Convert.ToSingle(0.05);
                            dx = 1;
                            dy = 75 / (150 / Math.Abs(dx));
                    }
                }
            }
            //если у нас самолет после посадки едет влево
            if (y > runways_y && x < 670 && d && dx < 0)
            {
                if (Math.Abs(dx) >= 1)
                {
                    dw = 0;
                    dh = 0;
                    dy = 0;
                    dx -= -Convert.ToSingle(0.05);
                }
                else
                if (x < 10 && Math.Abs(dx) < 1)
                {
                    dx = 0;
                    dy = 1;
                    imagePlane = new Bitmap(@"plane2.png");

                    for (int i = 0; i < stand_number - 3; i++)
                        if (runways[i].getposition() == 0)
                        {
                            aircrafts.runways_p = i;
                            runways[runways_p].change_position();
                            runways_xp = runways[i].getleft() + 40;
                            runways_yp = 300;
                            break;
                        }
                }
            }

            //если самолет после посадки едет вправо
            if (y > runways_y && x > 70 && d && dx > 0)
            {
                if (Math.Abs(dx) >= 1)
                {
                    dw = 0;
                    dh = 0;
                    dy = 0;
                    dx -= Convert.ToSingle(0.3);
                }
                else
                if (x > 740 && Math.Abs(dx) < 1)
                {
                    dy = 1;
                    dx = 0;
                    imagePlane = new Bitmap(@"plane2.png");

                    for (int i = stand_number - 3; i <= 0; i--)
                        if (runways[i].getposition() == 0)
                        {
                            aircrafts.runways_p = i;
                            runways[runways_p].change_position();
                            runways_xp = runways[i].getleft() + 40;
                            runways_yp = 300;
                            break;
                        }
                }
            }

            if (!k)
            {
                if (this.y > 345)
                {
                    if (x < runways_xp)
                    {
                        dx = 1;
                        dy = 0;
                        if (x < 10)
                        {
                            which_side = 1;
                            imagePlane = new Bitmap(@"plane.png");
                        }
                        if (x > 780)
                        {
                            which_side = 2;
                            imagePlane = new Bitmap(@"plane1.png");
                        }
                    }
                    else
                    {
                        if (y > runways_yp)
                        {
                            dy = -1;
                            dx = 0;
                            p = true;
                            imagePlane = new Bitmap(@"plane3.png");
                        }
                    }
                }
            }

            if (y <= runways_yp && p)
            {
                if (!k)
                {
                    dx = 0;
                    dy = 0;
                }
                if (my_timer < 300)
                    my_timer++;
                else
                {
                    o = true;
                    my_timer = 0;
                    Takeoff_for_plane(runways[runways_p], aircrafts);
                }
            }

            if (y > runways_yp + 45 && o)
            {
                if (which_side == 1)
                {
                    dx = -1;
                    imagePlane = new Bitmap(@"plane1.png");
                }

                if (which_side == 2)
                {
                    dx = 1;
                    imagePlane = new Bitmap(@"plane.png");
                }
                dy = 0;
                k = true;
            }
        }



        public void Takeoff_for_plane(Runways runways, Aircrafts aircrafts)
        {
            k = true;
            dy = 1;
            imagePlane = new Bitmap(@"plane2.png");
        }
    }
}



