using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGame;

namespace RPGame.Sprites
{
    class Enemy : Sprite
    {
        protected MouseState CurrentMouse;

        protected MouseState PreviousMouse;

        public double Damage = 4 + 1.2 * Game1.Score;


        public Enemy(Texture2D texture, Vector2 pos, int speed, int health)
            : base(texture)
        {
            base.Speed = (float)speed;
            base.Health = health;
            base.Position = pos;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move(gameTime);

            foreach (var sprite in sprites)
            {
                if (sprite is Enemy)
                    continue;

                if (sprite.Rectangle.Intersects(this.Rectangle))
                {
                    base.Speed++;
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
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                if (CurrentMouse.X <= Position.X + Texture.Width && CurrentMouse.X >= Position.X - Texture.Width)
                    if (CurrentMouse.Y <= Position.Y + Texture.Height && CurrentMouse.Y >= Position.Y - Texture.Height)
                        Health -= (int)Damage;

            if (Position.X > Game1.Sprites[0].Position.X + 16)
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Position.X < Game1.Sprites[0].Position.X - 16)
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.Y > Game1.Sprites[0].Position.Y + 16)
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Position.Y < Game1.Sprites[0].Position.Y - 16)
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.X <= Game1.Sprites[0].Position.X + 16 && Position.X >= Game1.Sprites[0].Position.X - 16)
                if (Position.Y <= Game1.Sprites[0].Position.Y + 16 && Position.Y >= Game1.Sprites[0].Position.Y - 16)
                    Game1.Sprites[0].Health -= 100;
        }
    }
}
