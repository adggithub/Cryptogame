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
    public class CaesarCypherState : State
    {
        public Texture2D introduzioneCritto;
        public Texture2D spiegazioneCaesar;
        public Texture2D iconaI;
        public Texture2D infoCaesar;
        public Texture2D contornoRosso;
        public Texture2D contornoBlu;
        public Texture2D righe;
        public Texture2D quadretti;
        public bool isPressed = false;
        public Rectangle rettangoloSpiegazioni;
        public Vector2 posizioneQuadratoRosso;
        public Vector2 posizioneQuadratoBlu;
        public int contatoreZona2 = 0;
        public Texture2D fogliettoChiave;
        public Texture2D chiaviMessaggio;
        public int indiceLettera = 0;


        public CaesarCypherState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //Caesar_Cipher Caesar = new Caesar_Cipher(_content); 
            
            ConstVar.font24 = _content.Load<SpriteFont>("Fonts/Text24");
            introduzioneCritto = _content.Load<Texture2D>("Introduzione");
            spiegazioneCaesar = _content.Load<Texture2D>("CesareSpiegazione");
            iconaI = _content.Load<Texture2D>("info_icon");
            infoCaesar = _content.Load<Texture2D>("infoCaesar");
            contornoRosso = _content.Load<Texture2D>("Rosso");
            contornoBlu = _content.Load<Texture2D>("Blu");
            righe = _content.Load<Texture2D>("Righe1");
            quadretti = _content.Load<Texture2D>("QuadrettiDEF");
            fogliettoChiave = _content.Load<Texture2D>("bob");
            chiaviMessaggio = _content.Load<Texture2D>("zmz");
            rettangoloSpiegazioni = new Rectangle(100, 50, (int)ConstVar.displayDim.X - 200, (int)ConstVar.displayDim.Y-200);
            posizioneQuadratoRosso = new Vector2(50, 120);
            posizioneQuadratoBlu = new Vector2(95, 150);
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
            KeyboardState kbState;
            kbState = Keyboard.GetState();
            if (ConstVar.changedWindow)
            {

                ConstVar.isMouseVisibleM = true;//nel gioco il mouse è invisibile
                ConstVar.abilitaTastiera = true;//mi dice che sono in gioco e abilitare tastiera                
                ConstVar.changedWindow = false;

                // ConstVar.backgroundMainText = _content.Load<Texture2D>("StartGarden");//schermata principale
            }
            else
            {
                if (ConstVar.zoneStoryCounter == 0&&ConstVar.contatoreMessaggi!=15&&(ConstVar.contatoreMessaggi<18||ConstVar.contatoreMessaggi==22))
                {
                    ConstVar.abilitatoreMessaggi = true;
                    if (ConstVar.keyPress == false)//attendo che venga premuto un pulsante
                    {
                        

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
                    if (ConstVar.contatoreMessaggi == 15)
                    {

                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }
                                /*metodo generale
                                if (((int)myChar[0] >= 65 && (int)myChar[0] <= 90) || ((int)myChar[0] >= 97 && (int)myChar[0] <= 122))
                                {
                                    if ((int)myChar[0] >= 97)
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0] - 22;        // converto in maiuscole
                                    }
                                    else
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0];//65 A, 90 Z , 97 a ,122 z codice ascii
                                    }
                                    isPressed = true;
                                }
                                */
                                if ((int)myChar[0] == 71 || (int)myChar[0] == 103)//premo G o g
                                {
                                    ConstVar.letterCaesar = 71;

                                    isPressed = true;
                                }
                            }

                            //punto di partenza 0 e devo sottrarre, angolo giro è 6.28 quindi devo dividere 6.28 per 26 e moltiplicarlo per la lettera
                            if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() > ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                            {
                                ConstVar.abilitatoreMessaggi = false;
                                Menu2State.Caesar.SetRotation();
                            }
                            else
                            {
                                if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() < ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                                {
                                    ConstVar.abilitatoreMessaggi = true;
                                    ConstVar.contatoreMessaggi += 1;
                                    ConstVar.letterCaesar = -1;
                                    isPressed = false;
                                }
                            }
                        }
                    }
                    if(ConstVar.contatoreMessaggi==18)
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;//utilizzo questo timer perchè è molto piu veloce dell'altro e qui non è necessario aspettare tutto quel tempo
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }
                                
                                if ((int)myChar[0] == 87 || (int)myChar[0] == 119)//premo W o w
                                {
                                    ConstVar.letterCaesar = 87;

                                    isPressed = true;
                                }
                            }

                           
                            if (ConstVar.letterCaesar != -1)
                            {

                                ConstVar.contatoreMessaggi += 1;
                                ConstVar.letterCaesar = -1;
                                isPressed = false;
                                righe = _content.Load<Texture2D>("Righe2");
                                posizioneQuadratoRosso = new Vector2(370, 40);
                                posizioneQuadratoBlu = new Vector2(340, 90);
                            }
                                                     
                        }
                    }
                    if (ConstVar.contatoreMessaggi == 19)
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }

                                if ((int)myChar[0] == 67 || (int)myChar[0] == 99)//premo C o c
                                {
                                    ConstVar.letterCaesar = 67;

                                    isPressed = true;
                                }
                            }


                            if (ConstVar.letterCaesar != -1)
                            {

                                ConstVar.contatoreMessaggi += 1;
                                ConstVar.letterCaesar = -1;
                                isPressed = false;
                                righe = _content.Load<Texture2D>("Righe3");
                                posizioneQuadratoRosso = new Vector2(10, 230);
                                posizioneQuadratoBlu = new Vector2(65, 240);
                            }

                        }
                    }
                    if (ConstVar.contatoreMessaggi == 20)
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }

                                if ((int)myChar[0] == 85 || (int)myChar[0] == 117)//premo U o u
                                {
                                    ConstVar.letterCaesar = 85;

                                    isPressed = true;
                                }
                            }


                            if (ConstVar.letterCaesar != -1)
                            {

                                ConstVar.contatoreMessaggi += 1;
                                ConstVar.letterCaesar = -1;
                                isPressed = false;
                                righe = _content.Load<Texture2D>("Righe4");
                                posizioneQuadratoRosso = new Vector2(470, 340);
                                posizioneQuadratoBlu = new Vector2(430, 325);
                            }

                        }
                    }
                    if (ConstVar.contatoreMessaggi == 21)
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }

                                if ((int)myChar[0] == 73 || (int)myChar[0] == 105)//premo I o i
                                {
                                    ConstVar.letterCaesar = 73;

                                    isPressed = true;
                                }
                            }


                            if (ConstVar.letterCaesar != -1)
                            {

                                ConstVar.contatoreMessaggi += 1;
                                ConstVar.letterCaesar = -1;
                                isPressed = false;
                                righe = _content.Load<Texture2D>("Righe5");
                            }

                        }
                    }
                    if(ConstVar.contatoreMessaggi==23)
                    {
                        ConstVar.zoneStoryCounter = 1;//si passa alla zona 1 della storia
                        ConstVar.changedWindow = true;// mi serve per allineare tutto
                        _game.ChangeState(Menu2State.MainHouseState);//entro in casa
                    }

                    if (ConstVar.contatoreMessaggi == 48)//inizio parla fabbro
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }
                                //metodo generale
                                if (((int)myChar[0] >= 65 && (int)myChar[0] <= 90) || ((int)myChar[0] >= 97 && (int)myChar[0] <= 122))
                                {
                                    if ((int)myChar[0] >= 97)
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0] - 22;        // converto in maiuscole
                                    }
                                    else
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0];//65 A, 90 Z , 97 a ,122 z codice ascii
                                    }
                                    isPressed = true;
                                }
                                /*
                                if ((int)myChar[0] == 71 || (int)myChar[0] == 103)//premo G o g
                                {
                                    ConstVar.letterCaesar = 71;

                                    isPressed = true;
                                }*/
                            }

                            //punto di partenza 0 e devo sottrarre, angolo giro è 6.28 quindi devo dividere 6.28 per 26 e moltiplicarlo per la lettera
                            if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() > ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                            {
                                ConstVar.abilitatoreMessaggi = false;
                                Menu2State.Caesar.SetRotation();
                            }
                            else
                            {//ruota e se la lettera non è corretta torna a 0
                                if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() < ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                                {
                                    if (ConstVar.letterCaesar != 67)//C
                                    {
                                        Menu2State.Caesar.ResetRotation();
                                        ConstVar.letterCaesar = -1;
                                        isPressed = false;
                                    }
                                    else
                                    {//se corretta
                                        //fogliettoChiavi = true;//abilita la draw del fogliettino con la parola CANE
                                        var myChar = kbState.GetPressedKeys();
                                        if (myChar.Length == 0) { return; }
                                        if (letteraEGiusta((char)myChar[0], indiceLettera,1))
                                        {
                                            indiceLettera++;//ho scritto giusto

                                            fogliettoChiave = _content.Load<Texture2D>("bob" + indiceLettera);//bob
                                            if (indiceLettera == 3)
                                            {
                                                isPressed = false;
                                                ConstVar.letterCaesar = -1;
                                                Menu2State.Caesar.ResetRotation();                                                
                                                ConstVar.contatoreMessaggi ++;//vado a 49
                                                indiceLettera = 0;//Resetto
                                                fogliettoChiave = _content.Load<Texture2D>("chiave");//chiave0
                                                chiaviMessaggio = _content.Load<Texture2D>("otumhq");

                                            }
                                        }

                                        //ConstVar.contatoreMessaggi += 1;
                                        //ConstVar.abilitatoreMessaggi = true;
                                    }
                                    /*
                                    ConstVar.abilitatoreMessaggi = true;
                                    ConstVar.contatoreMessaggi += 1;
                                    ConstVar.letterCaesar = -1;
                                    isPressed = false;
                                    */
                                }
                            }               
                        }
                    }
                    if (ConstVar.contatoreMessaggi == 49)//secondo fabbro
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }
                                //metodo generale
                                if (((int)myChar[0] >= 65 && (int)myChar[0] <= 90) || ((int)myChar[0] >= 97 && (int)myChar[0] <= 122))
                                {
                                    if ((int)myChar[0] >= 97)
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0] - 22;        // converto in maiuscole
                                    }
                                    else
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0];//65 A, 90 Z , 97 a ,122 z codice ascii
                                    }
                                    isPressed = true;
                                }
                                /*
                                if ((int)myChar[0] == 71 || (int)myChar[0] == 103)//premo G o g
                                {
                                    ConstVar.letterCaesar = 71;

                                    isPressed = true;
                                }*/
                            }

                            //punto di partenza 0 e devo sottrarre, angolo giro è 6.28 quindi devo dividere 6.28 per 26 e moltiplicarlo per la lettera
                            if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() > ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                            {
                                ConstVar.abilitatoreMessaggi = false;
                                Menu2State.Caesar.SetRotation();
                            }
                            else
                            {//ruota e se la lettera non è corretta torna a 0
                                if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() < ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                                {
                                    if (ConstVar.letterCaesar != 79)//O
                                    {
                                        Menu2State.Caesar.ResetRotation();
                                        ConstVar.letterCaesar = -1;
                                        isPressed = false;
                                    }
                                    else
                                    {//se corretta
                                        //fogliettoChiavi = true;//abilita la draw del fogliettino con la parola CANE
                                        var myChar = kbState.GetPressedKeys();
                                        if (myChar.Length == 0) { return; }
                                        if (letteraEGiusta((char)myChar[0], indiceLettera, 2))
                                        {
                                            indiceLettera++;//ho scritto giusto

                                            fogliettoChiave = _content.Load<Texture2D>("chiave" + indiceLettera);//chiave
                                            if (indiceLettera == 6)
                                            {
                                                isPressed = false;
                                                ConstVar.letterCaesar = -1;
                                                Menu2State.Caesar.ResetRotation();                                                
                                                ConstVar.contatoreMessaggi++;//vado a 50
                                                indiceLettera = 0;//Resetto
                                                fogliettoChiave = _content.Load<Texture2D>("albero");//Albero0
                                                chiaviMessaggio = _content.Load<Texture2D>("vgwzmj");
                                            }
                                        }

                                        //ConstVar.contatoreMessaggi += 1;
                                        //ConstVar.abilitatoreMessaggi = true;
                                    }
                                    /*
                                    ConstVar.abilitatoreMessaggi = true;
                                    ConstVar.contatoreMessaggi += 1;
                                    ConstVar.letterCaesar = -1;
                                    isPressed = false;
                                    */
                                }
                            }
                        }
                    }
                    if (ConstVar.contatoreMessaggi == 50)//terzo fabbro
                    {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//gametime.ElapsedGameTime returns the time elapsed since the last update, not the total game time. For this, you need gametime.TotalGameTime.
                        ConstVar.timerRotation -= elapsed;
                        if (ConstVar.timerRotation < 0)
                        {
                            ConstVar.timerRotation = ConstVar.TIMER_ROTATION;
                            if (isPressed == false)
                            {
                                var myChar = kbState.GetPressedKeys();
                                if (myChar.Length == 0) { return; }
                                //metodo generale
                                if (((int)myChar[0] >= 65 && (int)myChar[0] <= 90) || ((int)myChar[0] >= 97 && (int)myChar[0] <= 122))
                                {
                                    if ((int)myChar[0] >= 97)
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0] - 22;        // converto in maiuscole
                                    }
                                    else
                                    {
                                        ConstVar.letterCaesar = (int)myChar[0];//65 A, 90 Z , 97 a ,122 z codice ascii
                                    }
                                    isPressed = true;
                                }
                                /*
                                if ((int)myChar[0] == 71 || (int)myChar[0] == 103)//premo G o g
                                {
                                    ConstVar.letterCaesar = 71;

                                    isPressed = true;
                                }*/
                            }

                            //punto di partenza 0 e devo sottrarre, angolo giro è 6.28 quindi devo dividere 6.28 per 26 e moltiplicarlo per la lettera
                            if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() > ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                            {
                                ConstVar.abilitatoreMessaggi = false;
                                Menu2State.Caesar.SetRotation();
                            }
                            else
                            {//ruota e se la lettera non è corretta torna a 0
                                if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() < ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                                {
                                    if (ConstVar.letterCaesar != 70)//F
                                    {
                                        Menu2State.Caesar.ResetRotation();
                                        ConstVar.letterCaesar = -1;
                                        isPressed = false;
                                    }
                                    else
                                    {//se corretta
                                        //fogliettoChiavi = true;//abilita la draw del fogliettino con la parola CANE
                                        var myChar = kbState.GetPressedKeys();
                                        if (myChar.Length == 0) { return; }
                                        if (letteraEGiusta((char)myChar[0], indiceLettera, 3))
                                        {
                                            indiceLettera++;//ho scritto giusto

                                            fogliettoChiave = _content.Load<Texture2D>("albero" + indiceLettera);//Albero
                                            if (indiceLettera == 6)
                                            {
                                                isPressed = false;
                                                ConstVar.letterCaesar = -1;
                                                Menu2State.Caesar.ResetRotation();                                                
                                                ConstVar.contatoreMessaggi++;//vado a 51
                                                indiceLettera = 0;//Resetto
                                                
                                            }
                                        }

                                        //ConstVar.contatoreMessaggi += 1;
                                        //ConstVar.abilitatoreMessaggi = true;
                                    }
                                    /*
                                    ConstVar.abilitatoreMessaggi = true;
                                    ConstVar.contatoreMessaggi += 1;
                                    ConstVar.letterCaesar = -1;
                                    isPressed = false;
                                    */
                                }
                            }
                        }
                    }

                    if (ConstVar.contatoreMessaggi == 51)
                    {
                        ConstVar.zoneStoryCounter = 3;//si passa alla zona 3 della storia
                        ConstVar.changedWindow = true;// mi serve per allineare tutto
                        ConstVar.abilitatoreMessaggi = true;
                        ConstVar.keyPress = true;
                        _game.ChangeState(Menu2State.MainState);//esco dai cifrari inversi di Cesare e torno a parlare con il fabbro
                    }

                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(ConstVar.currentBackgroundText, ConstVar.displayRect, Color.White);
            spriteBatch.End();
            Menu2State.Caesar.Draw();
            spriteBatch.Begin();
            if (ConstVar.contatoreMessaggi >= 8 && ConstVar.contatoreMessaggi < 30)
            {
                spriteBatch.Draw(iconaI, new Rectangle(1050, 50, 100, 100), Color.White);
            }
            if (ConstVar.contatoreMessaggi == 9 || ConstVar.contatoreMessaggi == 10)
            {
                spriteBatch.Draw(introduzioneCritto, rettangoloSpiegazioni, Color.White);//qua introduzione
            }
            else
            {
                if (ConstVar.contatoreMessaggi == 11 || ConstVar.contatoreMessaggi == 12)
                {
                    spriteBatch.Draw(spiegazioneCaesar, rettangoloSpiegazioni, Color.White);//pannello spiegazione
                }
            }
            if (ConstVar.abilitatoreMessaggi == true)
            {
                ConstVar.commentoGenerale.Draw(ConstVar.commentoGenerale.textMessaggi(ConstVar.contatoreMessaggi));
            }           
            if (ConstVar.contatoreMessaggi>=13 && ConstVar.contatoreMessaggi < 30)
            {
                spriteBatch.Draw(infoCaesar, new Rectangle(900, 300, 300, 300), Color.White);
            }
            if(ConstVar.contatoreMessaggi>=18&&ConstVar.contatoreMessaggi<=21)
            {
                spriteBatch.Draw(contornoRosso, new Rectangle((int)posizioneQuadratoRosso.X, (int)posizioneQuadratoRosso.Y, 50, 50), Color.White);
                spriteBatch.Draw(contornoBlu, new Rectangle((int)posizioneQuadratoBlu.X, (int)posizioneQuadratoBlu.Y, 50, 50), Color.White);
            }
            if (ConstVar.contatoreMessaggi >= 18&&ConstVar.contatoreMessaggi<30)
            {
                spriteBatch.Draw(righe, new Rectangle(540, 450, 200, 100), Color.White);
            }
            if( ConstVar.contatoreMessaggi > 47&&ConstVar.contatoreMessaggi<51)
            {
                spriteBatch.Draw(chiaviMessaggio, new Rectangle(900, 300, 300, 300), Color.White);
                spriteBatch.Draw(fogliettoChiave, new Rectangle(540, 450, 200, 100), Color.White);
            }



            //spriteBatch.Draw(quadretti, new Rectangle(0, 0, (int)ConstVar.displayDim.X, (int)ConstVar.displayDim.Y), Color.White);//per mettere una griglia
            spriteBatch.End();
        }
        private bool letteraEGiusta(char lettera, int indiceLet, int indiceCif)//manda indietro true se è giusta e false se è sbagliata
        {
            switch (indiceCif)
            {
                case 1:
                    switch (indiceLet)
                    {
                        case 0:
                            return lettera == 'B';
                            break;
                        case 1:
                            return lettera == 'O';
                            break;
                        case 2:
                            return lettera == 'B';
                            break;                        
                        default:
                            return false;
                            break;
                    }

                    break;
                case 2:
                    switch (indiceLet)
                    {
                        case 0:
                            return lettera == 'C';
                            break;
                        case 1:
                            return lettera == 'H';
                            break;
                        case 2:
                            return lettera == 'I';
                            break;
                        case 3:
                            return lettera == 'A';
                            break;
                        case 4:
                            return lettera == 'V';
                            break;
                        case 5:
                            return lettera == 'E';
                            break;
                        default:
                            return false;
                            break;
                    }
                    break;
                case 3:
                    switch (indiceLet)
                    {
                        case 0:
                            return lettera == 'A';
                            break;
                        case 1:
                            return lettera == 'L';
                            break;
                        case 2:
                            return lettera == 'B';
                            break;
                        case 3:
                            return lettera == 'E';
                            break;
                        case 4:
                            return lettera == 'R';
                            break;
                        case 5:
                            return lettera == 'O';
                            break;
                        default:
                            return false;
                            break;
                    }
                    break;
                default:
                    return false;
                    break;
            }
        }
    }
}
