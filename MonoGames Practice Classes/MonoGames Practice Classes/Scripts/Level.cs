using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;

namespace MonoGames_Practice_Classes.Scripts
{
    internal class Level
    {
        private Texture2D wallTexture;
        private Vector2 textureWH = new Vector2(128, 128);
        private string[] levelContent;
        private int currentLevel;
        private Vector2 PlayerStart;
        public int basicEnemyCount = 0;
        public List<Vector2> basicEnemySpawns = new List<Vector2>();
        public Level()
        {
            currentLevel = 1;
            BuildNewLevel();

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

        public int GetCurrentLevel()
        {
            return currentLevel;
        }
        public void NextLevel()
        {
            currentLevel ++;
            basicEnemyCount = 0;
            basicEnemySpawns.Clear();
            BuildNewLevel();
        }

        
        private void BuildNewLevel()
        { 
            levelContent = File.ReadAllLines(@"..\Levels\Level " + currentLevel + ".txt");
            foreach (var line in levelContent)
            { 
                Console.WriteLine(line);
            }
            for (int column = 0; column < GetArrayHeight(); column++)
            {
                for (int row = 0; row < GetArrayWidth(); row++)
                {
                    if (levelContent[column][row] == 'P')
                    {
                        PlayerPosition(column, row);

                    }

                    if (levelContent[column][row] == 'E')
                    {
                        basicEnemyCount++;
                        basicEnemySpawns.Add(BasicEnemyPosition(column, row));
                    }
                }
            }
        }
        
        public void ResetLevels()
        { 
            currentLevel = 1;
            basicEnemyCount = 0;
            basicEnemySpawns.Clear();
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
                    
                    if (levelContent[column][row] == 'P')
                    {
                        PlayerPosition(column, row);
                    }
                    
                    if (levelContent[column][row] == '.')
                    {
                        // empty space, do nothing
                    }

                }
            }
        }

        public bool IsWall(int X, int Y)
        {
            if (levelContent[(int)(Y / textureWH.Y)][(int)(X / textureWH.X)] == 'W')
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private Vector2 PlayerPosition(int Y, int X)
        { 
            PlayerStart = new Vector2(X * textureWH.X, Y * textureWH.Y);
            return PlayerStart;
        }

        public Vector2 GetPlayerSpawn()
        {
            return PlayerStart;
        }

        private Vector2 BasicEnemyPosition(int Y, int X)
        {
            return new Vector2(X * textureWH.X, Y * textureWH.Y);
           
        }
    }
}
