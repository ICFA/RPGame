using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace RPGame.Sprites
{
    public class Sprite
    {
        public Vector2 Position;

        public Color Color = Color.White;

        public float Speed = 100f;

        public float LifeSpan;

        public Texture2D Texture;

        public bool IsRemoved = false;

        public int Health = 100;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprite)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if ((Texture != null) && Health > 0)
            {
                spriteBatch.Draw(Texture, Position, null, Color, 0f, new Vector2(Texture.Width, Texture.Height) / 2f, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(Game1.U32, Health.ToString(), Position, Color.Black, 0f, new Vector2(Texture.Width, Texture.Height) / 2f, 0.8f, SpriteEffects.None, 0f);
            }
        }
    }
}
