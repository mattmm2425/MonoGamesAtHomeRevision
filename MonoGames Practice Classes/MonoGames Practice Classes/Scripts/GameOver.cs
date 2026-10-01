using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class GameOver
    {
        private double timeLimit;
        private double totalTime;

        public GameOver()
        {
            
            totalTime = 0.0f;
            timeLimit = 3.0f;

        }

        public E_Gamestates Update(double deltaTime)
        {
            totalTime += deltaTime;
            Debug.Write(deltaTime);

            if (totalTime >= timeLimit)
            {
                totalTime = 0;
                return E_Gamestates.Menu;
            }
            else
            { 
                return E_Gamestates.GameOver;
            }
            

        }

        public void Draw(GraphicsDevice graphics)
        { 
            
            graphics.Clear(Color.White);

        }

    }
}
