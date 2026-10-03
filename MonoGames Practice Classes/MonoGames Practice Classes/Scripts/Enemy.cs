using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace MonoGames_Practice_Classes.Scripts
{
    internal class Enemy : Creature
    {
        private Vector2 playerPosition;
        public Enemy(Vector2 startPos) : base(startPos)
        {

        }

        public void Chase(Player player)    //uses parents movement functions to chase the player around the level
        { 
            if (player.GetCurrentPosition().X > currentPosition.X)
            {
                Right(1);
            }
            else if (player.GetCurrentPosition().X < currentPosition.X)
            {
                Left(1);
            }
            // two seperate if statements to allow this enemy to chase the player character to in both the x and y direction
            if (player.GetCurrentPosition().Y > currentPosition.Y)
            {
                Down(1);
            }
            else if (player.GetCurrentPosition().Y < currentPosition.Y)
            {
                Up(1);
            }
        }

        public bool Caught(Player player) //function to check if the player and enemy are sharing the same space, will be used in the play script to have consequences
        {
            if (player.GetCurrentPosition() == currentPosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
