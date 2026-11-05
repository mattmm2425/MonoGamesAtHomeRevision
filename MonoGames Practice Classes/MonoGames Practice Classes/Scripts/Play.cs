using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging.Effects;
using System.IO;
using System.Reflection;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Play
    {
        Player player; //references the player script
        Enemy enemy; //references the enemy script
        HorizontalEnemy horizonEnemy;

        Level level;
        private Texture2D enemySprite;
        List<Enemy> enemies = new List<Enemy>();

        private bool JumpIsPressed;

        public Play() //constructor that calls forth the enemy and player constructors and puts in their parameters
        {
            level = new Level();
            player = new Player(level.GetPlayerSpawn(), 3, level, new Rectangle(0,0,52,72));
            for (int i = 0; i < level.basicEnemyCount;)
            {
                enemies.Add(new Enemy(level.basicEnemySpawns[i], level, new Rectangle(0, 0, 52, 72)));
                enemies[i].NewSpawnPoint(level.basicEnemySpawns[i]);
                i++;
            }
            horizonEnemy = new HorizontalEnemy(new Vector2(800, 200), level, new Rectangle(0,306,52,72));
            JumpIsPressed = false;
        }

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics) 
        {
            player.LoadContent(cm, "CharacterSprites"); //loads the sprite sheet of CharacterSprites onto the 2D texture asset
            foreach (Enemy enemy in enemies)
            {
                enemy.LoadContent(cm, "OrcEnemySprites"); //loads the sprite sheet of the OrcEnemySprites onto the 2D texture asset
                enemySprite = enemy.Sprite;
            }
            horizonEnemy.LoadContent(cm, "OrcEnemySprites");
            level.LoadContent(cm, "StoneWallTexture", "Platform"); //loads the sprite sheet of the StoneWallTexture onto the 2D texture asset
        }

        public E_Gamestates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.Menu; //takes players back to the menu if they press the escape button
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && level.GetCurrentLevel() < 2) //if the player presses Enter then the game will reset the player and enemy positions, reset the players lives and reset the level
            {
                level.NextLevel();
                resetPlayingLevel();
                return E_Gamestates.Play;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.P) && level.GetCurrentLevel() > 1) //if the player presses P then the game will reset the player and enemy positions, reset the players lives and reset the level
            {
                level.ResetLevels();
                resetPlayingLevel();
                return E_Gamestates.Play;
            }
            
            foreach (Enemy enemy in enemies)
            {
                if (enemy.CollidesWith(player) == true || player.CollidesWith(enemy) == true) //if the player gets caught and they have no lives remaining then the player gets taken to the gameover screen
                {
                    player.LoseLife();
                    resetPlayingLevel();
                    if (player.GetLives() <= 0)
                    {
                        player.ResetLives();
                        level.ResetLevels();
                        return E_Gamestates.GameOver;
                    }
                    else
                    {
                        return E_Gamestates.Play;
                    }
                }
            }
            //if the player hasn't been caught or pressed escape the else function will run

            player.JumpOrFall();
            PlayerMovement(); //calls movement function
            foreach (Enemy enemy in enemies)
            {
                enemy.Chase(player);    //calls enemy chase function and inputs new instance of the player so the enemy character knows who to chases
            }
            horizonEnemy.Chase(player);
            //Console.WriteLine("Player Lives: " + player.GetLives());
            return E_Gamestates.Play;   //user continues in play state until conditions chase
        

            
        }

        private void resetPlayingLevel()
        {
            player.NewSpawnPoint(level.GetPlayerSpawn());
            //if (level.basicEnemyCount < (enemies.Count))
            //{
            //enemies.Remove(this.enemy);
            //}
            enemies.Clear();
            for (int i = 0; i < level.basicEnemyCount;)
            {
                if (level.basicEnemyCount > (enemies.Count))
                {
                    enemies.Add(new Enemy(level.basicEnemySpawns[i], level, new Rectangle(0, 0, 52, 72)));
                    
                }
                enemies[i].NewSpawnPoint(level.basicEnemySpawns[i]);
                i++;
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Sprite = enemySprite;
                enemy.ResetPosition();
            }
            player.ResetPosition();
        }


        public void Draw(GraphicsDevice graphics, SpriteBatch sprite)
        {
            graphics.Clear(Color.CornflowerBlue);   //sets background of the playing state
            //sprite.Begin();
            level.Draw(sprite);
            player.Draw(sprite, new Rectangle(0, player.spriteSheetY, 52, 72));  //creates a rectangle on the spritesheet that will be placed on given coordinates and outputs whatever sprite is there. 0,0 is the top left corner of the sprite sheet
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(sprite, new Rectangle(0, enemy.spriteSheetY, 52, 72));
            }
            horizonEnemy.Draw(sprite, new Rectangle(horizonEnemy.spriteSheetX, horizonEnemy.spriteSheetY, 52, 72));
            //sprite.End();
        }

        public Vector2 GetScreenWH()
        {
            return level.GetLevelSize(); //returns the level size to the scene manager so it can be used to set the game window size
        }

        private void PlayerMovement()    //player movement function that takes in an input of speed
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.Right();
            } 

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.Left();
            }

            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (!JumpIsPressed)
                {
                    JumpIsPressed = true;
                    player.SetIsJumping();
                }
                //player.Up(speed);
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                JumpIsPressed = false;
            }

            /*if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.Down(speed);
            }*/
        }
        
        public int GetLevel()
        {
            return level.GetCurrentLevel();
        }

    }
}
