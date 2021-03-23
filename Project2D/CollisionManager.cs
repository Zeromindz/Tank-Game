using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2D
{
	class CollisionManager
	{
		private static List<GameObject> m_ObjectList = new List<GameObject>();

		public static void AddObject(GameObject _obj)
		{
			m_ObjectList.Add(_obj);
		}

		public static void CheckCollision()
		{
			foreach(GameObject obj1 in m_ObjectList)
			{
				foreach(GameObject obj2 in m_ObjectList)
				{
					//Dont check if objects are the same
					if (obj1 == obj2)
						continue;
					
					//Test collision here
					// V2 diff = obj1.position - obj2.pos
					//float dist = diff.magnitude
					//floaat combinedradius = obj1.getradius() + obj2.getradius()
					//if dist < combinedradius
					//{resolve collision
				}
			}
		}
	}
}
