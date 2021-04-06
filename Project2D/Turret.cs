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
		float m_AmmoCount = 10;

		private Projectile m_Bullet = null;

		public Turret(string _fileName) : base(_fileName)
		{
			m_Bullet = new Projectile("../Images/Bullet_Small.png");
			//m_Bullet.SetPosition(currentGlobalPos);
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

			if(IsKeyPressed(KeyboardKey.KEY_SPACE))
			{
				
				FireGun();
				CollisionManager.AddObject(m_Bullet);
			}

			Matrix3 rotationMatrix = new Matrix3();
			rotationMatrix.SetRotateZ(rotation);

			m_LocalTransform = m_LocalTransform * rotationMatrix;

			m_Bullet.Update(_deltatime);
			m_Bullet.UpdateTransforms();

			m_Bullet.SetRotation(rotation);

			base.Update(_deltatime);
		}

		public void FireGun()
		{
			if (m_Bullet == null)
			{
				
			}
			
			m_Bullet.SetPosition(currentGlobalPos);
			m_Bullet.SetSpeed(0);

			
		}

		public override void Draw()
		{
			base.Draw();
			
			DrawLine((int)currentGlobalPos.x, (int)currentGlobalPos.y, (int)m_MousePosition.x, (int)m_MousePosition.y, RLColor.RED);
			m_Bullet.Draw();
		}

		
	}
}
