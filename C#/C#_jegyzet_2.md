# 2. óra

- Projekt létre hozás:
    - Console App templete (.net framework nélkül, az a régebbi)
    - .net 8 verzió (legújabb)
    - do not enable top leve statement (ha be van kapcsolva régebbi verzió, kezdetben érthetőbb, később jobb kikapcsolni)
- Kezdő projekt
    - CTRL + . - izzócska ikonra kattint
    - Top level statement: lehet pontosan egy namespace és osztály nélküli file, ami a belépési pont
        - ez a file bármilyen nevű file lehet
- Változók
    - int (fix 32 bit)
    - float szám végére kell egy fbetű, egyébként double-ként van értelmezve
    - long (64bit int)
    - dynamic (futási idejű gyengén típusosság, megeszi az erőforrást, ne használjuk)
    - var (fordítás idejű típus felismerés)
        - csak akkor használd, ha a jobb oldalból egyértelműen kiderül milyen típusról van szó
    - char - ''
    - string - ""
        - \n módosító
        - @ string előtt ignorálja a módsítókat
- referencia és érték típus
    - Érték típus csak értéket ad át, nem referenciát
    - referencia típus is alapértelmezetten csak értéket ad át, ref kulcsszó kell hogy referencia adódjon át

- TryParse
    - hiba kezelés, nem exception, nem kell try cache
- immutable
    - fix memória területen van az adat a heap-ben
    - Változtatásnál új helyet foglalunk le neki, oda másoljuk az adatot
    - TODO: ezt nem értem
- láthatósági módosítók
 - internal: szerelvényen belül látható csak
- Property-k
    - get; set;
    - init; - konstruktor helyett
    - változó legyen privát, csak a property legyen kipublikálva
    - _x - privát elé szoktunk rakni _

# 3. óra

```c#
namespace Ora 3 {
    // a struct érték típus => stack-en tárolódik
    // ne legyen több 16 bájt-nál!! => ARM érzékeny arra hogy van allign-olva a memóra - e felett ott elveszik a struktúra előnye. E felett arra ügyelni hogy mindig 16 báj többszöröse legyen ami benne van
    internal struct StrukturaPelda {
        // alapértelmezett érzét: sosem memória szemét
        // érték típusoknál alapértelmezett érték lesz (0)
        public int X { get; set; }
        public int Y { get; set; }
    }

    void Main {
        StrukturaPelda p; // minden változót explicit módon inicializálni kell, hiába kap alapértelmezett értéket
        p = new();
        Console.WriteLine(p);
    }
}
```

- IComparable<> interféc - soraba rendezhetőséget biztosító interface
    - -1 1 vagy 0-t kell vissza adni
- `<StructuraPelda>` gererikus
    - újra felhasználható típusokat kapsz
    - Olyan class adható meg neki, ami generikus
    - 
- `IEquatable<>` - egyenlő e két objektum
    - equals
    - ctrl + . - generate equals and gethashcode legenerálja (kis pipálócskát is be kell pipálni)
    - néhány operátor is felül definiálható
- `GetHashCode()` - egyedi szám az objetumra
    - végeteln adatra véges számú hash szám van
    - equals vagy Iequatible-nél ezt is felül kell írni
        - csúny bug-okat eredményez
    - generáltassuk

- branch - éselágazás
    - Lassú, a lehető legkevesebb kell
    - Proci előre kiszámolja mind a kettő lehetőséget és majd azt adja vissza, amelyiket választotta az elágazás
- && - sort surcite and operator - ha az első felel hamis, a másodikat már meg sem nézi

- Boxing
    - heap-ra máslogtajuk a dolgokat a stack-ről feleslegesen
    - kerülendő

```c#
//generikusok
List<int> szamok = new();
// nem láncolt lista, hanem dinamikus tömb
// manageli hogy mikor kell újra méreteznie magát

int[] ints = new int[5]; // new után kell megadni tömb méretet
// van mérete így át lehet simán adni
//  ha nem adom meg az elelmszámát, meg kell adni az elemeit
// tömb nem bővíthető menet közben

// list vs tömb
// ha csak olvasod vagy tudod hány eleme lesz akkor [], ha bővíteni kell List

// két féle képpen lehet Generikust csinálni:
// T helyére bármilyen generikus mehet


internal class SajatGenerikus<T> {
    // nem dolgozhat más fajta típussal már

    where T : class // csakis osztály lehet vagy struct, de egyszerre mindkettő nem
    where T: new() // Csak olyan aminek van paraméter nélküli konstruktora. Akkor kell ha egy metódus szeretné példányosítani
    where T : IEquitable<T>
}

internal class NemGenerikus {
    public void DoSimething<T>(T instance)
}
```

## Interfacek

- Statikus metódust is definiálhat -> generikus matematikai programozás is lehetséges vele
- 

```c#
// generikus szummázás

insternal static class GenericSum {
    // azért static hogy ne lehessen példányosítani

    public static T Sum<T>(IEnumerable<T> val) where T : Implements INumber<T> {
        // IEnumerable bejárható, mint gy lista (iterátor)
        // mivel megvalósítja mit kaphat a INumber-t, használható mint egy szám

        T result = T.Zero; // azért kell így mert ugye a vissza térési érték is T és nem tudjuk milyen szám lesz, osztásnál pedig gond lenne ha az egyik int lenne
        T counter = T.Zero;
        foreach (var item in values) {
            result += item;
            counter++;
        }
        return result/counter;
    }
}
```

## variancia és kontra variancia

HF megnézni mit jelentenek


## túlcsordulás

Pl. tömböknél ez nagy gond, banki szektorból

```c#
int asd = int.MaxValue();
int varible = int.Parse(Console.Readline()!); // ! kikerüli a null check-et
Console.WriteLine(asd + variable); // nem dovódik exceprion, simán túlcsordul és negatív lesz

// kivédése:
// long-al adunk össze
// ha ez is túlcsordul van int128
    // ez több molekula mint amekkora a látható unniverzumban van


// Csak csínnán vele, mivel try-catch lényegénen egy szofisztikált goto
// nagy alkalmazásnál nem tudod melyik catch fogja meg néha
try {
    checked {
        // futás idejű túlcsordulás ellenőrzés
        Console.WriteLine(asd + variable); // így már exception dobódik
    }
} 
catch (Exception e) { // minden az Exception-ből, sose csinálunk ilyen kivétel kezelést
// Ez a Pekémn exception - mindent elkap
// olyan hibákat is alkapunk amiket nem akartunk
// Maximum egy helyen a main-ben legyen ilyen, ha már semmi nem kapta el, az megfogja az OS előtt és kiírhasson még valamit a program leállása előtt

Console.WriteLine(e); // sose írassuk ki így, hogy hol történt az exception, mert ez alapján támadható a rendszer - DDOS támadási felalat
// Maximum a Message-t írassuk ki
}
finally {
    // mindenképp lefut
}
```
