using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gioco_Esame_Monogame.States;


namespace Gioco_Esame_Monogame
{
    public partial class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private State _currentState;

        private State _nextState;

        public void ChangeState(State state)//cambia stato 
        {
            _nextState = state;
            IsMouseVisible = ConstVar.isMouseVisibleM;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        
        protected override void Initialize()
        {
            IsMouseVisible = true;
            ConstVar.isMouseVisibleM = true;
            
            ConstVar.displayDim = new Vector2();
            //lo sfondo originale era 512x320 quindi sono 32x20 quadratini 16x16pixel
            ConstVar.constMainBackX =2.5f;//costanti moltiplicate per 512 e 320 per dare 1280 e 704
            ConstVar.constMainBackY =2.2f;
            ConstVar.displayDim.X = graphics.PreferredBackBufferWidth = 1280;//512x2.5
            ConstVar.displayDim.Y = graphics.PreferredBackBufferHeight = 704;//320x2.2
            ConstVar.displayRect = new Rectangle(0,0,(int)ConstVar.displayDim.X,(int)ConstVar.displayDim.Y);//per sfondo 

            ConstVar.dimButtons = new Vector2(330, 80);//dimensioni dei button
            //graphics.IsFullScreen = true; //esplode
            
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ConstVar.sb = spriteBatch;//per le classi
            ConstVar.backgroundMainText = Content.Load<Texture2D>("StartGarden");//schermata principale
            ConstVar.currentBackgroundText = ConstVar.backgroundMainText;
            ConstVar.mainHouseText = Content.Load<Texture2D>("internoMain");  // interno casa principale
            ConstVar.houseText = Content.Load<Texture2D>("house");  // interno casa
            
            ConstVar.characterText = Content.Load<Texture2D>("Man");//omino di default  

            ConstVar.carpenter = Content.Load<Texture2D>("FalegnameMain");

            //if (ConstVar.listaTexture.Count == 0)
            //{
            ConstVar.listaTexture.Add(Content.Load<Texture2D>("Man"));
            ConstVar.listaTexture.Add(Content.Load<Texture2D>("Man2"));
            ConstVar.listaTexture.Add(Content.Load<Texture2D>("Man3"));
            ConstVar.listaTexture.Add(Content.Load<Texture2D>("Woman"));
            ConstVar.listaTexture.Add(Content.Load<Texture2D>("Woman2"));
            ConstVar.listaTexture.Add(Content.Load<Texture2D>("Woman3"));
            //}
            ConstVar.fontCommenti = Content.Load<SpriteFont>("Fonts/FontCommenti");

            //ora texture sfondo menu la foto originale è 2576x1610
            ConstVar.menuBackgroundText = Content.Load<Texture2D>("SfondoConScrittaCryptoGame");//era sfondoMenuHD

            //ConstVar.logo = Content.Load<Texture2D>("logo");
            ConstVar.logoMenuRect = new Rectangle((int)ConstVar.displayDim.X / 2 - (int)(274*1.3f), 100, (int)(548*1.3f), (int)(140*1.3));//è scalata di 1.3, importante tenere nella x il valore - .... che sia la metà di width così centrato
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
            ConstVar.commentoGenerale = new Commenti(this, graphics.GraphicsDevice, Content);
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                
                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            if (ConstVar.abilitaTastiera)
            {
                KeyboardState kbState;
                

                kbState = Keyboard.GetState();
                KeyboardMngnt(kbState, gameTime);
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
