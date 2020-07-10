# Rendszerterv
#### Tartalom
- Rendszer célja
- Projekt terv és követelmények
- Funkcionális terv
- Teszt terv
- Absztrakt domain modell

## Rendszer célja
A program elsődleges célja kazettáról bedigitalizált hangoskönyvek fejezetenkénti rendszerezésének segítése: ezen könyvek végig, egy-két audiofájlban lettek elmentve, bármiféle indikátor nélkül, hogy hol kezdődik illetve végződik egy fejezet.
Az applikációban megnyitott hanganyagokban *marker*et lehet elhelyezni, majd ez FFmpeg parancsok kiadásával kezeli azokat. Végeredményként a jelöléseknél felosztott hangoskönyvet kapunk.

## Projekt terv és követelmények
A projekt C#-ban fog elkészülni, GUI-nak pedig a Windows Forms lesz felhasználva.

Más, harmadik féltől származó külső könyvtárak a fejlesztés közben, illetve utána lesznek világosabbak, de az egyik amelyik valószínűleg használva lesz, az az [NAudio](https://github.com/naudio/NAudio).

Az MP3 fájlok vágásához, kezeléséhez az FFmpeg könyvtár lesz segítségre.

## Funkcionális terv
Egyedüli rendszerszereplő a felhasználó, aki:
- megnyithat egy vagy több audiofájlt
- ezen hanganyagok mentén egy vagy több *marker*t is elhelyezhet
- mentheti az elhelyezett *marker*eket
- importálhat mentett *marker*eket
- elindíthatja az algoritmust, amely elvégzi az FFmpeg parancsok kiadását
- kiválaszthatja, hogy a különböző forrásokból származó fájlokat egybeillessze-e

Menühierarchia:
TBA

## Teszt terv
Az alkalmazás elkészítése során szükség van a folyamatos tesztelésre. Célja a program különböző részeinek működésének teljes vizsgálata, ellenőrzése.

#### Tesztelési folyamatok:
**Unit teszt**
A fejlesztés során is ajánlott már tesztelni a programot. Az úgynevezett unit teszteket szintén meg kell írni, figyelve a minél nagyobb kódlefedettségre.
Minden tesztesetnek hiba nélkül le kell futnia.

**Alpha teszt**
Ez a tesztfázis szintén a fejlesztés alatt történik. Tesztelni kell a meglévő funckiókat, többféle esetben is.
Nagyobb hangsúlyt kell fektetni azon programrészekre, melyeknek nagyobb a valószínűsége, hogy hibásak lehetnek.

**Beta teszt**
Ezt a tesztelést már felhasználok hajtják végre. Tesztelni kell többféle fájlra, többféle vágás esetre.