# Teszt
Az alkalmazás fejlesztése során elengedhetetlen volt a folyamatos futtatás és tesztelés.

Az alábbi bugok kerültek javításra a fejlesztés közben:

| Hiba felfedezés dátuma | Bug leírása | Elvárt működés  | Hiba javításának dátuma  |
| ------------ | ------------ | ------------ | ------------ |
| 2020-07-15 | Ha a fájl végére ért a lejátszás utána nem lehet újra elindítani | Utána is el kell tudnunk indítani. | 2020-07-27 |
| 2020-07-17 | Az FFmpeg parancs nem fut le | Az FFmpeg parancsnak hiba nélkül le kell futnia. | 2020-07-22 |
| 2020-07-17 | Lejátszás megállítása nem vitte az elejére a lejátszást | A lejátszás megállítása után ha újra elindítjuk, akkor az elejéről kezdi. | 2020-07-27 |
| 2020-07-20 | Nem lehet beletekerni az elindított lejátszásba | Bérhova bele lehet tekerni lejátszás közben is. | 2020-07-28 |
| 2020-07-31 | Az alkalmazás exceptionnel zárult be | A programnak hiba nélkül be kell záródnia. | 2020-08-03 |
| 2020-08-04 | Lejátszás forrógomb nem működik | - | 2020-08-05 |
| 2020-08-05 | Új hang megnyitása ha már meg volt egy nyitva | Az új audió megnyitása úgy működik, mintha először importálnánk azt. | 2020-08-05 |
| 2020-08-05 | Wave újrarenderelődik | A wave csak akkor renderelődjön újra, ha az ablak újraméretezve lett. | 2020-08-05 |

Teszteseteik:

| Teszt neve röviden  | Elvárt működés  | Hiba  | Átment  |
| ------------ | ------------ | ------------ | ------------ |
| Ha a fájl végére ért a lejátszás utána nem lehet újra elindítani | Utána is el kell tudnunk indítani. | - | PASS |
| Az FFmpeg parancs nem fut le | Az FFmpeg parancsnak hiba nélkül le kell futnia. | - | PASS |
| Lejátszás megállítása nem vitte az elejére a lejátszást | A lejátszás megállítása után ha újra elindítjuk, akkor az elejéről kezdi. | - | PASS |
| Nem lehet beletekerni az elindított lejátszásba | Bérhova bele lehet tekerni lejátszás közben is. | - | PASS |
| Az alkalmazás exceptionnel zárult be | A programnak hiba nélkül be kell záródnia. | - | PASS |
| Lejátszás forrógomb nem működik | - | - | PASS |
| Új hang megnyitása ha már meg volt egy nyitva | Az új audió megnyitása úgy működik, mintha először importálnánk azt. | - | PASS |
| Wave újrarenderelődik | A wave csak akkor renderelődjön újra, ha az ablak újraméretezve lett. | - | PASS |

A következő fázis az applikáció nagyobb funkcióinak alpha tesztelése.

A tesztesetek egy elvárt működéssel lesznek felcímkézve. Amelyet nem teljesít a program, azon megbukott, és az a hiba javításra szorul.

## Alpha teszt

### 2020-08-11
| Teszt neve röviden  | Elvárt működés  | Hiba  | Átment  |
| ------------ | ------------ | ------------ | ------------ |
| Ablak méretezés (wave nélkül) | Pár control alkalmazkodik az új ablakmérethez | - | PASS |
| Audiofájl megnyitása  | A gomb VAGY hotkeyre kiválaszthatjuk a hangfájlt, és az gond nélkül beimportálódik | - | PASS |
| Több audiofájl megnyitása  | A gomb VAGY hotkeyre kiválaszthatjuk a hangfájlokat, azok mergelődnek, és egy egészként nyitódik meg | - | PASS |
| Audio hossza  | Aaz audio hossza a wave felett jobb oldalon megjelenik | - | PASS |
| Wave rendering | Megnyitás után a hangfájl waveje lerenderelődik, és megjelenik a formon | - | PASS |
| Ablak méretezés | Pár control alkalmazkodik az új ablakmérethez, a wave újrarenderelődik | - | PASS |
| Gombok nyomhatóak | Audiofájl megnyitására pár gomb használhatóvá válik | - | PASS |
| Gombok nyomhatóak | 1 marker elhelyezése után még több gomb válik használhatóvá | - | PASS |
| Hang lejátszása | A megnyitott hanganyag probléma nélkül elindul | - | PASS |
| Hang megszakítása | A megnyitott hanganyag lejátszás közben megáll az adott helyen | - | PASS |
| Hang megállítása | A megnyitott hanganyag lejátszás közben megáll | - | PASS |
| Seeker | A seeker reprezentálja az adott időt a megnyitott hangfájlban, lejátszás során halad előre | - | PASS |
| Aktuális idő | A lejátszás során az aktuális idő folyamatosan frissitődik, és pontosan mutatja az időt | - | PASS |
| Seeker mozgatása | A hangwave bármely pontjába kattintunk, a seeker és a lejátszás onnantól folytatódik | - | PASS |
| Marker elhelyezése | A marker a megfelelő helyre, ill. megfelelő időponthoz kerül a wave-n | - | PASS |
| Marker elhelyezése az adott időhöz | Egy marker a seeker jelenlegi időpontjába helyeződik | - | PASS |
| Marker elhelyezése manuális időhöz | Egy marker a beírt időpontra helyeződik | - | PASS |
| Marker szerkesztése | Egy markert lehet szerkeszteni: hozzáadni/kivonni adott mértékegységű értékeket | - | PASS |
| Marker törlése | A kiválasztott marker törlésre kerül | - | PASS |
| Markerek mentése | Az összes elhelyzett marker mentésre kerül: vagy ms, vagy frames formátumban | - | PASS |
| Markerek betöltése | A program által elmentett markerek hiba nélkül beimportálódnak | - | PASS |
| Vágás | A megnyitott hang a markerek szerinti időpontokban elvágásra kerülnek, majd elmentődnek a felhasználó által kiválasztott helyre | - | PASS |
| Forrógombok | A dokumentált forrógombok működőképesek | - | PASS |
| Címkézett controlok | Az összes formon lévő control címkézett | - | PASS |