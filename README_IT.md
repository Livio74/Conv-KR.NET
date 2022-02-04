Progetto Conv-KR.NET
====================

E' un progetto di conversione da VB6 in C# di un progetto molto vecchio denominato Kripter (o KR) che serve a criptare con algoritmo di crittografia con Or Esclusivo ogni singolo file

Il programma , essendo vecchio ha diversi inesattezze e modalità di programmazione che ricalcano la mia poca esperienza del tempo. Ad esempio l'utilizzo non clean code del prefisso str per tutte le stringhe.

La soluzione , denominata KR.NET , è una semplice conversione , probabilmente in futuro andrò a creare un nuovo repository dove farò evelvere il programma per migliorarlo dal punto di vista del clean code (eliminando per esempio i prefissi str) e aggiungendo nuove funzionalità per evitare di utilizzare la crittografia simmetrica che è facilmente decriptabile.

Nella cartella [Kripter](Kripter) c'è il progetto Kripter VB6 mentre le altre cartelle sono il progetto o meglio la soluzione convertita in C# dove ogni cartella è un progetto.
Questo progetto aveva il vantaggio di essere stato modularizzato utilizzando i moduli di VB6 quindi la successiva conversione è stata più facile.

Creazione del [progetto](KR.NET/KRTest) di unit test
-------------------------------------------------

Vista la modularità del sorgente VB6 è stato facile creare tramite [Microsoft Visual Studio Test](https://docs.microsoft.com/it-it/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022) un set di test che potesse verificare la correttezza di ogni modulo vb6.
Questo tra l'altro ha favorito la correzione di bug così che la creazione delle interfacce utilizzando le funzioni dei moduli si è limitata alla creazione dei controlli e la conversione del solo codice relativo agli eventi e alle funzioni/subroutine interne alle form.

Essendo stata una conversione manuale sono stato positivamente soddisfatto di averlo fatto prima di aggiungere le form invece che dopo. Magari se si ha un tool affidabile (in futuro proverò [VB Migration Partner](https://www.vbmigration.com/)) aggiungerò dei test solamente dopo aver verificato le interfacce e scoperto dei bug.
