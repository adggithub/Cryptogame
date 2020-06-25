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
using Gioco_Esame_Monogame.Controls;


namespace Gioco_Esame_Monogame.States
{
    public class CasseCaesarState : State
    {
        private List<Component> _components;
        Texture2D cassa;
        Texture2D cassaAperta;
        Texture2D parete;
        bool activeCaesar;
        public bool isPressed = false;
        int counterStateOpen;
        int counterCipher;
        Texture2D chiaviMessaggio;
        ContentManager contentLink;
        Texture2D fogliettoChiaveSoluzione;
        bool fogliettoChiaviBool;
        int indiceLettera=0;
        Texture2D notaBianca;
        bool cassa1Open;
        bool cassa2Open;
        bool cassa3Open;
        Texture2D vigenerePiccoloCassa;
       
        
        public CasseCaesarState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
              : base(game, graphicsDevice, content)
        {
            contentLink = content;
            parete = content.Load<Texture2D>("parete");
            cassa = content.Load<Texture2D>("chest");
            cassaAperta = content.Load<Texture2D>("chest_aperta");
            notaBianca = content.Load<Texture2D>("nota_bianca");
            vigenerePiccoloCassa = content.Load<Texture2D>("VigenerePiccoloCassa");
            activeCaesar = false;
            ConstVar.isMouseVisibleM = true;//nel gioco il mouse è invisibile
            ConstVar.abilitaTastiera = true;//mi dice che sono in gioco e abilitare tastiera
            ConstVar.abilitatoreMessaggi = true;
            Menu2State.Caesar.ResetRotation();
            ConstVar.letterCaesar = -1;
            var corniceCharacterTexture = _content.Load<Texture2D>("CornicePersonaggio");
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            counterStateOpen = 0;
            counterCipher = 0;
            fogliettoChiaviBool = false;
            cassa1Open = false;
            cassa2Open = false;
            cassa3Open = false;
            

            var cassa1Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(140, 300),
                //Text = "New Game",
            };

            cassa1Button.Click += primaCassa_Click;

            var cassa2Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(490, 300),
                //Text = "New Game",
            };

            cassa2Button.Click += secondaCassa_Click;

            var cassa3Button = new Button(corniceCharacterTexture, buttonFont)
            {
                Position = new Vector2(840, 300),
                //Text = "New Game",
            };

            cassa3Button.Click += terzaCassa_Click;

            _components = new List<Component>()
            {
                cassa1Button,
                cassa2Button,
                cassa3Button,
            };
        }
        public override void PostUpdate(GameTime gameTime)
        {
            // per ora non ci serve
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kbState;
            kbState = Keyboard.GetState();
            if (ConstVar.contatoreMessaggi >= 39)
            {
                ConstVar.zoneStoryCounter = 2;//si passa alla zona 2 della storia
               // ConstVar.changedWindow = true;// qua non serve
                _game.ChangeState(Menu2State.MainHouseState);//torno in casa
            }
            if (ConstVar.contatoreMessaggi >= 27&&ConstVar.contatoreMessaggi<=38&&ConstVar.wait==false)
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
            if (activeCaesar == false)
            {
                foreach (var component in _components)
                    component.Update(gameTime);
            }
            else
            {

                switch(counterCipher)
                {
                    case 1:
                        if (!cassa1Open)
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
                                        if (ConstVar.letterCaesar != 74)
                                        {
                                            Menu2State.Caesar.ResetRotation();
                                            ConstVar.letterCaesar = -1;
                                            isPressed = false;
                                        }
                                        else
                                        {//se corretta
                                            fogliettoChiaviBool = true;//abilita la draw del fogliettino con la parola CANE
                                            var myChar = kbState.GetPressedKeys();
                                            if (myChar.Length == 0) { return; }
                                            if (letteraEGiusta((char)myChar[0], indiceLettera, counterCipher))
                                            {
                                                indiceLettera++;//ho scritto giusto
                                                
                                                fogliettoChiaveSoluzione = contentLink.Load<Texture2D>("cane" + indiceLettera);
                                                if (indiceLettera == 4)
                                                {
                                                    cassa1Open = true;
                                                    activeCaesar = false;
                                                    ConstVar.letterCaesar = -1;
                                                    Menu2State.Caesar.ResetRotation();
                                                    isPressed = false;
                                                    ConstVar.wait = false;
                                                    counterStateOpen += 1;
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

                        break;
                        

                    case 2:
                        if (!cassa2Open)
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
                                {
                                    if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() < ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                                    {
                                        if (ConstVar.letterCaesar != 76)
                                        {
                                            Menu2State.Caesar.ResetRotation();
                                            ConstVar.letterCaesar = -1;
                                            isPressed = false;
                                        }
                                        else
                                        {//se corretta
                                            fogliettoChiaviBool = true;//abilita la draw del fogliettino con la parola CANE
                                            var myChar = kbState.GetPressedKeys();
                                            if (myChar.Length == 0) { return; }
                                            if (letteraEGiusta((char)myChar[0], indiceLettera, counterCipher))
                                            {
                                                indiceLettera++;//ho scritto giusto

                                                fogliettoChiaveSoluzione = contentLink.Load<Texture2D>("gatto" + indiceLettera);
                                                if (indiceLettera == 5)
                                                {
                                                    cassa2Open = true;
                                                    activeCaesar = false;
                                                    ConstVar.letterCaesar = -1;
                                                    Menu2State.Caesar.ResetRotation();
                                                    isPressed = false;
                                                    ConstVar.wait = false;
                                                    counterStateOpen += 1;
                                                }
                                            }

                                            //ConstVar.contatoreMessaggi += 1;
                                            //ConstVar.abilitatoreMessaggi = true;
                                        }
                                    }
                                }
                            }
                        }
                    break;

                    case 3:
                        if (!cassa3Open)
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
                                {
                                    if (ConstVar.letterCaesar != -1 && Menu2State.Caesar.getRotation() < ConstVar.startRotation - (6.28f / 26) * (ConstVar.letterCaesar - 65))
                                    {
                                        if (ConstVar.letterCaesar != 84)
                                        {
                                            Menu2State.Caesar.ResetRotation();
                                            ConstVar.letterCaesar = -1;
                                            isPressed = false;
                                        }
                                        else
                                        {//se corretta
                                            fogliettoChiaviBool = true;//abilita la draw del fogliettino con la parola CANE
                                            var myChar = kbState.GetPressedKeys();
                                            if (myChar.Length == 0) { return; }
                                            if (letteraEGiusta((char)myChar[0], indiceLettera, counterCipher))
                                            {
                                                indiceLettera++;//ho scritto giusto

                                                fogliettoChiaveSoluzione = contentLink.Load<Texture2D>("cesare" + indiceLettera);
                                                if (indiceLettera == 6)
                                                {

                                                    cassa3Open = true;
                                                    activeCaesar = false;
                                                    ConstVar.letterCaesar = -1;
                                                    Menu2State.Caesar.ResetRotation();
                                                    isPressed = false;
                                                    ConstVar.wait = false;
                                                    counterStateOpen += 1;
                                                }
                                            }

                                            //ConstVar.contatoreMessaggi += 1;
                                            //ConstVar.abilitatoreMessaggi = true;
                                        }

                                    }
                                }
                            }
                        }
                    break;

                    default:
                    break;
                }               
            }
        }
    

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(); 
            if (activeCaesar == false)
            {
                foreach (var component in _components)
                    component.Draw(gameTime, spriteBatch, new Vector2(300, 300));
            }
            spriteBatch.Draw(parete, ConstVar.displayRect, Color.White);

            if (!cassa1Open) { spriteBatch.Draw(cassa, new Rectangle(140, 300, 300, 300), Color.White); }
            else { spriteBatch.Draw(cassaAperta, new Rectangle(140, 250, 300, 350), Color.White); }
            if (!cassa2Open) { spriteBatch.Draw(cassa, new Rectangle(490, 300, 300, 300), Color.White); }
            else { spriteBatch.Draw(cassaAperta, new Rectangle(490, 250, 300, 350), Color.White); }
            if (!cassa3Open) { spriteBatch.Draw(cassa, new Rectangle(840, 300, 300, 300), Color.White); }
            else { spriteBatch.Draw(cassaAperta, new Rectangle(840, 250, 300, 350), Color.White); }
            if (counterStateOpen>=6)//messo a 6 perchè ogni cassa conta 2:uno ad inizio e uno alla fine
            {
                switch(counterCipher)
                {
                    case 1:
                        spriteBatch.Draw(vigenerePiccoloCassa, new Rectangle(190, 250, 200, 200), Color.White);//140+((300-200)/2)
                        break;
                    case 2:
                        spriteBatch.Draw(vigenerePiccoloCassa, new Rectangle(540, 250, 200, 200), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(vigenerePiccoloCassa, new Rectangle(890, 250, 200, 200), Color.White);
                        break;
                    default:
                        break;
                }
            }
            if (ConstVar.abilitatoreMessaggi == true)
            {
                ConstVar.commentoGenerale.Draw(ConstVar.commentoGenerale.textMessaggi(ConstVar.contatoreMessaggi));
            }
            spriteBatch.End();
            if (activeCaesar)
            {
                Menu2State.Caesar.Draw();
                spriteBatch.Begin();
                spriteBatch.Draw(chiaviMessaggio, new Rectangle(900, 300, 300, 300), Color.White);
                if(fogliettoChiaviBool)
                {
                    spriteBatch.Draw(fogliettoChiaveSoluzione, new Rectangle(540, 450, 200, 100), Color.White);
                }
                spriteBatch.End();
               
            }
            //spriteBatch.Begin();
            
            //spriteBatch.Draw(quadretti, new Rectangle(0, 0, (int)ConstVar.displayDim.X, (int)ConstVar.displayDim.Y), Color.White);//per mettere una griglia
            //spriteBatch.End();
        }

        private void primaCassa_Click(object sender, EventArgs e)
        {
            if (!cassa1Open)//Se la cassa è chiusa
            {
                counterStateOpen += 1;
                counterCipher = 1;
                chiaviMessaggio = contentLink.Load<Texture2D>("infoCaesarCassa1");
                fogliettoChiaveSoluzione = contentLink.Load<Texture2D>("cane");
                Menu2State.Caesar.ResetRotation();
                indiceLettera = 0;
                activeCaesar = true;
            }
        }

        private void secondaCassa_Click(object sender, EventArgs e)
        {
            if (!cassa2Open)//Se la cassa è chiusa
            {
                counterStateOpen += 1;
                counterCipher = 2;
                chiaviMessaggio = contentLink.Load<Texture2D>("infoCaesarCassa2");
                fogliettoChiaveSoluzione = contentLink.Load<Texture2D>("gatto");
                Menu2State.Caesar.ResetRotation();
                indiceLettera = 0;
                activeCaesar = true;
            }
        }

        private void terzaCassa_Click(object sender, EventArgs e)
        {
            if (!cassa3Open)//Se la cassa è chiusa
            {
                counterStateOpen += 1;
                counterCipher = 3;
                chiaviMessaggio = contentLink.Load<Texture2D>("infoCaesarCassa3");
                fogliettoChiaveSoluzione = contentLink.Load<Texture2D>("cesare");
                Menu2State.Caesar.ResetRotation();
                indiceLettera = 0;
                activeCaesar = true;
            }
        }

        private bool letteraEGiusta(char lettera, int indiceLet, int indiceCif)//manda indietro true se è giusta e false se è sbagliata
        {
            switch (indiceCif)
            {
                case 1:
                    switch (indiceLet)
                    {
                        case 0:
                            return lettera == 'T';
                            break;
                        case 1:
                            return lettera == 'R';
                            break;
                        case 2:
                            return lettera == 'E';
                            break;
                        case 3:
                            return lettera == 'V';
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
                            return lettera == 'V';
                            break;
                        case 1:
                            return lettera == 'P';
                            break;
                        case 2:
                            return lettera == 'I';
                            break;
                        case 3:
                            return lettera == 'I';
                            break;
                        case 4:
                            return lettera == 'D';
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
                            return lettera == 'J';
                            break;
                        case 1:
                            return lettera == 'L';
                            break;
                        case 2:
                            return lettera == 'Z';
                            break;
                        case 3:
                            return lettera == 'H';
                            break;
                        case 4:
                            return lettera == 'Y';
                            break;
                        case 5:
                            return lettera == 'L';
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
