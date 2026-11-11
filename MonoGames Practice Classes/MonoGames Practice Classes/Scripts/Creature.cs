using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Creature
    {
        //protected variables that only the child classes can inherit
        protected Vector2 currentPosition;
        protected Vector2 startPosition;
        public Texture2D Sprite; //all characters need sprite so adding it in parent class saves time
        public int spriteSheetY { get; protected set; }
        public int spriteSheetX { get; protected set; }
        protected bool isFacingRight;

        public Level currentLevel;
        public Rectangle spriteDimensions;
        public bool isDead = false;
        protected bool isJumping;
        protected int maxHeight;
        protected int currentHeight;
        protected float Speed = 3f;
        protected float FallSpeed = 3f;

        public enum Animations
        {   
            Idle = 0,
            Walk = 1,
            Jump = 2,
            Fall = 3,
            LookUp = 4
        }

        private int FrameIndex = 0;
        private double currentFrameTime = 0;
        private double frameTimeLimit = 0;
        public Animations e_currentAnimation;

        //public constructor
        public Creature(Vector2 startPos, Level playLevel, Rectangle ParamSpriteDimensions) //creature constructor, sets all the most important variables in player and enemy child classes
        {
            startPosition = startPos;
            currentPosition = startPos;
            currentLevel = playLevel;
            spriteDimensions = ParamSpriteDimensions;
            isJumping = false;
            maxHeight = 170;
            currentHeight = 0;
            FrameIndex = 0;
            currentFrameTime = 0;
            frameTimeLimit = 0.2;
            e_currentAnimation = Animations.Idle;
        }

        public void LoadContent(ContentManager cm, string spriteName) //function for loading a 2d Texture asset on a character, the asset is defined by the string variable spriteName
        {
            Sprite = cm.Load<Texture2D>(spriteName);

        }

        public void Draw(SpriteBatch spriteBatch) //function that all characters will use to draw their correct sprite from their sprite sheet
        {
            switch (e_currentAnimation)
            { 
                case Animations.Idle:
                    spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 0, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    spriteSheetY = 0;
                    break;
                case Animations.Walk: 
                    spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 144, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    if (!isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 144, spriteDimensions.Width, spriteDimensions.Height), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    spriteSheetY = 144; 
                    break;
                case Animations.Jump:
                    spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 216, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    spriteSheetY = 216; 
                    break;
                default:
                    break;
            }
        }


        public virtual void Up(float speed) //moves character up by 1 unit on the Y axis
        {
            if (!currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Both") &&
                !currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Both"))
            {
                currentPosition.Y = currentPosition.Y - (speed * 2.5f);
                currentHeight += (int)(speed * 2);
            }
            else if (currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Wall") ||
                    currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Wall"))
            {
                isJumping = false;
            }
            else if (!currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y + 64, "Platform") && isJumping &&
                    !currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + 64, "Platform") && isJumping)
            {
                currentPosition.Y = currentPosition.Y - (speed * 2.5f);
                currentHeight += (int)(speed * 2);
            }
            else
            {
                if (isJumping)
                isJumping = false;
            }
        }

        public virtual void Down(float speed) //moves character down by 1 unit on the Y axis
        {
            bool OnFloor;
            bool OnPlatform;
            bool SameRow;

            OnFloor = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall");

            OnPlatform = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform");

            SameRow = currentLevel.IsInSameRow((int)currentPosition.Y + spriteDimensions.Height - 1,
                (int)currentPosition.Y + spriteDimensions.Height - 1 + (int)Speed);

            if (!OnFloor && !(OnPlatform && !SameRow))
            {
                currentPosition.Y += FallSpeed;
            }

            /*
            if (!currentLevel.IsWall((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + spriteDimensions.Height - 1) &&
                !currentLevel.IsWall((int)currentPosition.X, (int)currentPosition.Y + spriteDimensions.Width - 1))
            {
                currentPosition.Y += speed;
            }*/
        }

        public virtual void Left() //moves character left by 1 unit on the X axis
        {
            if (!currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Both") &&
                !currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y + spriteDimensions.Height - 1, "Both"))
            {
                currentPosition.X -= Speed;
            }
            else if (currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Platform") &&
                !currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y + spriteDimensions.Height - 10, "Wall"))
            {
                currentPosition.X -= Speed;
            }
        }

        public virtual void Right() //moves character right by 1 unit on the X axis
        {
            if (!currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + spriteDimensions.Height - 1, "Both") &&
                !currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Both"))
            {
                currentPosition.X += Speed;
            }
            else if(currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + spriteDimensions.Height + 10, "Wall") &&
                currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Platform"))
            {
                currentPosition.X += Speed;
            }
        }

        public void ResetPosition() //resets character position to starting position
        {
            currentPosition = startPosition;
        }

        public Vector2 GetCurrentPosition() //gets the current position of the character
        {
            currentPosition = new Vector2((int)currentPosition.X, (int)currentPosition.Y);
            return currentPosition;
        }

        protected Vector2 GetSpriteFrameSize()
        {
            return new Vector2(spriteDimensions.Width, spriteDimensions.Height);
        }

        public bool CollidesWith(Creature creature)
        {
            if (currentPosition.X <= creature.GetCurrentPosition().X + creature.spriteDimensions.Width - 1 &&
               currentPosition.X + spriteDimensions.Width - 1 >= creature.GetCurrentPosition().X &&
               currentPosition.Y <= creature.GetCurrentPosition().Y + creature.spriteDimensions.Height - 1 &&
               currentPosition.Y + spriteDimensions.Height - 1 >= creature.GetCurrentPosition().Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NewSpawnPoint(Vector2 spawnPoint)
        {
            startPosition = spawnPoint;
            currentPosition = spawnPoint;
        }

        public void SetIsJumping()
        {
            bool OnFloor;
            bool OnPlatform;
            bool SameRow;

            OnFloor = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall");

            OnPlatform = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform");

            SameRow = currentLevel.IsInSameRow((int)currentPosition.Y + (spriteDimensions.Height - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed);

            Console.WriteLine("{0}, {1}, {2}, {3}", OnFloor, OnPlatform, SameRow, isJumping);

            if (OnFloor || (OnPlatform && !SameRow))
            {
                isJumping = true;
                currentHeight = 0;
            }
        }
        public void JumpOrFall()
        {
            if (isJumping)
            {
                Up(Speed);
            }

            if (currentHeight >= maxHeight)
            {
                isJumping = false;
                Down(FallSpeed);
            }
            else
            {
                Down(FallSpeed);
            }
        }

        public void SetAnimation(Animations animation)
        {
            if (e_currentAnimation != animation)
            {
                e_currentAnimation = animation;
                FrameIndex = 0;
                currentFrameTime = 0;
            }
        }

        public void UpdateAnimation(Double deltaTime)
        {
            currentFrameTime += deltaTime;
            if (currentFrameTime >= frameTimeLimit)
            {
                FrameIndex++;
                currentFrameTime = 0;
                if (FrameIndex >= 2)
                {
                    FrameIndex = 0;
                }
            }
        }
    }
}