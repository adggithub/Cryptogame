using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Gioco_Esame_Monogame.Controls;

namespace Gioco_Esame_Monogame.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)//bottone nuovo gioco che passa a MenuState2
            {
                Position = new Vector2(ConstVar.displayDim.X/2-ConstVar.dimButtons.X/2, 400),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, buttonFont)//per ora non fa nulla
            {
                Position = new Vector2(ConstVar.displayDim.X / 2 - ConstVar.dimButtons.X / 2, 500),
                Text = "Load Game",
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)//exit
            {
                Position = new Vector2(ConstVar.displayDim.X / 2 - ConstVar.dimButtons.X / 2, 600),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()//lista dei componenti per fare draw
      {
        newGameButton,
        loadGameButton,
        quitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ConstVar.menuBackgroundText, ConstVar.displayRect, Color.White);
           // spriteBatch.Draw(ConstVar.logo, ConstVar.logoMenuRect, Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Game");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new Menu2State(_game, _graphicsDevice, _content));//passa al secondo menu di scelta personaggio
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
