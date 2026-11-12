using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Player : Creature
    {
        private int lives;
        private int initialLives;
        private int score;

        public Player(Vector2 startPos, int paramLives, Level paramCurrentLevel, Rectangle rect) : base(startPos, paramCurrentLevel, rect) //constructor for the play class to use to create an instance of the player once playing has begun
        {
            initialLives = paramLives;
            lives = paramLives;     //using speed as an input parameter so can change speed from play script (getting comfortable with overriding and adding input parameters into constructors)
            score = 0;
            e_currentAnimation = Animations.Idle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (e_currentAnimation)
            {
                case Animations.Idle:
                    if (isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 0, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    }
                    else if (!isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 0, spriteDimensions.Width, spriteDimensions.Height), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    break;
                case Animations.Walk:
                    if (isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 64, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    }
                    else if (!isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width, 64, spriteDimensions.Width, spriteDimensions.Height), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    break;
                case Animations.Jump:
                    if (isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(0, 320, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    }
                    else if (!isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(0, 320, spriteDimensions.Width, spriteDimensions.Height), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    break;
                case Animations.Fall:
                    if (isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(64, 320, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    }
                    else if (!isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(64, 320, spriteDimensions.Width, spriteDimensions.Height), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    break;
                default:
                    break;
            }
        }

        public override void Up(float speed)    //overriding up movement function and changing the speed of the player
        {
            maxFrameIndex = 0;
            base.Up(speed);
        }

        public override void Down(float speed)
        {
            maxFrameIndex = 0;
            base.Down(speed);
        }

        public override void Left()  //overriding left movement function and changing the speed of the player
        {
            maxFrameIndex = 5;
            base.Left();
            isFacingRight = false;
        }

        public override void Right() //overriding right movement function and changing the speed of the player
        {
            maxFrameIndex = 5;
            base.Right();
            isFacingRight = true;
        }

        public int GetLives()
        {
            return lives;
        }

        public void LoseLife()
        {
            lives--;
        }

        public void ResetLives()
        {
            lives = initialLives;
        }

        public void SetIdleFrames()
        {
            maxFrameIndex = 3;
        }

    }
}
