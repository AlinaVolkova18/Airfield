using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Airfield_C_
{
    [Serializable]
    public partial class Form1 : Form
    {
        private int counter = 5, left = 60, top = 80, e;
        private int helicopter_number = 5, plane_number = 4, stand_number = 8;
        private Runways[] runways_helicopter = new Runways[10];
        private Runways[] runways_plane = new Runways[4];
        private Runways[] runways_stand = new Runways[8];
        private Aircrafts[] aircrafts = new Aircrafts[5];
        private Random rand = new Random();
        private Timer timer;
        Image image, bmp;
        private Graphics graphic, gr;
        private Dispatcher dispatcher = new Dispatcher();
        public delegate void DelegateChoose_a_place_for_helicopter(int helicopter_number, Runways[] runways, Aircrafts aircrafts);
        public delegate void DelegateChoose_a_place_for_plane(int plane_number, Runways[] runways, Aircrafts aircrafts, int stand_number);
        public delegate void DelegateAircraftsMove(Graphics graphic, Aircrafts aircrafts, Runways[] runways, int stand_number);
        public event DelegateChoose_a_place_for_helicopter Choose_a_place_for_helicopter;
        public event DelegateChoose_a_place_for_plane Choose_a_place_for_plane;
        public event DelegateAircraftsMove AircraftsMove;

        private bool Save_The_Game;
        private BinaryFormatter bfAircrafts = new BinaryFormatter();
        private BinaryFormatter bfrunways_helicopter = new BinaryFormatter();
        private BinaryFormatter bfrunways_plane = new BinaryFormatter();
        private BinaryFormatter bfDispatcher = new BinaryFormatter();
        private BinaryFormatter bfrunways_stand = new BinaryFormatter();

        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream fsAircrafts = new FileStream("./aircrafts.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bfAircrafts.Serialize(fsAircrafts, aircrafts);
            }

            using (FileStream fsrunways_helicopter = new FileStream("./runways_helicopter.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bfrunways_helicopter.Serialize(fsrunways_helicopter, runways_helicopter);
            }

            using (FileStream fsrunways_plane = new FileStream("./runways_plane.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bfrunways_plane.Serialize(fsrunways_plane, runways_plane);
            }

            using (FileStream fsrunways_stand = new FileStream("./runways_stand.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bfrunways_plane.Serialize(fsrunways_stand, runways_stand);
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.Show();
        }

        public Form1(bool Save_The_Game)
        {
            InitializeComponent();
            image = new Bitmap("fon.png");
            bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            gr = Graphics.FromImage(bmp);
            graphic = this.CreateGraphics();
            this.Choose_a_place_for_helicopter += dispatcher.Choose_a_place_for_helicopter;
            this.Choose_a_place_for_plane += dispatcher.Choose_a_place_for_plane;
            this.AircraftsMove += dispatcher.move;
            timer = new Timer();
            timer.Interval = (10);
            this.Save_The_Game = Save_The_Game;
            timer.Tick += timer_Tick;
            timer.Start();


            if (Save_The_Game == true)
            {
                using (FileStream fsAircrafts = File.OpenRead("./Aircrafts.dat"))
                {
                    aircrafts = (Aircrafts[])bfAircrafts.Deserialize(fsAircrafts);
                }

                using (FileStream fsrunways_helicopter = File.OpenRead("./runways_helicopter.dat"))
                {
                    runways_helicopter = (Runways[])bfrunways_helicopter.Deserialize(fsrunways_helicopter);
                }

                using (FileStream fsrunways_plane = File.OpenRead("./runways_plane.dat"))
                {
                    runways_plane = (Runways[])bfrunways_plane.Deserialize(fsrunways_plane);
                }

                using (FileStream fsrunways_stand = File.OpenRead("./runways_stand.dat"))
                {
                    runways_stand = (Runways[])bfrunways_stand.Deserialize(fsrunways_stand);
                }

               
            }
            else
            {
                for (int i = 0; i < plane_number; i++)
                {
                    runways_plane[i] = new Runways(60, top, 680, 44);
                    top += 44;
                }

                for (int i = 0; i < helicopter_number; i++)
                {
                    runways_helicopter[i] = new Runways(left, 256, 136, 44);
                    left += 136;
                }

                for (int i = 0; i < stand_number; i++)
                {
                    runways_stand[i] = new Runways(left - 680, 300, 136, 44);
                    left += 136;

                    if (i == 5)
                        runways_stand[i] = new Runways(0, 80, 60, 265);
                    if (i == 6)
                        runways_stand[i] = new Runways(740, 80, 60, 265);
                    if (i == 7)
                        runways_stand[i] = new Runways(0, 345, 800, 44);
                }

                for (int i = 0; i < counter; i++)
                    CreateAircrafts(i);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            move();
            draw(graphic);
        }

        public void CreateAircrafts(int i)
        {
            switch (rand.Next(0, 4))
            {
                case 0:
                    aircrafts[i] = new Helicopter(0, 10 * rand.Next(1, 19), rand.Next(2, 5), 0, 45, 70, 0, 0);
                    Choose_a_place_for_helicopter(helicopter_number, runways_helicopter, aircrafts[i]);
                    break;
                case 1:
                    aircrafts[i] = new Helicopter(760, 10 * rand.Next(1, 19), -(rand.Next(2, 5)), 0, 45, 70, 0, 0);
                    Choose_a_place_for_helicopter(helicopter_number, runways_helicopter, aircrafts[i]);
                    break;
                case 2:
                    do { e = rand.Next(3, 10); }
                    while (e == 8 || e == 9 || e == 6 || e == 5 || e == 4);
                    aircrafts[i] = new Plane(0, 10 * e, rand.Next(2, 5), 0, 50, 60, 0, 0);
                    Choose_a_place_for_plane(plane_number, runways_plane, aircrafts[i], stand_number);
                    break;
                case 3:
                    do { e = rand.Next(3, 10); }
                    while (e == 8 || e == 9 || e == 6 || e == 5 || e == 4);
                    aircrafts[i] = new Plane(760, 10 * e, -(rand.Next(2, 5)), 0, 50, 60, 0, 0);
                    Choose_a_place_for_plane(plane_number, runways_plane, aircrafts[i], stand_number);
                    break;
            }
        }

        void draw(Graphics graphics)
        {
            gr.DrawImage(image, 0, 0);
            for (int i = 0; i < plane_number; i++)
                runways_plane[i].show(gr);

            for (int i = 0; i < helicopter_number; i++)
                runways_helicopter[i].show(gr);

            for (int i = 0; i < stand_number; i++)
                runways_stand[i].show(gr);

            for (int i = 0; i < counter; i++)
            {
                aircrafts[i].show(gr);
            }
            graphic.DrawImage(bmp, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphic = this.CreateGraphics();
            draw(graphic);
        }


        public void move()
        {
            for (int i = 0; i < counter; i++)
                if (aircrafts[i].getx() < -20 || aircrafts[i].getx() > 800 || aircrafts[i].gety() > 450 || aircrafts[i].gety() < 0)
                {
                    aircrafts[i] = null;
                    CreateAircrafts(i);
                }
                else
                {
                    AircraftsMove(graphic, aircrafts[i], runways_helicopter, stand_number);
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
