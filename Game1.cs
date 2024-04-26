using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame___Lesson4___Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture,explodeTexture;
        Rectangle bombRect;
        SpriteFont timeFont;
        float seconds,startTime;
        MouseState mouseState;
        SoundEffect explosionSound;
        bool bombEnabled = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            bombRect = new Rectangle(50,50,700,400);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("time");
            explosionSound = Content.Load<SoundEffect>("explosion");
            
            explodeTexture = Content.Load<Texture2D>("exploded");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (mouseState.LeftButton == ButtonState.Pressed)
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            if (seconds >= 15&&bombEnabled==false) // Takes a timestamp every 10 seconds.
            {
                explosionSound.Play();
                bombEnabled = true;
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            }
            if (bombEnabled && seconds >= 10)
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (bombEnabled)
                _spriteBatch.Draw(explodeTexture, bombRect, Color.White);
            else
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("0.00"), new Vector2(255, 200), Color.Black);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}