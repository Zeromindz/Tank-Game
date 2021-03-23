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
	class Turret : GameObject
	{
		//Tank m_Tank = null;
		//Vector2 m_MousePosition;
		float m_TurretTurnSpeed = 5;
		

		public Turret(string _fileName) : base(_fileName)
		{

			//m_MousePosition = GetMousePosition().ToVector2();

			
			
		}

		public override void Update(float _deltatime)
		{
			float m_Rotation = 0.0f;

			if (IsKeyDown(KeyboardKey.KEY_RIGHT))
			{
				m_Rotation += m_TurretTurnSpeed * _deltatime;
			}
			if (IsKeyDown(KeyboardKey.KEY_LEFT))
			{
				m_Rotation -= m_TurretTurnSpeed * _deltatime;
			}

			if (IsKeyDown(KeyboardKey.KEY_SPACE))
			{
				//Fire Gun
			}

			Matrix3 rotationMatrix = new Matrix3();
			rotationMatrix.SetRotateZ(m_Rotation);

			m_LocalTransform = m_LocalTransform * rotationMatrix;

			base.Update(_deltatime);
		}
	}
}
