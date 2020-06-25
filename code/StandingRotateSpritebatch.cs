using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gioco_Esame_Monogame
{
    public partial class StandingRotateSpritebatch
    {
        //per adesso non utilizziamo questi qui sotto commentati
        //Point spritePosition = new Point(0, 0);
        //Rectangle spriteRect;
        //Texture2D textureC;
        int currentCol = 0;
        int currentRow = 0;
        int currentSprite = 0;
        int totalCol;
        int totalFrames;
        
        float timer;

        public StandingRotateSpritebatch(int columns, int totFrames, float timerC)      //questo è il costruttore
        {
           
            totalCol = columns;
            totalFrames = totFrames;
            
            timer = timerC;
            currentRow = 0;
            currentCol = 0;
            currentSprite = 0;
        }
        public float Draw(float timerC)//algoritmo per far scorrere semplicemente le texture passando un timer per farle cambiare animazione ogni tot timer
        {
            timer = timerC;
            for (int i = 0; i < 6; i++)
            { 
            ConstVar.sb.Draw(ConstVar.listaTexture[i], new Rectangle(25+205*i,350,(int)ConstVar.dimCorniceCharacterMenu.X-5,(int)ConstVar.dimCorniceCharacterMenu.Y-10),//praticamente riempiono i quadrati nel menu i valori sono quelli calcolati
                new Rectangle(currentCol * (int)ConstVar.dimFrameWalking.X, currentRow * (int)ConstVar.dimFrameWalking.Y, (int)ConstVar.dimFrameWalking.X, (int)ConstVar.dimFrameWalking.Y),
                Color.White);
            }
            if (timer < 0)
            {
                timer = ConstVar.TIMER_ROTATE_MENU;
                if (++currentSprite >= totalFrames)
                {
                    currentSprite = 0;
                    currentCol = 0;
                    currentRow = 0;
                    return timer;
                }
                if (++currentCol >= totalCol)
                {
                    currentRow++;
                    currentCol = 0;
                }
            }

            return timer;

        }
    }
}
