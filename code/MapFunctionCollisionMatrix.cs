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

namespace Gioco_Esame_Monogame
{
    public partial class Character //così facendo estendo la classe character e posso chiamare da quella classe queste funzione senza intasare troppo il codice là(ecco partial cosa fa)
    {
        Vector2 matriceTile = new Vector2(0, 0);       

        //Tile è 40x35.2
        public int MapFunctionMatrix(int valoreIndice,Vector2 posizioneRispettoTile)//si arriva qua quando sicuramente i valori non sono 0 o 1.
        {//In base all'indice ricevuto calcolo se nella mappa "template" ,creata come tile costante con un codice, se nella posizione data
            //(che sarà la posizione futura) è possibile raggiungerla(0 se raggiungibile, 1 non raggiungibile)
          
            matriceTile = positionQuadratoTileMatrice(posizioneRispettoTile);
            switch (valoreIndice)
            {
                case 2://solo nel punto 1 1 pieno
                    return ConstVar.MatrixCode2[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 3://vuoto solo punto in alto a sinistra
                    return ConstVar.MatrixCode3[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 4://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode4[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 5://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode5[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 6://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode6[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 7://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode7[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 8://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode8[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 9://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode9[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 10://pieno solo punto in basso a destra
                    if (ConstVar.fogliettiTrovati<3)
                    {
                        return ConstVar.MatrixCode10[(int)matriceTile.X, (int)matriceTile.Y];
                    }
                    else
                    {
                        if (ConstVar.zoneStoryCounter == 1)
                        {
                            //ConstVar.changedWindow = true;
                            ConstVar.abilitatoreMessaggi = true;
                            ConstVar.keyPress = true;
                            gioco.ChangeState(new CasseCaesarState(gioco, graphicsDevice, content));     //gamechar deve essere di tipo state come accade in menu2
                            return 1;
                        }
                        else
                        {
                            return ConstVar.MatrixCode10[(int)matriceTile.X, (int)matriceTile.Y];
                        }
                    }
                    break;
                case 11://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode11[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 12://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode12[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 13://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode13[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 14://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode14[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 15://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode15[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 16://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode16[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 17://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode17[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 18://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode18[(int)matriceTile.X, (int)matriceTile.Y];
                    break;
                case 19://pieno solo punto in basso a destra
                    return ConstVar.MatrixCode19[(int)matriceTile.X, (int)matriceTile.Y];
                    break;


                case 50://zonecounter 2 e parlo con fabbro
                    if (ConstVar.zoneStoryCounter == 2)
                    {
                        ConstVar.abilitatoreMessaggi = true;
                        return 0;
                    }
                    else
                    {                        
                        return 0;
                    }

                case 60: // uscita soglia porta casa principale  
                    ConstVar.changedWindow = true;
                    gioco.ChangeState(Menu2State.MainState);     //gamechar deve essere di tipo state come accade in menu2
                    ConstVar.MainCharacter.setPos(ConstVar.startPositionExitCharacterMainHouse.X, ConstVar.startPositionExitCharacterMainHouse.Y);
                    return 1;
                case 61: // soglia porta casa principale
                    if (ConstVar.zoneStoryCounter == 0)
                    {
                        ConstVar.changedWindow = true;
                        gioco.ChangeState(Menu2State.CaesarCypherS);     //gamechar deve essere di tipo state come accade in menu2
                        return 1;
                    }
                    else
                    {
                        ConstVar.changedWindow = true;
                        gioco.ChangeState(Menu2State.MainHouseState);     //gamechar deve essere di tipo state come accade in menu2
                        return 1;
                    }

                case 62: // soglia porta casa fabbro
                    if (ConstVar.zoneStoryCounter <= 1)
                    {
                        return 1;
                    }
                    else
                    {
                        ConstVar.changedWindow = true;
                        gioco.ChangeState(Menu2State.House2State);     //gamechar deve essere di tipo state come accade in menu2
                        return 1;
                    }
                    break;

                case 64: // uscita soglia porta casa principale 
                    ConstVar.changedWindow = true;
                    gioco.ChangeState(Menu2State.MainState);     //gamechar deve essere di tipo state come accade in menu2
                    ConstVar.MainCharacter.setPos(ConstVar.startPositionExitCharacterCarpenterHouse.X, ConstVar.startPositionExitCharacterCarpenterHouse.Y);
                    return 1;

                case 70: //cifrario di cesare// funziona il collegamento già testato
                    ConstVar.changedWindow = true;
                    gioco.ChangeState(Menu2State.CaesarCypherS);     //gamechar deve essere di tipo state come accade in menu2
                    return 1;
                case 71: //cifrario di Vigenere// funziona il collegamento già testato
                    ConstVar.changedWindow = true;
                    gioco.ChangeState(Menu2State.VigenereS);     //gamechar deve essere di tipo state come accade in menu2
                    return 1;
                case 100:
                    if (Menu2State.MainHouseState.foglietto1)
                    {
                        Menu2State.MainHouseState.foglietto1 = false;
                        ConstVar.fogliettiTrovati += 1;
                    }
                    if (ConstVar.fogliettiTrovati == 3)
                    { ConstVar.abilitatoreMessaggi = true; }
                    return 0;
                    break;
                case 101:
                    if (Menu2State.MainHouseState.foglietto2)
                    {
                        Menu2State.MainHouseState.foglietto2 = false;
                        ConstVar.fogliettiTrovati += 1;
                        if (ConstVar.fogliettiTrovati == 3)
                        { ConstVar.abilitatoreMessaggi = true; }
                    }
                    return 0;
                    break;
                case 102:
                    if (Menu2State.MainHouseState.foglietto3)
                    {
                        Menu2State.MainHouseState.foglietto3 = false;
                        ConstVar.fogliettiTrovati += 1;
                        if (ConstVar.fogliettiTrovati == 3)
                        { ConstVar.abilitatoreMessaggi = true; }
                    }
                    return 0;
                    break;
            }
            return 1;//se non trova nulla del case diciamo che che non si può andare di default

        }

        public Vector2 positionQuadratoTileMatrice(Vector2 posizionePixel)//mi ritorna la posizione righe colonne 3x3  x y di dove si trova il mio pixel rispetto al tile, da inserire nella ricerca delle matrice template
        {
            //essendo 40 x 35,2 decido che le colonne iniziano a 0 13 27 e le righe 0 12 24
            if (posizionePixel.Y < 12)
                matriceTile.X = 0;
            else if (posizionePixel.Y < 24)
                matriceTile.X = 1;
            else
                matriceTile.X = 2;

            if (posizionePixel.X < 13)
                matriceTile.Y = 0;
            else if (posizionePixel.X < 24)
                matriceTile.Y = 1;
            else
                matriceTile.Y = 2;

            return matriceTile;

        }
    }
}
