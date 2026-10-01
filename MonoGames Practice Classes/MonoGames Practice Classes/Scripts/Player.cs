using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Player : Creature
    {
        private int lives;
        private int initialLives;
        private int score;

        public float speed { get; private set; } = 0.2f;

        public Player(Vector2 startPos, int paramLives) : base(startPos)
        {
            initialLives = paramLives;
            lives = paramLives;
            score = 0;
        }

        public override void Up(float speed)
        {
            base.Up(this.speed);
        }

        public override void Down(float speed)
        {
            base.Down(this.speed);
        }

        public override void Left(float speed)
        {
            base.Left(this.speed);
        }

        public override void Right(float speed)
        {
            base.Right(this.speed);
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
