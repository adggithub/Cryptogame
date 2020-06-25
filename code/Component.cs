using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gioco_Esame_Monogame
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 dimension);

        public abstract void Update(GameTime gameTime);
    }
}
