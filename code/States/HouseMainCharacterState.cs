using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gioco_Esame_Monogame.States;


namespace Gioco_Esame_Monogame.States
{
    public class HouseMainCharacterState : State
    {
        public bool foglietto1 = true;
        public bool foglietto2 = true;
        public bool foglietto3 = true;
        Texture2D foglietto;
        Texture2D frecciaRossa;
        Vector2 positionDraw = new Vector2((ConstVar.displayDim.X - ConstVar.mainHouseText.Width) / 2, (ConstVar.displayDim.Y - ConstVar.mainHouseText.Height) / 2);
        public HouseMainCharacterState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {            
            foglietto = content.Load<Texture2D>("foglietto");
            frecciaRossa = content.Load<Texture2D>("FrecciaRossa");
        }


        public override void PostUpdate(GameTime gameTime)
        {
            // per ora non ci serve
        }

        public override void Update(GameTime gameTime)
        {
            if (ConstVar.changedWindow)
            {
                ConstVar.collisioniMappaCorrente = ConstVar.internoMainHouseCollision;//collisioni di questo stato  
                if (ConstVar.contatoreMessaggi <=7) {
                    ConstVar.abilitatoreMessaggi = true;
                }
                ConstVar.isMouseVisibleM = false;//nel gioco il mouse è invisibile
                ConstVar.abilitaTastiera = true;//mi dice che sono in gioco e abilitare tastiera
                ConstVar.MainCharacter.setPos(ConstVar.startPositionCharacterMainHouse.X, ConstVar.startPositionCharacterMainHouse.Y);
                ConstVar.changedWindow = false;
                ConstVar.currentBackgroundText = ConstVar.mainHouseText;

               // ConstVar.backgroundMainText = _content.Load<Texture2D>("StartGarden");//schermata principale
            }
            else
            {
                if (ConstVar.abilitatoreMessaggi == true)
                {
                    if (ConstVar.keyPress == false)//attendo che venga premuto un pulsante
                    {
                        KeyboardState kbState;

                        kbState = Keyboard.GetState();
                        _game.KeyboardSkip(kbState, gameTime);
                    }
                    else//se è premuto devo aspettare tot tempo
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerText -= elapsed;
                        if (ConstVar.timerText < 0)
                        {
                            ConstVar.timerText = ConstVar.TIMERTEXT;
                            ConstVar.keyPress = false;
                        }
                    }
                }
                else
                {
                    //sopra aggiunto inizio
                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                    ConstVar.timerWalking -= elapsed;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //dovresti farla piu piccolo dello sfondo dello schermo 
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(ConstVar.mainHouseText,ConstVar.displayRect, Color.White);//disegna sfondo principale
                                                                                       // spriteBatch.Draw(ConstVar.carpenter, new Rectangle(680, 350, (int)ConstVar.dimCharacter.X,(int)ConstVar.dimCharacter.Y),Color.White);
                                                                                       //ConstVar.commentoGenerale.Draw(new Point(650,150),new Point(150,100),"ciao MATTEPASTO");
            if (ConstVar.abilitatoreMessaggi == true)
            {
                ConstVar.commentoGenerale.Draw(ConstVar.commentoGenerale.textMessaggi(ConstVar.contatoreMessaggi));
            }
            if(foglietto1)
            {
                spriteBatch.Draw(foglietto, new Rectangle(850, 390, 30, 30), Color.White);
            }
            if (foglietto2)
            {
                spriteBatch.Draw(foglietto, new Rectangle(500, 300, 30, 30), Color.White);
            }
            if (foglietto3)
            {
                spriteBatch.Draw(foglietto, new Rectangle(520, 500, 30, 30), Color.White);
            }
            if(ConstVar.isFrecciaRossa)
            {
                spriteBatch.Draw(frecciaRossa, new Rectangle(490, 490, 40, 60), Color.White);
            }

            ConstVar.MainCharacter.Draw();
            //ConstVar.timerWalking = ConstVar.walkingSprite.Draw(ConstVar.timerWalking);//disegna animazione per prova; Pre-modifiche

            spriteBatch.End();
        }
    }
}
