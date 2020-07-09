# Funkcionális specifikáció
#### Tartalom
- Áttekintés
- Jelenlegi helyzet
- Jelenlegi folyamatok modellje
- Igényelt folyamatok modellje
- Forgatókönyv

## Áttekintés
Egy olyan programot fejlesztek, amely hangoskönyvek feldarabolását segíti.

A felhasználó könnyedén elhelyezhet vágópontokat, *marker*eket (jelöléseket) a megnyitott mp3-fájlokban, majd azok mentén az applikáció az FFmpeg könyvtár segítségvel elvágja, vagy ahol esetlegesen szükséges, összeilleszti azt.

## Jelenlegi helyzet
A megrendelő szeretné a hangoskönyvek fejezetenkénti feldarabolását, összeillesztését egyszerűbbé tenni, ahelyett, hogy manuálisan kelljen parancssorban FFmpeg utasításokat kiadni, vagy esetleg más, harmadik féltől származó programot használni.

## Jelenlegi folyamatok modellje
Az [MVGYOSZ](https://www.mvgyosz.hu/ "MVGYOSZ") <sup>*(Magyar Vakok és Gyengénlátók Országos Szövetsége)*</sup> könyvtárában ezen hangoskönyvek kazettákról lettek bedigitalizálva. Egy kazetta két oldala két darab, körülbelül fél-fél órás mp3 fájlt eredményez.

A probléma az, hogy egy fejezet vége akár az egyik hanganyag közepébe is kerülhet, így könnyen el lehet benne veszni, illetve a hangoskönyvek rendszerezetlenek maradnak.

## Igényelt folyamatok modellje
A program célja az, hogy a felhasználó a program segítségével bárhol elvághatja a megnyitott hangoskönyvet. Ez nem csak vágást, de néha összeillesztést is jelenthet, hiszen egy fejezet folytatódhat egy másik fájlban is. 

Ezek után (ha a felhasználói hibákat nem vesszük figyelembe) a hangoskönyv rendszerezettebb lesz, hiszen végtermékként mindegyik fejezet külön mp3 fájlban lesz eltárolva.

## Forgatókönyv
Az applikáció futtatása után a menüből ki kell választanunk a vágni kívánt fájlokat. Ha több fájlt választunk, azokat sorrendben kell kijelölni.

A fájlok megnyitását követően a *timeline*on (idővonalon) megjelennek a hanganyagok, sorrendben, amelyekbe kedvünk szerint helyezhetünk *marker*eket.

Lehetőség lesz a menüben a *marker*ek mentésére, illetve betöltésére is.

Lesz olyan opció, mellyel befolyásolhatjuk azt, hogy a különböző forrásfájlokból beimportált audiót ne illessze össze. Példa: 
>**két darab mp3 fájlt nyitunk meg, A-t és B-t. Egy markert az A fájl közepébe, a másodikat pedig B fájl közepébe tesszük:**
- Ha összeillesztést választunk, eredményünk három darab fájl lesz.
- Ha nincs összeillesztés, az négy darab fájlt fog visszaadni.
