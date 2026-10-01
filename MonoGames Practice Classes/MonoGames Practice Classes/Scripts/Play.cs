using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Play
    {
        Player player;
        Enemy enemy;

        public Play()
        {
            player = new Player(new Vector2(300, 300), 3);
            enemy = new Enemy(new Vector2(100, 100));

        }

        public E_Gamestates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.Menu;
            }
            else if (enemy.Caught(player) == true && player.GetLives() <= 0)
            {
                return E_Gamestates.GameOver;
            }
            else if (enemy.Caught(player) == true && player.GetLives() > 0)
            {
                player.LoseLife();
                player.ResetPosition();
                enemy.ResetPosition();
                return E_Gamestates.Play;
            }
            else
            {
                enemy.Chase(player);
                Console.WriteLine("Enemy Position: " + enemy.GetCurrentPosition());
                Console.WriteLine("Player Position: " + player.GetCurrentPosition());
                Console.WriteLine("Player Lives: " + player.GetLives());
                return E_Gamestates.Play;
            }

            
        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.Clear(Color.CornflowerBlue);

        }
    }
}
