﻿using System;
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
		Vector2 m_CurrentPosition = new Vector2(0, 0);

		float m_TurnSpeed = 5.0f;

		float m_Speed = 30f;
		float m_CurrentVelocity = 0.0f;
		float m_MaxVelocity = 400.0f;
		float m_AccelerationRate = 20.0f;

		bool m_IsAccelerating = false;

		public Tank(string _fileName, float _startingPosX, float _startingPosY) : base(_fileName)
		{
			SetAlive(true);

			//starting position
			m_LocalTransform.m7 = _startingPosX;
			m_LocalTransform.m8 = _startingPosY;

		}

		

		public override void Update(float _deltaTime)
		{
			//Vector2 storing global position
			//Vector2 being set to the magnitude of V2Velocity
			m_CurrentPosition = GetGlobalPosition();
			m_CurrentVelocity = m_Velocity.Magnitude();

			float rotation = 0.0f;

			//Console.WriteLine("current velocity = " + m_Velocity.y);
			
			//Set is accellerating to true with w and s key presses
			m_IsAccelerating = (IsKeyDown(KeyboardKey.KEY_W)) || (IsKeyDown(KeyboardKey.KEY_S));

			//update velocity via input
			if (IsKeyDown(KeyboardKey.KEY_W))
			{
				m_Velocity.y -= m_AccelerationRate * m_Speed * _deltaTime;
			}
			if (IsKeyDown(KeyboardKey.KEY_S))
			{
				m_Velocity.y += m_AccelerationRate * m_Speed * _deltaTime;
			}

			//rotate turret
			if (IsKeyDown(KeyboardKey.KEY_D))
			{
				rotation += m_TurnSpeed * _deltaTime;
			}
			if (IsKeyDown(KeyboardKey.KEY_A))
			{
				rotation -= m_TurnSpeed * _deltaTime;
			}

			//if not accelerating and has velocity > 0, lerp from current y velocity to 0 over deltatime
			if(!m_IsAccelerating && m_CurrentVelocity > 0)
			{
				m_Velocity.y = Lerp(m_Velocity.y, 0, _deltaTime);

				//Console.WriteLine("Decelerating");
			}
			//cap velocity at max by multiplying y velocity by (max / current)
			if (m_CurrentVelocity > m_MaxVelocity)
		    {
		    	m_Velocity.y *= m_MaxVelocity / m_CurrentVelocity;
		    }

			

			//Add velocity to our local transform
			//avoid accessing localTransform's elements.
			Matrix3 translation = new Matrix3(true);
			translation.SetTranslation(m_Velocity * _deltaTime);
			
			m_LocalTransform = m_LocalTransform * translation;

			//same with rotation
			Matrix3 rotationMatrix = new Matrix3();
			rotationMatrix.SetRotateZ(rotation);
			
			m_LocalTransform = m_LocalTransform * rotationMatrix;

			base.Update(_deltaTime);
		}

		

		public float Lerp(float _start, float _end, float _time)
		{
			float result = _start * (1 - _time) + _end * _time;
			return result;
		}

		public override void OnCollision(GameObject _otherObj)
		{
			//Push objects apart

			m_LocalTransform.m7 = m_PreviousPos.x;
			m_LocalTransform.m8 = m_PreviousPos.y;
			UpdateTransforms();

			//Circle Collision
			Vector2 normal = _otherObj.GetGlobalPosition() - GetGlobalPosition();
			normal.Normalise();

			//Calculate reflection
			Vector2 reflection = -2.0f * m_Velocity.Dot(normal) * normal + m_Velocity;

			//Change Direction
			m_Velocity = reflection;

			base.OnCollision(_otherObj);

			Console.WriteLine("Colliding");
		}

		public override void Draw()
		{
			//Draw player object collider 
			DrawCircleV(new RLVector2 { x = m_CurrentPosition.x, y = m_CurrentPosition.y }, m_ColRadius, RLColor.BLUE);
			base.Draw();
		}
	}
}

