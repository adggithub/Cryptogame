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
    public class GameState : State
    {
        public Rectangle rettangoloSpiegazioni;
        public Texture2D spiegazioneVigenere;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
           //se si vuole fare qui la load bisogna mettere _content.Load invece di Content.Load
            ConstVar.isMouseVisibleM = false;//nel gioco il mouse è invisibile
            ConstVar.abilitaTastiera = true;//mi dice che sono in gioco e abilitare tastiera
            //inizializzati sotto nelle constvar
            //ConstVar.startPositionCharacter = new Vector2(300, 300);
            //ConstVar.dimCharacter = new Vector2(48, 72);
            ConstVar.dimFrameWalking = new Vector2(32, 48);//dimensione immagine man 96,192 che diviso per 3 colonne e 4 righe

            ConstVar.walkingSprite = new WalkingSpritebatch(ConstVar.characterText, new Rectangle((int)ConstVar.startPositionCharacter.X, (int)ConstVar.startPositionCharacter.Y,
                                 (int)ConstVar.dimCharacter.X, (int)ConstVar.dimCharacter.Y),
                                 ConstVar.walkingCols, ConstVar.walkingFrame, ConstVar.timerWalking);//passo texture e infine timer
            

            ConstVar.MainCharacter = new Character(ConstVar.startPositionCharacter, ConstVar.characterText, ConstVar.dimCharacter, _game, _graphicsDevice, _content);
            ConstVar.collisioniMappaCorrente = ConstVar.ostacoliArray;//collisioni di questo stato
            ConstVar.abilitatoreMessaggi = true;
            rettangoloSpiegazioni = new Rectangle(100, 50, (int)ConstVar.displayDim.X - 200, (int)ConstVar.displayDim.Y - 200);
            spiegazioneVigenere = _content.Load<Texture2D>("VigenereSpiegazione");
        }


        public override void PostUpdate(GameTime gameTime)
        {
            // per ora non ci serve
        }
        
        public override void Update(GameTime gameTime)
        {
            if (ConstVar.changedWindow)
            {
                ConstVar.changedWindow = false;
                ConstVar.collisioniMappaCorrente = ConstVar.ostacoliArray;
                ConstVar.backgroundMainText = _content.Load<Texture2D>("StartGarden");//schermata principale
                ConstVar.currentBackgroundText = ConstVar.backgroundMainText;
            }
            else {
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
            if (ConstVar.contatoreMessaggi == 48)
            {   
                ConstVar.changedWindow = true;
                _game.ChangeState(Menu2State.CaesarCypherS);
            }
            if (ConstVar.contatoreMessaggi == 57)
            {
                ConstVar.changedWindow = true;
                _game.ChangeState(Menu2State.VigenereS);
            }
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ConstVar.backgroundMainText, ConstVar.displayRect, Color.White);//disegna sfondo principale
            //ConstVar.commentoGenerale.DrawMessaggi();
            ConstVar.MainCharacter.Draw();
            //ConstVar.timerWalking = ConstVar.walkingSprite.Draw(ConstVar.timerWalking);//disegna animazione per prova; Pre-modifiche
            if (ConstVar.abilitatoreMessaggi == true){
                ConstVar.commentoGenerale.Draw(ConstVar.commentoGenerale.textMessaggi(ConstVar.contatoreMessaggi));
            }
            if(ConstVar.zoneStoryCounter==2||ConstVar.zoneStoryCounter==3)
            {
                spriteBatch.Draw(ConstVar.carpenter, new Rectangle(800, 480, 50, 100), Color.White);
            }
            if (ConstVar.contatoreMessaggi==56)
            {
               spriteBatch.Draw(spiegazioneVigenere, rettangoloSpiegazioni, Color.White);//pannello spiegazione
            }

            spriteBatch.End();
        }
        //private void HouseEntrance()
        //{
        //    _game.ChangeState(new HouseState(_game, _graphicsDevice, _content));
        //}
    }
}
