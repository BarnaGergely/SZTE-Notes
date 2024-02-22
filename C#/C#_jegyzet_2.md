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

```c#
public class PontOsztaly {
    public int x, y;
};

public struct Pont {
    public int X {get; set;}
    public int Y {get; set;}

    // TODO: contructor
};

Pont pont; // nem kell new
pont.x = 0;    // ha nem adunk meg valamit, alapértelemezett lesz

PontOsztaly p = new PontOsztaly(5, 6);
PontOsztaly p = new (5, 6);
```



