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
		private Wall m_Wall;
		private Wall[] m_WallArray;



		int m_WallCount = 4;
		float m_MaxAmmo = 10;
		//private Projectile m_Bullet = null;
		

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
			//m_Bullet = new Projectile("../Images/Bullet_Small.png");

			//Environmental objects
			//m_Wall = new Wall("../Images/Small_Metal_Box.png", 600, 500, 1, 1);
			m_WallArray = new Wall[m_WallCount];
			
			for(int i = 0; i < m_WallCount; i++)
			{
				m_WallArray[i] = new Wall("../Images/Small_Metal_Box.png", 600, 500, 1, 1);
				m_WallArray[i].SetParent(this);
				
				//CollisionManager.AddObject(m_WallArray[i]);
			}
			
			m_WallArray[0].SetPosition(new Vector2(200, 200));
			m_WallArray[1].SetPosition(new Vector2(800, 800));
			m_WallArray[2].SetPosition(new Vector2(100, 700));
			m_WallArray[3].SetPosition(new Vector2(1400, 100));

			//Set parents and add physics objects
			m_Tank1.SetParent(this);
			m_Turret.SetParent(m_Tank1);
			//m_Bullet.SetParent(m_Turret);

			//m_Wall.SetParent(this);
		}

		public override void Update(float _deltatime)
		{
			//Update all objects and transforms
			m_Tank1.Update(_deltatime);
			m_Tank1.UpdateTransforms();

			m_Turret.Update(_deltatime);
			m_Turret.UpdateTransforms();

			//m_Bullet.Update(_deltatime);
			//m_Bullet.UpdateTransforms();

			base.Update(_deltatime);
		}

		public override void Draw()
		{
			for (int i = 0; i < m_WallCount; i++)
			{
				m_WallArray[i].Draw();	
			}
			m_Tank1.Draw();
			m_Turret.Draw();

			//m_Wall.Draw();
			//m_Bullet.Draw();
		}
	}
}
