using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace MonoGames_Practice_Classes.Scripts
{
    internal class Enemy : Creature
    {

        public Enemy(Vector2 startPos, Level paramCurrentLevel, Rectangle rect) : base(startPos, paramCurrentLevel, rect)
        {
            Speed = 1.0f;
            FallSpeed = 1.0f;
        }

        public virtual void Chase(Player player)    //uses parents movement functions to chase the player around the level
        { 
            if (player.GetCurrentPosition().X > currentPosition.X)
            {
                e_currentAnimation = Animations.Walk;
                isFacingRight = true;
                Right();
                spriteSheetY = 144;
            }
            
            if (player.GetCurrentPosition().X < currentPosition.X)
            {
                e_currentAnimation = Animations.Walk;
                isFacingRight = false;
                Left();
                spriteSheetY = 72;
            }
            // two seperate if statements to allow this enemy to chase the player character to in both the x and y direction
            if (player.GetCurrentPosition().Y > currentPosition.Y)
            {
                Down(FallSpeed);
                spriteSheetY = 0;
            }

            if (player.GetCurrentPosition().Y < currentPosition.Y)
            {
                e_currentAnimation = Animations.Jump;
                Up(FallSpeed);
                spriteSheetY = 216;
            }
        }
        public void LoadContent(ContentManager contentManager)
        {

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
