## Mi is az AudioBookCutter?
Röviden egy olyan program, amely képes hangoskönyveket (igazából minden mp3 fájformátumú) fájlt elvágni a felhasználó által megjelölt helyen. Jelöléseket tehetünk a megnyitott audiofájl kívánt pontjaiba, majd a program ezen markereken elvágja azt.

Egy C#-ban írott, és Windows Forms-al implementált szoftver, amely az ffmpeg könyvtár segítségével végzi el a vágásokat.

Több infó a dokumentumokban.

## Használata
#### 1. Hanganyag megnyitása
- Fájl > "Hanganyagok megnyitása...", vagy CTRL + O billentyűkombináció
- Több fájl megnyitása esetén a fájlokat sorrendben kell kiválasztani!
- Tipp, ha a fájlok névszerinti sorrendben vannak: egyszer kattintsünk rá az első fájlra (hogy ki legyen jelölve), majd keressük meg az utolsót, majd arra SHIFT billentyűvel együtt kattintsunk. Az összes fájl sorrendben lesz kijelölve és beimportálva.

#### 2. Markerek hozzáadása
- Markert hozzáadhatunk a seeker aktuális pozíciójához a "Marker a jelenlegi pozícióba" gombbal, vagy CTRL + N billentyűkombinációval
- Manuálisan is elhelyezhetünk markert, ha a "Marker ehhez az időhoz" gomb alá beírjuk a kívánt értéket. Az érték órájára fókuszálhatunk a H billentyűvel, onnantól pedig a percre, másodperce, milliszekundumra a TAB billentyűvel tudunk továbblépni. (Sorban) Az értékek beírása után a "Marker ehhez az időhoz" gombbal, vagy M billentyűvel helyezhetjük le a markert.

#### 2a. Markerek szerkesztése
- Az összes felhelyezett marker a marker listában megjelenik, amelyre az **L** billenytűvel fókuszálhatunk. A listán belül a markerek között a **fel** és **le** billentyűkkel navigálhatunk.
- A kiválasztott markert szerkeszthetjük: a mértékegység kiválasztásához a **W** billentyűvel fókuszálhatunk. Ez is egy lista, melyben a **fel** és **le** billentyűkkel navigálhatunk. Az érték beállítása az **E** billenytűvel történik. Ezután az értéket hozzáadhatjuk (**CTRL + P**) vagy kivonhatjuk (**CTRL + I**) a kiválasztott markerből.
- A kiválasztott markert törölhetjük a "Marker törlése" gombbal, vagy a **CTRL + D** billentyűkombinációval.

#### 2b. Markerek kezelése
- A felhelyezett markereket elmenthetjük a Fájl > Marker mentése menüpont kiválasztásával. Itt kétféle opciónk van: ms-ben elmenteni, ami a preferált opció, és ehhez tartozik a **CTRL + S** billentyűkombináció.
- Az elmentett markereket beimportálhatjuk a Fájl > Markerek megnyitása opcióval.

## What is AudioBookCutter?
It can be used to cut any audiobooks (any mp3 format file, for that matter) at one, or at multiple user-placed timestamps. We can place markers at any given point in the opened audio file, and it will be cut there.

It's written in C# using Windows Forms, while for cutting it is using the ffmpeg library.