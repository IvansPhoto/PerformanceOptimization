# About
This project is a real case from my work about the consequences of negligent writing a simple mapper. Because the code from the project cannot be shared, I created a very similar mapper for weather observations.
## Rules
The purpose of the mapper is to merge data from two arrays of the Input record: Temperature[] Temperatures and string[] Places into a third array of the Output record.
The resulting array should be the same size as the Temperatures array and should contain values and dates from the Temperatures array. Data for states and seasons fields should be received from the Places array. Each date in the Temperatures array is unique.
The Places array can be less than, equal to, or greater than the Temperatures array.
The Places array contains strings that can have one, two or three segments, with the ‘;’ delimiter. The first segment is a date in the format: month/day/year and the constant time stamp 00:00:00, the date is unique per array; this segment is always present. The second segment is two letters of a state and optional. The third segment is an abbreviation of a state and it and the delimiter are also optional. State and season abbreviations should be transformed into the full names of states and seasons, mappers should be case-insensitive.
Arrays should be matched by the date. If the Places array does not have a record with the corresponding date, or a record does not have the second and/or the third segment the states and/or seasons fields should be null.
## Results
The benchmark results are placed in the result folder.
This is a table from the basic benchmark.

| Method                | _n    | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0     | Gen1     | Gen2     | Allocated | Alloc Ratio |
|---------------------- |------ |---------:|----------:|----------:|------:|--------:|---------:|---------:|---------:|----------:|------------:|
| MapOptimized          | 10000 | 9.612 ms | 0.1916 ms | 0.2207 ms |  1.00 |    0.00 | 562.5000 | 468.7500 | 187.5000 |   4.36 MB |        1.00 |
| MapOptimizedFor       | 10000 | 9.420 ms | 0.1874 ms | 0.3232 ms |  0.97 |    0.04 | 687.5000 | 625.0000 | 312.5000 |   4.36 MB |        1.00 |
| MapOptimizedStruct    | 10000 | 5.206 ms | 0.0702 ms | 0.0622 ms |  0.54 |    0.02 | 656.2500 | 617.1875 | 492.1875 |   3.39 MB |        0.78 |
| MapOptimizedStructFor | 10000 | 5.214 ms | 0.1020 ms | 0.1134 ms |  0.54 |    0.02 | 656.2500 | 617.1875 | 492.1875 |   3.39 MB |        0.78 |