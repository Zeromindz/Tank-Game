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
			SetAlive(true);

			//Create new objects
			InitObjects();

			
			
		}

		public void InitObjects()
		{
			//Player objects
			m_Tank1 = new Tank("../Images/Car_v3.png", GetScreenWidth() / 2, GetScreenHeight() / 2);
			m_Turret = new Turret("../Images/Gun_v2.png");

			//Environmental objects
			m_Wall = new Wall("../Images/Small_Metal_Box.png", 600, 200, 1, 1);

			//Set parents and add physics objects
			m_Tank1.SetParent(this);
			m_Turret.SetParent(m_Tank1);
			m_Wall.SetParent(this);

			CollisionManager.AddObject(m_Tank1);
			CollisionManager.AddObject(m_Wall);
		}


		public override void Update(float _deltatime)
		{
			//Update all objects and transforms
			m_Tank1.Update(_deltatime);
			m_Tank1.UpdateTransforms();

			m_Turret.Update(_deltatime);
			m_Turret.UpdateTransforms();

			m_Wall.Update(_deltatime);
			m_Wall.UpdateTransforms();
			base.Update(_deltatime);
		}

		public override void Draw()
		{
			m_Wall.Draw();
			m_Tank1.Draw();
			m_Turret.Draw();
		}
	}
}
