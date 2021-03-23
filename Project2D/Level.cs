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
	class Level : GameObject
	{
		private Tank m_Tank = null;
		private Turret m_Turret = null;
		private Wall m_Wall = null;

		public Level() : base("")
		{
			m_Tank = new Tank("../Images/Car_v3.png");
			m_Turret = new Turret("../Images/Gun_v2.png");
			m_Wall = new Wall();

			m_Tank.SetParent(this);
			m_Turret.SetParent(m_Tank);
		}

		public override void Update(float _deltatime)
		{
			m_Tank.Update(_deltatime);
			m_Tank.UpdateTransforms();

			m_Turret.Update(_deltatime);
			m_Turret.UpdateTransforms();

			base.Update(_deltatime);
		}

		public void Draw()
		{
			m_Tank.Draw();
			m_Turret.Draw();
			m_Wall.Draw();
		}
	}
}
