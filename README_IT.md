Progetto Conv-KR.NET
====================

E' un progetto di conversione da VB6 in C# di un progetto molto vecchio denominato Kripter (o KR) che serve a criptare con algoritmo di crittografia con Or Esclusivo ogni singolo file. Poter criptare ogni singolo file poteva essere utile con utility di backup che fanno la differenza fra i file; se decripto la cartella modifico un file e cripto di nuovo quando faccio il backup viene aggiornato solo quel file modificato.

Il programma , essendo vecchio ha diverse inesattezze e modalità di programmazione non ottimali che ricalcano la mia poca esperienza del tempo. Ad esempio l'utilizzo non clean code del prefisso str per tutte le stringhe.

La soluzione , denominata KR.NET , è una semplice conversione , probabilmente in futuro andrò a creare un nuovo repository dove farò evolvere il programma per migliorarlo dal punto di vista del clean code (eliminando per esempio i prefissi str) e aggiungendo nuove funzionalità per evitare per esempio di utilizzare la crittografia simmetrica che è facilmente decriptabile.

Nella cartella [Kripter](Kripter) c'è il progetto Kripter VB6 mentre le altre cartelle sono il progetto o meglio la soluzione convertita in C# dove ogni cartella è un progetto.
Il progetto VB6 aveva il vantaggio di essere stato originariamente modularizzato utilizzando i moduli di VB6 quindi la successiva conversione è stata più facile.

Creazione del [progetto](KR.NET/KRTest) di unit test
----------------------------------------------------

Vista la modularità del sorgente VB6 è stato facile creare tramite [Microsoft Visual Studio Test](https://docs.microsoft.com/it-it/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022) un set di test che potesse verificare la correttezza di ogni modulo vb6 convertito.
Questo tra l'altro ha favorito la correzione di bug così che la creazione delle interfacce utilizzando le funzioni dei moduli si è limitata alla creazione dei controlli e la conversione del solo codice relativo agli eventi e alle funzioni/subroutine interne alle form.

Essendo stata una conversione manuale sono stato positivamente soddisfatto di averlo fatto prima di aggiungere le form invece che dopo. Magari se si ha un tool affidabile (in futuro potrei provare [VB Migration Partner](https://www.vbmigration.com/)) aggiungerò dei test solamente dopo aver verificato le interfacce e scoperto dei bug.

Il lancio dei test necessita innanzi tutto la creazione dell'ambiente di test che è una cartella contenente i progetti stessi in cui in una sotocartella questi sono stati criptati con l'eseguibile VB6. Questo test è il metodo First presente in [UnitTestCore.cs](KR.NET/KRTest/UnitTestCore.cs).

Considerazioni di dettaglio sulla conversione
---------------------------------------------

La conversione è stata fatta in modo puntuale tuttavia alcune parti le ho dovute o volute mettere a posto in particolare di seguito il dettaglio.

Il progetto originale in certi casi utilizzava le winapi ma nel 
[commit](../../commits/0799056a178e9a39a1c5786be6e9b37431e812b1) e nel [commit](../../commit/55a440e896551ccb431d450d65e6263d3a7105a9) le ho sostituite con l'equivalente del framework .Net.

In generale ho corretto alcuni bug che avevo trovato senza però stravolgere la funzione corretta come ad esempio nel [commit](commit/6919f24654d41988616fe7c44ca119fb7fd5b189).

Verso la fine del progetto tutti i moduli VB6 convertiti li ho dovuti necessariamente portare in un progetto di libreria poichè nel progetto VB6 ci sono più VBP che li usano. Questo quindi è stato l'unico modo per non doverli duplicare. I commit in questione iniziano tutti per **Conversione di progetto KR Livio : Aggiunta Libreria classi con moduli**. 

Ho aggiunto alcune parti specifiche per il progetto che non erano incluse nel procedimento di conversione; queste modifiche erano necessarie per poter rendere il progetto KR.NET utilizzabile senza problemi , in particolare per il checkbox di blocco che impedisce di eseguire la criptatura se non è volutamente deselezionato , vedi [commit](https://github.com/Livio74/Conv-KR.NET/commit/39d820a07a5e0dcb66f7c948b561a8f9c122bada).
Ho anche aggiunto un controllo che impedisce di decriptare cartelle importanti come C:\Windows ecc, di seguito i commit:
- [commit](../../commit/a2e7d78e0ac3ca9c018a64eecb1fb1105c9fd631)
- [commit](../../commit/808129a9b2228072f4e99fb187163571b3449264)
- [commit](../../commit/b3ee31f7c9a6e21b99700a56d0fc2a330d789e94)
- [commit](../../commit/a6355845d4b8f5da4443e5307f999db7d671f808)

Istruzione per utilizzare il progetto e lanciare i test
-------------------------------------------------------

Il progetto è stato realizzato tramite Visual Studio 2019 , immagino che dovrebbe funzionare anche con versioni successive.
Per utilizzarlo è quindi sufficiente , dopo aver installato visual studio , scaricare il repository e aprire [la soluzione](KR.NET/KR.NET.sln) contenente tutti i progetti convertiti.
Una volta scaricato è possibile rebuildarlo e lanciare i test. Il lancio del test crea di default una cartella C:\KRTest configurata di default nel [file di configurazione](KR.NET/KRTest/test.runsettings) col parametro denominato "WorkTestRoot" che è quindi possibile cambiare a piacere.
Il lancio dei test necessita prima il singolo lancio del metodo di test il metodo First presente nel test "UnitTestCore" e successivamente il lancio di tutta la suite per evitare che il secondo lancio possa non essere eseguito per primo e quindi faccia fallire i test.

Note
----

Ho scelto di proposito di includere gli eseguibili e i file di installazione generati. Se non servono faccio sempre in tempo a rimuoverli.
