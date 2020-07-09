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
