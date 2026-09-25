using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGames_Practice_Classes.Scripts
{
    public enum E_Gamestates
    {
        Menu = 0,
        Play = 1,
        GameOver = 2
    }


    internal class SceneManager
    {
        E_Gamestates scene;
        Menu menu;
        Play playing;
        GameOver gameEnd;

        public SceneManager(Vector2 dimensions)
        {
            scene = E_Gamestates.Menu;
            playing = new Play();
            menu = new Menu(dimensions);
            gameEnd = new GameOver();

        }

        public void Update(Game1 game, GameTime time)
        {


        }

        //graphics is used to clear colour
        public void Draw(GraphicsDevice graphics)
        { 
        

        }

        //method for reading current state
        private void SwitchState(E_Gamestates state)
        {
            scene = state;
        
        }

    }
}
