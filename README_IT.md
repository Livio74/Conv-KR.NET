Progetto Conv-KR.NET
====================

E' un progetto di conversione da VB6 in C# di un progetto molto vecchio denominato Kripter (o KR) che serve a criptare con algoritmo di crittografia con Or Esclusivo ogni singolo file. Poter criptare ogni singolo file poteva essere utile con utility di backup che fanno la differenza fra i file così se decripto la cartella modifico un file e cripto di nuovo quando faccio il backup viene aggiornato solo quel file modificato.

Il programma , essendo vecchio ha diversi inesattezze e modalità di programmazione che ricalcano la mia poca esperienza del tempo. Ad esempio l'utilizzo non clean code del prefisso str per tutte le stringhe.

La soluzione , denominata KR.NET , è una semplice conversione , probabilmente in futuro andrò a creare un nuovo repository dove farò evolvere il programma per migliorarlo dal punto di vista del clean code (eliminando per esempio i prefissi str) e aggiungendo nuove funzionalità per evitare per esempio di utilizzare la crittografia simmetrica che è facilmente decriptabile.

Nella cartella [Kripter](Kripter) c'è il progetto Kripter VB6 mentre le altre cartelle sono il progetto o meglio la soluzione convertita in C# dove ogni cartella è un progetto.
Questo progetto aveva il vantaggio di essere stato originariamente modularizzato utilizzando i moduli di VB6 quindi la successiva conversione è stata più facile.

Creazione del [progetto](KR.NET/KRTest) di unit test
----------------------------------------------------

Vista la modularità del sorgente VB6 è stato facile creare tramite [Microsoft Visual Studio Test](https://docs.microsoft.com/it-it/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022) un set di test che potesse verificare la correttezza di ogni modulo vb6 convertito.
Questo tra l'altro ha favorito la correzione di bug così che la creazione delle interfacce utilizzando le funzioni dei moduli si è limitata alla creazione dei controlli e la conversione del solo codice relativo agli eventi e alle funzioni/subroutine interne alle form.

Essendo stata una conversione manuale sono stato positivamente soddisfatto di averlo fatto prima di aggiungere le form invece che dopo. Magari se si ha un tool affidabile (in futuro potrei provare [VB Migration Partner](https://www.vbmigration.com/)) aggiungerò dei test solamente dopo aver verificato le interfacce e scoperto dei bug.

Considerazioni di dettaglio sulla conversione
---------------------------------------------

La conversione è stata fatta in modo puntuale tuttavia alcune parti le ho dovute o volute mettere a posto in particolare di seguito il dettaglio.

Il progetto originale in certi casi utilizzava le winapi ma nel 
[commit](../../commits/0799056a178e9a39a1c5786be6e9b37431e812b1) e nel [commit](../../commit/55a440e896551ccb431d450d65e6263d3a7105a9) le ho sostituite con l'equivalente del framework .Net.

In generale ho corretto alcuni bug che avevo trovato senza però stravolgere la funzione corretta come ad esempio nel [commit](commit/6919f24654d41988616fe7c44ca119fb7fd5b189).

Verso la fine del progetto tutti i moduli VB6 convertiti li ho dovuti necessariamente portare in un progetto di libreria poichè i vari nel progetto VB6 ci sono più VBP che li usano. Questo quindi è stato l'unico modo per non doverli duplicare. I commit in questione iniziano tutti per **Conversione di progetto KR Livio : Aggiunta Libreria classi con moduli**. 
