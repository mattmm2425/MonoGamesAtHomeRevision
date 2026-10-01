using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;
using System.Runtime.Intrinsics.X86;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Creature
    {
        protected Vector2 currentPosition;
        protected Vector2 startPosition;
        protected Texture2D Sprite; 


        public Creature(Vector2 startPos)
        {
            startPosition = startPos;
            currentPosition = startPos;
        }

        public void LoadContent(ContentManager cm, string spriteName)
        {
            Sprite = cm.Load<Texture2D>(spriteName);

        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(Sprite, startPosition, rect, Color.White);
        }


        public virtual void Up(float speed) //moves character up by 1 unit on the Y axis
        {
            currentPosition.Y -= speed;
        }

        public virtual void Down(float speed) //moves character down by 1 unit on the Y axis
        { 
            currentPosition.Y += speed;
        }

        public virtual void Left(float speed) //moves character left by 1 unit on the X axis
        {
            currentPosition.X -= speed;
        }

        public virtual void Right(float speed) //moves character right by 1 unit on the X axis
        {
            currentPosition.X += speed;
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