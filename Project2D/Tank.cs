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
		
	    Vector2 m_Velocity = new Vector2(0, 0);

		float m_MovementSpeed = 100f;
		float m_TurnSpeed = 4f;

		public Tank(string _fileName) : base(_fileName)
		{
			//starting position

			m_LocalTransform.m7 = 500;
			m_LocalTransform.m8 = 500;
		}

		public override void Update(float _deltatime)
		{
			float rotation = 0.0f;

			//update velocity via input
			if (IsKeyDown(KeyboardKey.KEY_W))
			{
				m_Velocity.y -= m_MovementSpeed *_deltatime;
			}
			if (IsKeyDown(KeyboardKey.KEY_S))
			{
				m_Velocity.y += m_MovementSpeed * _deltatime;
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

	}
}
