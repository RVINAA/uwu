# uwu | (An attempt to be a) Lightweight serializer

## 🎲 Info
I'm following the ECMA-404 (instead of JSON RFC which says ie. just a number is valid JSON.. meh).
Will only support common & value types and there are many optimizations pending..

## 📓 TODO
A lot of things, but we'll see xdd

## 🚩 Benchmarks
I find it interesting to contrast the efficiency of what is coming out with other serializers that have been around for quite some time.<br>
I don't pretend to be equal to any of them, nor do I take it as something competitive.
.NET 5 - x64 Jit ~ Intel I7-10700KF 5.1Ghz

#### [✘] IDictionary<string, string> to JSON.
I think I won't try to optimize or change this anymore.. btw, I know two of this are binary serializers.<br>
I'm benching with their main/defaults methods (handling if they generate dynamic stuff under-hood) just like a reference.

|                                      Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|-------------------------------------------- |------------:|---------:|---------:|-------:|-------:|----------:|
|             Serialize_Small_Dict_Newtonsoft |    539.1 ns |  0.77 ns |  0.68 ns | 0.1745 |      - |   1,464 B |
|                 Serialize_Small_Dict_System |    401.8 ns |  5.81 ns |  5.43 ns | 0.0391 |      - |     328 B |
|                    Serialize_Small_Dict_Jil |    375.3 ns |  0.37 ns |  0.29 ns | 0.0620 |      - |     520 B |
|               Serialize_Small_Dict_Utf8Json |    246.8 ns |  0.27 ns |  0.26 ns | 0.0210 |      - |     176 B |
|  SerializeWithoutNullOrEmpty_Small_Dict_Uwu |    179.2 ns |  0.33 ns |  0.27 ns | 0.0210 |      - |     176 B |
|                    Serialize_Small_Dict_Uwu |    165.0 ns |  0.66 ns |  0.61 ns | 0.0210 |      - |     176 B |
|              SerializeUnsafe_Small_Dict_Uwu |    162.5 ns |  0.23 ns |  0.19 ns | 0.0210 |      - |     176 B |
|                                          🎔 |             |          |          |        |        |           |
|                   Serialize_Medium_Dict_Jil |  3,578.5 ns |  2.50 ns |  2.34 ns | 0.4997 | 0.0076 |   4,200 B |
|                Serialize_Medium_Dict_System |  3,317.4 ns |  1.44 ns |  1.34 ns | 0.2251 |      - |   1,904 B |
|            Serialize_Medium_Dict_Newtonsoft |  3,286.9 ns |  3.67 ns |  2.87 ns | 0.5913 | 0.0076 |   4,968 B |
| SerializeWithoutNullOrEmpty_Medium_Dict_Uwu |  2,215.5 ns |  2.17 ns |  2.03 ns | 0.1945 |      - |   1,648 B |
|                   Serialize_Medium_Dict_Uwu |  1,971.4 ns |  1.97 ns |  1.74 ns | 0.1945 |      - |   1,648 B |
|             SerializeUnsafe_Medium_Dict_Uwu |  1,952.8 ns |  4.88 ns |  3.81 ns | 0.1945 |      - |   1,648 B |
|              Serialize_Medium_Dict_Utf8Json |  1,825.4 ns | 13.58 ns | 12.71 ns | 0.1965 |      - |   1,648 B |
|                                          🎔 |             |          |          |        |        |           |
|             Serialize_Large_Dict_Newtonsoft | 10,931.6 ns | 16.60 ns | 15.53 ns | 1.0529 | 0.0305 |   8,872 B |
|                 Serialize_Large_Dict_System |  9,278.1 ns | 25.11 ns | 23.49 ns | 0.4730 |      - |   4,040 B |
|                    Serialize_Large_Dict_Jil |  8,953.9 ns | 10.40 ns |  8.68 ns | 0.9918 | 0.0153 |   8,360 B |
|  SerializeWithoutNullOrEmpty_Large_Dict_Uwu |  5,997.8 ns | 17.83 ns | 16.68 ns | 0.4349 |      - |   3,688 B |
|                    Serialize_Large_Dict_Uwu |  5,466.7 ns |  3.59 ns |  3.00 ns | 0.4349 |      - |   3,688 B |
|              SerializeUnsafe_Large_Dict_Uwu |  5,394.8 ns |  4.55 ns |  4.03 ns | 0.4349 |      - |   3,688 B |
|               Serialize_Large_Dict_Utf8Json |  5,373.8 ns | 14.91 ns | 13.95 ns | 0.4349 |      - |   3,688 B |

#### [✘] IDictionary<string, object> to JSON.
Will bench against a dictionary with common (supported) types; against a dictionary with arrays/inner dictionary of common types.. and union of both.<br>
For Jil, I'm using SerializeDinamyc<> method as the other one (Serialize<>) returns {} for each boxed value..<br>
and like the other benchs, I'm doing in the global setup method a pre-call for each benchmark in order to 'cache' stuff.

|                                               Method |      Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Allocated |
|----------------------------------------------------- |----------:|----------:|----------:|-------:|-------:|----------:|
|             Serialize_Dictionary_Of_Common_Types_Jil |  4.316 us | 0.0112 us | 0.0105 us | 0.5722 |      - |   4,800 B |
|      Serialize_Dictionary_Of_Common_Types_Newtonsoft |  3.270 us | 0.0084 us | 0.0078 us | 0.4005 | 0.0038 |   3,368 B |
|          Serialize_Dictionary_Of_Common_Types_System |  2.888 us | 0.0089 us | 0.0074 us | 0.1183 |      - |     992 B |
|        Serialize_Dictionary_Of_Common_Types_Utf8Json |  2.218 us | 0.0077 us | 0.0068 us | 0.1106 |      - |     936 B |
|             Serialize_Dictionary_Of_Common_Types_Uwu |  1.402 us | 0.0012 us | 0.0011 us | 0.1450 |      - |   1,224 B |
|                                                   🎔 |             |          |          |        |        |           |
| Serialize_Dictionary_Of_Ienumerable_Types_Newtonsoft | 13.352 us | 0.0358 us | 0.0318 us | 1.3885 | 0.0305 |  11,736 B |
|        Serialize_Dictionary_Of_Ienumerable_Types_Jil | 11.065 us | 0.0201 us | 0.0188 us | 1.4801 | 0.0305 |  12,488 B |
|     Serialize_Dictionary_Of_Ienumerable_Types_System |  8.666 us | 0.0267 us | 0.0249 us | 0.3204 |      - |   2,800 B |
|        Serialize_Dictionary_Of_Ienumerable_Types_Uwu |  6.354 us | 0.0059 us | 0.0052 us | 0.6104 |      - |   5,168 B |
|   Serialize_Dictionary_Of_Ienumerable_Types_Utf8Json |  6.286 us | 0.0217 us | 0.0193 us | 0.3128 |      - |   2,672 B |
|                                                   🎔 |             |          |          |        |        |           |
|      Serialize_Dictionary_Of_Mixed_Values_Newtonsoft | 16.262 us | 0.0407 us | 0.0381 us | 1.5564 | 0.0305 |  13,144 B |
|             Serialize_Dictionary_Of_Mixed_Values_Jil | 15.301 us | 0.0556 us | 0.0493 us | 1.8616 | 0.0305 |  15,680 B |
|          Serialize_Dictionary_Of_Mixed_Values_System | 11.674 us | 0.0497 us | 0.0440 us | 0.4120 |      - |   3,552 B |
|        Serialize_Dictionary_Of_Mixed_Values_Utf8Json |  8.742 us | 0.0403 us | 0.0377 us | 0.4120 |      - |   3,520 B |
|             Serialize_Dictionary_Of_Mixed_Values_Uwu |  7.779 us | 0.0056 us | 0.0052 us | 0.7477 |      - |   6,312 B |

#### [✘] IEnumerable<object> to JSON.
Omw one day.