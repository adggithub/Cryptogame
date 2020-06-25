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
    public partial class WalkingSpritebatch
    {
        //NB:per ora non serve a nulla fare tutto questo abbiamo saltato tutto facendo la classe character e abbiamo evitato walkingspritebatch
        //questa è servita come stampo per standingrotate.
        //per ora usiamo solo la funzione in fondo
        Point spritePosition = new Point(0, 0);
        Rectangle spriteRect;
        int currentCol = 0;
        int currentRow = 1;
        int currentSprite = 0;
        int totalCol;
        int totalFrames;
        Texture2D textureC;
        float timer;
        bool rightFootRow;//mi serve per sapere quale piede mettere davanti per camminare
        public WalkingSpritebatch(Texture2D textureCh, Rectangle singleSprite, int columns, int totFrames,float timerC)     //questo è il costruttore
        {
            textureC = textureCh;
            totalCol = columns;
            totalFrames = totFrames;
            spriteRect = singleSprite;
           // timer = timerC;
            currentRow = 0;//1 va a destra
            currentCol = 0;
            currentSprite = 0;
            rightFootRow = true;
        }

        //per ora usiamo solo questo qua sotto
        public static void Draw(Rectangle rettangoloDraw,Texture2D textureP,Vector2 RowCol)//per altri utilizzi static potrebbe non andare bene
        {
            
            ConstVar.sb.Draw(textureP, rettangoloDraw,
                new Rectangle((int)RowCol.Y * (int)ConstVar.dimFrameWalking.X,(int)RowCol.X * (int)ConstVar.dimFrameWalking.Y, (int)ConstVar.dimFrameWalking.X, (int)ConstVar.dimFrameWalking.Y),
                Color.White);
           
        }
    }
}
