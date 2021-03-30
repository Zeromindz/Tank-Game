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
		private Vector3 m_Origin;
		private Vector3 m_Direction;
		private float m_Length;

		public Ray()
		{

		}

		public Ray(Vector3 _start, Vector3 _direction, float _length = float.MaxValue)
		{
			m_Origin = _start;
			m_Direction = _direction;
			m_Length = _length;
		}

		//more to go here
		float Clamp(float t, float a, float b)
		{
			return Math.Max(a, Math.Min(a, t));
		}

		public Vector3 ClosestPoint(Vector3 _point)
		{
			//Find the vector to arbitrary point
			Vector3 p = _point - m_Origin;

			//Dot product the vector against the ray direction and clamp by length
			float t = Clamp(p.Dot(m_Direction), 0, m_Length);

			//return position in direction of ray
			return m_Origin + m_Direction * t;
		}

		public bool IntersectsWithCircle(Vector3 _point, float _radius, Vector3 I = null)
		{
			//Ray origin to sphere center
			Vector3 p = _point - m_Origin;

			//project sphere center onto ray
			float t = p.Dot(m_Direction);

			//Get sqr distance from sphere to ray
			float dd = p.Dot(p) - t * t;

			//Subtract penetration amount from projected distance
			t -= (float)Math.Sqrt(_radius * _radius - dd);

			//it intersects if within ray length
			if (t >= 0 && t <= m_Length)
			{
				//Store intersection point if requested
				if (I != null)
				{
					I = m_Origin + m_Direction * t;
				}
				return true;
			}

			//default no intersection
			return false;
		}

		public bool IntersectsWithPlane(Vector3 _normal, float _offset, Vector3 I = null, Vector3 R = null)
		{
			//project ray onto plane normal
			float t = m_Direction.Dot(_normal);

			//must face the place
			if(t > 0)
			{
				return false;
			}

			//Get distance of ray origin to the plane
			float d = m_Origin.Dot(_normal) + _offset;

			return false;
		}
		
		public bool IntersectsWithAABB(Vector3 _min, float _max, Vector3 I = null, Vector3 R = null)
		{


			return false;
		}

	}
}
