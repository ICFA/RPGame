using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Sprites
{
    public class Button : Sprite
    {
        public new Color Color = Color.PapayaWhip * 0.8f;

        protected MouseState CurrentMouse;
        protected MouseState PreviousMouse;

        public Button(Texture2D texture, Vector2 position)
            : base(texture)
        {
            base.Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                if (CurrentMouse.X <= Position.X + 32 && CurrentMouse.X >= Position.X - 32)
                    if (CurrentMouse.Y <= Position.Y + 32 && CurrentMouse.Y >= Position.Y - 32)
                        Game1.Sprites[0].Position = new Vector2(100, 300);
                        Game1.Sprites[0].Health = 10000;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, 0f, new Vector2(Texture.Width, Texture.Height) / 2f, new Vector2(4f, 2f), SpriteEffects.None, 0f);
            spriteBatch.DrawString(Game1.U32, "Заново", new Vector2(Position.X - 30, Position.Y), Color.Black, 0f, new Vector2(Texture.Width, Texture.Height) / 4f, 0.8f, SpriteEffects.None, 0f);
        }

    }
}
