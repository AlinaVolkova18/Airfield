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
	public class Runways
	{
		private int position;
		private int left, top, right, bottom;
		public Runways(int left, int top, int right, int bottom)
		{
			position = 0;
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		public void show(Graphics graphic)
		{
			Pen blackPen = new Pen(Color.WhiteSmoke, 3);
			//SolidBrush blackBrush = new SolidBrush(Color.);
			graphic.DrawRectangle(new Pen(Color.WhiteSmoke, 3), new Rectangle(left, top, right, bottom));
			//graphic.FillRectangle(new SolidBrush(Color.Black), new Rectangle(left, top, right, bottom));
		}

		public void change_position()
		{
			if (position != 0)
				position = 0;
			else
				position = 1;
		}

		public int getposition()
		{
			return position;
		}

		public int getleft()
		{
			return left;
		}

		public int gettop()
		{
			return top;
		}
	}
}

