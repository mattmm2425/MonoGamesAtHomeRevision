using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

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
        E_Gamestates e_scene;
        Menu menu;
        Play playing;
        GameOver gameEnd;

        public SceneManager(Vector2 dimensions)
        {
            e_scene = E_Gamestates.Menu;
            playing = new Play();
            menu = new Menu(dimensions);
            gameEnd = new GameOver();

        }

        public void Update(Game1 game, GameTime time)
        {
            double deltaTime = time.ElapsedGameTime.TotalSeconds;

            switch (e_scene)
            { 
                case E_Gamestates.Menu:
                    SwitchState(menu.Update(game));
                    break;
                case E_Gamestates.Play:
                    SwitchState(playing.Update());
                    break;
                case E_Gamestates.GameOver:
                    SwitchState(gameEnd.Update(deltaTime));
                    break;
                default: break;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                Debug.Write(e_scene);
            }
        }

        //graphics is used to clear colour
        public void Draw(GraphicsDevice graphics)
        {

            switch (e_scene)
            {
                case E_Gamestates.Menu:
                    menu.Draw(graphics);
                    break;
                case E_Gamestates.Play:
                    playing.Draw(graphics);
                    break;
                case E_Gamestates.GameOver:
                    gameEnd.Draw(graphics);
                    break;
                default : break;

            }


        }

        //method for reading current state
        private void SwitchState(E_Gamestates state)
        {
            e_scene = state;
        
        }

    }
}
