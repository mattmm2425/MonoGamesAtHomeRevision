using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Play
    {
        Player player; //references the player script
        Enemy enemy; //references the enemy script

        public Play() //constructor that calls forth bought the enemy and player construcotrs and puts in their parameters
        {
            player = new Player(new Vector2(500, 500), 3, 0.2f);
            enemy = new Enemy(new Vector2(100, 100));

        }

        public void LoadContent(ContentManager cm) 
        {
            player.LoadContent(cm, "CharacterSprites"); //loads the sprite sheet of CharacterSprites onto the 2D texture asset
            enemy.LoadContent(cm, "OrcEnemySprites"); //loads the sprite sheet of the OrcEnemySprites onto the 2D texture asset
        }

        public E_Gamestates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.Menu; //takes players back to the menu if they press the escape button
            }
            else if (enemy.Caught(player) == true && player.GetLives() <= 0) //if the player gets caught and they have no lives remaining then the player gets taken to the gameover screen
            {
                Console.WriteLine("Player Is Dead");
                return E_Gamestates.GameOver;
            }
            else if (enemy.Caught(player) == true && player.GetLives() > 0) //if the player gets caught and they do have lives remaining then they lose a life and both their locations get reset
            {
                player.LoseLife();
                player.ResetPosition();
                enemy.ResetPosition();
                return E_Gamestates.Play; //user remains in the play state
            }
            else //if the player hasn't been caught or pressed escape the else function will run
            {
                player.Down(0.5f);
                PlayerMovement(0.2f); //calls movement function
                enemy.Chase(player);    //calls enemy chase function and inputs new instance of the player so the enemy character knows who to chases
                Console.WriteLine("Player Position: " + player.GetCurrentPosition());
                //Console.WriteLine("Player Lives: " + player.GetLives());
                return E_Gamestates.Play;   //user continues in play state until conditions chase
            }

            
        }

        public void Draw(GraphicsDevice graphics, SpriteBatch sprite)
        {
            graphics.Clear(Color.CornflowerBlue);   //sets background of the playing state
            sprite.Begin();
            player.Draw(sprite, new Rectangle(0, 0 , 52, 72));  //creates a rectangle on the spritesheet that will be placed on given coordinates and outputs whatever sprite is there. 0,0 is the top left corner of the sprite sheet
            enemy.Draw(sprite, new Rectangle(0, 0, 52, 72));
            sprite.End();
        }

        private void PlayerMovement(float speed)    //player movement function that takes in an input
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.Right(speed);
            } 

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.Left(speed);
            }

            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                player.Up(speed);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.Down(speed);
            }
        }
    }
}
