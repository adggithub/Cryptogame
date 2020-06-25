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
    public class HouseState : State
    {
        public HouseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //se si vuole fare qui la load bisogna mettere _content.Load invece di Content.Load
           
            //inizializzati sotto nelle constvar
            //ConstVar.startPositionCharacterHouse = new Vector2(300, 300);
            //ConstVar.dimCharacter = new Vector2(48, 72);
            //ConstVar.dimFrameWalking = new Vector2(32, 48);//dimensione immagine man 96,192 che diviso per 3 colonne e 4 righe

            /*ConstVar.walkingSprite = new WalkingSpritebatch(ConstVar.characterText, new Rectangle((int)ConstVar.startPositionCharacterHouse.X, (int)ConstVar.startPositionCharacterHouse.Y,
                                 (int)ConstVar.dimCharacter.X, (int)ConstVar.dimCharacter.Y),
                                 ConstVar.walkingCols, ConstVar.walkingFrame, ConstVar.timerWalking);//passo texture e infine timer
            */
            //non serve fare un nuovo character ----> in realtà è più facile posizionarlo (Pasto)
            //ConstVar.MainCharacter = new Character(ConstVar.startPositionCharacterHouse, ConstVar.characterText, ConstVar.dimCharacter, _game, _graphicsDevice, _content);     // qui startpos..ecc non va bene; dovrò far spawnare omino sulla soglia dentro casa
            
           
        }


        public override void PostUpdate(GameTime gameTime)
        {
            // per ora non ci serve
        }

        public override void Update(GameTime gameTime)
        {
            if (ConstVar.changedWindow)
            {
                ConstVar.isMouseVisibleM = false;//nel gioco il mouse è invisibile
                ConstVar.abilitaTastiera = true;//mi dice che sono in gioco e abilitare tastiera
                ConstVar.MainCharacter.setPos(ConstVar.startPositionCharacterHouse.X, ConstVar.startPositionCharacterHouse.Y);
                ConstVar.changedWindow = false;
                ConstVar.collisioniMappaCorrente = ConstVar.internoCarpenterHouse;//collisioni di questo stato
                // ConstVar.backgroundMainText = _content.Load<Texture2D>("StartGarden");//schermata principale
                ConstVar.currentBackgroundText = ConstVar.houseText;
            }
            else
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                ConstVar.timerWalking -= elapsed;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(ConstVar.houseText, ConstVar.displayRect, Color.White);//disegna sfondo principale

            ConstVar.MainCharacter.Draw();
            //ConstVar.timerWalking = ConstVar.walkingSprite.Draw(ConstVar.timerWalking);//disegna animazione per prova; Pre-modifiche

            spriteBatch.End();
        }
    }
}
