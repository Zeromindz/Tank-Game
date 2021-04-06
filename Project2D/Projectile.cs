using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using MathLibrary;
using static Raylib.Raylib;

namespace Project2D
{
	class Projectile : GameObject
	{
		Vector2 m_CurrentPos;
		private Vector2 m_Velocity = new Vector2(0, 0);
		float m_MoveSpeed = 500f;
		bool m_IsDestroyed = false;
		
		

		public Projectile(string _filename) : base(_filename)
		{
			SetAlive(true);

			CollisionManager.AddObject(this);

			m_ObjectTag = 1;
		}

		public override void Update(float _deltaTime)
		{
			//---------------------------------------------------------
			// Bullet Physics
			//---------------------------------------------------------
			m_CurrentPos = GetGlobalPosition();

			m_Velocity.y += -m_MoveSpeed * _deltaTime;

			Matrix3 translation = new Matrix3(true);
			translation.SetTranslation(m_Velocity * _deltaTime);
			
			m_LocalTransform = m_LocalTransform * translation;

			//Bounce off edges of screen
			if (m_CurrentPos.x >= GetScreenWidth() - m_ColRadius || m_CurrentPos.x <= m_ColRadius)
			{
				m_Velocity = Bounce(m_Velocity);
				
			}

			if(m_CurrentPos.y >= GetScreenHeight()- m_ColRadius || m_CurrentPos.y <= m_ColRadius)
			{
				m_Velocity = Bounce(m_Velocity);

			}

			
			base.Update(_deltaTime);
		}

		public void ResetBullet()
		{

		}


		public void SetPosition(Vector2 _position)
		{
			m_LocalTransform.m7 = _position.x;
			m_LocalTransform.m8 = _position.y;
			
		}

		public void SetRotation(float _rotation)
		{
			Matrix3 rotationMatrix = new Matrix3();
			rotationMatrix.SetRotateZ(_rotation);

			m_LocalTransform = m_LocalTransform * rotationMatrix;
		}

		public void SetSpeed(float _speed)
		{
			m_Velocity.y = _speed;
		}

		public override void OnCollision(GameObject _otherObj)
		{
			_otherObj.Destroy();
			base.OnCollision(_otherObj);
		}

		public Vector2 Bounce(Vector2 _velocity)
		{
			//if(m_PreviousPos == null)
			//{
			//	m_PreviousPos = GetGlobalPosition();
			//}
			//m_LocalTransform.m7 = m_PreviousPos.x;
			//m_LocalTransform.m8 = m_PreviousPos.y;
			Vector2 result = -1.0f * _velocity;
			
			return result;
		}

		public override void Draw()
		{
			DrawText("X: " + m_Velocity.x.ToString(), (int)GetGlobalPosition().x - 50, (int)GetGlobalPosition().y + 100, 16, RLColor.RED);
			DrawText("Y: " + m_Velocity.y.ToString(), (int)GetGlobalPosition().x + 50, (int)GetGlobalPosition().y + 100, 16, RLColor.RED);

			DrawCircleV(new RLVector2 { x = m_CurrentPos.x, y = m_CurrentPos.y }, m_ColRadius, RLColor.GREEN);
			base.Draw();
		}
	}
}
