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
    public class VigenereCypher
    {

        Texture2D textureVigenereBackground;//NB dimensioni texture541x541 perch 27 caselle 20x20 senza contare barretta che chiude a destra 
        Texture2D textureVigenereHorizontal;
        Texture2D textureVigenereVertical;
        Texture2D textureVigenereLetterBlue;
        Vector2 positionBackground;
        Vector2 positionHorizontalStart;//cosi da collegare 2 strisce in punti diversi della stessa immagine
        Vector2 positionHorizontalEnd;
        Vector2 positionVerticalStart;
        Vector2 positionVerticalEnd;
        Vector2 positionLetter;            //lettera che sarà l"incrocio tra verticale e orizzontale    
        int indiceLettera;
        public VigenereCypher(ContentManager content)
        {
            positionBackground = new Vector2(0, 0);
            positionHorizontalStart = new Vector2(20, 20);//tra la prima colonna che non va contata e prima riga c"è spazio 20
            positionHorizontalEnd = new Vector2(0, 0);
            positionVerticalStart = new Vector2(20, 20);
            positionVerticalEnd = new Vector2(0, 0);
            positionLetter = new Vector2(20, 20);

            textureVigenereBackground = content.Load<Texture2D>("VigenereScalato");
            textureVigenereHorizontal = content.Load<Texture2D>("VigenereLetterOrizzontaliScalato2");
            textureVigenereVertical = content.Load<Texture2D>("VigenereLetterVerticaliScalato2");
            textureVigenereLetterBlue = content.Load<Texture2D>("VigenereLetterOrizzontaliColorateScalato2");

            /*
            textureBackRect = textureBackgroundCipher.Bounds;
            textureBackRect.Width /= 2;//è troppo grande per il mio schermo quindi la divido per 2 da 1104 a 552 (è un quadrato)
            textureBackRect.Height /= 2;
            textureCircleRect = textureCircleCipher.Bounds;
            textureCircleRect.Width /= 2;//sono coincidenti le due texture
            textureCircleRect.Height /= 2;
            origin = new Vector2(textureCircleRect.Width, textureCircleRect.Height);//origine della figura che gira nel centro preciso però è calcolato rispetto alla dimensione della texture
            textureCircleRect.X = textureCircleRect.Width / 2; //sposto indietro la draw della texture perchè disegna a partire dall"origine
            textureCircleRect.Y = textureCircleRect.Height / 2;
            */
        }

        public void Draw(bool visibile, int riga, int colonna)//da 0 a 25 compresi
        {
            ConstVar.sb.Begin();

            ConstVar.sb.Draw(textureVigenereBackground, positionBackground, Color.White);//disegna sfondo principale e gli passo la posizioni, le dimensioni sono della texture
            if (visibile == true)
            {
                positionHorizontalStart = new Vector2(20, 20 + riga * 20);
                positionVerticalStart = new Vector2(20 + colonna * 20, 20);
                positionHorizontalEnd = new Vector2(20 + 26 * 20 - riga * 20, 20 + riga * 20);///calcoli magici (se ti interessa il primo è per saltare la prima colonna dichiarativa, piu 26 per spostarsi in fondo e tornare indietro di tot posizioni per mettere la A nella giusta posizione
                positionVerticalEnd = new Vector2(20 + colonna * 20, 20 + 26 * 20 - colonna * 20);
                positionLetter = new Vector2(20 + colonna * 20, 20 + riga * 20);

                //I seguenti calcoli sono per collegare la lettera A dopo la Z 

                ConstVar.sb.Draw(textureVigenereHorizontal, positionHorizontalStart, new Rectangle(riga * 20, 0, textureVigenereHorizontal.Width - riga * 20, textureVigenereHorizontal.Height), Color.White);//dico texture , posizione, sourceRect e colore;      
                //ConstVar.sb.Draw(textureVigenereHorizontal, positionHorizontalStart, null, Color.White);//non serve piu
                ConstVar.sb.Draw(textureVigenereHorizontal, positionHorizontalEnd, new Rectangle(0, 0, riga * 20, textureVigenereHorizontal.Height), Color.White);//parte dall"inizio della figura e copre i punti rimanenti, 

                ConstVar.sb.Draw(textureVigenereVertical, positionVerticalStart, new Rectangle(0, colonna * 20, textureVigenereVertical.Width, textureVigenereVertical.Height - colonna * 20), Color.White);
                //ConstVar.sb.Draw(textureVigenereVertical, positionVerticalStart, null, Color.White);//non serve piu
                ConstVar.sb.Draw(textureVigenereVertical, positionVerticalEnd, new Rectangle(0, 0, textureVigenereVertical.Width, colonna * 20), Color.White);

                ConstVar.sb.Draw(textureVigenereLetterBlue, positionLetter, new Rectangle((colonna * 20 + riga * 20) % textureVigenereLetterBlue.Width, 0, 20, 20), Color.White);

                indiceLettera = (colonna + riga) % 26;
                //ConstVar.sb.DrawString(ConstVar.font24, ConverterIndexToChar(indiceLettera), new Vector2(500, 600), Color.Black);//!!RIGA MOLTO UTILE PER FAR VEDERE RISULTATO

            }
            ConstVar.sb.End();
        }

        public String ConverterIndexToChar(int index)//per trovare che lettera printare a schermo
        {
            switch (index)
            {
                case 0:
                    return "A";
                    break;
                case 1:
                    return "B";
                    break;
                case 2:
                    return "C";
                    break;
                case 3:
                    return "D";
                    break;
                case 4:
                    return "E";
                    break;
                case 5:
                    return "F";
                    break;
                case 6:
                    return "G";
                    break;
                case 7:
                    return "H";
                    break;
                case 8:
                    return "I";
                    break;
                case 9:
                    return "J";
                    break;
                case 10:
                    return "K";
                    break;
                case 11:
                    return "L";
                    break;
                case 12:
                    return "M";
                    break;
                case 13:
                    return "N";
                    break;
                case 14:
                    return "O";
                    break;
                case 15:
                    return "P";
                    break;
                case 16:
                    return "Q";
                    break;
                case 17:
                    return "R";
                    break;
                case 18:
                    return "S";
                    break;
                case 19:
                    return "T";
                    break;
                case 20:
                    return "U";
                    break;
                case 21:
                    return "V";
                    break;
                case 22:
                    return "W";
                    break;
                case 23:
                    return "X";
                    break;
                case 24:
                    return "Y";
                    break;
                case 25:
                    return "Z";
                    break;
                default:
                    return "A";//in caso di errori non crasha                   
            }
        }
    }

}


