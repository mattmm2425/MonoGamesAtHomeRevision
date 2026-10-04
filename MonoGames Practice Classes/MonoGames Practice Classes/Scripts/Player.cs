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

        private float speed;
        public int spriteChooser;

        public Player(Vector2 startPos, int paramLives) : base(startPos) //constructor for the play class to use to create an instance of the player once playing has begun
        {
            initialLives = paramLives;
            lives = paramLives;     //using speed as an input parameter so can change speed from play script (getting comfortable with overriding and adding input parameters into constructors)
            score = 0;
        }

        public override void Up(float speed)    //overriding up movement function and changing the speed of the player
        {
            base.Up(speed);
            spriteSheetY = 216;
        }

        public override void Down(float speed)
        {
            base.Down(speed);
            spriteSheetY = 0;
        }

        public override void Left(float speed)  //overriding left movement function and changing the speed of the player
        {
            base.Left(speed);
            spriteSheetY = 72;
        }

        public override void Right(float speed) //overriding right movement function and changing the speed of the player
        {
            base.Right(speed);
            spriteSheetY = 144;
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
