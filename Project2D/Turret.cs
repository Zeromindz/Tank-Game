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
		Vector2 currentGlobalPos;
		Vector2 targetDirection;
		Vector2 m_MousePosition;
		float m_TurretTurnSpeed = 5;
		bool m_FiringGun = false;
		

		public Turret(string _fileName) : base(_fileName)
		{
			SetAlive(true);

		}

		public override void Update(float _deltatime)
		{
			
			m_MousePosition = GetMousePosition().ToVector2();
			currentGlobalPos = GetGlobalPosition();
			targetDirection = m_MousePosition - currentGlobalPos;
			targetDirection.Normalise();

			float rotation = 0.0f;

			if (IsKeyDown(KeyboardKey.KEY_RIGHT))
			{
				rotation += m_TurretTurnSpeed * _deltatime;
			}
			if (IsKeyDown(KeyboardKey.KEY_LEFT))
			{
				rotation -= m_TurretTurnSpeed * _deltatime;
			}

			m_FiringGun = IsKeyDown(KeyboardKey.KEY_SPACE);
			if (m_FiringGun)
			{
				//Fire Gun
				
			}

			Matrix3 rotationMatrix = new Matrix3();
			rotationMatrix.SetRotateZ(rotation);

			m_LocalTransform = m_LocalTransform * rotationMatrix;

			base.Update(_deltatime);
		}

		public override void Draw()
		{
			
			DrawLine((int)currentGlobalPos.x, (int)currentGlobalPos.y, (int)m_MousePosition.x, (int)m_MousePosition.y, RLColor.RED);
			
			base.Draw();
		}

		
	}
}
