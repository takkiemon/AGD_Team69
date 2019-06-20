# Vaatimusmäärittely

## Yleiskuva

Sovellus on Unitylla ja C#:lla toteutettu Diablo-tyylinen dungeon crawler -peli.
Pelaaja yrittää selvitä tasojen läpi ja päästä mahdollisimman syvälle luolastoon.
Pelaajan pitää taistella luolaston kummituksia vastaan.
Jos pelaaja saa kummituksilta liikaa osumia, pelihahmo kuolee ja peli päättyy.

### Tasot

Tasot ovat proseduraalisesti generoituja.
Tasokattoa ei ole, eli pelaaja pystyy käytännössä pelaamaan loputtomiin loputtoman erilaisia tasoja, olettaen että hän selviää.
Tasot vaikenevat ja kasvavat huomattavaa nopeutta, kun pelaaja pääsee pidemmälle.

### Visuaalinen ilme

Peli on toteutettu 3D:ssä.
Grafiikka on piirtämääni pikseli- ja vokseligrafiikkaa.

## Inspiraatio

Pelit Fate (WildTangent, 2005) ja Diablo III (Blizzard Entertainment, 2012) olivat tämän pelin inspiraation lähteet.

## Jatkokehitysideoita

- Lokaali tallennus. Pelin pystyisi tallentamaan ja lopettamaan kesken, ja jatkamaan myöhemmin
- Lisää erilaisia vihollisia
- Pelaajalle näkyvä tason minimap, joka laajenee sitä mukaa kuin pelaaja tutkii tasoa
- Loot system
  - Hirviöt pudottavat kuollessaan kultaa, aseita ja varusteita
  - Pelaaja voi käyttää aseita ja varusteita, joita pelin aikana löytää
    - Erilaiset aseet toimivat eri tavalla: Ranged aseet, dual wield yms.
  - Tietyiltä tasoilta löytyy myyntimies, jolle pelaaja voi myyydä tarpeettomat esineet, ja jolta voi ostaa esim. taikajuomia
- Leveling system
  - Pelaaja kerää kokemusta (experience points)
  - Tarpeeksi pisteitä saatuaan pelihahmo saavuttaa uuden kokemustason jolloin hahmon attribuutit paranevat
    - Attribuutteja esimerkiksi liikkumisen nopeus, hyökkäysnopeus, hyökkäyksen voimakkuus ja pelihahmon hallitseman valaistuksen voimakkuus
  - Uudella tasolla pelaaja saa käyttöönsä pisteitä, joilla pelaaja voi itse kehittää hahmoaan attribuutteja parantamalla tai esim. uusilla loitsuilla
- Loitsut
  - Pelaaja voi käyttää regeneroituvaa resurssia, manaa, loitsimiseen
  - Taisteluloitsuja, parantamisloitsu, teleport-loitsu, yms.
- Checkpoints
  - Pelaaja pääsee tallentamaan oman edistymisensä, ja kuoleman sattuessa pääsee jatkamaan tietystä pisteestä
- End game scenario
