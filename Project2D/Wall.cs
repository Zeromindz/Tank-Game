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
		Rectangle rec = new Rectangle();

		private float m_Height;
		private float m_Width;
		Vector2 m_CurrentPosition;

		public Wall(float _startingPosX, float _startingPosY, float _height, float _width) : base("")
		{
			//m_CurrentPosition = GetGlobalPosition();
			m_EnabledCollision = true;
			SetAlive(true);

			//starting position
			m_LocalTransform.m7 = _startingPosX;
			m_LocalTransform.m8 = _startingPosY;

			m_Height = _height;
			m_Width = _width;

			rec.height = m_Height;
			rec.width = m_Width;
		}

		public override void Draw()
		{
			
			BeginDrawing();
			DrawRectangleRec(rec, Fade(RLColor.GREEN, 0.5f));
			base.Draw();
		}
	}
}
