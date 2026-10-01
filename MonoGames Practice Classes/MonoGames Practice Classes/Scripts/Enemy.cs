using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
                Right();
            }
            else if (player.GetCurrentPosition().X < currentPosition.X)
            {
                Left();
            }

            if (player.GetCurrentPosition().Y > currentPosition.Y)
            {
                Down();
            }
            else if (player.GetCurrentPosition().Y < currentPosition.Y)
            {
                Up();
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
