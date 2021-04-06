using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;

namespace Project2D
{
	class CollisionManager
	{
		private static List<GameObject> m_ObjectList = new List<GameObject>();

		public static void AddObject(GameObject _obj)
		{
			m_ObjectList.Add(_obj);
		}

		public static void RemoveObject(GameObject _obj)
		{
			m_ObjectList.Remove(_obj);
		}

		public static void CheckCollision()
		{
			foreach(GameObject obj1 in m_ObjectList.ToList())
			{
				foreach (GameObject obj2 in m_ObjectList.ToList())
				{
					// ----------------------------------------------------------------
					// Dont check if objects are the same
					// Dont check if the tank and projectile is colliding 
					// Tags: 
					// 0 = default
					// 1 = Tank/Projectile
					// 2 = Walls
					// ----------------------------------------------------------------
					if (obj1 == obj2)
						continue;
					if (obj1.GetTag() == obj2.GetTag())
					{
						//Console.WriteLine(obj1 + " and " + obj2 + " colliding");
						continue;
					}

					// ----------------------------------------------------------------
					// Calculate circle collision
					// ----------------------------------------------------------------
					Vector2 diff = obj1.GetGlobalPosition() - obj2.GetGlobalPosition();
					float dist = diff.Magnitude();
					float combinedRadius = obj1.GetRadius() + obj2.GetRadius();
					
					//Test circle collision here
					if (dist < combinedRadius)
					{
						//resolve collision
						obj1.OnCollision(obj2);

						//Console.WriteLine("Obj " + obj1 + " colliding with " + obj2);

						Vector2 obj1Min = obj1.GetMin() + obj1.GetGlobalPosition();
						Vector2 obj1Max = obj1.GetMax() + obj1.GetGlobalPosition();
						Vector2 obj2Min = obj1.GetMin() + obj2.GetGlobalPosition();
						Vector2 obj2Max = obj1.GetMax() + obj2.GetGlobalPosition();

						
						if(obj1Max.x > obj2Min.x && obj1Max.y > obj2Min.y &&
							obj1Min.x < obj2Max.x && obj1Min.y < obj2Max.y)
						{
							
							obj1.OnCollision(obj2);

							return;
						}

					}
				}
			}
		}
	}
}
