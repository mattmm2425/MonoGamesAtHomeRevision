using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class HorizontalEnemy : Enemy
    {

        public HorizontalEnemy(Vector2 startPos, Level paramCurrentLevel, Rectangle rect) : base(startPos, paramCurrentLevel, rect)
        {
            FallSpeed = 10f;
            Down(FallSpeed);
            spriteSheet = new Vector2(302,0);
            maxFrameIndex = 2;
            SetEnemyDown();
            SetAnimation(Animations.Walk);
        }

        public override void Chase(Player player)
        {
            MovementDirection();
        }

        private void SetEnemyDown() //sets the direction of the enemy based on if it is falling or not
        {
            if (isFalling)
            {
                Down(FallSpeed);
            }
        }

        private void MovementDirection() //checks if the enemy is falling or not and sets the direction accordingly
        {
            if (isFacingRight)
            {
                Right();
            }
            else
            {
                Left();
            }
        }

        public override void Right() //moves character right by 1 unit on the X axis
        {
            if(!currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + spriteDimensions.Height, "Both"))
            {
                isFacingRight = false;
            }
            else
            {
                currentPosition.X += Speed;
            }

        }

        public override void Left() //moves character left by 1 unit on the X axis
        {
            if(!currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y + spriteDimensions.Height, "Both"))
            {
                isFacingRight = true;
            }
            else
            {
                currentPosition.X -= Speed;
            }
        }

    }
}
