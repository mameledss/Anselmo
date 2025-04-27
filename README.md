# 🐰Anselmo's Happy Easter
## 📖Descrizione Progetto
Anselmo's Happy Easter è una simulazione in cui potrai vestire i panni di Anselmo, un coniglio incaricato di nascondere delle uova composte da due metà colorate nel suo prato. Ma attenzione: non puoi lasciarle ovunque ti pare! Ogni coppia di uova adiacenti deve avere almeno il colore di una metà in comune!
## ✨Features principali
- Generazione casuale di uova con coppie di colori
- Visualizzazione grafica delle uova nella fabbrica e nel prato
- Animazione del processo di backtracking
- Interfaccia ridimensionabile con layout adattivo
## ➕Features Aggiuntive
- Log delle operazioni eseguite con scrittura in file di testo "log.txt"
- Velocità delle animazioni regolabile dalla sezione "Settings" del menu
- Musica di sottofondo regolabile dalla sezione "Music" del menu
## 🛠️Descrizione funzionalità
Appena aperta l'applicazione, l'utente visualizza il menu principale. 

Premendo "Play" il form del menu si chiude e se ne aprono due nuovi: il form della simulazione e il form di log delle varie azioni. 

Il form di simulazione è "suddiviso" in due parti: la parte superiore rappresenta il nastro trasportatore della fabbrica, mentre la parte inferiore rappresenta il prato dove Anselmo nasconde le uova.
Nella parte superiore del form è presente il menu delle impostazioni da cui è possibile regolare la velocità delle animazioni, la musica di sottofondo e di visualizzare l'autore della simulazione.
Nella parte inferiore è presente un "NumericUpDown" che permette di selezionare il numero di uova da generare attraverso il pulsante "Generate Eggs" situato a fianco (le uova vengono generate cercando di bilanciare i colori disponibili in modo uniforme, con eventuali metà extra distribuite tra i primi colori, e poi accoppiate casualmente a due a due).
Premendo il pulsante "Hide" inizierà il processo di nascondere le uova tramite algoritmo di backtracking, mentre con "Stop" sarà possibile metterlo in pausa.
Se non è possibile l'inserimento di tutte le uova nel prato, verrà visualizzata la combinazione migliore possibile (es. solo 3 uova nascoste su 4 generate).

Il form di log permette di visualizzare tutte le operazioni in corso ed è possibile "svuotarlo" premendo "Clean Log"
## 🧩Classi
- Uovo: rappresenta un uovo con due colori (metà superiore e inferiore)
- UovoControl: permette di visualizzare le uova a livello grafico
- ContenitoreUova: permette di visualizzare gruppi di uova
- Form1: form principale che gestisce l'interfaccia utente e  l'algoritmo di backtracking
- FormMenu: menu dell'applicazione, che permette di inizializzarla
- FormLogger: form per mostrare il log delle operazioni
## ✍️Autore
- [@mameledss](https://www.github.com/mameledss)
## 📄Readme
- Readme realizzato con https://readme.so
