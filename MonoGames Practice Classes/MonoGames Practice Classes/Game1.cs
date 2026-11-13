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
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private SceneManager scene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            scene = new SceneManager();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics.PreferredBackBufferHeight = 1024;
            _graphics.PreferredBackBufferWidth = 1664;
            scene.LoadContent(Content, _graphics, GraphicsDevice);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();

            scene.Update(this, gameTime, GraphicsDevice);

            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.FromNonPremultiplied(Red, Green, Blue, 225));

            // TODO: Add your drawing code here
            scene.Draw(_graphics, _spriteBatch, GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}