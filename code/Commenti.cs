using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Gioco_Esame_Monogame
{
    public class Commenti//devo dire dove piazzare il disegno e cosa scriverci dentro
    {
        Point position;
        SpriteFont font;
        Point dimension;
        Texture2D nuvolettaText;
        Texture2D messaggioBaseText;
        public Commenti(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)      //questo è il costruttore
        {
            nuvolettaText = content.Load<Texture2D>("NuvoletteMessaggi1");
            messaggioBaseText = content.Load<Texture2D>("TextBox");
            font = content.Load<SpriteFont>("Fonts/Font");
            position = new Point(0, 0);
            dimension = new Point(200, 200);
        }
        public void Draw(Point pos,Point dim,String Text)
        {
            position = pos;
            dimension = dim;
            ConstVar.sb.Draw(nuvolettaText,new Rectangle(position,dimension),Color.White);
            

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (position.X + (dimension.X / 2)) - ((ConstVar.fontCommenti.MeasureString(Text).X * ConstVar.scaleTextCommenti) / 2);
                var y = (position.Y + (dimension.Y / 2)) - ((ConstVar.fontCommenti.MeasureString(Text).Y * ConstVar.scaleTextCommenti) / 2);

                ConstVar.sb.DrawString(ConstVar.fontCommenti, Text, new Vector2(x, y), Color.Black, 0, new Vector2(0, 0), ConstVar.scaleTextCommenti, new SpriteEffects(), 0);
                
            }
        }

        public void Draw(String Text)
        {
            position = new Point(90, (int)ConstVar.displayDim.Y-120);
            dimension = new Point((int)ConstVar.displayDim.X-180, 100);
            ConstVar.sb.Draw(messaggioBaseText, new Rectangle(position, dimension), Color.White);


            if (!string.IsNullOrEmpty(Text))
            {
                var x = (position.X + (dimension.X / 2)) - ((ConstVar.fontCommenti.MeasureString(Text).X * ConstVar.scaleTextCommenti) / 2);
                var y = (position.Y + (dimension.Y / 2)) - ((ConstVar.fontCommenti.MeasureString(Text).Y * ConstVar.scaleTextCommenti) / 2);

                ConstVar.sb.DrawString(ConstVar.fontCommenti, Text, new Vector2(x, y), Color.Black, 0, new Vector2(0, 0), ConstVar.scaleTextCommenti, new SpriteEffects(), 0);

            }
        }

        public String textMessaggi(int numMessaggi)
        {
            int contatore = numMessaggi;         
            

            switch (contatore)
            {
                case 0:
                    return "Ciao e Benvenuto in CryptoGame!";
                    break;
                case 1:
                    return "Immergiti in questa nuova avventura per imparare le basi della crittografia in modo facile e divertente,\nrisolvendo i compiti che ti assegneranno i personaggi all'interno del mondo!";
                    break;
                case 2:
                    return "Per muoverti, usa le frecce direzionali.\nPer interagire con gli oggetti, usa il tasto Invio.\nPer uscire dal gioco, premi Esc.";
                    break;
                case 3:
                    return "Premi un tasto per iniziare.";
                    break;
                case 4:                    
                    return "Per prima cosa entra nella tua casa qui vicino";                    
                    break;
                case 5://viene skippato
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi += 1;
                    return "";
                    break;
                case 6:
                    return "La porta di casa e' chiusa da un codice.\nBisogna digitarlo per entrare in casa.";
                    break;
                case 7:
                    return "Quello che vedi a sinistra e' il cifrario di Cesare.\nOgni lettera del testo in chiaro e' sostituita nel testo cifrato dalla lettera\nche si trova un certo numero di posizioni dopo nell'alfabeto.";
                    break;
                case 8:
                    return "In alto a destra c'e' il pulsante 'i' delle informazioni.\nPremilo per avere informazioni sul cifrario e su come si utilizza";
                    break;
                case 9:
                    return "Ecco la pagina informativa introduttiva sulla crittografia.\nLeggi con attenzione per proseguire nel gioco.";
                    break;
                case 10:
                    return "";
                    break;
                case 11:
                    return "Ecco adesso la spiegazione del cifrario di Cesare,";
                    break;
                case 12:
                    return "Adesso vediamo come utilizzarlo";
                    break;
                case 13:
                    return "Protagonista: 'Mi ricordo che la mia chiave e' la lettera G e il testo in chiaro e' CIAO.\nAiutami per favore a sbloccare la porta.'";
                    break;
                case 14:
                    return "A destra hai segnate le informazioni in tuo possesso";
                    break;
                case 15:
                    return "La lettera per allineare il cifrario e' la G, percio' premi G";
                    break;
                case 16:                
                    return "Ecco ora il cifrario e' in posizione";
                    break;
                case 17:
                    return "Sul disco esterno troviamo l'alfabeto in chiaro su cui dobbiamo trovare le lettere della parola CIAO,\nmentre sul disco interno abbiamo le lettere dell'alfabeto cifrato corrispondente.";
                    break;
                case 18:
                    return "Per prima cosa la lettera C come vedi corrisponde alla lettera W. Quindi premi W";
                    break;
                case 19:
                    return "Ora tocca alla lettera I e quindi premi la corrispettiva lettera C";
                    break;
                case 20:
                    return "Per la lettera A premi quella corrispondente nel cifrario";
                    break;
                case 21:
                    return "e infine premi la lettera cifrata per la lettera O";
                    break;
                case 22:
                    return "*click*  Ecco la serratura di casa e' stata aperta.\n        Entriamo in casa.";
                    break;



                case 23:
                    return "Ecco la tua casa, accogliente vero?";
                    break;
                case 24:
                    return "Da qui inizierai la tua avventura";
                    break;
                case 25:
                    return "Per prima cosa devi raccogliere l'occorrente dalle casse qui vicino";
                    break;
                case 26:
                    return "Pero' prima devi trovare i 3 foglietti con le chiavi delle casse in giro per la stanza";
                    break;               
                case 27://viene skippato
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi += 1;
                    return "";
                    break;
                case 28:
                    ConstVar.isFrecciaRossa = true;
                    return "Ecco ora che hai i 3 foglietti vai dalle casse in basso a sinistra";
                    break;
                case 29://viene skippato
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi += 1;
                    ConstVar.keyPress = true;
                    return "";
                    break;
                case 30:
                    ConstVar.isFrecciaRossa = false;
                    return "Scegli una delle 3 casse per iniziare ad aprirla";
                    break;
                case 31:
                    return "Si trova solo in una di queste casse";
                    break;
                case 32://viene skippato
                    ConstVar.wait = true;
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.keyPress = true;
                    ConstVar.contatoreMessaggi += 1;
                    return "";
                    break;
                case 33:
                    return "Mi dispiace non si trova in questa cassa";
                    break;
                case 34://viene skippato
                    ConstVar.wait = true;
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.keyPress = true;
                    ConstVar.contatoreMessaggi += 1;
                    return "";
                    break;
                case 35:
                    return "Mi dispiace non si trova in questa cassa";
                    break;
                case 36://viene skippato
                    ConstVar.wait = true;
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.keyPress = true;
                    ConstVar.contatoreMessaggi += 1;
                    return "";
                    break;
                case 37:
                    return "Ecco ora che hai trovato l'occorrente esci dalla casa";
                    break;
                case 38:
                    return "Ora esci di casa e vai a parlare con il fabbro";
                    break;
                case 39://viene skippato

                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi += 1;
                    return "";
                    break;
                case 40:
                    return "Bob: Ehi ciao io sono Bob il fabbro";
                    break;
                case 41:
                    return "Bob: Voglio testare le tue abilita' come crittografo prima di svelarti le mie conoscenze";
                    break;
                case 42:
                    return "Bob: Sicuramente avrai gia' visto come crittare\nun messaggio in chiaro con il cifrario di Cesare";
                    break;
                case 43:
                    return "Bob: Adesso dovrai risolvere dei problemi inversi,\ndovrai decifrare il messaggio che io ho cifrato";
                    break;
                case 44:
                    return "Bob: Chissa' se ne sarai in grado";
                    break;
                case 45:
                    return "NOTA: Per decifrare il messaggio ora bisogna utilizzare le lettere scritte sul foglio per l'anello interno\ne scrivere le lettere trovate sull'anello esterno come soluzione";
                    break;
                case 46:
                    return "Dovrai risolvere 3 cifrari di fila. Buona fortuna! Iniziamo:";
                    break;
                case 47://viene skippato
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi += 1;                    
                    return "";
                    break;
                case 48://viene skippato e fa primo cesare
                    ConstVar.abilitatoreMessaggi = false;                    
                    return "";
                    break;
                case 49://viene skippato secondo cesare
                    ConstVar.abilitatoreMessaggi = false;
                    return "";
                    break;
                case 50://viene skippato terzo cesare
                    ConstVar.abilitatoreMessaggi = false;
                    return "";
                    break;
                case 51://viene skippato terzo cesare
                    ConstVar.abilitatoreMessaggi = true;
                    return "Bob: Abbiamo finito ora guardo i risultati.";
                    break;
                case 52:
                    return "Bob: Bravo hai superato la prova iniziale";
                    break;
                case 53:
                    return "Bob: Hai eseguito ogni passaggio alla perfezione i miei complimenti.\nOra sei pronto per fare un altro passo in avanti";
                    break;
                case 54:
                    return "Bob: Adesso vediamo insieme come funziona il cifrario di Vigenere";
                    break;
                case 55:
                    return "Stai molto attento che e' piu' complicato di quello di Cesare";
                    break;
                case 56://si vede descrizione Vigenere                    
                    return "";
                    break;
                case 57://passaggio a Vigenere                    
                    return "E' piu' difficile a parole, se non hai capito la spiegazione non preoccuparti ora proviamo insieme";
                    break;
                case 58://si vede descrizione Vigenere                    
                    return "Ecco il cifrario di Vigenere. Come vedi e' una tabella in cui ogni riga e' traslata\nin avanti di uno rispetto a quella precedente";
                    break;

                case 59://si vede descrizione Vigenere                    
                    return "Adesso vediamo insieme come funziona";
                    break;

                case 60://si vede descrizione Vigenere                    
                    return "Per prima cosa possiamo vedere la prima riga e la prima colonna evidenziate.\nSaranno quelle i nostri punti di riferimento per la cifratura";
                    break;
                case 61://si vede descrizione Vigenere                    
                    return "Per prima cosa inserisci la lettera da ricercare nella prima riga,\nossia la prima lettera del nostro testo in chiaro che e' la lettera B";
                    break;
                case 62://si vede descrizione Vigenere                    
                    return "Perfetto! Ora inserisci la lettera da ricercare nella prima colonna,\novvero la prima lettera della nostra chiave che e' la lettera C";
                    break;
                case 63://si vede descrizione Vigenere                    
                    return "Ottimo! Le due lettere sono state inserite adesso puoi vedere l'intersezione tra le due evidenziata in blu.\nPremila per inserirla nel testo cifrato";
                    break;
                case 64://si vede descrizione Vigenere                    
                    return "Bene ora sta a te continuare cosi' fino alla fine buona fortuna!";
                    break;
                case 65://skippa e fa fare vigenere da soli             
                    ConstVar.abilitatoreMessaggi = false;
                    return "";
                    break;
                case 66:               
                    return "Bob:Complimenti hai completato il testo cifrato.\nAdesso prova con questo e poi andremo avanti con la spiegazione";
                    break;
                case 67:                   
                    return "Bob:Complimenti hai completato il testo cifrato.\nAdesso prova con questo e poi andremo avanti con la spiegazione";
                    break;
                case 68:
                    ConstVar.abilitatoreMessaggi = false;
                    return "";
                    break;
                case 69:                    
                    return "Ottimo adesso sei pronto per imparare il procedimento inverso.";
                    break;
                case 70:                    
                    return "Dovrai trovare il testo in chiaro conoscendo la chiave e il testo cifrato";
                    break;
                case 71:                    
                    return "Per prima cosa inserisci la lettera della chiave\ne successivamente premi la lettera del testo cifrato corrispondente";
                    break;
                case 72:
                    return "Una volta fatto questo ti appariranno le linee blu. Ti basta seguire la colonna\ne trovare nella prima linea la lettera corrispondente al testo cifrato";
                    break;
                case 73:
                    return "Ora provaci tu ecco a te il testo cifrato e la chiave. Buona fortuna!";
                    break;
                case 74:
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi++;
                    return "";
                    break;
                case 75:                    
                    return "ehi";
                    break;
                case 76:                    
                    return "Bob: Ottimo hai completato anche questo sei strabiliante! Ora posso finalmente insegnarti l'ultimo cifrario.";
                    break;
                case 77:
                    return "Bob: Come avrai notato nel cifrario di Vigenere la chiave era la stessa di 3 o 4 lettere\nripetuta fino ad arrivare alla lunghezza del messaggio.";
                    break;
                case 78:
                    return "Bob: Le chiavi erano CIAO, DAI, MAO ripetute per l'intera lunghezza della frase in chiaro.\nQuesto purtroppo indebolisce la sicurezza della nostra cifratura.";
                case 79:
                    return "Bob: Ora ti faro' conoscere il cifrario di Vernam anche detto Otp, questo cifrario e' sicuro al 100%,\nsempre se la chiave non venga rubata per altre vie.";
                case 80:
                    return "Bob: Ecco a te il pannello illustrativo del cifrario.\nCome vedi la lunghezza della chiave e' lunga come quella del messaggio in chiaro";
                case 81:
                    return "Bob: Non ti preoccupare alla fine il procedimento e' uguale a quello del cifrario di Vigenere,\nsuperata questa prova avrai completato il gioco.";                    
                case 82:
                    return "Bob: Buona fortuna!";                    
                case 83:
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi++;
                    return "";                    
                case 84:
                    return "ehi";                   
                case 85:
                    return "Complimenti hai completato questo viaggio alla scoperta dei cifrari.\nGrazie per aver giocato a questo gioco.";
                case 86:
                    return "Speriamo che ti sia piaciuto e che ti abbia insegnato molto";
                case 87:
                    return "Se vuoi approfondire altri cifrari cerca su internet e ne troverai moltissimi.";
                case 88:
                    return "Grazie di tutto da Matteo Fresta e Matteo Pastorino Ghezzi. Progetto corso Computer Games 2019/2020";
                case 89:
                    ConstVar.abilitatoreMessaggi = false;
                    ConstVar.contatoreMessaggi++;
                    return "";
                case 90:
                    return "";
                case 91:
                    return "";






                case 110:
                    return "Ciao e Benvenuto in CryptoGameMAO!";
                    break;
                case 111:
                    return "Ciao e Benvenuto in CryptoGameAO!";
                    break;
                case 112:
                    return "Ciao e Benvenuto in CryptoGameKEKEKEK!";
                    break;


                default:
                    ConstVar.abilitatoreMessaggi = false;
                    return "ciao MattoMatteo8";
                    break;

            }
           
        }
    }
}
