using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGames_Practice_Classes.Scripts;
using SharpDX.Direct3D11;
using System.Runtime.CompilerServices;

namespace MonoGames_Practice_Classes
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int Red = 45;
        private int Green = 160;
        private int Blue = 120;

        private SceneManager scene;
        public Vector2 screenWH;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1024;
            screenWH = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            scene = new SceneManager(screenWH);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();

            scene.Update(this, gameTime);

            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.FromNonPremultiplied(Red, Green, Blue, 225));

            // TODO: Add your drawing code here
            scene.Draw(GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
