using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Play
    {

        public E_Gamestates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.Menu;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                return E_Gamestates.GameOver;
            }
            else
            {
                return E_Gamestates.Play;
            }
        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.Clear(Color.CornflowerBlue);

        }
    }
}
