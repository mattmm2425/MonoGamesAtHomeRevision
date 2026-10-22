using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class HorizontalEnemy:Enemy
    {
        
        public HorizontalEnemy(Vector2 startPos, Level paramCurrentLevel, Rectangle rect) : base(startPos, paramCurrentLevel, rect)
        {

        }

        public override void Chase(Player player)
        {
            if (player.GetCurrentPosition().X > currentPosition.X)
            {
                Right(2.0f);
                spriteSheetY = 72 * 2;
                spriteSheetX = 52 * 6;
            }
            else if (player.GetCurrentPosition().X < currentPosition.X)
            {
                Left(2.0f);
                spriteSheetY = 72;
                spriteSheetX = 52 * 6;
            }
        }

    }
}
