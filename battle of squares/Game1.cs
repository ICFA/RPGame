using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame.Sprites;
using System.Runtime.ConstrainedExecution;

namespace RPGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static SpriteFont U32;
        private Texture2D Green;
        private Texture2D Blue;
        private Texture2D Red;
        private Texture2D King;
        private Texture2D Back;

        Random rnd = new Random();

        public static int Score = 0;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public static List<Sprite> Sprites;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //this.Window.AllowUserResizing = true;
            //this.Window.Title = "RPGame";

            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var mainCharacterTexture = Content.Load<Texture2D>("MainCharacter");
            Green = Content.Load<Texture2D>("EnemyGreen");
            Blue = Content.Load<Texture2D>("EnemyBlue");
            Red = Content.Load<Texture2D>("EnemyRed");
            King = Content.Load<Texture2D>("KingB");
            Back = Content.Load<Texture2D>("Back");

            Sprites = new List<Sprite>();
            Sprites.Add(new MainCharacter(mainCharacterTexture, new Vector2(100, 100)));
            Sprites.Add(new Enemy(Green, new Vector2(rnd.Next(1, ScreenWidth), rnd.Next(1, ScreenHeight)), 20, 20));
            Sprites.Add(new Enemy(Green, new Vector2(rnd.Next(1, ScreenWidth), rnd.Next(1, ScreenHeight)), 20, 20));

            U32 = Content.Load<SpriteFont>("U32");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Sprites[0].Health <= 0)
                Exit();

            //ScreenWidth = graphics.PreferredBackBufferWidth;
            //ScreenHeight = graphics.PreferredBackBufferHeight;

            for (int i = 0; i < Sprites.Count; i++)
                if (Sprites[i].Health <= 0)
                {
                    Score += 1;
                    Sprites.RemoveAt(i);
                    if (Score <= 4)
                        Sprites.Add(new Enemy(Green, new Vector2(rnd.Next(1, ScreenWidth), rnd.Next(1, ScreenHeight)), 20, 20));
                    else if (Score < 9)
                        Sprites.Add(new Enemy(Blue, new Vector2(rnd.Next(1, ScreenWidth), rnd.Next(1, ScreenHeight)), 40, 40));
                    else if (Score < 16)
                        Sprites.Add(new Enemy(Red, new Vector2(rnd.Next(1, ScreenWidth), rnd.Next(1, ScreenHeight)), 60, 60));
                    else
                        Sprites.Add(new Enemy(King, new Vector2(rnd.Next(1, ScreenWidth), rnd.Next(1, ScreenHeight)), 100, 500));
                }
                else
                    Sprites[i].Update(gameTime, Sprites);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Back, new Vector2(0, 0), Color.White * 0.33f);
            foreach (var sprite in Sprites)
                sprite.Draw(spriteBatch);
            spriteBatch.DrawString(U32, "Score: " + Score.ToString(), new Vector2(ScreenWidth * 0.84f, 20), Color.White);
            spriteBatch.DrawString(U32, "Вы можете использовать Shift для ускорения", new Vector2(ScreenWidth * 0.73f, 40), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Texture2D MainCharacterTexture;
        //Vector2 MainCharacterPosition;
        //float MainCharacterSpeed;
        ////float MainCharacterYaw;
        ////float MainCharacterPitch;
        ////float MainCharacterRoll;

        //Texture2D EnemyGreenTexture;
        //Vector2 EnemyGreenPosition;
        //float EnemyGreenSpeed;

        //Texture2D EnemyBlueTexture;
        //Vector2 EnemyBluePosition;
        //float EnemyBlueSpeed;

        //Random rand = new Random();

        //private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;

        //public Game1()
        //{
        //    _graphics = new GraphicsDeviceManager(this);
        //    Content.RootDirectory = "Content";
        //    IsMouseVisible = true;
        //}

        //protected override void Initialize()
        //{
        //    // TODO: Add your initialization logic here
        //    MainCharacterPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        //    MainCharacterSpeed = 100f;
        //    EnemyGreenPosition = new Vector2(rand.Next(1, _graphics.PreferredBackBufferWidth), rand.Next(1, _graphics.PreferredBackBufferHeight));
        //    EnemyGreenSpeed = 20f;
        //    EnemyBluePosition = new Vector2(rand.Next(1, _graphics.PreferredBackBufferWidth), rand.Next(1, _graphics.PreferredBackBufferHeight));
        //    EnemyBlueSpeed = 40f;

        //    base.Initialize();
        //}

        //protected override void LoadContent()
        //{
        //    _spriteBatch = new SpriteBatch(GraphicsDevice);

        //    // TODO: use this.Content to load your game content here
        //    MainCharacterTexture = Content.Load<Texture2D>("MainCharacter");
        //    EnemyGreenTexture = Content.Load<Texture2D>("EnemyGreen");
        //    EnemyBlueTexture = Content.Load<Texture2D>("EnemyBlue");
        //}

        //protected override void Update(GameTime gameTime)
        //{
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //        Exit();

        //    // TODO: Add your update logic here
        //    var kstate = Keyboard.GetState();

        //    if (kstate.IsKeyDown(Keys.W))
        //    {
        //        MainCharacterPosition.Y -= MainCharacterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    }

        //    if (kstate.IsKeyDown(Keys.S))
        //    {
        //        MainCharacterPosition.Y += MainCharacterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    }

        //    if (kstate.IsKeyDown(Keys.A))
        //    {
        //        MainCharacterPosition.X -= MainCharacterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    }

        //    if (kstate.IsKeyDown(Keys.D))
        //    {
        //        MainCharacterPosition.X += MainCharacterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    }

        //    if (MainCharacterPosition.X > _graphics.PreferredBackBufferWidth - MainCharacterTexture.Width / 2)
        //    {
        //        MainCharacterPosition.X = _graphics.PreferredBackBufferWidth - MainCharacterTexture.Width / 2;
        //    }
        //    else if (MainCharacterPosition.X < MainCharacterTexture.Width / 2)
        //    {
        //        MainCharacterPosition.X = MainCharacterTexture.Width / 2;
        //    }

        //    if (MainCharacterPosition.Y > _graphics.PreferredBackBufferHeight - MainCharacterTexture.Height / 2)
        //    {
        //        MainCharacterPosition.Y = _graphics.PreferredBackBufferHeight - MainCharacterTexture.Height / 2;
        //    }
        //    else if (MainCharacterPosition.Y < MainCharacterTexture.Height / 2)
        //    {
        //        MainCharacterPosition.Y = MainCharacterTexture.Height / 2;
        //    }

        //    if (EnemyGreenPosition.X > MainCharacterPosition.X)
        //        EnemyGreenPosition.X -= EnemyGreenSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    else
        //        EnemyGreenPosition.X += EnemyGreenSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    if (EnemyGreenPosition.Y > MainCharacterPosition.Y)
        //        EnemyGreenPosition.Y -= EnemyGreenSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    else
        //        EnemyGreenPosition.Y += EnemyGreenSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    if (EnemyBluePosition.X > MainCharacterPosition.X)
        //        EnemyBluePosition.X -= EnemyBlueSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    else
        //        EnemyBluePosition.X += EnemyBlueSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    if (EnemyBluePosition.Y > MainCharacterPosition.Y)
        //        EnemyBluePosition.Y -= EnemyBlueSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    else
        //        EnemyBluePosition.Y += EnemyBlueSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    base.Update(gameTime);
        //}

        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(Color.CornflowerBlue);

        //    // TODO: Add your drawing code here
        //    _spriteBatch.Begin();
        //    _spriteBatch.Draw(
        //        MainCharacterTexture,
        //        MainCharacterPosition,
        //        null,
        //        Color.White,
        //        0f,
        //        new Vector2(MainCharacterTexture.Width / 2, MainCharacterTexture.Height / 2),
        //        Vector2.One,
        //        SpriteEffects.None,
        //        0f
        //        );
        //    _spriteBatch.Draw(
        //        EnemyGreenTexture,
        //        EnemyGreenPosition,
        //        null,
        //        Color.Whi,
        //        0f,
        //        new Vector2(EnemyGreenTexture.Width / 2, EnemyGreenTexture.Height / 2),
        //        Vector2.One,
        //        SpriteEffects.None,
        //        0f
        //        );
        //    if ((gameTime.TotalGameTime.TotalSeconds - 5.0) > 0)
        //        _spriteBatch.Draw(
        //        EnemyBlueTexture,
        //        EnemyBluePosition,
        //        null,
        //        Color.Blue,
        //        0f,
        //        new Vector2(EnemyBlueTexture.Width / 2, EnemyBlueTexture.Height / 2),
        //        Vector2.One,
        //        SpriteEffects.None,
        //        0f
        //        );
        //    _spriteBatch.End();

        //    base.Draw(gameTime);
        //}
    }
}