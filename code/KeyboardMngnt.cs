using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gioco_Esame_Monogame.States;

namespace Gioco_Esame_Monogame
{
    public partial class Game1 : Game
    {
        
        public void KeyboardMngnt (KeyboardState kbState, GameTime gameTime)//classe tipica per la gestione della tastiera
        {
            var keyPressed = kbState.GetPressedKeys();
            if (keyPressed.Length == 0) { return; }
            switch (keyPressed[0])
            {                
                case Keys.Escape:
                    Exit();
                    break;
                case Keys.Down:
                    if (ConstVar.timerWalking < 0)
                    {
                        ConstVar.timerWalking = ConstVar.TIMER_WALK;
                        ConstVar.MainCharacter.setPosYPlus(ConstVar.speedMoveCharacter);
                    }
                   
                    break;
                case Keys.Up:
                    if (ConstVar.timerWalking < 0)
                    {
                        ConstVar.timerWalking = ConstVar.TIMER_WALK;
                        ConstVar.MainCharacter.setPosYMinus(ConstVar.speedMoveCharacter);
                    }
                   
                    break;
                case Keys.Right:
                    if (ConstVar.timerWalking < 0)
                    {
                        ConstVar.timerWalking = ConstVar.TIMER_WALK;
                        ConstVar.MainCharacter.setPosXRight(ConstVar.speedMoveCharacter);
                    }
                    
                    break;
                case Keys.Left:
                    if (ConstVar.timerWalking < 0)
                    {
                        ConstVar.timerWalking = ConstVar.TIMER_WALK;
                        ConstVar.MainCharacter.setPosXLeft(ConstVar.speedMoveCharacter);
                    }
                    
                    break;



                //case Keys.Space:
               
                //    break;
            }
        }

        public void KeyboardSkip(KeyboardState kbState, GameTime gameTime)//classe tipica per la gestione della tastiera
        {
            var keyPressed = kbState.GetPressedKeys();
            if (keyPressed.Length == 0) { return; }
            else
            {
                ConstVar.keyPress = true;
                ConstVar.contatoreMessaggi += 1;
                return;
            }

        }
    }
}
