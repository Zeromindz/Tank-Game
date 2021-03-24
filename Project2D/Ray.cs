using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;

namespace Project2D
{
	class Ray
	{
		Vector3 m_Origin;
		Vector3 m_Direction;
		float m_Length;

		public Ray()
		{

		}

		public Ray(Vector3 _start, Vector3 _direction, float _length = float.MaxValue)
		{
			m_Origin = _start;
			m_Direction = _direction;
			m_Length = _length;
		}

	}
}
