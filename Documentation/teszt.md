# Teszt
Az alkalmazás fejlesztése során elengedhetetlen volt a folyamatos futtatás és tesztelés. A következő fázis az applikáció nagyobb funkcióinak alpha tesztelése.

A tesztesetek egy elvárt működéssel lesznek felcímkézve. Amelyet nem teljesít a program, azon megbukott, és az a hiba javításra szorul.

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