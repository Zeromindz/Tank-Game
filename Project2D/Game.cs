using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathLibrary;

namespace Project2D
{
    class Game
    {

		#region Timer
        Stopwatch stopwatch = new Stopwatch();
		private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;
		#endregion

		//private Image m_BackgroundImage;
		//private Texture2D m_BackgroundTexture;
		private Level m_Level;

        public Game()
        {

        }

        public void Init()
        {
			#region Stopwatch
			stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }
			#endregion

			//Initialize objects here
			//m_BackgroundImage = LoadImage("../Images/Grass_Tile.png");
			//m_BackgroundTexture = LoadTextureFromImage(m_BackgroundImage);

			m_Level = new Level();

		}

        public void Shutdown()
        {
        }

        public void Update()
        {
			#region DeltaTime
			lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;
			#endregion

			//Update game objects here 

			m_Level.Update(deltaTime);
			m_Level.UpdateTransforms();
			
			//Check collision after all objects have been updated
			CollisionManager.CheckCollision();
		}

		public void Draw()
        {
            BeginDrawing();

            ClearBackground(RLColor.WHITE);

			//DrawTexture(m_BackgroundTexture, 100, 100, RLColor.WHITE);

			//Draw game objects here
            DrawText(fps.ToString(), 10, 10, 14, RLColor.RED);

			m_Level.Draw();

			EndDrawing();
        }
		
    }
}
