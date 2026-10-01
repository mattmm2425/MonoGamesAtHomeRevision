using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Creature
    {
        protected Vector2 currentPosition;
        protected Vector2 startPosition;


        public Creature(Vector2 startPos)
        {
            startPosition = startPos;
            currentPosition = startPos;
        }


        public void Up() //moves character up by 1 unit on the Y axis
        {
            currentPosition.Y -= 1;
        }

        public void Down() //moves character down by 1 unit on the Y axis
        { 
            currentPosition.Y += 1;
        }

        public void Left() //moves character left by 1 unit on the X axis
        {
            currentPosition.X -= 1;
        }

        public void Right() //moves character right by 1 unit on the X axis
        {
            currentPosition.X += 1;
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

    }
}