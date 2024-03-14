using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airfield_C_
{
    [Serializable]
    public class Aircrafts
    {
        public Random rand = new Random();
        public float runways_x, runways_y;
        public int runways_p;
        public int runways_h;
        public float w, h, dw, dh;
        public float x, y, dx, dy;
        public bool flew_away_or_not = false;
        public int my_timer = 0;
        public bool b = false; 
        public bool d = false; 
        public float a;
        public int signal = 0;
        public float min = 1000;
        public int value;
        public int c;

        public Aircrafts(float x, float y, float dx, float dy, float h, float w, float dh, float dw)
        {
            value = rand.Next(150, 200);
            c = rand.Next(0, 2);
            this.x = x;
            this.y = y;
            this.dx = dx;
            this.dy = dy;
            this.w = w;
            this.h = h;
            this.dw = dw;
            this.dh = dh;
        }
        public virtual void show(Graphics graphic) { }
        public virtual void move() { }
        public virtual void Landing(Runways []runways, Aircrafts aircrafts, int stand_number) { }
        public virtual void Takeoff_for_helicopter(Runways runways, Aircrafts aircrafts) { }
        public float getx()
        {
            return x;
        }

        public float gety()
        {
            return y;
        }
    }
}
