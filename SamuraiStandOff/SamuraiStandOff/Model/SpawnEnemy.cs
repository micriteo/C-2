using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamuraiStandOf;

namespace SamuraiStandOff.Controllers
{
    internal class SpawnEnemy
    {
        int wave = 0;
        int wavePower = 0;
        List<Enemy> possibleEnemies;

        public SpawnEnemy()
        {
            possibleEnemies = new()
            {
                new Tank_Enemy(),
                new Melee_Enemy(),
                new Speed_Enemy()
            };
        }

        public List<Enemy> CreateWave(int waveNum)
        {
            wave = waveNum;
            wavePower = 10 * wave;

            List<Enemy> enemies = new();
            Random _random = new();
            while (wavePower != 0)
            {
                int rand = _random.Next(0, 3);
                int emnemyPwr = possibleEnemies[rand].PowerLevel;
                if (wavePower - emnemyPwr >= 0)
                {
                    wavePower -= emnemyPwr;
                    Enemy enemy = possibleEnemies[rand].Clone();
                    enemy.Health += wavePower;
                    Debug.WriteLine("EnemyType: "+ enemy +" HP: "+ enemy.Health);
                    enemies.Add(enemy);
                }
            }
            return enemies;
        }
    }
}