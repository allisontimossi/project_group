## Progettazione webapp gioielli

#### Questo README ha lo scopo di progettare l'impostazione generale dell'applicazione web dedicata al catalogo della gestione dei prodotti.

### Compiti da organizzare

- [ ] Progettare struttura pagine web
- [ ] Occuparsi della gestione dei ruoli

### Requisiti

- [ ] Responsività del sito (bootstrap)
- [ ] Funzionalità CRUD
- [ ] Partial Views per parti di codice che vengono ripetute
- [ ] ViewModels Specifici per ogni pagina
- [ ] Decidere tutti gli elementi relativi allo stile delle pagine
- [ ] Subscription based website
- [ ] Bundle Gioelli di diverse categorie
- [ ] Nuova classe azienda estensione di customer
 

## Struttura del sito

#### La homepage sarà costituita da un carousel dei prodotti piu venduti , card di prodotti in offerta, un carousel con i brand, un bottone che rimanda alla registrazione per usufruire dei vantaggi relativi all'utente registrato.
## Pagine
- Homepage
- Catalogo prodotti
#### La pagina del catalogo dei gioielli sarà costituita da un'area centrale che conterrà le cards che rappresentano i prodotti del catalogo. Inoltre saranno presenti delle sezioni dedicate al filtro dei prodotti, alla ricerca per nome e all'ordinamento. Il filtro permette di visualizzare i prodotti per categoria, prezzo, quantità etc. Le funzionalità saranno vincolate al ruolo dell'utente in sessione. 

- `Utente non loggato`: ha la possibilità di visualizzare il catalogo dei prodotti ma non può fare nessun acquisto.
- `Utente loggato normale`: Può anche acquistare prodotti e visualizzare il carrello.
- `Utente loggato produttore `: Può mettere i prodotti in vendita sul sito.
- `Admin`: Avrà una pagina dedicata dove potrà aggiungere rimuovere e modifcare prodotti e categorie.

## Prorietà aggiuntive del model
- materiale(oro, argento, acciao).
- brand
- tipo di pietra(diamante, zaffiro, quarzo)
- categoria (bracciale, collana, orecchini , anello)

#### Viewmodels

- Catalogo prodotti: