# uwu | (An attempt to be a) Lightweight serializer

## 📓 TODO
A lot of things, but we'll see xdd

## 🚩 Benchmarks
I find it interesting to contrast the efficiency of what is coming out with other serializers that have been around for quite some time.<br>
I don't pretend to be equal to any of them, nor do I take it as something competitive.

#### [✘] IDictionary<string, string> to JSON.

|                            Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|---------------------------------- |------------:|---------:|---------:|-------:|-------:|----------:|
|    SerializeDictionary_Newtonsoft |    523.0 ns |  0.74 ns |  0.65 ns | 0.1745 |      - |   1,464 B |
|           SerializeDictionary_Jil |    355.1 ns |  0.23 ns |  0.19 ns | 0.0620 |      - |     520 B |
|      SerializeDictionary_Utf8Json |    255.6 ns |  0.68 ns |  0.63 ns | 0.0210 |      - |     176 B |
|           SerializeDictionary_Uwu |    173.8 ns |  0.17 ns |  0.16 ns | 0.0210 |      - |     176 B |
|                                🎔 |             |          |          |        |        |           |
| SerializeDictionaryOne_Newtonsoft |  3,286.1 ns |  2.74 ns |  2.56 ns | 0.5913 | 0.0076 |   4,968 B |
|        SerializeDictionaryOne_Jil |  3,132.6 ns |  7.58 ns |  6.33 ns | 0.4997 | 0.0076 |   4,200 B |
|        SerializeDictionaryOne_Uwu |  2,050.0 ns |  1.30 ns |  1.15 ns | 0.1945 |      - |   1,648 B |
|   SerializeDictionaryOne_Utf8Json |  1,809.9 ns |  7.18 ns |  6.37 ns | 0.1965 |      - |   1,648 B |
|                                🎔 |             |          |          |        |        |           |
| SerializeDictionaryTwo_Newtonsoft | 10,871.8 ns |  8.63 ns |  7.21 ns | 1.0529 | 0.0305 |   8,872 B |
|        SerializeDictionaryTwo_Jil |  8,230.3 ns | 19.56 ns | 18.30 ns | 0.9918 | 0.0153 |   8,360 B |
|        SerializeDictionaryTwo_Uwu |  5,677.2 ns |  2.81 ns |  2.63 ns | 0.4349 |      - |   3,688 B |
|   SerializeDictionaryTwo_Utf8Json |  5,401.9 ns | 17.17 ns | 16.06 ns | 0.4349 |      - |   3,688 B |

#### [✘] IDictionary<string, object> to JSON.
On going?