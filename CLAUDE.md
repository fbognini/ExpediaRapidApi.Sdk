# Convenzioni di scrittura del codice

Regole trasversali di stile e progettazione. Valgono per ogni linguaggio salvo dove indicato.

## Commenti

Default: nessun commento. Identificatori ben scelti spiegano già il *cosa*. Aggiungi un commento solo dove il *perché* non è ovvio — vincoli nascosti, invarianti sottili, workaround per un bug specifico, astrazioni genuinamente complesse. Niente blocchi decorativi, niente XML doc di più paragrafi, niente commenti che riformulano la riga sotto.

Non spezzare mai una riga di commento a metà frase. Un'interruzione di riga dentro un commento è ammessa solo dopo una frase chiusa da un punto — altrimenti la frase resta su una riga sola, per quanto lunga diventi. Non riformattare i commenti a una larghezza di colonna.

Vale per ogni sintassi di commento: `//`, `/* … */`, `///` XML doc, JSDoc, `#`, `--`, `<!-- … -->`, docstring.

```csharp
// Sbagliato — la frase è tagliata a metà.
// Stripe rejects a capture below the gateway minimum, so the exceeding
// amount is credited instead.

// Giusto — una frase, una riga.
// Stripe rejects a capture below the gateway minimum, so the exceeding amount is credited instead.

// Giusto — due frasi, due righe, ciascuna chiusa dal punto.
// Stripe rejects a capture below the gateway minimum.
// The exceeding amount is credited instead.
```

È una regola di formattazione: non rende i commenti più graditi. Continua a scriverne pochi, e solo per il *perché* non ovvio.
