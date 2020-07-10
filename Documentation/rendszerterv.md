# Rendszerterv
#### Tartalom
- Rendszer célja
- Projekt terv
- Követelmények
- Funkcionális terv
- Teszt terv
- Absztrakt domain modell

## Rendszer célja
A program elsődleges célja kazettáról bedigitalizált hangoskönyvek fejezetenkénti rendszerezésének segítése: ezen könyvek végig, egy-két audiofájlban lettek elmentve, bármiféle indikátor nélkül, hogy hol kezdődik illetve végződik egy fejezet.
Az applikációban megnyitott hanganyagokban *marker*et lehet elhelyezni, majd ez FFmpeg parancsok kiadásával kezeli azokat. Végeredményként a jelöléseknél felosztott hangoskönyvet kapunk.
