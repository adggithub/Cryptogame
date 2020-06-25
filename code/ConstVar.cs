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
    public class ConstVar
    {
        public static int zoneStoryCounter = 0; // progresso della storia: 0=inzio assoluto fino a completamento primo cesare//inizio 0
        public static int contatoreMessaggi=0;//0 all'inizio//0-0;1-26;2-39;3-51;

        public static Vector2 displayDim;//1280x704
        public static Rectangle displayRect;
        public static SpriteBatch sb;
        public static SpriteFont fontCommenti;

        public static Texture2D backgroundMainText;//qui texture sfondo schermata principale
        public static Texture2D characterText;//qui texture omino che verrà sovrascritta dopo che faremo scegliere quale delle 6 possibilità usare di omino
        public static Texture2D mainHouseText;   // texture per casa
        public static Texture2D houseText;   // texture per casa
        public static Texture2D carpenter;   // falegname

        public static float constMainBackX=2.5f;//a quanto un quadratino 16x16 pixel corrisponde qui
        public static float constMainBackY=2.2f;

        public static WalkingSpritebatch walkingSprite;
        public static Vector2 dimFrameWalking=new Vector2(32, 48);//dimensione immagine man 96,192 che diviso per 3 colonne e 4 righe

        public static float timerWalking = 0;//valore che cambia
        
        public static float TIMER_WALK = 0.15f;//valore di reset anche 100f

        public static float TIMER_ROTATE_MENU = 0.2f;//valore di reset anche 100f

        public static float timer_rotate_menu = 0;

        public static Vector2 startPositionCharacter = new Vector2(300, 320);       // vale solo per quando inizia gioco
        public static Vector2 startPositionCharacterMainHouse = new Vector2(680, 470);//valori per ora a caso
        public static Vector2 startPositionExitCharacterMainHouse = new Vector2(440, 250);//valori per ora a caso
        public static Vector2 startPositionExitCharacterCarpenterHouse = new Vector2(840, 500);//valori per ora a caso
        public static Vector2 startPositionCharacterHouse = new Vector2(590, 540); // qui sono dentro la casa e davanti alla porta di casa (sul tappeto)       

        public static Vector2 dimCharacter = new Vector2(48, 72);

        public static int walkingCols=3;//gli omini hanno 3 colonne e 4 righe 
        public static int walkingFrame=12;//totale 12 frame

        public static bool isMouseVisibleM;

        public static Vector2 dimButtons;
        public static float scaleTextMenu = 2;//scala scritta menu
        public static float scaleTextCommenti = 1.5f;//scala scritta commenti ///era 1
        public static Texture2D menuBackgroundText;
        public static Texture2D currentBackgroundText;

        public static Texture2D logo; 
        public static Rectangle logoMenuRect;

        public static Vector2 dimCorniceCharacterMenu = new Vector2(200, 300);

        public static List<Texture2D> listaTexture= new List<Texture2D>();

        public static StandingRotateSpritebatch standingMan1;
        public static StandingRotateSpritebatch standingMan2;
        public static StandingRotateSpritebatch standingMan3;
        public static StandingRotateSpritebatch standingWoman1;
        public static StandingRotateSpritebatch standingWoman2;
        public static StandingRotateSpritebatch standingWoman3;

        public static Character MainCharacter;

        public static bool abilitaTastiera = false;//abilita tastiera inizializzata a false perchè nei menu non serve

        public static int speedMoveCharacter= 10;//quanti pixel si muove

        public static Vector2 DimTileInPixel = new Vector2(40, 35.2f);

        public static int[,] collisioniMappaCorrente;//posso usare questo per le collisioni 

        public static int[,] ostacoliArray = new int[,]{{0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0},
                                                        {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                                                        {0,0,1,1,0,0,0,0,0,0,4,5,7,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                                                        {0,0,1,0,0,0,0,0,0,4,3,1,6,7,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                                                        {0,0,1,0,0,0,0,0,0,3,1,1,1,6,7,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
                                                        {0,0,1,0,0,0,0,0,0,1,1,1,1,1,8,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0},
                                                        {0,0,1,0,0,0,0,9,1,1,1,1,1,1,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                                                        {0,1,1,0,0,0,0,9,1,1,1,61,1,1,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},  // 15 è la porta per cambiare stato
                                                        {0,1,0,0,0,0,0,0,0,1,1,0,1,1,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                                        {0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                                                        {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1},
                                                        {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1},
                                                        {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1},
                                                        {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1},
                                                        {0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,62,1,1,1,0,0,0,0,0,0,1},
                                                        {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,50,1,1,0,0,0,0,0,0,0,1},
                                                        {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,50,50,50,50,0,0,0,0,0,0,0,1},
                                                        {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,50,50,50,0,0,0,0,0,0,0,0,1},
                                                        {0,0,0,1,0,0,0,0,1,1,1,1,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1},
                                                        {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};

        public static int[,] internoMainHouseCollision = new int[,]{{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,1,13,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,1,13,12,0,101,0,0,14,14,0,1,8,0,1,8,1,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,1,8,10,11,0,19,1,18,0,0,1,8,0,1,8,1,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,1,8,1,8,0,1,1,1,0,0,14,12,0,14,12,1,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,1,8,1,8,0,14,17,14,0,0,0,0,0,100,0,1,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,1,8,0,0,0,0,0,0,0,0,0,0,0,0,15,1,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,1,6,7,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,1,6,7,0,102,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,1,6,7,10,10,1,1,60,1,1,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};


        public static int[,] internoCarpenterHouse = new int[,]{{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,1,1,0,0,0,1,1,1,1,0,0,1,0,0,0,1,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,1,1,0,0,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,0,0,0,1,1,1,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,1,0,0,0,0,0,64,64,64,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0},
                                                                 {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
        public static int[,] MatrixCode2 = new int[,] {{0,0,0},//centro
                                                       {0,1,0},
                                                       {0,0,0}};

        public static int[,] MatrixCode3 = new int[,] {{0,1,1},//angolo basso destra
                                                       {1,1,1},
                                                       {1,1,1}};

        public static int[,] MatrixCode4 = new int[,] {{0,0,0},//angolo basso destra
                                                       {0,0,0},
                                                       {0,0,1}};

        public static int[,] MatrixCode5 = new int[,] {{0,0,0},//angolo basso destra
                                                       {1,1,1},
                                                       {1,1,1}};

        public static int[,] MatrixCode6 = new int[,] {{1,1,0},//angolo basso destra
                                                       {1,1,1},
                                                       {1,1,1}};

        public static int[,] MatrixCode7 = new int[,] {{0,0,0},//angolo basso destra
                                                       {0,0,0},
                                                       {1,0,0}};

        public static int[,] MatrixCode8 = new int[,] {{1,0,0},//angolo basso destra
                                                       {1,0,0},
                                                       {1,0,0}};

        public static int[,] MatrixCode9 = new int[,] {{0,0,1},//angolo basso destra
                                                       {0,0,1},
                                                       {0,0,1}};

        public static int[,] MatrixCode10 = new int[,] {{0,0,0},//angolo basso destra
                                                       {1,1,1},
                                                       {1,1,1}};

        public static int[,] MatrixCode11 = new int[,] {{0,0,0},//angolo basso destra
                                                       {1,0,0},
                                                       {1,0,0}};

        public static int[,] MatrixCode12 = new int[,] {{1,0,0},//angolo basso destra
                                                       {0,0,0},
                                                       {0,0,0}};

        public static int[,] MatrixCode13 = new int[,] {{1,1,1},//angolo basso destra
                                                       {1,1,1},
                                                       {1,1,0}};

        public static int[,] MatrixCode14 = new int[,] {{1,1,1},//angolo basso destra
                                                       {0,0,0},
                                                       {0,0,0}};

        public static int[,] MatrixCode15 = new int[,] {{0,0,1},//angolo basso destra
                                                       {0,1,1},
                                                       {1,1,1}};

        public static int[,] MatrixCode16 = new int[,] {{0,0,0},//angolo basso destra
                                                       {0,0,0},
                                                       {0,0,1}};

        public static int[,] MatrixCode17 = new int[,] {{1,1,1},//angolo basso destra
                                                       {1,1,1},
                                                       {0,0,0}};

        public static int[,] MatrixCode18 = new int[,] {{0,0,1},//angolo basso destra
                                                       {0,0,1},
                                                       {0,0,1}};

        public static int[,] MatrixCode19 = new int[,] {{1,1,0},//angolo basso destra
                                                       {1,1,0},
                                                       {1,1,0}};


        public static Commenti commentoGenerale;

        public static bool changedWindow=false;
        
        public static KeyboardState kState;
        public static bool keyPress = false; 
        
        public static bool abilitatoreMessaggi = false;//sceglie se mettere a schermo i messaggi oppure no e per contare il messaggio da visualizzare
        public static float timerText = 0.5f;//inizializzo a 1 secondi tempo tra premere un tasto e il successivo
        public static float TIMERTEXT = 0.5f;

        //per cifrario di Cesare
        public static SpriteFont font24;//abbiamo gia fontCommenti
        //public static SpriteBatch sb;
       // public static Vector2 displayDim = new Vector2(1024, 681);

        public static float timerRotation = 0;
        public static float TIMER_ROTATION = 0.01f;//valore di reset
        public static float startRotation = 0;

        public static float letterCaesar = -1;
        public static int fogliettiTrovati = 0;
        public static bool wait = false; //serve nel casseCaesarState
        public static bool isFrecciaRossa = false;
    }
}
