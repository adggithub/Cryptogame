using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gioco_Esame_Monogame.Controls
{
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture;

        public Rectangle rectangle;

        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle//del menu 1
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)ConstVar.dimButtons.X, (int)ConstVar.dimButtons.Y);
            }
        }

        public Rectangle rectangle2//del menu 2
        {
            get
            {
                return rectangle;
            }
        }



        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - ((_font.MeasureString(Text).X*ConstVar.scaleTextMenu) / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - ((_font.MeasureString(Text).Y*ConstVar.scaleTextMenu) / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour,0,new Vector2(0,0),2,new SpriteEffects(),0);
               // spriteBatch.DrawString()
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch,Vector2 dimension)
        {
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)dimension.X, (int)dimension.Y);
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - ((_font.MeasureString(Text).X * ConstVar.scaleTextMenu) / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - ((_font.MeasureString(Text).Y * ConstVar.scaleTextMenu) / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour, 0, new Vector2(0, 0), 2, new SpriteEffects(), 0);
                // spriteBatch.DrawString()
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
            if (mouseRectangle.Intersects(rectangle2))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }

        }

        #endregion
    }
}
