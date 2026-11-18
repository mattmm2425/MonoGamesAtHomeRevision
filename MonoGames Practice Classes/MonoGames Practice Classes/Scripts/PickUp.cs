using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class PickUp
    {
        protected int Score;
        protected Vector2 Position;

        public bool IsActive;
        public Texture2D CollectibleSprite;
        public Rectangle spriteDimensions;
        public Player player;

        public PickUp(Vector2 levelPosition, Player playerParam, Rectangle SpriteDimensions)
        { 
            Score = 0;
            Position = levelPosition;
            spriteDimensions = SpriteDimensions;
        }

        public void CollidesWith(Player player) //checks if the character collides with another creature
        {
            if (IsActive == true)
            {
                if (Position.X <= player.GetCurrentPosition().X + player.spriteDimensions.Width - 5 &&
                    Position.X + spriteDimensions.Width - 5 >= player.GetCurrentPosition().X &&
                    Position.Y <= player.GetCurrentPosition().Y + player.spriteDimensions.Height - 5 &&
                    Position.Y + spriteDimensions.Height - 5 >= player.GetCurrentPosition().Y)
                {
                    player.AddScore(Score);
                }
            }
        }

        public void LoadContent(ContentManager cm, string collectibleName) //function for loading a 2d Texture asset on a character, the asset is defined by the string variable spriteName
        {
            CollectibleSprite = cm.Load<Texture2D>(collectibleName);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive == true)
            {
                spriteBatch.Draw(CollectibleSprite, Position, new Rectangle(0 , 0 , spriteDimensions.Width, spriteDimensions.Height), Color.White);
            }
        }
    }
}
