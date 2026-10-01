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

        public void Chase()
        { 
        
        }

        public bool Caught()
        { 
            return false;
        }

    }
}
