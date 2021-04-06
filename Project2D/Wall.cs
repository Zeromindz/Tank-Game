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
		Rectangle rec;

		private float m_Height;
		private float m_Width;
		Vector2 m_Position = new Vector2(0, 0);

		

		public Wall(string _fileName, float _startingPosX, float _startingPosY, float _height, float _width) : base(_fileName)
		{
			m_Position = GetGlobalPosition();
			SetAlive(true);
			m_EnabledCollision = true;

			m_ObjectTag = 2;

			//starting position
			m_LocalTransform.m7 = _startingPosX;
			m_LocalTransform.m8 = _startingPosY;

			m_Height = _height;
			m_Width = _width;

			//rec = new Rectangle {x = _startingPosX, y = _startingPosY, height = _height, width = _width};
			CollisionManager.AddObject(this);
		}

		public override void Update(float _deltatime)
		{

			base.Update(_deltatime);
		}
		public override void OnCollision(GameObject _otherObj)
		{
			

			base.OnCollision(_otherObj);
		}

		public void SetPosition(Vector2 _position)
		{
			m_LocalTransform.m7 = _position.x;
			m_LocalTransform.m8 = _position.y;
		}

		public override void Draw()
		{
			//DrawRectangleRec(rec, Fade(RLColor.GREEN, 0.5f));
			
			base.Draw();
			
			if(GetAlive() == true)
				DrawCircleV(new RLVector2 { x = m_LocalTransform.m7, y = m_LocalTransform.m8 }, m_ColRadius, RLColor.BLUE);
			

		}
	}
}
