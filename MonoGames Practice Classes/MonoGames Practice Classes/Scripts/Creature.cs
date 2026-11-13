using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Creature
    {
        //protected variables that only the child classes can inherit
        protected Vector2 currentPosition;  //used for the current position of the creatures in the level, used for the draw method to draw the creature at the correct position in the level
        protected Vector2 startPosition;    //used to store the starting position of the creature in the level, used for resetting the creature back to its starting position when the player dies

        public Texture2D Sprite; //all characters need sprite so adding it in parent class saves time
        protected Vector2 spriteSheet;  //used to store the location of the sprite in the sprite sheet for the creatures, used for the draw method to draw the correct sprite for the creature
        protected bool isFacingRight;   //used to store the direction the creature is facing, used for the draw method to draw the correct sprite for the creature, flips the creature depending on the bool value, means a secondary animation state doesn't have to be used.

        public Level currentLevel;  //used to store the current level the creature is in, used for the draw method to draw the correct sprite for the creature, and for the movement methods to check for collisions with the level
        public Rectangle spriteDimensions;  //used to store the dimensions of the sprite for the creature, used for the draw method to draw the correct sprite for the creature, and for the movement methods to check for collisions with the level

        public bool isDead = false; //used to store the state of the creature, used for the draw method to draw the correct sprite for the creature, and for the movement methods to check for collisions with the level
        protected bool isJumping;   //used for the state of the creature, used for the draw method as well as the movement methods
        protected bool isFalling;   //used for the state of the creature, used for the draw method as well as the movement methods

        protected int maxHeight;    //used for the jump method, once the player reaches the max height the player will start to fall
        protected int currentHeight;    //used for the jump method, checks if the player has reached the max height
        protected float Speed = 3f; //used for the movement methods

        protected float FallSpeed = 3f; //used for the down movement method
        protected int maxFrameIndex;    //used for the animation method, checks if the player has reached the max frame index, made it a variable because different animations will have different amount of frames in them.
        public enum Animations  //enum to store different animation states, used for the draw method
        {
            Idle = 0,
            Walk = 1,
            Jump = 2,
            Fall = 3,
            LookUp = 4
        }

        protected int FrameIndex = 0;   //current animation frame, used for the animation method
        private double currentFrameTime = 0;    //used for the animation method, checks if the player has reached the max frame time
        private double frameTimeLimit = 0;  //used for the animation method, checks if the player has reached the max frame time
        public Animations e_currentAnimation;   //used for the draw method, checks what the current animation state is, used to draw the correct sprite for the creature

        private bool CanScroll;
        //public constructor
        public Creature(Vector2 startPos, Level playLevel, Rectangle ParamSpriteDimensions) //creature constructor, sets all the most important variables in player and enemy child classes
        {
            startPosition = startPos;
            currentPosition = startPos;
            currentLevel = playLevel;
            spriteDimensions = ParamSpriteDimensions;
            isJumping = false;
            maxHeight = 170;
            currentHeight = 0;
            FrameIndex = 0;
            currentFrameTime = 0;
            frameTimeLimit = 0.15;
            e_currentAnimation = Animations.Idle;
        }

        public void LoadContent(ContentManager cm, string spriteName) //function for loading a 2d Texture asset on a character, the asset is defined by the string variable spriteName
        {
            Sprite = cm.Load<Texture2D>(spriteName);

        }

        public virtual void Draw(SpriteBatch spriteBatch) //function that all characters will use to draw their correct sprite from their sprite sheet
        {
            switch (e_currentAnimation)
            {
                case Animations.Idle:
                    spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width + (int)spriteSheet.X, 0, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    break;
                case Animations.Walk:
                    if (isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width + (int)spriteSheet.X, 144, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    }
                    else if (!isFacingRight)
                    {
                        spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width + (int)spriteSheet.X, 144, spriteDimensions.Width, spriteDimensions.Height), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    break;
                case Animations.Jump:
                    spriteBatch.Draw(Sprite, currentPosition, new Rectangle(FrameIndex * spriteDimensions.Width + spriteDimensions.Width + (int)spriteSheet.X, 216, spriteDimensions.Width, spriteDimensions.Height), Color.White);
                    break;
                default:
                    break;
            }
        }


        public virtual void Up(float speed) //moves character up by 1 unit on the Y axis
        {
            if (!currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Both") &&   //checks if the top left corner of the character is not collidning with a wall or platform AND
                !currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Both"))    //checks if the top right corner of the character is not colliding with a wall or platform
            {
                currentPosition.Y = currentPosition.Y - (speed * 2.5f); //if both checks are false then the player jumps 
                currentHeight += (int)(speed * 2);  //adds value to the current height so it can be checked against max height to limit player jump height
                isFalling = false;
            }
            else if (currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Wall") ||  //checks to see if the top right corner of the player is colliding with a wall OR
                    currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Wall"))  //checks to see if the top left corner of the player is colliding with a wall 
            {
                isJumping = false;  //if either check is false then the jumping is cancelled and the player is no longer jumping
            }
            else if (!currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y + 80, "Platform") && isJumping &&    //checks to see if the midpoint of the character is not colliding with a platform on the left side AND  
                    !currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + 80, "Platform") && isJumping)  //checks to see if the midpoint of the character is not colliding with a platform on the right side 
            {
                currentPosition.Y = currentPosition.Y - (speed * 2.5f); //if both checks are false then the player jumps until the midpoint of the sprite hits the platform. The checks see if the mid point of the character is colliding because of how the checks registers platforms in level. This allows the players to reach the bottom of the platform rather than stopping short of it.
                currentHeight += (int)(speed * 2);  //adds value to the current height so it can be checked against max height to limit player jump height
                isFalling = false;
            }
            else //otherwise the player is not jumping and falls
            {
                if (isJumping)
                    isJumping = false;
            }
        }

        public virtual void Down(float speed) //moves character down by the Speed value on the Y axis
        {
            bool OnFloor;
            bool OnPlatform;
            bool SameRow;

            OnFloor = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall");

            OnPlatform = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform");

            SameRow = currentLevel.IsInSameRow((int)currentPosition.Y + spriteDimensions.Height - 1,
                (int)currentPosition.Y + spriteDimensions.Height - 1 + (int)Speed);

            if (!OnFloor && !(OnPlatform && !SameRow))  //checks to see if the player can fall or if they are already on the floor or on a platform
            {
                currentPosition.Y += FallSpeed;
                isFalling = true;
            }
            else
            {
                isFalling = false;
            }

            /*
            if (!currentLevel.IsWall((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + spriteDimensions.Height - 1) &&
                !currentLevel.IsWall((int)currentPosition.X, (int)currentPosition.Y + spriteDimensions.Width - 1))
            {
                currentPosition.Y += speed;
            }*/
        }

        public virtual void Left() //moves character left by 1 unit on the X axis
        {
            if (!currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Both") &&   //checks if the top left corner of the character is not colliding with a wall or platform AND
                !currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y + spriteDimensions.Height - 1, "Both"))   //checks if the bottom left corner of the character is not colliding with a wall or platform
            {
                currentPosition.X -= Speed; //move character left by using speed
                CanScroll = true;
            }
            else if (currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y, "Platform") ||   //checks if the top left corner of the character is colliding with a platform OR
                currentLevel.IsWallOrPlatform((int)currentPosition.X, (int)currentPosition.Y + spriteDimensions.Height + 10, "Platform"))   //checks if the bottom left corner of the character is colliding with a platform    //if either of these are true, the character is allowed to move left to move underneath platforms on the same row as the player.
            {
                currentPosition.X -= Speed; //move character left by using speed
                CanScroll = true;
            }
            else
            {
                CanScroll = false;
            }
        }

        public virtual void Right() //moves character right by 1 unit on the X axis
        {
            if (!currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y + spriteDimensions.Height - 1, "Both") &&    //checks if the character is not colliding on the bottom right corner with a wall or platform AND
                !currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Both"))    //checks if the character is not colliding on the top right corner with a wall or platform
            {
                currentPosition.X += Speed; //moves character to the right by using speed
                CanScroll = true;
            }
            else if (currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width + 1, (int)currentPosition.Y + spriteDimensions.Height + 10, "Platform") ||   //checks if the character is collding on the bottom right corner with a platform OR
                currentLevel.IsWallOrPlatform((int)currentPosition.X + spriteDimensions.Width - 1, (int)currentPosition.Y, "Platform")) //checks if the character is colliding on the top right corner with a platform      //if either of these are true, the character is allowed to move right to move underneath platforms on the same row as the player.
            {   //
                currentPosition.X += Speed; //moves character to the right by using speed
                CanScroll = true;
            }
            else 
            {
                CanScroll = true;
            }
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

        protected Vector2 GetSpriteFrameSize()  //gets the size of the sprite frame
        {
            return new Vector2(spriteDimensions.Width, spriteDimensions.Height);
        }

        public bool CollidesWith(Creature creature) //checks if the character collides with another creature
        {
            if (currentPosition.X <= creature.GetCurrentPosition().X + creature.spriteDimensions.Width - 1 &&
               currentPosition.X + spriteDimensions.Width - 1 >= creature.GetCurrentPosition().X &&
               currentPosition.Y <= creature.GetCurrentPosition().Y + creature.spriteDimensions.Height - 1 &&
               currentPosition.Y + spriteDimensions.Height - 1 >= creature.GetCurrentPosition().Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NewSpawnPoint(Vector2 spawnPoint)   //Sets a new spawn point for the character
        {
            startPosition = spawnPoint;
            currentPosition = spawnPoint;
        }

        public void SetIsJumping()  //Sets the isJumping variable to true or false, depending on if they character is on a floor OR on a platform AND not in the same row
        {
            bool OnFloor;
            bool OnPlatform;
            bool SameRow;

            OnFloor = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Wall");

            OnPlatform = currentLevel.IsWallOrPlatform((int)currentPosition.X,
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform") ||
                currentLevel.IsWallOrPlatform((int)currentPosition.X + (spriteDimensions.Width - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed, "Platform");

            SameRow = currentLevel.IsInSameRow((int)currentPosition.Y + (spriteDimensions.Height - 1),
                (int)currentPosition.Y + (spriteDimensions.Height - 1) + (int)Speed);

            Console.WriteLine("{0}, {1}, {2}, {3}", OnFloor, OnPlatform, SameRow, isJumping);

            if (OnFloor || (OnPlatform && !SameRow))
            {
                isJumping = true;
                currentHeight = 0;
            }
        }
        public void JumpOrFall()    //checks to see conditions to make the player fall or jump
        {
            if (isJumping)
            {
                Up(Speed);
            }

            if (currentHeight >= maxHeight)
            {
                isJumping = false;
                Down(FallSpeed);
            }
            else
            {
                Down(FallSpeed);
            }
        }

        public void SetAnimation(Animations animation)  //changes the animation state to match whatever the creture child class is outputting to match animations to inputs.
        {
            if (e_currentAnimation != animation)
            {
                e_currentAnimation = animation;
                FrameIndex = 0;
                currentFrameTime = 0;
            }
        }

        public void UpdateAnimation(Double deltaTime) //Updates animations by using the sprite sheet to make the sprite animated.
        {
            currentFrameTime += deltaTime;
            if (currentFrameTime >= frameTimeLimit)
            {
                FrameIndex++;
                currentFrameTime = 0;
                if (FrameIndex >= maxFrameIndex)
                {
                    FrameIndex = 0;
                }
            }
        }

        public bool GetIsJumping()  //bool for seeing if the player is jumping, used for animations
        {
            return isJumping;
        }

        public bool GetIsFalling()  //bool for seeing if the player is falling, used for animations
        {
            return isFalling;
        }

        public float GetSpeed()
        {
            return Speed;
        }

        public bool GetCanScroll()
        {
            return CanScroll;
        }
    }
}