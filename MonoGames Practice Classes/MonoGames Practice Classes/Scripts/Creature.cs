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
        //protected variables that only the child classes can inherit
        protected Vector2 currentPosition;
        protected Vector2 startPosition;
        protected Texture2D Sprite; //all characters need sprite so adding it in parent class saves time

        //public constructor
        public Creature(Vector2 startPos)
        {
            startPosition = startPos;
            currentPosition = startPos;
        }

        public void LoadContent(ContentManager cm, string spriteName) //function for loading a 2d Texture asset on a character, the asset is defined by the string variable spriteName
        {
            Sprite = cm.Load<Texture2D>(spriteName);

        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect) //function that all characters will use to draw their correct sprite from their sprite sheet
        {
            spriteBatch.Draw(Sprite, currentPosition, rect, Color.White);
        }


        public virtual void Up(float speed) //moves character up by 1 unit on the Y axis
        {
            currentPosition.Y -= speed;
        }

        public virtual void Down(float speed) //moves character down by 1 unit on the Y axis
        { 
            currentPosition.Y = currentPosition.Y + speed;

        }

        public virtual void Left(float speed) //moves character left by 1 unit on the X axis
        {
            currentPosition.X -= speed;
        }

        public virtual void Right(float speed) //moves character right by 1 unit on the X axis
        {
            currentPosition.X = currentPosition.X + speed;
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