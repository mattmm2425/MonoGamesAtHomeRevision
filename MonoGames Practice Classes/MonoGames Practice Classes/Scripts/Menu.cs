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
        Player player;

        public Menu(Vector2 dimensions)
        {
            screenWH = dimensions; //used to know how large the screen is and will be used to detect if the mouse cursor is within these dimensions
        }

        public E_Gamestates Update(Game1 game)
        {
            if (Mouse.GetState().X >= 0 && Mouse.GetState().X <= 1023 && Mouse.GetState().Y >= 0 && Mouse.GetState().Y <= 767 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return E_Gamestates.Play; //whilst in the menu if the player presses left mouse on the game screen they will be taken into the playing state
            }
            else if (Mouse.GetState().X >= 0 && Mouse.GetState().X <= 1023 && Mouse.GetState().Y >= 0 && Mouse.GetState().Y <= 767 && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                return E_Gamestates.GameOver; //whilst in the menu if the player presses right mouse on the game screen they will be taken into the playing state
            }
            else
            {
                return E_Gamestates.Menu; //if nothing is pressed or if the mouse cursor isnt within the screen dimensions then the player will remain within the menu
            }

        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.Clear(Color.Crimson); //draws background of the menu screen
        
        }
    }
}
