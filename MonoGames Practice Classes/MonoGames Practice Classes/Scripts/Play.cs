using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        private string LivesText;

        private RenderTarget2D renderTarget;

        private Texture2D background;
        private Texture2D background2;
        private Texture2D foreground;
        private Texture2D foreground2;
        private Vector2 playingPosition;
        public Play() //constructor that calls forth the enemy and player constructors and puts in their parameters
        {
            level = new Level();
            player = new Player(level.GetPlayerSpawn(), 3, level, new Rectangle(0, 0, 64, 64));
            for (int i = 0; i < level.basicEnemyCount;)
            {
                enemies.Add(new Enemy(level.basicEnemySpawns[i], level, new Rectangle(0, 0, 52, 72)));
                enemies[i].NewSpawnPoint(level.basicEnemySpawns[i]);
                i++;
            }
            horizonEnemy = new HorizontalEnemy(new Vector2(800, 200), level, new Rectangle(0, 306, 52, 72));
            JumpIsPressed = false;
        }

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphicsManager, GraphicsDevice graphics)
        {
            renderTarget = new RenderTarget2D(graphics, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
            player.LoadContent(cm, "MainCharacterFox"); //loads the sprite sheet of CharacterSprites onto the 2D texture asset
            foreach (Enemy enemy in enemies)
            {
                enemy.LoadContent(cm, "OrcEnemySprites"); //loads the sprite sheet of the OrcEnemySprites onto the 2D texture asset
                enemySprite = enemy.Sprite;
            }
            horizonEnemy.LoadContent(cm, "OrcEnemySprites");
            level.LoadContent(cm, "StoneWallTexture", "Platform"); //loads the sprite sheet of the StoneWallTexture onto the 2D texture asset

            background = cm.Load<Texture2D>("BackgroundSunnyLand");
            background2 = cm.Load<Texture2D>("middle");
            foreground = cm.Load<Texture2D>("tree");
            foreground2 = cm.Load<Texture2D>("rock-2");
        }

        public E_Gamestates Update(double DeltaTime, GraphicsDevice graphics, GraphicsDeviceManager graphicsManager)
        {
            LivesText = "Lives: " + player.GetLives();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.Menu; //takes players back to the menu if they press the escape button
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && level.GetCurrentLevel() < 2) //if the player presses Enter then the game will reset the player and enemy positions, reset the players lives and reset the level
            {
                level.NextLevel();
                resetPlayingLevel(graphics);
                return E_Gamestates.Play;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P) && level.GetCurrentLevel() > 1) //if the player presses P then the game will reset the player and enemy positions, reset the players lives and reset the level
            {
                level.ResetLevels();
                resetPlayingLevel(graphics);
                return E_Gamestates.Play;
            }

            foreach (Enemy enemy in enemies)
            {
                if (enemy.CollidesWith(player) == true || player.CollidesWith(enemy) == true) //if the player gets caught and they have no lives remaining then the player gets taken to the gameover screen
                {
                    player.LoseLife();
                    resetPlayingLevel(graphics);
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
            //if the player hasn't been caught or pressed escape this code will run and the player will be able to move around the level and the enemies will chase the player

            if (horizonEnemy.CollidesWith(player) == true || player.CollidesWith(horizonEnemy) == true) //if the player gets caught and they have no lives remaining then the player gets taken to the gameover screen
            {
                player.LoseLife();
                resetPlayingLevel(graphics);
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

            horizonEnemy.JumpOrFall();
            player.JumpOrFall();
            PlayerMovement(DeltaTime, graphicsManager); //calls movement function
            foreach (Enemy enemy in enemies)
            {
                enemy.Chase(player);    //calls enemy chase function and inputs new instance of the player so the enemy character knows who to chases
                enemy.UpdateAnimation(DeltaTime); //updates the animation of the enemy based on the current state
            }
            Console.WriteLine(playingPosition.X);
            horizonEnemy.Chase(player);
            horizonEnemy.UpdateAnimation(DeltaTime);
            //Console.WriteLine("Player Lives: " + player.GetLives());
            return E_Gamestates.Play;   //user continues in play state until conditions chase



        }

        private void resetPlayingLevel(GraphicsDevice graphics)
        {
            renderTarget = new RenderTarget2D(graphics, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
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
            horizonEnemy.NewSpawnPoint(new Vector2(800, 200));
            player.ResetPosition();
        }


        public void Draw(GraphicsDevice graphics, SpriteBatch sprite, HUD gameHUD, GraphicsDeviceManager graphicsManager)
        {
            DrawRenderTarget(sprite, graphics, graphicsManager);
            graphics.Clear(Color.CornflowerBlue);   //sets background of the playing state
            sprite.Begin();

            int rectx = (int)player.GetCurrentPosition().X - graphicsManager.PreferredBackBufferWidth / 2;
            int recty = (int)player.GetCurrentPosition().Y - graphicsManager.PreferredBackBufferHeight / 2;

            if (rectx > (int)GetLevelWH().X - graphicsManager.PreferredBackBufferWidth)
            {
                rectx = (int)GetLevelWH().X - graphicsManager.PreferredBackBufferWidth;
            }
            else if (rectx < 0)
                rectx = 0;

            if (recty > (int)GetLevelWH().Y - graphicsManager.PreferredBackBufferHeight)
            {
                recty = (int)GetLevelWH().Y - graphicsManager.PreferredBackBufferHeight;
            }
            else if (recty < 0)
                recty = 0;

            sprite.Draw(renderTarget, new Vector2(0, 0), new Rectangle(rectx, recty, graphicsManager.PreferredBackBufferWidth, graphicsManager.PreferredBackBufferHeight), Color.White);

            gameHUD.SetMessage("Level: " + level.GetCurrentLevel());
            gameHUD.DrawString(sprite, new Vector2(graphicsManager.PreferredBackBufferWidth - 200, 10), Color.Black);
            gameHUD.DrawLives(sprite, new Vector2(20, 20), player.GetLives());
            gameHUD.SetMessage("Score: " + player.GetScore());
            gameHUD.DrawString(sprite, new Vector2(graphicsManager.PreferredBackBufferWidth / 2, 20), Color.Black);
            sprite.End();
        }

        public Vector2 GetLevelWH()
        {
            return level.GetLevelSize(); //returns the level size to the scene manager so it can be used to set the game window size
        }

        private void PlayerMovement(double deltaTime, GraphicsDeviceManager graphicsManager)    //player movement function that takes in an input of speed
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.Right();

                if (player.GetCanScroll())
                {
                    if (player.GetCurrentPosition().X < (int)GetLevelWH().X - graphicsManager.PreferredBackBufferWidth / 2 &&
                        player.GetCurrentPosition().X > graphicsManager.PreferredBackBufferWidth/2)
                    {
                        playingPosition.X -= player.GetSpeed();
                    }
                }

                if (player.GetIsJumping() == true)
                {
                    player.SetAnimation(Creature.Animations.Jump);
                }
                else if (player.GetIsFalling() == true)
                {
                    player.SetAnimation(Creature.Animations.Fall);
                }
                else
                {
                    player.SetAnimation(Creature.Animations.Walk);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.Left();

                if (player.GetCanScroll())
                {
                    if (player.GetCurrentPosition().X < (int)GetLevelWH().X - graphicsManager.PreferredBackBufferWidth / 2 &&
                        player.GetCurrentPosition().X > graphicsManager.PreferredBackBufferWidth / 2)
                    {
                        playingPosition.X += player.GetSpeed();
                    }
                }

                if (player.GetIsJumping() == true)
                {
                    player.SetAnimation(Creature.Animations.Jump);
                }
                else if (player.GetIsFalling() == true)
                {
                    player.SetAnimation(Creature.Animations.Fall);
                }
                else if (player.GetIsFalling() == false && player.GetIsJumping() == false)
                {
                    player.SetAnimation(Creature.Animations.Walk);
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (!JumpIsPressed)
                {
                    JumpIsPressed = true;
                    player.SetIsJumping();
                    player.SetAnimation(Creature.Animations.Jump);
                }
                //player.Up(speed);
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                JumpIsPressed = false;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.W))
            {
                if (player.GetIsJumping() == true)
                {
                    player.SetAnimation(Creature.Animations.Jump);
                }
                else if (player.GetIsFalling() == true)
                {
                    player.SetAnimation(Creature.Animations.Fall);
                }
                else
                {
                    player.SetIdleFrames();
                    player.SetAnimation(Creature.Animations.Idle);
                }
            }

            /*if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.Down(speed);
            }*/

            player.UpdateAnimation(deltaTime); //updates the animation of the player based on the current state

        }

        private void DrawRenderTarget(SpriteBatch sprite, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsManager)
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.CornflowerBlue);
            sprite.Begin();
            for (int Back1 = 0; Back1 < 2; Back1++)
            {
                sprite.Draw(background, new Vector2((background.Width * Back1) + playingPosition.X * -0.5f, 0), new Rectangle(0, 0, background.Width, background.Height), Color.White);
            }
            level.Draw(sprite);
            player.Draw(sprite);  //creates a rectangle on the spritesheet that will be placed on given coordinates and outputs whatever sprite is there. 0,0 is the top left corner of the sprite sheet
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(sprite);
            }
            horizonEnemy.Draw(sprite);
            sprite.End();
            graphicsDevice.SetRenderTarget(null);
        }
    }
}
