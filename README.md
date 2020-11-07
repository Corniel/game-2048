# 2048

This implementation in C# contains a solver that does an okay job playing the
addictive 2048 game.

## Characteristics 
The Monte Carlo simulator plays about 6.9 million positions per second. When
run 200 times with max 100 ms per turn has the following characteristics.

|  Max | Count | Rel.  |
|-----:|------:|------:|
|  512 |     2 |  1.0% |
| 1024 |    39 | 19.5% |
| 2048 |   141 | 70.5% |
| 4096 |    18 |  9,0% |

Avg: 30,756.4
Max: 73,840
