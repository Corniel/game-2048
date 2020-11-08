# 2048

This implementation in C# contains a solver that does an okay job playing the
addictive 2048 game.

## Characteristics 
The Monte Carlo simulator plays about 6.9 million positions per second. When
run 200 times with max 100 ms per turn has the following characteristics.

|  Max | Count | Rel.  |
|-----:|------:|------:|
| 1024 |     5 |  2.5% |
| 2048 |   108 | 54.0% |
| 4096 |    87 | 43,5% |
 
Avg: 48,523.7

Max: 80,872
