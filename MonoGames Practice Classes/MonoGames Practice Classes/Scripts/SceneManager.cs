using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
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

        //protected SpriteFont gameFont;
        //private string gameText;
        private HUD gameHUD;

        public SceneManager()
        {
            e_scene = E_Gamestates.Menu;
            playing = new Play();
            gameEnd = new GameOver();
            gameHUD = new HUD();
        }

        public void Update(Game1 game, GameTime time)
        {
            double deltaTime = time.ElapsedGameTime.TotalSeconds;

            switch (e_scene)
            {
                case E_Gamestates.Menu:
                    SwitchState(menu.Update(game));
                    gameHUD.SetMessage("Start Game");
                    break;
                case E_Gamestates.Play:
                    SwitchState(playing.Update(deltaTime));
                    break;
                case E_Gamestates.GameOver:
                    SwitchState(gameEnd.Update(deltaTime));
                    gameHUD.SetMessage("GameOver");
                    break;
                default: break;
            }

        }

        //graphics is used to clear colour
        public virtual void Draw(GraphicsDevice graphics, SpriteBatch sprite)
        {
            switch (e_scene)
            {

                case E_Gamestates.Menu:
                    sprite.Begin();
                    menu.Draw(graphics);
                    gameHUD.DrawString(sprite, new Vector2(playing.GetScreenWH().X / 2 - 120, playing.GetScreenWH().Y / 2 - 200), Color.White);
                    //sprite.DrawString(gameFont, gameText, new Vector2(playing.GetScreenWH().X / 2 - 500, playing.GetScreenWH().Y / 2), Color.White);
                    sprite.End();
                    break;
                case E_Gamestates.Play:
                    sprite.Begin();
                    playing.Draw(graphics, sprite);
                    //sprite.DrawString(gameFont, gameText, new Vector2(playing.GetScreenWH().X / 2 - 400, 40), Color.White);
                    sprite.End();
                    break;
                case E_Gamestates.GameOver:
                    sprite.Begin();
                    gameEnd.Draw(graphics);
                    gameHUD.DrawString(sprite, new Vector2(playing.GetScreenWH().X / 2 - 120, playing.GetScreenWH().Y / 2), Color.Black);
                    //sprite.DrawString(gameFont, gameText, new Vector2(playing.GetScreenWH().X / 2 - 500, playing.GetScreenWH().Y / 2), Color.Black);
                    sprite.End();
                    break;
                default: break;

            }

        }

        //method for reading current state
        private void SwitchState(E_Gamestates state)
        {
            e_scene = state;
        }


        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics)
        {
            playing.LoadContent(cm, graphics);
            gameHUD.LoadContent(cm);
            graphics.PreferredBackBufferWidth = (int)playing.GetScreenWH().X;
            graphics.PreferredBackBufferHeight = (int)playing.GetScreenWH().Y;
            graphics.ApplyChanges();
            menu = new Menu(new Vector2(playing.GetScreenWH().X, playing.GetScreenWH().Y));
        }

    }
}
