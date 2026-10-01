using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Player : Creature
    {
        private int lives;
        private int initialLives;
        private int score;

        public Player(Vector2 startPos, int lives) : base(startPos)
        {
            this.initialLives = lives;
            this. score = 0;
        }

        public int GetLives()
        {
            return lives;
        }

        public void LoseLife()
        {
            lives--;
        }

        public void ResetLives()
        {
            lives = initialLives;
        }


    }
}
