using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class GameOver
    {
        private double timeLimit;
        private double totalTime;

        public GameOver()
        {
            
            totalTime = 0.0f;
            timeLimit = 5.0f;

        }

        public E_Gamestates Update(GameTime gameTime)
        {
            totalTime = gameTime.ElapsedGameTime.TotalSeconds;

            if (totalTime >= timeLimit)
            {
                totalTime = 0;
                return E_Gamestates.Menu;
            }
            else
            { 
                return E_Gamestates.Menu;
            }

        }

        public void Draw(GraphicsDevice graphics)
        { 
            
            graphics.Clear(Color.White);

        }

    }
}
