using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gioco_Esame_Monogame

{
    public class Caesar_Cipher
    {
        Vector2 origin;
        public float rotation;
        Texture2D textureBackgroundCipher;
        Texture2D textureCircleCipher;
        Rectangle textureBackRect;
        Rectangle textureCircleRect;


        float moveRotation = 0.005f;

        //public double speed;
        //public double initSpeed;



        public Caesar_Cipher(ContentManager content)//userà constvar cipherRect
        {

            rotation = ConstVar.startRotation;// punto iniziale
            textureBackgroundCipher = content.Load<Texture2D>("CaesarBack");
            textureCircleCipher = content.Load<Texture2D>("CaesarCircle");
            // origin = new Vector2((1124*ConstVar.displayDim.X)/textureCircleCipher.Width, 681 * ConstVar.displayDim.Y) / textureCircleCipher.Height;

            textureBackRect = textureBackgroundCipher.Bounds;
            textureBackRect.Width /= 2;//è troppo grande per il mio schermo quindi la divido per 2 da 1104 a 552 (è un quadrato)
            textureBackRect.Height /= 2;
            textureCircleRect = textureCircleCipher.Bounds;
            textureCircleRect.Width /= 2;//sono coincidenti le due texture
            textureCircleRect.Height /= 2;
            origin = new Vector2(textureCircleRect.Width, textureCircleRect.Height);//origine della figura che gira nel centro preciso però è calcolato rispetto alla dimensione della texture
            textureCircleRect.X = textureCircleRect.Width / 2; //sposto indietro la draw della texture perchè disegna a partire dall'origine
            textureCircleRect.Y = textureCircleRect.Height / 2;
        }

        public void Draw()
        {
            ConstVar.sb.Begin();
            ConstVar.sb.Draw(textureBackgroundCipher, textureBackRect, Color.White);//disegna sfondo principale
            ConstVar.sb.Draw(textureCircleCipher, textureCircleRect, null, Color.White, rotation, origin, new SpriteEffects(), 0);


            ConstVar.sb.End();
        }

        public void SetRotation()
        {
            rotation -= moveRotation;
        }

        public void ResetRotation()
        {
            rotation = 0;
        }

        public float getRotation()
        {
            return rotation;
        }


    }
}