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
	class Tank : GameObject
	{

		private Vector2 m_Velocity = new Vector2(0, 0);
		Vector2 m_CurrentGlobalPos = new Vector2(0, 0);

		float m_MaxSpeed = 30f;
		float m_CurrentSpeed;
		float m_Acceleration = 15;
		
		float m_TurnSpeed = 4f;

		public Tank(string _fileName, float _startingPosX, float _startingPosY) : base(_fileName)
		{
			//starting position

			m_LocalTransform.m7 = _startingPosX;
			m_LocalTransform.m8 = _startingPosY;

		}

		public override void Update(float _deltatime)
		{
			
			m_CurrentGlobalPos = GetGlobalPosition();

			float rotation = 0.0f;
			
			if (m_CurrentSpeed >= m_MaxSpeed)
				m_CurrentSpeed = m_MaxSpeed;

			Console.WriteLine("m_CurrentSpeed = " + m_CurrentSpeed);

			//update velocity via input
			if (IsKeyDown(KeyboardKey.KEY_W))
			{
				m_CurrentSpeed += m_Acceleration;
				m_Velocity.y -= m_CurrentSpeed * _deltatime;
			}
			
			if (IsKeyDown(KeyboardKey.KEY_S))
			{
				m_Velocity.y += m_Acceleration * _deltatime;
			}
			else
			{
				m_CurrentSpeed /= 2;
			}
			if (IsKeyDown(KeyboardKey.KEY_D))
			{
				rotation += m_TurnSpeed * _deltatime;
			}
			if (IsKeyDown(KeyboardKey.KEY_A))
			{
				rotation -= m_TurnSpeed * _deltatime;
			}

			//Add velocity to our local transform
			//avoid accessing localTransform's elements.
			Matrix3 translation = new Matrix3(true);
			translation.SetTranslation(m_Velocity * _deltatime);
			
			m_LocalTransform = m_LocalTransform * translation;

			//same with rotation
			Matrix3 rotationMatrix = new Matrix3();
			rotationMatrix.SetRotateZ(rotation);
			
			m_LocalTransform = m_LocalTransform * rotationMatrix;

			base.Update(_deltatime);
		}

		
		public override void OnCollision(GameObject _otherObj)
		{
			//Push objects apart
			//Matrix3 translation = new Matrix3(true);
			//translation.SetTranslation(_hitDirection * _penetration);
			//m_LocalTransform = m_LocalTransform * translation;
			//UpdateTransforms();
			m_LocalTransform.m7 = m_PreviousPos.x;
			m_LocalTransform.m8 = m_PreviousPos.y;


			//Circle Collision
			Vector2 normal = _otherObj.GetGlobalPosition() - GetGlobalPosition();
			normal.Normalise();

			//Calculate reflection
			Vector2 reflection = -2.0f * m_Velocity.Dot(normal) * normal + m_Velocity;

			//Change Direction
			m_Velocity = reflection;

			base.OnCollision(_otherObj);
		}

		public override void Draw()
		{
			DrawCircleV(new RLVector2 { x = m_CurrentGlobalPos.x, y = m_CurrentGlobalPos.y }, m_ColRadius, RLColor.BLUE);
			base.Draw();
		}
	}
}

//namespace Raylib
//{
//	partial struct RLVector2
//	{
//		public static implicit RLVector2
//	}
//}
