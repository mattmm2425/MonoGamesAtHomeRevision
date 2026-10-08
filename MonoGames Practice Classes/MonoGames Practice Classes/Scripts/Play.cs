using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing.Imaging.Effects;
using System.IO;
using System.Reflection;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Play
    {
        Player player; //references the player script
        Enemy enemy; //references the enemy script

        private Level level;

        public Play() //constructor that calls forth the enemy and player constructors and puts in their parameters
        {
            player = new Player(new Vector2 (200, 500), 3);
            enemy = new Enemy(new Vector2(100, 100));
            level = new Level();

        }

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics) 
        {
            player.LoadContent(cm, "CharacterSprites"); //loads the sprite sheet of CharacterSprites onto the 2D texture asset
            enemy.LoadContent(cm, "OrcEnemySprites"); //loads the sprite sheet of the OrcEnemySprites onto the 2D texture asset
            level.LoadContent(cm, "StoneWallTexture"); //loads the sprite sheet of the StoneWallTexture onto the 2D texture asset
        }

        public E_Gamestates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.Menu; //takes players back to the menu if they press the escape button
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && level.GetCurrentLevel() < 2) //if the player presses Enter then the game will reset the player and enemy positions, reset the players lives and reset the level
            {
                level.NextLevel();
                player.ResetPosition();
                enemy.ResetPosition();
                return E_Gamestates.Play;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) && level.GetCurrentLevel() > 1) //if the player presses P then the game will reset the player and enemy positions, reset the players lives and reset the level
            {
                level.ResetLevels();
                player.ResetPosition();
                enemy.ResetPosition();
                return E_Gamestates.Play;
            }
            else if (enemy.Caught(player) == true && player.GetLives() <= 0) //if the player gets caught and they have no lives remaining then the player gets taken to the gameover screen
            {
                enemy.ResetPosition();
                player.ResetPosition();
                player.ResetLives();
                level.ResetLevels();
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

                PlayerMovement(4f); //calls movement function
                enemy.Chase(player);    //calls enemy chase function and inputs new instance of the player so the enemy character knows who to chases
                Console.WriteLine("Player Lives: " + player.GetLives());
                return E_Gamestates.Play;   //user continues in play state until conditions chase
            }

            
        }

        public void Draw(GraphicsDevice graphics, SpriteBatch sprite)
        {
            graphics.Clear(Color.CornflowerBlue);   //sets background of the playing state
            //sprite.Begin();
            level.Draw(sprite);
            player.Draw(sprite, new Rectangle(0, player.spriteSheetY, 52, 72));  //creates a rectangle on the spritesheet that will be placed on given coordinates and outputs whatever sprite is there. 0,0 is the top left corner of the sprite sheet
            enemy.Draw(sprite, new Rectangle(0, enemy.spriteSheetY, 52, 72));
            //sprite.End();
        }

        public Vector2 GetScreenWH()
        {
            return level.GetLevelSize(); //returns the level size to the scene manager so it can be used to set the game window size
        }

        private void PlayerMovement(float speed)    //player movement function that takes in an input of speed
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
        
        public int GetLevel()
        {
            return level.GetCurrentLevel();
        }

    }
}
