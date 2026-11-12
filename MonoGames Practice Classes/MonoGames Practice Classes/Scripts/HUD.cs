using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class HUD
    {
        private SpriteFont LevelTextFont;
        private string LevelMessage;
        private Texture2D HeartIcon;

        public void LoadContent(ContentManager cm)
        {
            LevelTextFont = cm.Load<SpriteFont>("GameFont");
            HeartIcon = cm.Load<Texture2D>("HUD_Heart");
        }

        public void SetMessage(string message)
        { 
            LevelMessage = message;
        }

        public void DrawString(SpriteBatch sprite, Vector2 position, Color colour)
        { 
            sprite.DrawString(LevelTextFont, LevelMessage, position, colour);
        }

        public void DrawLives(SpriteBatch sprite, Vector2 position, int lives)
        {
            for (int i = 0; i < lives; i++)
            { 
                sprite.Draw(HeartIcon,
                    new Vector2(position.X + (i * HeartIcon.Width), position.Y),
                    new Rectangle(0, 0, HeartIcon.Width, HeartIcon.Height), Color.White);
            }
        }
    }
}
