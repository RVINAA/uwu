# uwu | (An attempt to be a) Lightweight serializer

## 🎲 Info
I'm following the ECMA-404 (instead of JSON RFC which says ie. just a number is valid JSON.. meh).
Will only support common & value types and there are many optimizations pending..

## 📓 TODO
A lot of things, but we'll see xdd
(ie. support Guid, expose IEnumerable<> sz.. support KeyValue<>/nested dictionaries.. and maybe deserialization)

## 🚩 Benchmarks
I find it interesting to contrast the efficiency of what is coming out with other serializers that have been around for quite some time.<br>
I don't pretend to be equal to any of them, nor do I take it as something competitive.

#### [✘] IDictionary<string, string> to JSON.
I think I won't try to optimize or change this anymore.. btw, I know two of this are binary serializers.<br>
I'm benching with their main/defaults methods (handling if they generate dynamic stuff under-hood) just like a reference.

|                                      Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|-------------------------------------------- |------------:|---------:|---------:|-------:|-------:|----------:|
|             Serialize_Small_Dict_Newtonsoft |    539.1 ns |  0.77 ns |  0.68 ns | 0.1745 |      - |   1,464 B |
|                    Serialize_Small_Dict_Jil |    375.3 ns |  0.37 ns |  0.29 ns | 0.0620 |      - |     520 B |
|               Serialize_Small_Dict_Utf8Json |    246.8 ns |  0.27 ns |  0.26 ns | 0.0210 |      - |     176 B |
|  SerializeWithoutNullOrEmpty_Small_Dict_Uwu |    179.2 ns |  0.33 ns |  0.27 ns | 0.0210 |      - |     176 B |
|                    Serialize_Small_Dict_Uwu |    165.0 ns |  0.66 ns |  0.61 ns | 0.0210 |      - |     176 B |
|              SerializeUnsafe_Small_Dict_Uwu |    133.8 ns |  0.29 ns |  0.27 ns | 0.0181 |      - |     152 B |
|                                          🎔 |             |          |          |        |        |           |
|                   Serialize_Medium_Dict_Jil |  3,578.5 ns |  2.50 ns |  2.34 ns | 0.4997 | 0.0076 |   4,200 B |
|            Serialize_Medium_Dict_Newtonsoft |  3,286.9 ns |  3.67 ns |  2.87 ns | 0.5913 | 0.0076 |   4,968 B |
| SerializeWithoutNullOrEmpty_Medium_Dict_Uwu |  2,215.5 ns |  2.17 ns |  2.03 ns | 0.1945 |      - |   1,648 B |
|                   Serialize_Medium_Dict_Uwu |  1,971.4 ns |  1.97 ns |  1.74 ns | 0.1945 |      - |   1,648 B |
|              Serialize_Medium_Dict_Utf8Json |  1,825.4 ns | 13.58 ns | 12.71 ns | 0.1965 |      - |   1,648 B |
|             SerializeUnsafe_Medium_Dict_Uwu |  1,243.9 ns |  2.79 ns |  2.33 ns | 0.1202 |      - |   1,016 B |
|                                          🎔 |             |          |          |        |        |           |
|             Serialize_Large_Dict_Newtonsoft | 10,931.6 ns | 16.60 ns | 15.53 ns | 1.0529 | 0.0305 |   8,872 B |
|                    Serialize_Large_Dict_Jil |  8,953.9 ns | 10.40 ns |  8.68 ns | 0.9918 | 0.0153 |   8,360 B |
|  SerializeWithoutNullOrEmpty_Large_Dict_Uwu |  5,997.8 ns | 17.83 ns | 16.68 ns | 0.4349 |      - |   3,688 B |
|                    Serialize_Large_Dict_Uwu |  5,466.7 ns |  3.59 ns |  3.00 ns | 0.4349 |      - |   3,688 B |
|               Serialize_Large_Dict_Utf8Json |  5,373.8 ns | 14.91 ns | 13.95 ns | 0.4349 |      - |   3,688 B |
|              SerializeUnsafe_Large_Dict_Uwu |  3,288.5 ns | 12.60 ns | 11.79 ns | 0.2632 |      - |   2,216 B |

#### [✘] IDictionary<string, object> to JSON.
Temporal stuff until done. //< FIXME: Refactor this tests like previously ones..

|                               Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------------------------- |------------:|---------:|---------:|-------:|-------:|----------:|
|      SerializeDictionaryObj_Utf8Json |  2,243.4 ns | 13.07 ns | 12.23 ns | 0.1183 |      - |     992 B |
|    SerializeDictionaryObj_Newtonsoft |  3,302.5 ns |  5.59 ns |  4.95 ns | 0.4082 |      - |   3,416 B |
|           SerializeDictionaryObj_Uwu |  1,472.8 ns |  6.42 ns |  6.01 ns | 0.1411 |      - |   1,184 B |
|                                   🎔 |             |          |          |        |        |           |
|   SerializeDictionaryObjOne_Utf8Json |  3,303.6 ns | 12.86 ns | 12.03 ns | 0.1717 |      - |   1,456 B |
| SerializeDictionaryObjOne_Newtonsoft |  8,816.1 ns | 42.20 ns | 35.24 ns | 0.8545 |      - |   7,240 B |
|        SerializeDictionaryObjOne_Uwu |  4,102.2 ns |  5.39 ns |  5.05 ns | 0.3662 |      - |   3,064 B |

#### [✘] IEnumerable<object> to JSON.
Omw one day.