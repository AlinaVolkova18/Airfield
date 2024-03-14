using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Airfield_C_
{
	[Serializable]
	class Dispatcher
	{
		public void Choose_a_place_for_helicopter(int helicopter_number, Runways[] runways, Aircrafts aircrafts)
		{
			if (aircrafts.dx > 0) // если двигаемся вправо
			{
				for (int i = 0; i < helicopter_number; i++) 
					if (runways[i].getposition() == 0)
					{
						runways[i].change_position();
						aircrafts.runways_x = runways[i].getleft() + 39;
						aircrafts.runways_y = 260;
						aircrafts.runways_h = i;
						break;
					}
			}

			if (aircrafts.dx < 0) // если двигаемся влево
			{
				for (int i = helicopter_number - 1; i >= 0; i--)
					if (runways[i].getposition() == 0)
					{
						runways[i].change_position();
						aircrafts.runways_x = runways[i].getleft() + 39;
						aircrafts.runways_y = 260;
						aircrafts.runways_h = i;
						break;
					}
			}
		}

		public void Choose_a_place_for_plane(int plane_number, Runways[] runways, Aircrafts aircrafts, int stand_number)
		{
			if (aircrafts.dx > 0)
			{
				for (int i = 0; i < plane_number; i++)
				{
					if (runways[i].getposition() == 0)
					{
						if ((runways[i].gettop() - aircrafts.gety()) < Math.Abs(aircrafts.min))
						{
							aircrafts.a = runways[i].gettop();
							aircrafts.min = aircrafts.a - aircrafts.gety();
							aircrafts.runways_p = i;
						}
					}
					if (runways[i].gettop() == 212)
					{
						runways[aircrafts.runways_p].change_position();
						aircrafts.runways_y = aircrafts.a;
						aircrafts.runways_x = 750;
						break;
					}
				}
			}

			if (aircrafts.dx < 0)
            {
				for (int i = 0; i < plane_number; i++)
				{
					if (runways[i].getposition() == 0)
					{
						if ((runways[i].gettop() - aircrafts.gety()) < Math.Abs(aircrafts.min))
						{
							aircrafts.a = runways[i].gettop();
							aircrafts.min = aircrafts.a - aircrafts.gety();
							aircrafts.runways_p = i;
						}
					}
					if (runways[i].gettop() == 212)
					{
						runways[aircrafts.runways_p].change_position();
						aircrafts.runways_y = aircrafts.a;
						aircrafts.runways_x = 50;
						break;
					}
				}
			}
	}
		
		public void move(Graphics graphic, Aircrafts aircrafts, Runways[] runways, int stand_number)
		{
			aircrafts.Takeoff_for_helicopter(runways[aircrafts.runways_h], aircrafts);
			aircrafts.Landing(runways, aircrafts, stand_number);
			aircrafts.move();
		}
	}
}

