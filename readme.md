## Mi is az AudioBookCutter?
Röviden egy olyan program, amely képes hangoskönyveket (igazából minden mp3 fájformátumú) fájlt elvágni a felhasználó által megjelölt helyen. Jelöléseket tehetünk a megnyitott audiofájl kívánt pontjaiba, majd a program ezen markereken elvágja azt.

Egy C#-ban írott, és Windows Forms-al implementált szoftver, amely az ffmpeg könyvtár segítségével végzi el a vágásokat.

Több infó a dokumentumokban.

## Használata
#### 0. FFmpeg
- Győződjünk meg róla, hogy az FFmpeg.exe egy mappában van az AudioBookCutter.exe-vel
- Ha nincs, akkor töltsük le, csomagoljuk ki, majd az FFmpeg bin mappájából másoljuk ki az FFmpeg.exe-t az AudioBookCutter mappájába

#### 1. Hanganyag megnyitása
- Fájl > "Hanganyagok megnyitása...", vagy **CTRL + O** billentyűkombináció
- Több fájl megnyitása esetén a fájlokat sorrendben kell kiválasztani!
- Tipp, ha a fájlok névszerinti sorrendben vannak: egyszer kattintsünk rá az első fájlra (hogy ki legyen jelölve), majd keressük meg az utolsót, majd arra **SHIFT** billentyűvel együtt kattintsunk. Az összes fájl sorrendben lesz kijelölve és beimportálva.

#### 1a. Hang lejátszása
- Az audio lejátszás elindíthatjuk a Play gombbal, vagy a **SPACE** billenytűvel.
- A lejátszást megállíthatjuk az adott helyzetben a Pause gombbal, vagy szintén a **SPACE** billentyűvel.
- A lejátszást teljesen megállíthatjuk a Stop gombbal, vagy az **S** billentyűvel.
- Ha már helyeztünk fel markereket, akkor azok szerint előre ugorhatunk a lejátszásban az "Ugrás a következő markerhez" gombbal, vagy a **K** billentyűvel.

#### 2. Markerek hozzáadása
- Markert hozzáadhatunk a seeker aktuális pozíciójához a "Marker a jelenlegi pozícióba" gombbal, vagy **CTRL + N** billentyűkombinációval
- Manuálisan is elhelyezhetünk markert, ha a "Marker ehhez az időhoz" gomb alá beírjuk a kívánt értéket. Az érték órájára fókuszálhatunk a **H** billentyűvel, onnantól pedig a percre, másodperce, milliszekundumra a **TAB** billentyűvel tudunk továbblépni. (Sorban) Az értékek beírása után a "Marker ehhez az időhoz" gombbal, vagy **M** billentyűvel helyezhetjük le a markert.
- Alapértelmezetten, ha több hanganyagot nyitunk meg, a program mergeli, és abban helyezhetünk el markert. Ám a fájlok listájában, melyre az **F** billenytűvel fókuszálhatunk, kiválaszhatjuk melyik fájlban szeretnénk markert elhelyezni. A listában a **fel** és **le** gombokkal navigálhatunk.
- Ez által relatívan is tudunk markert elhelyezni, pl: 1.mp3, 2.mp3, 3.mp3 fájl mergelve: kiválasztjuk a 2.mp3-at, majd abban elhelyezünk egy markert 10 másodperchez. A marker a mergel fájlban relatívan fog megjelenni, és úgy elmentődni.

#### 2a. Markerek szerkesztése
- A kiválasztott markert törölhetjük a "Marker törlése" gombbal, vagy a **DELETE** billentyűkombinációval.

#### 2b. Markerek kezelése
- A felhelyezett markereket elmenthetjük a Fájl > Marker mentése menüpont kiválasztásával. Itt kétféle opciónk van: ms-ben elmenteni, ami a preferált opció, és ehhez tartozik a **CTRL + S** billentyűkombináció.
- Az elmentett markereket beimportálhatjuk a Fájl > Markerek megnyitása opcióval.

#### 3. Vágás
- Ha felhelyeztünk minden markert amit szerettünk volna, akkor a "Vágás" gombbal, vagy a **CTRL + C** billentyűkombinációval indíthatjuk el a vágást a megnyitott hanganyagon.
- Egy felugró ablakban kiválaszhatjuk a menteni kivánt fájlok helyét és összegző nevét.

## What is AudioBookCutter?
It can be used to cut any audiobooks (any mp3 format file, for that matter) at one, or at multiple user-placed timestamps. We can place markers at any given point in the opened audio file, and it will be cut there.

It's written in C# using Windows Forms, while for cutting it is using the ffmpeg library.

# CREDITS
**FFmpeg** - https://ffmpeg.org/ - for merging and cutting audio

**NAudio** - https://github.com/naudio/NAudio - for audio playback and audio wave rendering

**Serilog** - https://serilog.net/ - for logging