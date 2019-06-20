# Testausdokumentti

Ohjelmaa on testattu yksikkötesteillä NUnitilla ja manuaalisesti pelitestattu.

## Unitylla tehtävien sovellusten testaus

Unity on tehokas, monipuolinen ja helppokäyttöinen pelinkehitysalusta.
Testaus on kuitenkin sen heikkous.
Jos kehittäjä haluaa kirjoittaa kattavia yksikkötestejä, tämän pitää pitää tämä mielessä alusta asti, ja kirjoittaa koodia niin, että sen yksikkötestaaminen on lopulta yksinkertaista ja toimivaa.
Itse käytin apunani [tätä](https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/) Unityn julkaisemaa blogipostia, ja rakensin koodikannan sellaiseksi, että sovelluslogiikka on eristetty omiin luokkiinsa (ks. [arkkitehtuurikuvaus](Arkkitehtuuri.md).)

## Yksikkötestaus

Sovelluslogiikka jakautuu kahteen osaan:
Peliolioiden eli GameObjectien toimintaa ohjaavat BehaviourControllerit, ja tasogeneraatiosta vastaava LevelGenerator sekä sen hyödyntämät Room-luokat.
Jokaiselle BehaviourControllerille on oma testitiedostonsa yksikkötesteineen.
Myös Room-luokat on testattu.

### Tasogeneraatio

Tasogeneroinnin yksikkötestaus on hoidettu niin, että LevelGenerator-luokalla on kaksi konstruktoria:
Normaalissa pelissä käytetty parametriton konstruktori ja testauksessa käytetty debug-konstruktori, joka ottaa parametrikseen seedin.
Testin taso generoidaan tietyn seedin mukaan, jolloin generaation satunnaisuus ei enää tuota ongelmallisia tilanteita testien kannalta.
Oikein toimivan koodin generoima taso tällä tietyllä seedillä tunnetaan, jolloin testit voivat testata kaikki tason yksityiskohdat ja näin havaita ongelmia tasogeneraatiossa.

### Testauskattavuus

Unity ei valitettavasti tue testikattavuuden selvittämistä.
Empiirisen arvion mukaan sovelluslogiikasta vastaavasta koodista on yksikkötestein katettu 90-95%.

## Pelitestaus

Pelitestaus, eli pelin manuaalinen testaus peliä pelaamalla, on suoritettu Linuxilla ja Windowsilla. Se on tapahtunut Unityn sisäisesti, Build And Run-toiminnolla ja valmiista buildistä.

### Toiminnallisuudet

Kaikki määrittelydokumentin ja käyttöohjeen listaamat toiminnallisuudet on käyty läpi.
