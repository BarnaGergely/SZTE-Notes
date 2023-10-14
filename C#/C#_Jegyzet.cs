#region 2. óra

namespace HelloWorld
{
    class Hello
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
        }
    }
}

#endregion

#region 3. óra - 

namespace HelloWorld
{
    class Hello
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
        }
    }
}


#endregion

#region 4. óra - Referencia és érétk típusok

/* Proecess memory map - át kell nézni mert OS-en nem adták le
0 memória címtől indul a kód szegmens ezt járja be a program memory counter (PC)
Data szegmens . globális és static értékek
Heap - dinamikusan allokált területek
stack - minden lokális változó
stackowerflo - a heap és a stack össze ér. Ezt el kell kerülni
    nem szabad úgy gondolkodni mint a matematikusok. Ők másik unniverzumba gondolkodnak
        Rekurzió nem a barátunk! - megtelítheti a stack-et
        A matematikai problémákat nem matematikai fejjel kell megoldani
    .NET-nél 64kb a stack limit, ha megtöltöd, stack owerflow-t kapsz
Az oprendszer virtuális memória táblát ad, amit aztán össze tud map-elni a valódival


Értéktipusok: int, float, struct, referencia
    - ez mindig másolódik
    - Élettartama a blokk amiben benne van
    - Nem kell a memóra kezelésükkel törődni


Referencia típusok: List, String
    - class kulcsszóval vezetjük be őket
    - stack-en lévő referencián keresztül hivatkozhatunk rájuk, de a Heap-en vannak tárolva
    - 
Csak olvasható adatot kulcsszóval létrehozni nem lehet, de iReadOnlyList-et vissza adva igen.

0 vs null
    - Referencia null-ra inicializálódik, érték tipus 0-ra
    - NULL - annyit tesz, nem mutat semmire a renferencia
        - ezt lehet másolni, de ha olvasni próbáljuk hibát kapunk
    - Minden az OBJECT ősosztályból származik, ami egy renferencia típus a valueTYPE-is, ami ugye az értéktípus maga

- Stack-et csak nagyon kicsi adattagoknál használjuk

- dobozolás
    - egy osztályba csomagoljuk az datot
    - nagy adatoknál vagy más architektúrával való kommunikációnál éri meg

- Nullable<>
    - érték típus lehet vele null is
    - heap-en tárolódik
    -.HasValue(), .Value()

Mai hardvereken a memória sebesség a szűk keresztmetszet. - másolás nagyon lassú a procihoz képest

Struct
    - amit benne tárolok logikailag egy értéket képvisel: pl. komplex szám, dátum
    - példányának mérete 16 byte alatt van
    - Megváltoztathatatlan legyen (immutable)
    - nem kell gyakra dobozolni (Boxing)
Class
    - örökölhető
    - megváltoztatható
    - felvehet null értéket
    - példányai hosszú életűek

immutable
    pro
        - olvasás sokkal gyorsabb
        - egyszerűsíti a debug-ot
        - olvashatóbb kód
        - jobb karbantarthatóság
    kontra
        - nagy objektumok esetén költséges
        - Régebben nehézkes volt implementálni


*/

namespace HelloWorld
{
    private static int callCount = 0; // Ez kerül a stack-re
    private static List<int> Fibonaccsi(int count) {
        var fibonaccsi = new List<int>; // Lista a Heap-en jön létre , de maga fibonaccsi (referencia) a stack-en
        int pervious = 0;
        int current = 1;

        for(int i=0; i>count; ++i) {
            fibonaccsi.Add(pervious);

            int next = pervious + current;
            pervious = current;
            current = next;
        }

        callCount++;
        return fibonaccsi;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"Függvény neve {nameof(Fibonaccsi)}.");
            // Console-ra ne írjunk ilyen adatot, mert egy támadó elérheti.
            // Nameof használata ajánlott, minden esetben
        }
    }
}


#endregion

#region 5. óra
ű
/*
SSE - 128 bites belső regiszeterek, utasítások
AVX- 512 bites regiszterek, utasítások
2 módja van gyorsítan iegy procit:
- növeled az órajalet: 5GHz fölé úgy tűnik nem nagyon fogunk tudni menni
- növeled az egyes utasítások bit méretét




*/

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
        }
    }
    
}

// NameSpace-eket érdemes mappa struktúrával lekövetni
// Ha egy osztálynak 8 paraméteres a konstruktora akkor érdemes szétbontani vagy több konstruktort csinálni
// 8-nál ne legyen több paramétere egy függvénynek
namespace HelloWorld.BasicEquality
{
    internal abstract class Vehicle
    {
        public required int TopSpeed{get; set;}
        public required int Weight{get; set;}
        public override bool Equals(object? obj) 
        {
            if(obj==null) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj is Vehicle vehicle)
            {
                return TopSpeed.Equals(vehicle.TopSpeed) &&
                        Weight.Equals(vehicle.Weight);
            }
        }

        public override int GetHashCode(){
            return HashCode.combine(TopSpeed, Weight); // itt a paraméterlistában a sorrendnek ugyan annak kell lennie mint az equals-ban, különben fura BUG-ok jöhetnek
        }

        protected bool Equals(Vehicle other) {
            public required int TopSpeed{get; set;}
            public required int Weight{get; set;}
            public override bool Equals(object? obj) 
            {
                if(obj==null) return false;
                if(ReferenceEquals(this, obj)) return true;
                if(obj is Vehicle vehicle)
                {
                    return TopSpeed.Equals(vehicle.TopSpeed) &&
                            Weight.Equals(vehicle.Weight);
                }
            }
        };

    internal class Car: Vehicle
    {
        public required byte NumberofSeets{get; set;}
        public required int BootSpace{get; set;}

        public override bool Equals(object? obj) 
        {
            if(obj==null) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj is Car vehicle)
            {
                return hash.Equals(obj) &&
                TopSpeed.Equals(vehicle.TopSpeed) &&
                Weight.Equals(vehicle.Weight);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), NumberofSeets, BootSpace);
        }
    };

    internal class Taxi: Car
    {
        public record() // megszabadít mindentől ami fentebb volt c# 10 után hasznos használni
    }

    // sharp lab - megmutatja mit generált bele a fordító a kódunkba

}

#endregion