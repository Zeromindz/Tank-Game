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
		Vector2 m_CurrentPosition;

		float m_Rotation = 0.0f;
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

			m_ObjectTag = 1;

			CollisionManager.AddObject(this);
		}

		public override void Update(float _deltaTime)
		{
			//Vector2 being set to the magnitude of V2Velocity
			m_CurrentVelocity = m_Velocity.Magnitude();
			m_CurrentPosition = GetGlobalPosition();
			//Tank rotation
			m_Rotation = 0.0f;

			//---------------------------------------------------------------------------------
			// Tank Movement
			// Set IsAccellerating to true with w and s key presses
			// Update y velocity via W+S input
			// Rotate tank via A+D input
			// If not accelerating and has velocity > 0, lerp from current y velocity to 0 over deltatime (Deceleration)
			// Cap velocity at max by multiplying y velocity by (max / current)
			//---------------------------------------------------------------------------------
			m_IsAccelerating = (IsKeyDown(KeyboardKey.KEY_W)) || (IsKeyDown(KeyboardKey.KEY_S));
			
			if (IsKeyDown(KeyboardKey.KEY_W))
			{
				m_Velocity.y -= m_AccelerationRate * m_Speed * _deltaTime;
			}
			if (IsKeyDown(KeyboardKey.KEY_S))
			{
				m_Velocity.y += m_AccelerationRate * m_Speed * _deltaTime;
			}
			if (IsKeyDown(KeyboardKey.KEY_D))
			{
				m_Rotation += m_TurnSpeed * _deltaTime;
			}
			if (IsKeyDown(KeyboardKey.KEY_A))
			{
				m_Rotation -= m_TurnSpeed * _deltaTime;
			}

			if(!m_IsAccelerating && m_CurrentVelocity > 0)
			{
				m_Velocity.y = Lerp(m_Velocity.y, 0, _deltaTime);
			}

			if (m_CurrentVelocity > m_MaxVelocity)
		    {
		    	m_Velocity.y *= m_MaxVelocity / m_CurrentVelocity;
		    }

			//---------------------------------------------------------------------------------
			// Add velocity to our local transform
			// Avoid accessing localTransform's elements.
			// Same with rotation
			//---------------------------------------------------------------------------------
			Matrix3 translation = new Matrix3(true);
			translation.SetTranslation(m_Velocity * _deltaTime);
			
			m_LocalTransform = m_LocalTransform * translation;

			Matrix3 rotationMatrix = new Matrix3();
			rotationMatrix.SetRotateZ(m_Rotation);
			
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
			Console.WriteLine("Colliding");

			//---------------------------------------------------------------------------------
			// Circle Collision
			// Calculate reflection
			// Push objects apart
			// Change Direction
			//---------------------------------------------------------------------------------
			m_LocalTransform.m7 = m_PreviousPos.x;
			m_LocalTransform.m8 = m_PreviousPos.y;
			
			Vector2 normal = GetGlobalPosition() - _otherObj.GetGlobalPosition();
			normal.Normalise();

			Vector2 reflection = -1.0f * m_Velocity;
			m_Velocity = reflection;

			UpdateTransforms();
			base.OnCollision(_otherObj);

			DrawText("Reflect X : " + reflection.x.ToString(), 50, 100, 16, RLColor.RED);
			DrawText("Reflect Y : " + reflection.y.ToString(), 50, 130, 16, RLColor.RED);
		}
		
		

		public override void Draw()
		{
			//Draw player object collider 
			DrawText("X: " + m_Velocity.x.ToString(), (int)GetGlobalPosition().x - 50, (int)GetGlobalPosition().y + 100, 16, RLColor.RED);
			DrawText("Y: " + m_Velocity.y.ToString(), (int)GetGlobalPosition().x + 50, (int)GetGlobalPosition().y + 100, 16, RLColor.RED);
			//DrawCircleV(new RLVector2 { x = m_CurrentPosition.x, y = m_CurrentPosition.y }, m_ColRadius, RLColor.BLUE);

			base.Draw();
		}
	}
}

