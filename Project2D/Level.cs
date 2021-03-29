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
		private Tank m_Tank1 = null;
		private Turret m_Turret = null;
		private Wall m_Wall = null;

		public Level() : base("")
		{
			m_Tank1 = new Tank("../Images/Car_v3.png", 500, 500);
			m_Turret = new Turret("../Images/Gun_v2.png");
			m_Wall = new Wall(100, 100, 100, 100);

			m_Tank1.SetParent(this);
			m_Turret.SetParent(m_Tank1);
			m_Wall.SetParent(this);
			
		}

		public override void Update(float _deltatime)
		{
			
			m_Tank1.Update(_deltatime);
			m_Tank1.UpdateTransforms();

			m_Turret.Update(_deltatime);
			m_Turret.UpdateTransforms();

			base.Update(_deltatime);
		}

		public override void Draw()
		{
			m_Tank1.Draw();
			m_Turret.Draw();
			m_Wall.Draw();
		}
	}
}
