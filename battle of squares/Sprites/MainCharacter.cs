using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGame.Sprites
{
    class MainCharacter : Sprite
    {
        protected KeyboardState CurrentKey;

        protected KeyboardState PreviousKey;


        public MainCharacter(Texture2D texture, Vector2 pos)
            : base(texture)
        {
            base.Position = pos;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move(gameTime);

            if (Health <= 0)
                return;

            foreach (var sprite in sprites)
            {
                if (sprite is MainCharacter)
                    continue;


                if (sprite.Rectangle.Intersects(this.Rectangle))
                {
                    Speed++;
                    sprite.IsRemoved = true;
                }
            }
        }

        public virtual void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }

        private void Move(GameTime gameTime)
        {
            PreviousKey = CurrentKey;
            CurrentKey = Keyboard.GetState();
            //PreviousMouse = CurrentMouse;
            //CurrentMouse = Mouse.GetState();

            //if (CurrentMouse.LeftButton == ButtonState.Pressed && PreviousMouse.LeftButton == ButtonState.Released)
            //    if (CurrentMouse.X <= Game1.Sprites[1].Position.X + 24 && CurrentMouse.X >= Game1.Sprites[1].Position.X - 24)
            //        if (CurrentMouse.Y <= Game1.Sprites[1].Position.Y + 24 && CurrentMouse.Y >= Game1.Sprites[1].Position.Y - 24)
            //            Game1.Sprites[1].Health -= 4;

            if (CurrentKey.IsKeyDown(Keys.A))
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentKey.IsKeyDown(Keys.D))
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentKey.IsKeyDown(Keys.W))
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentKey.IsKeyDown(Keys.S))
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (PreviousKey.IsKeyDown(Keys.LeftShift) || PreviousKey.IsKeyDown(Keys.RightShift))
            {
                if (CurrentKey.IsKeyDown(Keys.A))
                    Position.X -= 4 * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (CurrentKey.IsKeyDown(Keys.D))
                    Position.X += 4 * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (CurrentKey.IsKeyDown(Keys.W))
                    Position.Y -= 4 * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (CurrentKey.IsKeyDown(Keys.S))
                    Position.Y += 4 * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Position.X > Game1.ScreenWidth - Texture.Width / 2)
            {
                Position.X = Game1.ScreenWidth - Texture.Width / 2;
            }
            else if (Position.X < Texture.Width / 2)
            {
                Position.X = Texture.Width / 2;
            }

            if (Position.Y > Game1.ScreenHeight - Texture.Height / 2)
            {
                Position.Y = Game1.ScreenHeight - Texture.Height / 2;
            }
            else if (Position.Y < Texture.Height / 2)
            {
                Position.Y = Texture.Height / 2;
            }


            //if (MainCharacterPosition.X > _graphics.PreferredBackBufferWidth - MainCharacterTexture.Width / 2)
            //{
            //    MainCharacterPosition.X = _graphics.PreferredBackBufferWidth - MainCharacterTexture.Width / 2;
            //}
            //else if (MainCharacterPosition.X < MainCharacterTexture.Width / 2)
            //{
            //    MainCharacterPosition.X = MainCharacterTexture.Width / 2;
            //}

            //if (MainCharacterPosition.Y > _graphics.PreferredBackBufferHeight - MainCharacterTexture.Height / 2)
            //{
            //    MainCharacterPosition.Y = _graphics.PreferredBackBufferHeight - MainCharacterTexture.Height / 2;
            //}
            //else if (MainCharacterPosition.Y < MainCharacterTexture.Height / 2)
            //{
            //    MainCharacterPosition.Y = MainCharacterTexture.Height / 2;
            //}
        }
    }
}
