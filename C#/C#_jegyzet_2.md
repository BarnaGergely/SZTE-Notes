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

# 4. óra Márc 7 - SOLID elvek

## SOLID

Design patterns

Tapasztalatok mentén megfogalmazódott irányelvek

Singleton - anti pattern

- Single responsibility: 
    - Egy osztálynak a feladatban ha kötő szavat kell használnod, akkor nem teljesíti
    - Egy osztálynak egy funkciója legyen
    - előnyök
        - side effectek minimalizálása
    - sokszor nehéz szétválasztani a funkciókat
    - 
- Open/closed:
    - Egy osztálynak nyitottnak a bővítésre, de zárt a módosításra
    - Inkább öröklődéssel bővítsünk, ne az ősosztályt babráljuk
    - gyakorlatban nehéz kivitelezni
    - C#-ben virtual vagy abstract kulcsszót kell használni az ősosztályban is
        - sealed - öröklés megakadályozása
- Liskov Substitution
    - ha az ősosztályod definiál egy metódust, ami vissza tér valamilyen értékkel akkor a visszatérési érték típusát ne definiáljuk felül
    - A leszármazott ugyan olyan dologgal vagy nagyon hasonlóval térjen vissza mint az ős
    - mellékhatás is ide tartozik
    - EZ interface-ekre is vonatkozik
- Interface segregation:
    - interface-eket bontsd minél kisebb részekre
    - A komplex interface-eket bontsd kisebb részekre
    - Ne legyen a client interface, ami mindent is csinál egyszerre.
    MInt a single responsibility
- Dependency inversion: 
    - Ha egy osztály példányosít magának másik osztályok, nem jó, mert nem tesztelhető és opne/closed is sérülni fog
    - Megoldás: A interface-t használni a két osztály között
        - Így lehet dummy B-t létre hozni A-hoz
        - Ez a repository pattern

## más hasznos elvek

### Keep it simple

- USA hadserege találta ki a 40-esévekbe
- Legyen minden a lehető legegyszerűbb, a lehető legkevesebb komponenssel, mert úgy romlik el a legkisebb eséllyel

### YAGNO

- You Aint Gonna Need It
- A lényegre koncentrálj
- Kérdezd meg: Biztos szükséges ez a feature? - Vissza kell kérdezni a megrendelőtől
- A munka 50%-át kérdésekkel megspórolhatod
- F-16 története, mig-29. - de miért a kérdés mindig. Mindent kérdezzünk meg.
    - Minél több kérdés annál jobb

## Software Architecture

- a fejlesztéssel közben meghozott döntések sorozata
- Vannak döntések, amiket nagyon sokba kerül megváltoztatni (pl. Angular vs React)

### Klasszikus Layered architecture

- Alul database, felül view
- Application és domain hozza a pénzt, ez tárolja az igazi tudást, funciókat
- nem valami flexibilis, de egyszerű implementálni

# Clean architecture

- könyebb platformok között átvinni


```c#
namespace Shell {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello");
        }
    }

    // rétegtől független bárhol implementálható funkciók
namespace Shell.Infrastructure {
    

    internal interface IShellCommand {
        string Name {Get; }
        void Execute(IHost host, string[] args);
    }

    /// A futtató alkalmazás interface-e, olyan funkciókat definiál, amihez egy IshellCommand hozzá férhet
    internal interface IHost {
        // azt az alkalmazást írja le ami futtatja a command okat
        sting ReadLine();
        void WriteLine(string message);
        void Exit(); // ez már sérti az interface segregation-t, de példa programban jó ez így mert így nem kell tonnányi interface
    }

}

namespace Shell.Userinterface {
    internal class Ui {
private readonly ComnadProv _comandProvider;
private read only host;

        public Ui(CommandProvider commandProvider, IHost host) {
            _host = 
            _Command prov
        }

        public void Run(){
            while(true) {
                string input = host.ReadLine();
                string[] splittedInput = input.Split(' ');
                IShellCommand? commandToExecute = FindCommandName(splittedinput[0]);
                if (commandToExecute != null) { // null check nagyon fontos. 90%-ban emiatt crash-el az app
                    // TODO: folytatni 
                    commandToExecute.Execute();
                }

            }
        }

        private IShellCommand? FindCommandName(string commandName) {
            // iterációs változó egyes számban, amit iterálunk (tömb) többes számban
            // ne fancy-zz az nagollal, mert valaki úgysem fogja érteni, csak egyszerűen
            // egy dolgot ha valahogy elneveztél, ahhoz konzisztensen ragaszkodj
            // a nevezéktant nem változtatjuk
            foreach(var command in _commandProvider.Commands) {
                if (command.Name.Equals(commandName, String.Comparison.CurrentCultureIgnoreCase)) { 
                    // ne kommentálj to lover-re, ha nem akarod figyelembe venni a kis és nagy betűt mivel megeszi a memóriát
                    /* currentCulture v InariantCulture
                        - Németül a ss = B
                        - Ha invariant a culture, akkor megkülönbözteti a kettőt
                        - Ordinal UTF szabvány szerinti összehasonlítás
                    */

                    return command;
                }
            }
            return null;
        }
    }

    // command ok szolgáltatása az app nak
    internal class CommandProvider {
        public IShellCommand[] Commands { 
            get;
            /* probléma: ahányszor hozzá nyúluk a List-hez újra fogja példányosítani a tömböt. 
            get {
                return new IshellCommand[] {
                    new Exitcommand(),
                }
            }
            */
        }

        // megoldás
        public CommandProvider {
            Commands= new IshellCommand[] {
                    new Exitcommand(),
                }
        }
    }
}

namespace Shell.Application {
    internal call ExitCommand : IShellCommand { // neve hasonlítson az implementált interface-ekre

        /*
        public string Name { // működik de van rövidebb is
            get{ return "exit";}
        }
        */

        //rövidebben
        public string Name => "exit";

        public void Execute(IHost host, string[] args) {
            host.Exit();
        }
    }
}
}

// C#-ben minden mappa egy új namespace
    

```