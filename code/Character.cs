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

    public partial class Character
    {
        public Vector2 position;
        public Rectangle rectangleForDraw;
        Texture2D textureCharacter;
        public Vector2 dimCharacterRectangle;
        public Vector2 animatedSpritebatchValue;//riga e colonna in cui siamo al momento
        bool rightFootRow;//se il piede destro è avanti o no
        int totalCol = 2;
        //per la mappa
        Vector2 positionTile = new Vector2(0, 0);
        Vector2 positionPixel = new Vector2(0, 0);

        Vector2 calcoliPixel = new Vector2(0, 0);
        Vector2 calcoliTile = new Vector2(0, 0);
        Vector2 calcoliPixelZoom = new Vector2(0, 0);//per trovare il pixel del quadrato 3x3 all'interno di un tile
        Vector2 posizioneOriginePixelZoom = new Vector2(0, 0);

        int indiceVerifica = 0;//codice matrici semplici

        int indiceVerificaFunzioneMap = 0;//codice per le matrici zoomate

        Game1 gioco;
        GraphicsDevice graphicsDevice;
        ContentManager content;

        public Character(Vector2 InitPos, Texture2D tex, Vector2 dimRect, Game1 game, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            gioco = game;
            graphicsDevice = GraphicsDevice;
            content = Content;
            textureCharacter = tex;
            position = InitPos;
            dimCharacterRectangle = dimRect;
            animatedSpritebatchValue = new Vector2(0, 0);//righe e colonne
            rectangleForDraw = new Rectangle((int)position.X, (int)position.Y, (int)dimCharacterRectangle.X, (int)dimCharacterRectangle.Y);
            rightFootRow = true;
        }//costruttore di character



        #region setAllPos
        public void setPosXRight(double pixel)
        {
            animatedSpritebatchValue.X = 1;//destra


            /*punto origine in alto a sinistra
            calcoli.X = rectangleForDraw.X + (int)pixel;
            calcoli.Y = rectangleForDraw.Y;
            calcoli = GetTileFromPixel(calcoli);//mi torna il mio tile dentro cui sarò
            */

            calcoliPixel.X = rectangleForDraw.X + (int)pixel + (ConstVar.dimCharacter.X / 2);//a metà corpo
            calcoliPixel.Y = rectangleForDraw.Y + (int)((ConstVar.dimCharacter.Y * 2) / 3) + 4;//circa altezza ginocchia
            calcoliTile = GetTileFromPixel(calcoliPixel);//mi torna il mio tile dentro cui sarò

            indiceVerifica = ConstVar.collisioniMappaCorrente[(int)calcoliTile.Y, (int)calcoliTile.X];

            switch (indiceVerifica)
            {


                case 0://se vale 1 è muro pieno                

                    //nessun problema puoi andare
                    Case0Right(pixel);

                    break;

                case 1:
                    //caso pieno non faccio nulla
                    break;

                default://tutti gli altri casi

                    //ho calcoliPixel che mi dice il pixel esatto in cui sto
                    posizioneOriginePixelZoom = GetPixelFromTile(calcoliTile);
                    calcoliPixelZoom = new Vector2(calcoliPixel.X - posizioneOriginePixelZoom.X, calcoliPixel.Y - posizioneOriginePixelZoom.Y);
                    indiceVerificaFunzioneMap = MapFunctionMatrix(indiceVerifica, calcoliPixelZoom);
                    if (indiceVerificaFunzioneMap == 0)
                    {
                        Case0Right(pixel);
                    }
                    break;

                    //se vale 2 è centro occupato di 3x3

            }

        }//controllo il passo futuro che si sta per fare e si verifica se è accettabile usando la mappa di valori
                        
        public void setPosXLeft(double pixel)
        {
            animatedSpritebatchValue.X = 0;//sinistra

            /*punto origine in alto a sinistra
            calcoli.X = rectangleForDraw.X + (int)pixel;
            calcoli.Y = rectangleForDraw.Y;
            calcoli = GetTileFromPixel(calcoli);//mi torna il mio tile dentro cui sarò
            */

            calcoliPixel.X = rectangleForDraw.X - (int)pixel + (ConstVar.dimCharacter.X / 2);//a metà corpo
            calcoliPixel.Y = rectangleForDraw.Y + (int)((ConstVar.dimCharacter.Y * 2) / 3) + 4;//circa altezza ginocchia
            calcoliTile = GetTileFromPixel(calcoliPixel);//mi torna il mio tile dentro cui sarò

            indiceVerifica = ConstVar.collisioniMappaCorrente[(int)calcoliTile.Y, (int)calcoliTile.X];

            switch (indiceVerifica)
            {


                case 0://se vale 1 è muro pieno                

                    //nessun problema puoi andare
                    Case0Left(pixel);

                    break;

                case 1:
                    //caso pieno non faccio nulla
                    break;

                default://tutti gli altri casi

                    //ho calcoliPixel che mi dice il pixel esatto in cui sto
                    posizioneOriginePixelZoom = GetPixelFromTile(calcoliTile);
                    calcoliPixelZoom = new Vector2(calcoliPixel.X - posizioneOriginePixelZoom.X, calcoliPixel.Y - posizioneOriginePixelZoom.Y);
                    indiceVerificaFunzioneMap = MapFunctionMatrix(indiceVerifica, calcoliPixelZoom);
                    if (indiceVerificaFunzioneMap == 0)
                    {
                        Case0Left(pixel);
                    }
                    break;

                    //se vale 2 è centro occupato di 3x3
            }
        }//stessa cosa ma con freccia a sinistra e animazioni associate
                      
        public void setPosYPlus(double pixel)//plus nel senso che somma quindi si abbassa
        {

            if (animatedSpritebatchValue.X == 2)//sinistra
                animatedSpritebatchValue.X = 0;
            if (animatedSpritebatchValue.X == 3)//destra
                animatedSpritebatchValue.X = 1;

            /*punto origine in alto a sinistra
            calcoli.X = rectangleForDraw.X + (int)pixel;
            calcoli.Y = rectangleForDraw.Y;
            calcoli = GetTileFromPixel(calcoli);//mi torna il mio tile dentro cui sarò
            */

            calcoliPixel.X = rectangleForDraw.X + (ConstVar.dimCharacter.X / 2);//a metà corpo
            calcoliPixel.Y = rectangleForDraw.Y + (int)pixel + (int)((ConstVar.dimCharacter.Y * 2) / 3) + 4;//circa altezza ginocchia
            calcoliTile = GetTileFromPixel(calcoliPixel);//mi torna il mio tile dentro cui sarò

            indiceVerifica = ConstVar.collisioniMappaCorrente[(int)calcoliTile.Y, (int)calcoliTile.X];

            switch (indiceVerifica)
            {


                case 0://se vale 1 è muro pieno                

                    //nessun problema puoi andare
                    Case0Plus(pixel);

                    break;

                case 1:
                    //caso pieno non faccio nulla
                    break;

                default://tutti gli altri casi

                    //ho calcoliPixel che mi dice il pixel esatto in cui sto
                    posizioneOriginePixelZoom = GetPixelFromTile(calcoliTile);
                    calcoliPixelZoom = new Vector2(calcoliPixel.X - posizioneOriginePixelZoom.X, calcoliPixel.Y - posizioneOriginePixelZoom.Y);
                    indiceVerificaFunzioneMap = MapFunctionMatrix(indiceVerifica, calcoliPixelZoom);
                    if (indiceVerificaFunzioneMap == 0)
                    {
                        Case0Plus(pixel);
                    }
                    break;

                    //se vale 2 è centro occupato di 3x3
            }   
        }
                    
        public void setPosYMinus(double pixel)//minus inteso fa meno la funzione quindi si alza
        {

            if (animatedSpritebatchValue.X == 0)//sinistra
                animatedSpritebatchValue.X = 2;
            if (animatedSpritebatchValue.X == 1)//destra
                animatedSpritebatchValue.X = 3;

            /*punto origine in alto a sinistra
           calcoli.X = rectangleForDraw.X + (int)pixel;
           calcoli.Y = rectangleForDraw.Y;
           calcoli = GetTileFromPixel(calcoli);//mi torna il mio tile dentro cui sarò
           */

            calcoliPixel.X = rectangleForDraw.X + (ConstVar.dimCharacter.X / 2);//a metà corpo
            calcoliPixel.Y = rectangleForDraw.Y - (int)pixel + (int)((ConstVar.dimCharacter.Y * 2) / 3) + 4;//circa altezza ginocchia
            calcoliTile = GetTileFromPixel(calcoliPixel);//mi torna il mio tile dentro cui sarò

            indiceVerifica = ConstVar.collisioniMappaCorrente[(int)calcoliTile.Y, (int)calcoliTile.X];

            switch (indiceVerifica)
            {

                case 0://se vale 1 è muro pieno                

                    //nessun problema puoi andare
                    Case0Minus(pixel);

                    break;

                case 1:
                    //caso pieno non faccio nulla
                    break;

                default://tutti gli altri casi

                    //ho calcoliPixel che mi dice il pixel esatto in cui sto
                    posizioneOriginePixelZoom = GetPixelFromTile(calcoliTile);
                    calcoliPixelZoom = new Vector2(calcoliPixel.X - posizioneOriginePixelZoom.X, calcoliPixel.Y - posizioneOriginePixelZoom.Y);
                    indiceVerificaFunzioneMap = MapFunctionMatrix(indiceVerifica, calcoliPixelZoom);
                    if (indiceVerificaFunzioneMap == 0)
                    {
                        Case0Minus(pixel);
                    }
                    break;

                    //se vale 2 è centro occupato di 3x3
            }
        }
        #endregion


        public void setPos(double x, double y) //nel caso ci serva nel passare a una nuova schermata (èèèè?? -pasto)
        {
            rectangleForDraw.X = (int)x;
            rectangleForDraw.Y = (int)y;
        }


        public void Update()//non utilizzata ancora
        {
           
        }

        public void Draw()
        {
            //ConstVar.sb.Draw(textureCharacter, rectangleForDraw, null, Color.White, 0, origin, new SpriteEffects(), 0);

            WalkingSpritebatch.Draw(rectangleForDraw, textureCharacter, animatedSpritebatchValue);
        }//draw il personaggio tenendo conto dell'animazione


        //per la mappa
        public Vector2 GetTileFromPixel(Vector2 posPixelXY)//funziona con mappe 512 x 320
        {
            positionTile.X = (int)(posPixelXY.X / 40);//16 * ConstVar.constMainBackX=40
            positionTile.Y = (int)(posPixelXY.Y / (35.2f));//16 * ConstVar.constMainBackY=35.2
            return positionTile;
        }//dandoci i pixel mi dice in che tile mi trovo
        //per la mappa
        public Vector2 GetPixelFromTile(Vector2 posTileXY)
        {
            positionPixel.X = (int)(posTileXY.X * 40);//16 * ConstVar.constMainBackX=40
            positionPixel.Y = (int)(posTileXY.Y * 35.2f);//16 * ConstVar.constMainBackY=35.2
            return positionPixel;
        }//dandoci il tile mi dice i valori di origine (alto a sinistra) del tile in cui mi trovo
        

        //queste funzioni qui sotto servono per muovere effettivamente e per controllare l'animazione; sono chiamate da SetAllPos
        public void Case0Right(double pixel)
        {
            rectangleForDraw.X += (int)pixel;

            if (animatedSpritebatchValue.Y < totalCol & rightFootRow == true)
            {
                animatedSpritebatchValue.Y++;
            }
            else
            {
                if (animatedSpritebatchValue.Y >= 2)
                {
                    rightFootRow = false;
                    animatedSpritebatchValue.Y--;
                }
                else
                {
                    if (animatedSpritebatchValue.Y > 0 & rightFootRow == false)
                    {
                        animatedSpritebatchValue.Y--;
                    }
                    else
                    {
                        if (animatedSpritebatchValue.Y <= 0 & rightFootRow == false)
                        {
                            animatedSpritebatchValue.Y++;
                            rightFootRow = true;
                        }
                    }
                }
            }
        }//intuibile
      
        public void Case0Left(double pixel)
        {
            rectangleForDraw.X -= (int)pixel;

            if (animatedSpritebatchValue.Y < totalCol & rightFootRow == true)
            {
                animatedSpritebatchValue.Y++;
            }
            else
            {
                if (animatedSpritebatchValue.Y >= 2)
                {
                    rightFootRow = false;
                    animatedSpritebatchValue.Y--;
                }
                else
                {
                    if (animatedSpritebatchValue.Y > 0 & rightFootRow == false)
                    {
                        animatedSpritebatchValue.Y--;
                    }
                    else
                    {
                        if (animatedSpritebatchValue.Y <= 0 & rightFootRow == false)
                        {
                            animatedSpritebatchValue.Y++;
                            rightFootRow = true;
                        }
                    }

                }
            }
        }
    
        public void Case0Plus(double pixel)
        {
                rectangleForDraw.Y += (int) pixel;

                if (animatedSpritebatchValue.Y<totalCol & rightFootRow == true)
                {
                    animatedSpritebatchValue.Y++;
                }
                else
                {
                    if (animatedSpritebatchValue.Y >= 2)
                    {
                        rightFootRow = false;
                        animatedSpritebatchValue.Y--;
                    }
                    else
                    {
                        if (animatedSpritebatchValue.Y > 0 & rightFootRow == false)
                        {
                            animatedSpritebatchValue.Y--;
                        }
                        else
                        {
                            if (animatedSpritebatchValue.Y <= 0 & rightFootRow == false)
                            {
                                animatedSpritebatchValue.Y++;
                                rightFootRow = true;
                            }
                        }
                    }
                }
        }//plus = aumentano Y e si abbassa la figura
    
        public void Case0Minus(double pixel)
        {                
                rectangleForDraw.Y -= (int) pixel;
                if (animatedSpritebatchValue.Y<totalCol & rightFootRow == true)
                {
                    animatedSpritebatchValue.Y++;
                }
                else
                {
                    if (animatedSpritebatchValue.Y >= 2)
                    {
                        rightFootRow = false;
                        animatedSpritebatchValue.Y--;
                    }
                    else
                    {
                        if (animatedSpritebatchValue.Y > 0 & rightFootRow == false)
                        {
                            animatedSpritebatchValue.Y--;
                        }
                        else
                        {
                            if (animatedSpritebatchValue.Y <= 0 & rightFootRow == false)
                            {
                                animatedSpritebatchValue.Y++;
                                rightFootRow = true;
                            }
                        }
                    }
                }
        }//minus=diminuiscono Y e si alza la figura
    }
}
