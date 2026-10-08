using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Level
    {
        private Texture2D wallTexture;
        private Vector2 textureWH;
        private string[] levelContent;
        private int currentLevel;

        public Level()
        {
            currentLevel = 1;
        }

        public void LoadContent(ContentManager cm, string filename)
        {
            wallTexture = cm.Load<Texture2D>(filename);
            textureWH = new Vector2(wallTexture.Width, wallTexture.Height);

            BuildNewLevel();
        }

        private int GetArrayWidth()
        {
            return levelContent[0].Length;
        }

        private int GetArrayHeight()
        {
            return levelContent.Length;
        }

        public Vector2 GetLevelSize()
        { 
            return new Vector2(GetArrayWidth() * textureWH.X, GetArrayHeight() * textureWH.Y);
        }

        private int NextLevel()
        {
            return currentLevel++;
        }

        
        public void BuildNewLevel()
        { 
            levelContent = File.ReadAllLines(@"..\Levels\Level " + currentLevel + ".txt");
            foreach (var line in levelContent)
            { 
                Console.WriteLine(line);
            }
        }
        
        public void ResetLevels()
        { 
            currentLevel = 1;
            BuildNewLevel();
        }
        
        public void Draw(SpriteBatch spriteBatch)
        { 
            for (int column = 0; column < GetArrayHeight(); column++)
            {
                for (int row = 0; row < GetArrayWidth(); row++)
                {
                    if (levelContent[column][row] == 'W')
                    {
                        spriteBatch.Draw(wallTexture, new Vector2(row * textureWH.X, column * textureWH.Y), Color.White);
                    }
                }
            }
        }
    }
}
