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

## Projekt terv
A projekt C#-ban fog elkészülni, GUI-nak pedig a Windows Forms lesz felhasználva.

Más, harmadik féltől származó külső könyvtárak a fejlesztés közben, illetve utána lesznek világosabbak, de az egyik amelyik valószínűleg használva lesz, az az [NAudio](https://github.com/naudio/NAudio).

Az MP3 fájlok vágásához, kezeléséhez az FFmpeg könyvtár lesz segítségre.
