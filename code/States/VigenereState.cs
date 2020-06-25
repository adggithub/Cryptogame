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
    public class VigenereState : State
    {
        public Texture2D fogliettoChiaro;
        public Texture2D fogliettoChiave;
        public Texture2D fogliettoCifrata;
        public Texture2D fogliettoLetteraChiaro;
        public Texture2D fogliettoLetteraChiave;
        public Texture2D fogliettoLetteraCifrata;
        public Texture2D fogliettoFrase;
        public int indiceLettera;
        public int indiceLettera2;
        public int indiceLettera3;
        public int indiceLettera4;
        public int valoreRiga;
        public int valoreColonna;
        public bool evidenziato;//se evidenzio incrocio tra le lettere
        public int indiceIndovinato;//per cambiare foglietto parola cifrata
        public bool firstTime;
        public bool pressed;
        public Rectangle rettangoloSpiegazioni;
        public Texture2D OtpSpiegazione;

        public VigenereState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //Caesar_Cipher Caesar = new Caesar_Cipher(_content); 

            // ConstVar.font24 = _content.Load<SpriteFont>("Fonts/Text24");
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
            evidenziato = false;
            
            valoreRiga = 0;//indico riga e colonna del cifrario di vigenere 
            valoreColonna = 0;
            indiceLettera2 = 0;
            indiceLettera3 = 0;
            indiceLettera4 = 0;
            fogliettoFrase= _content.Load<Texture2D>("bobilfabbro");
            fogliettoChiaro = _content.Load<Texture2D>("nota testo in chiaro");
            fogliettoChiave = _content.Load<Texture2D>("nota lettera chiave");
            fogliettoCifrata = _content.Load<Texture2D>("nota lettera testo cifrato");
            fogliettoLetteraChiaro = _content.Load<Texture2D>("bianco");
            fogliettoLetteraChiave = _content.Load<Texture2D>("bianco");
            fogliettoLetteraCifrata = _content.Load<Texture2D>("bianco");
            firstTime = true;
            pressed = true;
            rettangoloSpiegazioni = new Rectangle(100, 50, (int)ConstVar.displayDim.X - 200, (int)ConstVar.displayDim.Y - 200);
            OtpSpiegazione = _content.Load<Texture2D>("OTPSpiegazione");
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

                ConstVar.isMouseVisibleM = false;//nel gioco il mouse è invisibile
                ConstVar.abilitaTastiera = true;//mi dice che sono in gioco e abilitare tastiera                
                ConstVar.changedWindow = false;

               // ConstVar.backgroundMainText = _content.Load<Texture2D>("StartGarden");//schermata principale
            }
            else
            {
                if (ConstVar.contatoreMessaggi == 61)
                {
                    indiceLettera = 0;
                    var myChar = kbState.GetPressedKeys();
                    if (myChar.Length == 0) { return; }
                    if (letteraEGiusta((char)myChar[0], indiceLettera))
                    {
                        indiceLettera++;//ho scritto giusto

                        fogliettoLetteraChiaro = _content.Load<Texture2D>("B");
                        valoreColonna = 1;
                        ConstVar.contatoreMessaggi++;//vado a 62
                        
                            

                        
                    }
                }                
                else
                {
                    if (ConstVar.contatoreMessaggi == 62)
                    {
                        indiceLettera = 1;
                        var myChar = kbState.GetPressedKeys();
                        if (myChar.Length == 0) { return; }
                        if (letteraEGiusta((char)myChar[0], indiceLettera))
                        {
                            indiceLettera++;//ho scritto giusto

                            fogliettoLetteraChiave = _content.Load<Texture2D>("C");
                            valoreRiga = 2;
                            evidenziato = true;
                            ConstVar.contatoreMessaggi++;//vado a 63



                        }
                    }
                    else
                    {
                        if (ConstVar.contatoreMessaggi == 63)
                        {
                            indiceLettera = 2;
                            var myChar = kbState.GetPressedKeys();
                            if (myChar.Length == 0) { return; }
                            if (letteraEGiusta((char)myChar[0], indiceLettera))
                            {
                                indiceLettera++;//ho scritto giusto

                                fogliettoLetteraCifrata = _content.Load<Texture2D>("D");
                                indiceIndovinato = 1;
                                fogliettoFrase = _content.Load<Texture2D>("bobilfabbro"+indiceIndovinato);
                                ConstVar.contatoreMessaggi++;//vado a 64



                            }
                        }
                        else
                        {
                            if (ConstVar.zoneStoryCounter == 3 && ConstVar.contatoreMessaggi <= 65)
                            {
                                if (ConstVar.contatoreMessaggi == 65) { resetAll(); }
                                //ConstVar.abilitatoreMessaggi = true;
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
                                if (ConstVar.contatoreMessaggi == 66)
                                {
                                    var myChar = kbState.GetPressedKeys();
                                    if (myChar.Length == 0) { return; }
                                    if (letteraEGiusta((char)myChar[0], indiceLettera))//la prima volta che entro indiceLettera vale 3
                                    {
                                        indiceLettera++;//ho scritto giusto 


                                    }
                                }
                                else {if(ConstVar.contatoreMessaggi==67)
                                    {
                                        var myChar = kbState.GetPressedKeys();
                                        if (myChar.Length == 0) {
                                            pressed = false; return; }
                                        if (pressed == false)//devo mollare i tasti premuti
                                        {
                                            ConstVar.contatoreMessaggi++;
                                        } 
                                    }
                                else
                                    {
                                        if (ConstVar.contatoreMessaggi == 68)
                                        {
                                            if (firstTime == true)
                                            {
                                                fogliettoFrase = _content.Load<Texture2D>("iosovigenere");
                                                firstTime = false;
                                            }
                                            var myChar = kbState.GetPressedKeys();
                                            if (myChar.Length == 0) { return; }
                                            if (letteraEGiusta2((char)myChar[0], indiceLettera2))//la prima volta che entro indiceLettera vale 0
                                            {
                                                indiceLettera2++;//ho scritto giusto


                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                if (ConstVar.zoneStoryCounter == 3 && ConstVar.contatoreMessaggi >= 69 && ConstVar.contatoreMessaggi <= 74|| ConstVar.contatoreMessaggi >= 76 && ConstVar.contatoreMessaggi <= 83)
                {
                    if (ConstVar.contatoreMessaggi == 69) { resetAll();firstTime = true;}
                    if (ConstVar.contatoreMessaggi == 77) { resetAll(); firstTime = true; }
                    //ConstVar.abilitatoreMessaggi = true;
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
                if (ConstVar.contatoreMessaggi == 75)
                {
                    if (firstTime == true)
                    {
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare");
                        indiceLettera3 = 1;//prima volta
                        firstTime = false;
                    }
                    var myChar = kbState.GetPressedKeys();
                    if (myChar.Length == 0) { return; }
                    if (letteraEGiusta3((char)myChar[0], indiceLettera3))//la prima volta che entro indiceLettera vale 0
                    {
                        indiceLettera3++;//ho scritto giusto


                    }
                }
                if (ConstVar.contatoreMessaggi == 84)
                {
                    if (firstTime == true)
                    {
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo");
                        indiceLettera4 = 0;//prima volta
                        firstTime = false;
                    }
                    var myChar = kbState.GetPressedKeys();
                    if (myChar.Length == 0) { return; }
                    if (letteraEGiusta4((char)myChar[0], indiceLettera4))//la prima volta che entro indiceLettera vale 0
                    {
                        indiceLettera4++;//ho scritto giusto


                    }
                }
                if (ConstVar.contatoreMessaggi == 85)
                {
                    ConstVar.zoneStoryCounter = 4;
                    ConstVar.changedWindow = true;// mi serve per allineare tutto
                    ConstVar.abilitatoreMessaggi = true;
                    ConstVar.keyPress = true;
                    _game.ChangeState(Menu2State.MainState);
                }

            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(ConstVar.currentBackgroundText, ConstVar.displayRect, Color.White);//la prima volta è il giardino
            spriteBatch.End();
            Menu2State.Vigenere.Draw(evidenziato, valoreRiga, valoreColonna);//tra 0 e 25
            spriteBatch.Begin();
            spriteBatch.Draw(fogliettoFrase, new Rectangle(950, 225, 300, 150), Color.White);
            spriteBatch.Draw(fogliettoChiaro, new Rectangle(550, 400, 150, 150), Color.White);
            spriteBatch.Draw(fogliettoChiave, new Rectangle(725, 400, 150, 150), Color.White);
            spriteBatch.Draw(fogliettoCifrata, new Rectangle(900, 400, 150, 150), Color.White);
            spriteBatch.Draw(fogliettoLetteraChiaro, new Rectangle(600, 475, 50, 50), Color.White);
            spriteBatch.Draw(fogliettoLetteraChiave, new Rectangle(775, 475, 50, 50), Color.White);
            spriteBatch.Draw(fogliettoLetteraCifrata, new Rectangle(950, 475, 50, 50), Color.White);
            if (ConstVar.abilitatoreMessaggi == true)
            {
                ConstVar.commentoGenerale.Draw(ConstVar.commentoGenerale.textMessaggi(ConstVar.contatoreMessaggi));
            }
            if (ConstVar.contatoreMessaggi == 80)
            {
                spriteBatch.Draw(OtpSpiegazione, rettangoloSpiegazioni, Color.White);//pannello spiegazione
            }
            spriteBatch.End();

        }
        private bool letteraEGiusta(char lettera, int indiceLet)//manda indietro true se è giusta e false se è sbagliata
        {
            switch (indiceLet)
            {                
                  case 0:
                        return lettera == 'B'|| lettera == 'b';
                  break;
                  case 1:
                        return lettera == 'C' || lettera == 'c';
                  break;
                  case 2:
                        return lettera == 'D' || lettera == 'd';
                  break;
                  case 3:
                        if(lettera == 'O' || lettera == 'o')
                        {
                            fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                            valoreColonna = 14;
                        }
                        return lettera == 'O' || lettera == 'o';
                case 4:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 5:
                    if (lettera == 'W' || lettera == 'w')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("W");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'W' || lettera == 'w';
                case 6:
                    resetAll();
                    if (lettera == 'B' || lettera == 'b')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("B");
                        valoreColonna = 1;
                    }
                    return lettera == 'B' || lettera == 'b';
                case 7:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 8:
                    if (lettera == 'B' || lettera == 'b')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("B");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'B' || lettera == 'b';
                case 9:
                    resetAll();
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("I");
                        valoreColonna = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 10:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("O");
                        evidenziato = true;
                        valoreRiga = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 11:
                    if (lettera == 'W' || lettera == 'w')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("W");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'W' || lettera == 'w';
                case 12:
                    resetAll();
                    if (lettera == 'L' || lettera == 'l')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("L");
                        valoreColonna = 11;
                    }
                    return lettera == 'L' || lettera == 'l';
                case 13:
                    if (lettera == 'C' || lettera == 'c')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("C");
                        evidenziato = true;
                        valoreRiga = 2;
                    }
                    return lettera == 'C' || lettera == 'c';
                case 14:
                    if (lettera == 'N' || lettera == 'n')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("N");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'N' || lettera == 'n';
                case 15:
                    resetAll();
                    if (lettera == 'F' || lettera == 'f')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("F");
                        valoreColonna = 5;
                    }
                    return lettera == 'F' || lettera == 'f';
                case 16:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 17:
                    if (lettera == 'N' || lettera == 'n')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("N");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'N' || lettera == 'n';
                case 18:
                    resetAll();
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("A");
                        valoreColonna = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 19:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 20:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("A");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'A' || lettera == 'a';
                case 21:
                    resetAll();
                    if (lettera == 'B' || lettera == 'b')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("B");
                        valoreColonna = 1;
                    }
                    return lettera == 'B' || lettera == 'b';
                case 22:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("O");
                        evidenziato = true;
                        valoreRiga = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 23:
                    if (lettera == 'P' || lettera == 'p')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("P");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'P' || lettera == 'p';
                case 24:
                    resetAll();
                    if (lettera == 'B' || lettera == 'b')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("B");
                        valoreColonna = 1;
                    }
                    return lettera == 'B' || lettera == 'b';
                case 25:
                    if (lettera == 'C' || lettera == 'c')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("C");
                        evidenziato = true;
                        valoreRiga = 2;
                    }
                    return lettera == 'C' || lettera == 'c';
                case 26:
                    if (lettera == 'D' || lettera == 'd')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("D");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'D' || lettera == 'd';
                case 27:
                    resetAll();
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("R");
                        valoreColonna = 17;
                    }
                    return lettera == 'R' || lettera == 'r';
                case 28:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 29:
                    if (lettera == 'Z' || lettera == 'z')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("Z");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                    }
                    return lettera == 'Z' || lettera == 'z';
                case 30:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        valoreColonna = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 31:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 32:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("O");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("bobilfabbro" + indiceIndovinato);
                        ConstVar.abilitatoreMessaggi = true;//riparte dal 66 e al 67 si blocca
                        resetAll();
                        ConstVar.contatoreMessaggi++;
                    }
                    return lettera == 'O' || lettera == 'o';





                default:
                        return false;
                  break;
            }              
                  
                  
        }

        private bool letteraEGiusta2(char lettera, int indiceLet)//manda indietro true se è giusta e false se è sbagliata
        {
            switch (indiceLet)
            {
                case 0:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        indiceIndovinato = 0;
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("I");
                        valoreColonna = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 1:
                    if (lettera == 'D' || lettera == 'd')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("D");
                        evidenziato = true;
                        valoreRiga = 3;
                    }
                    return lettera == 'D' || lettera == 'd';
                    break;
                case 2:
                    if (lettera == 'L' || lettera == 'l')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("L");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'L' || lettera == 'l';
                    break;
                case 3:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        valoreColonna = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 4:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 5:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("O");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'O' || lettera == 'o';
                case 6:
                    resetAll();
                    if (lettera == 'S' || lettera == 's')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("S");
                        valoreColonna = 18;
                    }
                    return lettera == 'S' || lettera == 's';
                case 7:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 8:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("A");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'A' || lettera == 'a';
                case 9:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        valoreColonna = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 10:
                    if (lettera == 'D' || lettera == 'd')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("D");
                        evidenziato = true;
                        valoreRiga = 3;
                    }
                    return lettera == 'D' || lettera == 'd';
                case 11:
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("R");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'R' || lettera == 'r';
                case 12:
                    resetAll();
                    if (lettera == 'V' || lettera == 'v')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("V");
                        valoreColonna = 21;
                    }
                    return lettera == 'V' || lettera == 'v';
                case 13:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 14:
                    if (lettera == 'V' || lettera == 'v')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("V");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'V' || lettera == 'v';
                case 15:
                    resetAll();
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("I");
                        valoreColonna = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 16:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 17:
                    if (lettera == 'Q' || lettera == 'q')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("Q");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'Q' || lettera == 'q';
                case 18:
                    resetAll();
                    if (lettera == 'G' || lettera == 'g')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("G");
                        valoreColonna = 6;
                    }
                    return lettera == 'G' || lettera == 'g';
                case 19:
                    if (lettera == 'D' || lettera == 'd')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("D");
                        evidenziato = true;
                        valoreRiga = 3;
                    }
                    return lettera == 'D' || lettera == 'd';
                    break;
                case 20:
                    if (lettera == 'J' || lettera == 'j')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("J");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'J' || lettera == 'j';
                case 21:
                    resetAll();
                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("E");
                        valoreColonna = 4;
                    }
                    return lettera == 'E' || lettera == 'e';
                case 22:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 23:
                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("E");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'E' || lettera == 'e';
                case 24:
                    resetAll();
                    if (lettera == 'N' || lettera == 'n')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("N");
                        valoreColonna = 13;
                    }
                    return lettera == 'N' || lettera == 'n';
                case 25:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 26:
                    if (lettera == 'V' || lettera == 'v')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("V");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'V' || lettera == 'v';
                case 27:
                    resetAll();
                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("E");
                        valoreColonna = 4;
                    }
                    return lettera == 'E' || lettera == 'e';
                case 28:
                    if (lettera == 'D' || lettera == 'd')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("D");
                        evidenziato = true;
                        valoreRiga = 3;
                    }
                    return lettera == 'D' || lettera == 'd';
                case 29:
                    if (lettera == 'H' || lettera == 'h')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("H");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'H' || lettera == 'h';
                case 30:
                    resetAll();
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("R");
                        valoreColonna = 17;
                    }
                    return lettera == 'R' || lettera == 'r';
                case 31:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 32:
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("R");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                    }
                    return lettera == 'R' || lettera == 'r';
                case 33:
                    resetAll();
                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("E");
                        valoreColonna = 4;
                    }
                    return lettera == 'E' || lettera == 'e';
                case 34:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 35:
                    if (lettera == 'M' || lettera == 'm')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("M");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("iosovigenere" + indiceIndovinato);
                        ConstVar.abilitatoreMessaggi = true;//riparte dal 67 e al 68 si blocca
                        resetAll();
                        ConstVar.contatoreMessaggi++;
                    }
                    return lettera == 'M' || lettera == 'm';





                default:
                    return false;
                    break;
            }
        }


        private bool letteraEGiusta3(char lettera, int indiceLet)//manda indietro true se è giusta e false se è sbagliata
        {
            switch (indiceLet)//decifra quindi non è esattamente come gli scorsi
            {               
                case 1:
                    resetAll();
                    if (lettera == 'M' || lettera == 'm')
                    {
                        indiceIndovinato = 0;
                        fogliettoLetteraChiave = _content.Load<Texture2D>("M");                        
                        valoreRiga = 12;
                    }
                    return lettera == 'M' || lettera == 'm';
                    break;
                case 2:
                    if (lettera == 'B' || lettera == 'b')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("B");
                        valoreColonna = 15;//P
                        evidenziato = true;
                        
                        
                        
                    }
                    return lettera == 'B' || lettera == 'b';
                    break;
                case 3:
                    
                    if (lettera == 'P' || lettera == 'p')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("P");                        
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'P' || lettera == 'p';
                case 4:
                    resetAll();
                    if (lettera == 'A' || lettera == 'a')
                    {                        
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                    break;
                case 5:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("O");
                        valoreColonna = 14;//O
                        evidenziato = true;



                    }
                    return lettera == 'O' || lettera == 'o';
                    break;
                case 6:

                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'O' || lettera == 'o';
                case 7:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {                        
                        fogliettoLetteraChiave = _content.Load<Texture2D>("O");
                        valoreRiga = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                    break;
                case 8:
                    if (lettera == 'G' || lettera == 'g')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("G");
                        valoreColonna = 18;//S
                        evidenziato = true;



                    }
                    return lettera == 'G' || lettera == 'g';
                    break;
                case 9:

                    if (lettera == 'S' || lettera == 's')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("S");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'S' || lettera == 's';
                case 10:
                    resetAll();
                    if (lettera == 'M' || lettera == 'm')
                    {   
                        fogliettoLetteraChiave = _content.Load<Texture2D>("M");
                        valoreRiga = 12;
                    }
                    return lettera == 'M' || lettera == 'm';
                    break;
                case 11:
                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("E");
                        valoreColonna = 18;//S
                        evidenziato = true;



                    }
                    return lettera == 'E' || lettera == 'e';
                    break;
                case 12:

                    if (lettera == 'S' || lettera == 's')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("S");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'S' || lettera == 's';
                case 13:
                    resetAll();
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                    break;
                case 14:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("O");
                        valoreColonna = 14;//O
                        evidenziato = true;



                    }
                    return lettera == 'O' || lettera == 'o';
                    break;
                case 15:

                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'O' || lettera == 'o';
                case 16:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("O");
                        valoreRiga = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                    break;
                case 17:
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("R");
                        valoreColonna = 3;//D
                        evidenziato = true;



                    }
                    return lettera == 'R' || lettera == 'r';
                    break;
                case 18:

                    if (lettera == 'D' || lettera == 'd')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("D");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'D' || lettera == 'd';
                case 19:
                    resetAll();
                    if (lettera == 'M' || lettera == 'm')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("M");
                        valoreRiga = 12;
                    }
                    return lettera == 'M' || lettera == 'm';
                    break;
                case 20:
                    if (lettera == 'Q' || lettera == 'q')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("Q");
                        valoreColonna = 4;//E
                        evidenziato = true;

                    }
                    return lettera == 'Q' || lettera == 'q';
                    break;
                case 21:

                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("e");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'E' || lettera == 'e';
                case 22:
                    resetAll();
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                    break;
                case 23:
                    if (lettera == 'C' || lettera == 'c')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("C");
                        valoreColonna = 2;//P
                        evidenziato = true;



                    }
                    return lettera == 'C' || lettera == 'c';
                    break;
                case 24:

                    if (lettera == 'C' || lettera == 'c')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("C");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'C' || lettera == 'c';
                case 25:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("O");
                        valoreRiga = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                    break;
                case 26:
                    if (lettera == 'W' || lettera == 'w')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("W");
                        valoreColonna = 8;//I
                        evidenziato = true;



                    }
                    return lettera == 'W' || lettera == 'w';
                    break;
                case 27:

                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("I");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'I' || lettera == 'i';
                case 28:
                    resetAll();
                    if (lettera == 'M' || lettera == 'm')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("M");
                        valoreRiga = 12;
                    }
                    return lettera == 'M' || lettera == 'm';
                    break;
                case 29:
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("R");
                        valoreColonna = 5;//F
                        evidenziato = true;



                    }
                    return lettera == 'R' || lettera == 'r';
                    break;
                case 30:

                    if (lettera == 'F' || lettera == 'f')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("F");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'F' || lettera == 'f';
                case 31:
                    resetAll();
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                    break;
                case 32:
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("R");
                        valoreColonna = 17;//R
                        evidenziato = true;



                    }
                    return lettera == 'R' || lettera == 'r';
                    break;
                case 33:

                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("R");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'R' || lettera == 'r';
                case 34:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("O");
                        valoreRiga = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                    break;
                case 35:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("O");
                        valoreColonna = 0;//A
                        evidenziato = true;



                    }
                    return lettera == 'O' || lettera == 'o';
                    break;
                case 36:

                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("A");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'A' || lettera == 'a';
                case 37:
                    resetAll();
                    if (lettera == 'M' || lettera == 'm')
                    {
                        
                        fogliettoLetteraChiave = _content.Load<Texture2D>("M");
                        valoreRiga = 12;
                    }
                    return lettera == 'M' || lettera == 'm';
                    break;
                case 38:
                    if (lettera == 'D' || lettera == 'd')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("D");
                        valoreColonna = 17;//R
                        evidenziato = true;



                    }
                    return lettera == 'D' || lettera == 'd';
                    break;
                case 39:

                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("R");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                    }
                    return lettera == 'R' || lettera == 'r';
                case 40:
                    resetAll();
                    if (lettera == 'A' || lettera == 'a')
                    {
                        
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                    break;
                case 41:
                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("E");
                        valoreColonna = 4;//E
                        evidenziato = true;



                    }
                    return lettera == 'E' || lettera == 'e';
                    break;
                case 42:

                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("E");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("possodecifrare" + indiceIndovinato);
                        ConstVar.abilitatoreMessaggi = true;
                        resetAll();
                        ConstVar.contatoreMessaggi++;//76

                    }
                    return lettera == 'E' || lettera == 'e';  
                default:
                    return false;
                    break;
            }
        }


        private bool letteraEGiusta4(char lettera, int indiceLet)//esattamente come letteraegiusta2
        {
            switch (indiceLet)
            {
                case 0:
                    if (lettera == 'C' || lettera == 'c')
                    {
                        indiceIndovinato = 0;
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("C");
                        valoreColonna = 2;
                    }
                    return lettera == 'C' || lettera == 'c';
                case 1:
                    if (lettera == 'T' || lettera == 't')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("T");
                        evidenziato = true;
                        valoreRiga = 19;
                    }
                    return lettera == 'T' || lettera == 't';
                    break;
                case 2:
                    if (lettera == 'V' || lettera == 'v')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("V");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'V' || lettera == 'v';
                    break;
                case 3:
                    resetAll();
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("I");
                        valoreColonna = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 4:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 5:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("I");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'I' || lettera == 'i';
                case 6:
                    resetAll();
                    if (lettera == 'F' || lettera == 'f')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("F");
                        valoreColonna = 5;
                    }
                    return lettera == 'F' || lettera == 'f';
                case 7:
                    if (lettera == 'G' || lettera == 'g')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("G");
                        evidenziato = true;
                        valoreRiga = 6;
                    }
                    return lettera == 'G' || lettera == 'g';
                case 8:
                    if (lettera == 'L' || lettera == 'l')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("L");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'L' || lettera == 'l';
                case 9:
                    resetAll();
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("R");
                        valoreColonna = 17;
                    }
                    return lettera == 'R' || lettera == 'r';
                case 10:
                    if (lettera == 'L' || lettera == 'l')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("L");
                        evidenziato = true;
                        valoreRiga = 11;
                    }
                    return lettera == 'L' || lettera == 'l';
                case 11:
                    if (lettera == 'C' || lettera == 'c')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("C");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'C' || lettera == 'c';
                case 12:
                    resetAll();
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("A");
                        valoreColonna = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 13:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 14:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("I");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'I' || lettera == 'i';
                case 15:
                    resetAll();
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("R");
                        valoreColonna = 17;
                    }
                    return lettera == 'R' || lettera == 'r';
                case 16:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("O");
                        evidenziato = true;
                        valoreRiga = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 17:
                    if (lettera == 'F' || lettera == 'f')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("F");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'F' || lettera == 'f';
                case 18:
                    resetAll();
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("I");
                        valoreColonna = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 19:
                    if (lettera == 'G' || lettera == 'g')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("G");
                        evidenziato = true;
                        valoreRiga = 6;
                    }
                    return lettera == 'G' || lettera == 'g';
                    break;
                case 20:
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("O");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'O' || lettera == 'o';
                case 21:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        valoreColonna = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 22:
                    if (lettera == 'L' || lettera == 'l')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("L");
                        evidenziato = true;
                        valoreRiga = 11;
                    }
                    return lettera == 'L' || lettera == 'l';
                case 23:
                    if (lettera == 'Z' || lettera == 'z')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("Z");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'Z' || lettera == 'z';
                case 24:
                    resetAll();
                    if (lettera == 'S' || lettera == 's')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("S");
                        valoreColonna = 18;
                    }
                    return lettera == 'S' || lettera == 's';
                case 25:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 26:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("A");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'A' || lettera == 'a';
                case 27:
                    resetAll();
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("I");
                        valoreColonna = 8;
                    }
                    return lettera == 'I' || lettera == 'i';
                case 28:
                    if (lettera == 'A' || lettera == 'a')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("A");
                        evidenziato = true;
                        valoreRiga = 0;
                    }
                    return lettera == 'A' || lettera == 'a';
                case 29:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("I");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'I' || lettera == 'i';
                case 30:
                    resetAll();
                    if (lettera == 'C' || lettera == 'c')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("C");
                        valoreColonna = 2;
                    }
                    return lettera == 'C' || lettera == 'c';
                case 31:
                    if (lettera == 'L' || lettera == 'l')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("L");
                        evidenziato = true;
                        valoreRiga = 11;
                    }
                    return lettera == 'L' || lettera == 'l';
                case 32:
                    if (lettera == 'N' || lettera == 'n')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("N");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'N' || lettera == 'n';
                case 33:
                    resetAll();
                    if (lettera == 'U' || lettera == 'u')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("U");
                        valoreColonna = 20;
                    }
                    return lettera == 'U' || lettera == 'u';
                case 34:
                    if (lettera == 'B' || lettera == 'b')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("B");
                        evidenziato = true;
                        valoreRiga = 1;
                    }
                    return lettera == 'B' || lettera == 'b';
                case 35:
                    if (lettera == 'V' || lettera == 'v')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("V");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'V' || lettera == 'v';
                case 36:
                    resetAll();
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("R");
                        valoreColonna = 17;
                    }
                    return lettera == 'R' || lettera == 'r';
                case 37:
                    if (lettera == 'E' || lettera == 'e')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("E");
                        evidenziato = true;
                        valoreRiga = 4;
                    }
                    return lettera == 'E' || lettera == 'e';
                case 38:
                    if (lettera == 'V' || lettera == 'v')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("V");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'V' || lettera == 'v';
                case 39:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        valoreColonna = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 40:
                    if (lettera == 'R' || lettera == 'r')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("R");
                        evidenziato = true;
                        valoreRiga = 17;
                    }
                    return lettera == 'R' || lettera == 'r';
                case 41:
                    if (lettera == 'F' || lettera == 'f')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("F");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                    }
                    return lettera == 'F' || lettera == 'f';
                case 42:
                    resetAll();
                    if (lettera == 'O' || lettera == 'o')
                    {
                        fogliettoLetteraChiaro = _content.Load<Texture2D>("O");
                        valoreColonna = 14;
                    }
                    return lettera == 'O' || lettera == 'o';
                case 43:
                    if (lettera == 'I' || lettera == 'i')
                    {
                        fogliettoLetteraChiave = _content.Load<Texture2D>("I");
                        evidenziato = true;
                        valoreRiga = 8;
                    }
                    return lettera == 'I' || lettera == 'i';                
                case 44:
                    if (lettera == 'W' || lettera == 'w')
                    {
                        fogliettoLetteraCifrata = _content.Load<Texture2D>("W");
                        indiceIndovinato++;
                        fogliettoFrase = _content.Load<Texture2D>("cifrariosicuroo" + indiceIndovinato);
                        ConstVar.abilitatoreMessaggi = true;
                        resetAll();
                        ConstVar.contatoreMessaggi++;
                    }
                    return lettera == 'W' || lettera == 'w';





                default:
                    return false;
                    break;
            }
        }

        public void resetAll()
        {
            fogliettoLetteraChiaro = _content.Load<Texture2D>("bianco");
            fogliettoLetteraChiave = _content.Load<Texture2D>("bianco");
            fogliettoLetteraCifrata = _content.Load<Texture2D>("bianco");
            valoreColonna = 0;
            valoreRiga = 0;
            evidenziato = false;
        }
    }
}
