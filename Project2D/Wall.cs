using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
	class Wall : GameObject
	{
		public Wall() : base("")
		{
			Rectangle rec = new Rectangle();
			rec.height = 100f;
			rec.width = 100f;

			BeginDrawing();
			DrawRectangleRec(rec, Fade(RLColor.GREEN, 0.5f));
		}
	}
}
