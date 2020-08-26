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

Funkciók bevezetése:

| Funckió bevezetésének dátuma | Funkció leírása | Funckió impl. befejezése  |
| ------------ | ------------ | ------------ |
| 2020-07-20 | Több hanganyag megnyitása és mergelése | 2020-07-24 |
| 2020-07-20 | Markerek szerkesztése | 2020-07-21 |
| 2020-07-24 | Markerek mentése/betöltése | 2020-07-29 |
| 2020-07-30 | Hanghullám renderelési idő csökkentése | 2020-08-10 |
| 2020-07-31 | Többféle marker mentési lehetőség | 2020-08-03 |
| 2020-08-07 | Akadálymentesítési címkék | 2020-08-07 |
| 2020-08-14 | Relatív marker elhelyezés | 2020-08-18 |
| 2020-08-20 | Vágás auto fájlnevezés változtatása | 2020-08-20 |
| 2020-08-20 | Ugrás manuális markerhez | 2020-08-20 |
| 2020-08-20 | 1mp-s beletekerés | 2020-08-20 |



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

### 2020-08-19
| Teszt neve röviden  | Elvárt működés  | Hiba  | Átment  |
| ------------ | ------------ | ------------ | ------------ |
| Több fájl megnyitása, fájlok listába kerülnek | A fájlok nevei a listába bekerülnek, és lehet közülük választani | - | PASS |
| Divhez ugrás | A lejátszás a következő divtől folytatódik (HA van) | - | PASS |
| Markerhez ugrás | A lejátszás a következő markertől folytatódik (HA van) | - | PASS |
| Két ugyanolyan marker | A program nem enged ugyanoda lerakni egynél több markert | - | PASS |
| Relatív marker elhelyezés | Bármely fájl kiválasztása esetén a marker a megfelelő helyre, relatívan lesz elhelyezve | - | PASS |
| Marker szerkesztése | A program ellenőrzi, hogy a módosítani kívánt érték ne menjen 0 alá, illetve ne mutasson a hanganyagon kívülre | - | PASS |
| Címkézett controlok | Az összes formon lévő control címkézett | - | PASS |

### 2020-08-25
| Teszt neve röviden  | Elvárt működés  | Hiba  | Átment  |
| ------------ | ------------ | ------------ | ------------ |
| FFmpeg merge | Hangfájlok mergelése hiba nélkül megtörténik és megnyitódik | - | PASS |
| FFmpeg cut | A hangfájl a markerek mentén elvágásra és mentésre kerül | - | PASS |
| Beletekerés billentyűvel | Bal és jobb gomb folyamatos lenyomásával bele lehet tekerni az aktuálisan lejátszott audiófájlba | - | PASS |

### 2020-08-26
| Teszt neve röviden  | Elvárt működés  | Hiba  | Átment  |
| ------------ | ------------ | ------------ | ------------ |
| FFmpeg új merge | Hangfájlok mergelése hiba nélkül megtörténik és megnyitódik | - | PASS |
| Markerre ugrás | A markerlistában kiválasztott markerre lehet ugrani dupla kattintással vagy az ENTER billentyűvel | - | PASS |


## Béta teszt

### 2020-08-13

**Javaslatok:**

- Relatív marker elhelyezés

### 2020-08-19

**Javaslatok:**

- Vágás auto fájlnevezés változtatása
- Ugrás manuális markerhez
- 1mp-s beletekerés
- Forrógombok változtatása

**Hibák:**

- Minél bentebb van a vágás, annál jobban csúszik el
- Nem pontos a vágás

### 2020-08-25

**Hibák:**

- Minél bentebb van a vágás, annál jobban csúszik el
- Nem pontos a vágás