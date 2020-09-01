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
- ha több fájl került összeillesztésre, azokban relatívan is elhelyezhet markert

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

#### Tesztelendő funkciók:
- a programnak meg kell tudnia nyitni audiofájlokat
- ezeket a nyitási sorrendben az idővonalra helyezni
- *marker*eket lehet helyezni az idővonalra, a hanganyagokba
- a *marker*eket el is lehet menteni
- az előzőleg mentett *marker*eket meg lehet nyitni
- a vágást a *marker*ek mentén el lehet indítani
- a különböző forrású fájlokat felhasználói input alapján kezeli
- megbizonyosodni arról, hogy tényleg ott lett-e elvágva a fájl, ahol meg volt jelölve
- annyi darab fájlt eredményezett a vágás, amennyit elvártunk
- stb.

## Absztrakt domain modell
|  Szó |  Fogalom |
| ------------ | ------------ |
| Marker  | Jelölés. Az idővonalon, a megnyitott fájlban tehetünk többet is, ahol elszeretnénk azt vágni.  |
| FFmpeg  | Szoftver könyvtár, amely képes videókat és audiókat kezelni.  |
| GUI  | Grafikus interfész.  |
