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
    public class Menu2State : State
    {
        private List<Component> _components;
        public static GameState MainState;
        public static HouseMainCharacterState MainHouseState;
        public static HouseState House2State;
        public static CaesarCypherState CaesarCypherS;
        public static Caesar_Cipher Caesar;
        public static VigenereState VigenereS;
        public static VigenereCypher Vigenere;

        private Component _componentNotClickable;
        public Menu2State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //se si vuole fare qui la load bisogna mettere _content.Load invece di Content.Load
            ConstVar.isMouseVisibleM = true;
            var corniceCharacterTexture = _content.Load<Texture2D>("CornicePersonaggio");
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            if (ConstVar.listaTexture.Count == 0)
            {
                ConstVar.listaTexture.Add(_content.Load<Texture2D>("Man"));
                ConstVar.listaTexture.Add(_content.Load<Texture2D>("Man2"));
                ConstVar.listaTexture.Add(_content.Load<Texture2D>("Man3"));
                ConstVar.listaTexture.Add(_content.Load<Texture2D>("Woman"));
                ConstVar.listaTexture.Add(_content.Load<Texture2D>("Woman2"));
                ConstVar.listaTexture.Add(_content.Load<Texture2D>("Woman3"));
            }
            ConstVar.standingMan1 = new StandingRotateSpritebatch(ConstVar.walkingCols, ConstVar.walkingFrame,ConstVar.timerWalking);//passo texture e infine timer
            /*altri possibili personaggi ma con signature pre-modifiche
            ConstVar.standingMan2 = new StandingRotateSpritebatch(ConstVar.characterText, new Rectangle((int)ConstVar.positionCharacter.X, (int)ConstVar.positionCharacter.Y,
                                  (int)ConstVar.dimCharacter.X, (int)ConstVar.dimCharacter.Y),
                                  ConstVar.walkingCols, ConstVar.walkingFrame, ConstVar.timerWalking);//passo texture e infine timer
            ConstVar.standingMan3 = new StandingRotateSpritebatch(ConstVar.characterText, new Rectangle((int)ConstVar.positionCharacter.X, (int)ConstVar.positionCharacter.Y,
                                 (int)ConstVar.dimCharacter.X, (int)ConstVar.dimCharacter.Y),
                                 ConstVar.walkingCols, ConstVar.walkingFrame, ConstVar.timerWalking);//passo texture e infine timer
            ConstVar.standingWoman1 = new StandingRotateSpritebatch(ConstVar.characterText, new Rectangle((int)ConstVar.positionCharacter.X, (int)ConstVar.positionCharacter.Y,
                                 (int)ConstVar.dimCharacter.X, (int)ConstVar.dimCharacter.Y),
                                 ConstVar.walkingCols, ConstVar.walkingFrame, ConstVar.timerWalking);//passo texture e infine timer
            ConstVar.standingWoman2 = new StandingRotateSpritebatch(ConstVar.characterText, new Rectangle((int)ConstVar.positionCharacter.X, (int)ConstVar.positionCharacter.Y,
                                 (int)ConstVar.dimCharacter.X, (int)ConstVar.dimCharacter.Y),
                                 ConstVar.walkingCols, ConstVar.walkingFrame, ConstVar.timerWalking);//passo texture e infine timer
            ConstVar.standingWoman3 = new StandingRotateSpritebatch(ConstVar.characterText, new Rectangle((int)ConstVar.positionCharacter.X, (int)ConstVar.positionCharacter.Y,
                                 (int)ConstVar.dimCharacter.X, (int)ConstVar.dimCharacter.Y),
                                 ConstVar.walkingCols, ConstVar.walkingFrame, ConstVar.timerWalking);//passo texture e infine timer
*/

            var chooseCharacterButton = new Button(buttonTexture, buttonFont)//non è proprio un bottone è una scritta da migliorare
            {
                Position = new Vector2(ConstVar.displayDim.X / 2 - ConstVar.dimButtons.X / 2, 100),
                Text = "Choose your character",
            };//è un finto bottone in realtà

            _componentNotClickable = chooseCharacterButton;
            //ora 6 possibilità di scelta
            //1
            var male1Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(25, 350),
                //Text = "New Game",
            };

            male1Button.Click += Male1Button_Click;
            //2
            var male2Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(230, 350),
               // Text = "New Game",
            };

            male2Button.Click += Male2Button_Click;
            //3
            var male3Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(435, 350),
               // Text = "New Game",
            };

            male3Button.Click += Male3Button_Click;
            //4
            var female1Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(640, 350),
                //Text = "New Game",
            };

            female1Button.Click += Female1Button_Click;
            //5
            var female2Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(845, 350),
               // Text = "New Game",
            };

            female2Button.Click += Female2Button_Click;
            //6
            var female3Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(1050, 350),
               // Text = "New Game",
            };

            female3Button.Click += Female3Button_Click;


            _components = new List<Component>()
            {
                male1Button,
                male2Button,
                male3Button,
                female1Button,
                female2Button,
                female3Button,
            };
            MainHouseState = new HouseMainCharacterState(_game, _graphicsDevice, _content);
            House2State = new HouseState(_game, _graphicsDevice, _content);
            CaesarCypherS = new CaesarCypherState(_game, _graphicsDevice, _content);
            Caesar = new Caesar_Cipher(_content);
            VigenereS=new VigenereState(_game, _graphicsDevice, _content);
            Vigenere = new VigenereCypher(_content);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // per ora non ci serve
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
            ConstVar.timer_rotate_menu -= elapsed;
            
            foreach (var component in _components)
                component.Update(gameTime);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _componentNotClickable.Draw(gameTime, spriteBatch);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch, ConstVar.dimCorniceCharacterMenu);
            ConstVar.timer_rotate_menu = ConstVar.standingMan1.Draw(ConstVar.timer_rotate_menu);//disegna animazione per prova

            spriteBatch.End();
        }
        //metto tutti i casi di click, ora tutti porteranno al cambio stato in gamestate però ognuno di essi caricherà una skin differente
        private void Male1Button_Click(object sender, EventArgs e)
        {
            ConstVar.characterText=_content.Load<Texture2D>("Man");
            MainState = new GameState(_game, _graphicsDevice, _content);
            _game.ChangeState(MainState);
        }

        private void Male2Button_Click(object sender, EventArgs e)
        {
            ConstVar.characterText = _content.Load<Texture2D>("Man2");
            MainState = new GameState(_game, _graphicsDevice, _content);
            _game.ChangeState(MainState);
        }

        private void Male3Button_Click(object sender, EventArgs e)
        {
            ConstVar.characterText = _content.Load<Texture2D>("Man3");
            MainState = new GameState(_game, _graphicsDevice, _content);
            _game.ChangeState(MainState);
        }

        private void Female1Button_Click(object sender, EventArgs e)
        {
            ConstVar.characterText = _content.Load<Texture2D>("Woman");
            MainState = new GameState(_game, _graphicsDevice, _content);
            _game.ChangeState(MainState);
        }

        private void Female2Button_Click(object sender, EventArgs e)
        {
            ConstVar.characterText = _content.Load<Texture2D>("Woman2");
            MainState = new GameState(_game, _graphicsDevice, _content);
            _game.ChangeState(MainState);
        }

        private void Female3Button_Click(object sender, EventArgs e)
        {
            ConstVar.characterText = _content.Load<Texture2D>("Woman3");
            MainState = new GameState(_game, _graphicsDevice, _content);
            _game.ChangeState(MainState);
        }
    }
}
