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

		GameObject testObject;

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
			//logo = LoadImage("../Images/Car_Red.png");
			//texture = LoadTextureFromImage(logo);

			testObject = new Tank("../Images/Car_Red.png");
			
			
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

			testObject.Update(deltaTime);
			testObject.UpdateTransforms();
		}

		public void Draw()
        {
            BeginDrawing();

            ClearBackground(RLColor.WHITE);

			//Draw game objects here
            DrawText(fps.ToString(), 10, 10, 14, RLColor.RED);

			testObject.Draw();
			//DrawTexture(texture, GetScreenWidth() / 2 - texture.width / 2, GetScreenHeight() / 2 - texture.height / 2, RLColor.WHITE);

			EndDrawing();
        }

    }
}
