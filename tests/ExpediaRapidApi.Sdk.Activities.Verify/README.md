# Verifica funzionale del client Activities

33 controlli sul client Activities dell'SDK, eseguiti contro un **trasporto HTTP finto**.
Nessuna credenziale, nessuna rete, nessun ambiente Expedia.

```bash
dotnet run
```

Deve stampare `TUTTI I CONTROLLI SUPERATI` ed uscire con codice 0. Ogni controllo fallito stampa la
richiesta effettivamente costruita, così si vede subito cosa è andato storto.

## Cosa verifica

Le cose che il compilatore non può cogliere e che è facile sbagliare:

- **Query string**: gli id si ripetono (`activity_id=1&activity_id=2`) invece di essere indicizzati; i
  parametri di *path* (region id, activity id) **non** finiscono anche in query; snake_case e date ISO.
- **Paginazione**: l'header `Link` viene letto cercando `rel="next"`, e l'ultima pagina non ha un next.
  Viene letto anche `Pagination-Total-Results`.
- **Deserializzazione**: enum (`cancellation_policy.type`), rating annidati, media indicizzati per taglia.
- **Token HATEOAS**: `payment` e `create` estratti correttamente dai link della price-check.
- **Header**: `Customer-Ip` e `Test`.
- **Biglietti**: la codifica `ticketId-count`, con le quantità a zero scartate.
- **Cancellazione**: il **202 è `Unknown`, non `Cancelled`** — trattarlo come successo significherebbe
  rimborsare un cliente la cui prenotazione potrebbe essere ancora viva.

## Da fare

Convertirlo in xUnit e agganciarlo alla CI, che oggi non esegue nessun test.
