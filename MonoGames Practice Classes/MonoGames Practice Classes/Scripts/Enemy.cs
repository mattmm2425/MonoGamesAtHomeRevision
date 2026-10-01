using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace MonoGames_Practice_Classes.Scripts
{
    internal class Enemy : Creature
    {
        public Enemy(Vector2 startPos) : base(startPos)
        {

        }

        public void Chase(Player player)
        { 
            if (player.GetCurrentPosition().X > currentPosition.X)
            {
                Right(1);
            }
            else if (player.GetCurrentPosition().X < currentPosition.X)
            {
                Left(1);
            }

            if (player.GetCurrentPosition().Y > currentPosition.Y)
            {
                Down(1);
            }
            else if (player.GetCurrentPosition().Y < currentPosition.Y)
            {
                Up(1);
            }
        }

        public bool Caught(Player player)
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
