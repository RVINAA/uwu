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

|                               Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------------------------- |------------:|---------:|---------:|-------:|-------:|----------:|
|    SerializeDictionaryStr_Newtonsoft |    530.0 ns |  0.59 ns |  0.52 ns | 0.1745 |      - |   1,464 B |
|           SerializeDictionaryStr_Jil |    361.4 ns |  0.24 ns |  0.20 ns | 0.0620 |      - |     520 B |
|      SerializeDictionaryStr_Utf8Json |    254.7 ns |  0.60 ns |  0.56 ns | 0.0210 |      - |     176 B |
|           SerializeDictionaryStr_Uwu |    176.1 ns |  0.15 ns |  0.14 ns | 0.0210 |      - |     176 B |
|                                   🎔 |             |          |          |        |        |           |
| SerializeDictionaryStrOne_Newtonsoft |  3,280.7 ns |  4.06 ns |  3.80 ns | 0.5913 | 0.0076 |   4,968 B |
|        SerializeDictionaryStrOne_Jil |  3,205.2 ns |  9.47 ns |  8.86 ns | 0.4997 | 0.0076 |   4,200 B |
|        SerializeDictionaryStrOne_Uwu |  2,099.4 ns |  1.56 ns |  1.46 ns | 0.1945 |      - |   1,648 B |
|   SerializeDictionaryStrOne_Utf8Json |  1,745.1 ns | 10.56 ns |  9.36 ns | 0.1965 |      - |   1,648 B |
|                                   🎔 |             |          |          |        |        |           |
| SerializeDictionaryStrTwo_Newtonsoft | 10,706.3 ns | 11.99 ns | 11.21 ns | 1.0529 | 0.0305 |   8,872 B |
|        SerializeDictionaryStrTwo_Jil |  8,417.3 ns |  6.55 ns |  5.47 ns | 0.9918 | 0.0153 |   8,360 B |
|        SerializeDictionaryStrTwo_Uwu |  5,651.7 ns |  2.55 ns |  2.39 ns | 0.4349 |      - |   3,688 B |
|   SerializeDictionaryStrTwo_Utf8Json |  5,370.0 ns | 14.49 ns | 12.84 ns | 0.4349 |      - |   3,688 B |

#### [✘] IDictionary<string, object> to JSON.
Temporal stuff until done.

|                               Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------------------------- |------------:|---------:|---------:|-------:|-------:|----------:|
|      SerializeDictionaryObj_Utf8Json |  2,243.4 ns | 13.07 ns | 12.23 ns | 0.1183 |      - |     992 B |
|    SerializeDictionaryObj_Newtonsoft |  3,302.5 ns |  5.59 ns |  4.95 ns | 0.4082 |      - |   3,416 B |
|           SerializeDictionaryObj_Uwu |  1,472.8 ns |  6.42 ns |  6.01 ns | 0.1411 |      - |   1,184 B |
|                                   🎔 |             |          |          |        |        |           |
|   SerializeDictionaryObjOne_Utf8Json |  3,303.6 ns | 12.86 ns | 12.03 ns | 0.1717 |      - |   1,456 B |
| SerializeDictionaryObjOne_Newtonsoft |  8,816.1 ns | 42.20 ns | 35.24 ns | 0.8545 |      - |   7,240 B |
|        SerializeDictionaryObjOne_Uwu |  4,102.2 ns |  5.39 ns |  5.05 ns | 0.3662 |      - |   3,064 B |