using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
using System.Text.RegularExpressions;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Menu
    {
        Vector2 screenWH;

        public Menu(Vector2 dimensions)
        {
            screenWH = dimensions;
        
        }

        public E_Gamestates Update(Game1 game)
        {
            if(Mouse.GetState().X >= 0 && Mouse.GetState().X <=749 && Mouse.GetState().Y >=0 && Mouse.GetState().Y <=399)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    return E_Gamestates.Play;
                }
                else if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    return E_Gamestates.GameOver;
                }

            }

            return E_Gamestates.Menu;
        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.Clear(Color.Crimson);
        
        }
    }
}
