using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace MonoGames_Practice_Classes.Scripts
{
    internal class Enemy : Creature
    {

        public Enemy(Vector2 startPos, Level paramCurrentLevel, Rectangle rect) : base(startPos, paramCurrentLevel, rect)   //constructore for the play class to create an instance of the enemy once playing has begun.
        {
            Speed = 1.0f;
            FallSpeed = 1.0f;
            e_currentAnimation = Animations.Idle;
        }

        public virtual void Chase(Player player)    //uses parents movement functions to chase the player around the level
        { 
            if (player.GetCurrentPosition().X > currentPosition.X) //if the player is to the right of the enemy, move right
            {
                e_currentAnimation = Animations.Walk;
                isFacingRight = true;
                Right();
            }
            
            if (player.GetCurrentPosition().X < currentPosition.X) //if the player is to the left of the enemy, move left
            {
                e_currentAnimation = Animations.Walk;
                isFacingRight = false;
                Left();
            }
            // four seperate if statements to allow this enemy to chase the player character to in both the x and y direction
            if (player.GetCurrentPosition().Y > currentPosition.Y) //if the player is below the enemy, move down
            {
                Down(FallSpeed);
            }

            if (player.GetCurrentPosition().Y < currentPosition.Y) // if th player is above the enemy, move up
            {
                e_currentAnimation = Animations.Jump;
                Up(FallSpeed);
            }
        }


        /* public bool Caught(Player player) //function to check if the player and enemy are sharing the same space, will be used in the play script to have consequences
         {
             if (player.GetCurrentPosition() == currentPosition)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         }*/

    }
}
