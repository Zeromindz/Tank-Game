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
	class AABB : GameObject
	{
		List<Vector2> points = new List<Vector2>();

		Vector2 position;

		Vector2 min = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
		Vector2 max = new Vector2(float.PositiveInfinity, float.PositiveInfinity);


		public AABB() : base("")
		{
			position = GetGlobalPosition();

			for(int p = 0; p < points.Count; p++)
			{

			}
		}

		public bool IsEmpty()
		{
			if(float.IsNegativeInfinity(min.x) && float.IsNegativeInfinity(min.y) && 
				float.IsInfinity(max.x) && float.IsInfinity(max.y))
			{
				return true;
			}
			return false;
		}

		public void Empty()
		{
			min = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
			max = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
		}

		void SetToTransformedBox(AABB _box, Matrix3 _m)
		{
			//if box is empty, exit
			if(_box.IsEmpty())
			{
				Empty();
				return;
			}
			
			//examine each element 
			if(_m.m1 > 0.0f)
			{
				min.x += _m.m1 * _box.min.x;
				max.x += _m.m1 * _box.max.x;
			}
		}
		
		public static Vector2 Min(Vector2 _a, Vector2 _b)
		{
			return new Vector2(Math.Min(_a.x, _b.x), Math.Min(_a.y, _b.y));
		}

		public static Vector2 Max(Vector2 _a, Vector2 _b)
		{
			return new Vector2(Math.Max(_a.x, _b.x), Math.Max(_a.y, _b.y));
		}

		public Vector2 Center()
		{
			return (min + max * 0.5f);
		}

		public Vector2 Extents()
		{
			return new Vector2(Math.Abs(max.x - min.x) * 0.5f, Math.Abs(max.y - min.y) * 0.5f);
		}

		public override void Draw()
		{
			base.Draw();

			
		}
	}
}
