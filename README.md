# Anselmo

## Testo
Anselmo chiede il tuo aiuto per nascondere le uova nel prato. I suoi amici coniglietti gli inviano le uova tramite uno scivolo (CODA). Lui le prende una alla volta e decide se nasconderle nel prato (CODA).

Le uova sono dipinte con 2 COLORI selezionati casualmente da un set di sei: Verde, Azzurro, Giallo, Arancione, Rosa e Viola. (I due colori estratti possono essere anche uguali) 

La fabbrica dei coniglietti invia ad Anselmo le uova in modo casuale. Tuttavia le crea utilizzando delle metà già create in precedenza e le assembla in modo casuale. Questa operazione continua fino a che tutte le metà sono state utilizzate.
Tuttavia, Anselmo vuole nascondere le uova in un modo divertente, quindi ha stabilito la seguente regola: 

“Due uova nascoste in successione devono avere almeno un COLORE in comune”

Se l’uovo che riceve dallo scivolo non rispetta questa regola lo rimanda indietro. Le uova rimandate alla fabbrica verranno rimesse in partenza per essere nascoste successivamente. 

Quindi, se il primo uovo nascosto è interamente giallo, il secondo deve avere una metà gialla e l'altra metà di un altro colore.
Un esempio di una sequenza valida di colori potrebbe essere:
Giallo-Verde / Azzurro-Verde / Azzurro-Viola / Viola-Viola / Viola-Magenta ….

Ovviamente possono rimanere delle uova alla fabbrica perché nessuna di quelle uova può essere posizionate alla fine della coda. Per questo è previsto il BACKTRACKING che per cui Anselmo se non può nascondere più uova torna indietro sui suoi passi fino a che non può ricominciare a nasconde le sue preziosissime uova.
