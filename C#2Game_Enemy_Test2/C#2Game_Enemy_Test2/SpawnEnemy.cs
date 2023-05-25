using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_2Game_Enemy_Test2
{
    internal class SpawnEnemy
    {
        int wave = 0;
        int wavePower = 0;
        List<EnemyV2> possibleEnemies;

        public SpawnEnemy(int wave)
        {
            this.wave = wave;
            wavePower = this.wave * 10;
            possibleEnemies = new()
            {
                new Tank_Enemy(),
                new Melee_Enemy(),
                new Speed_Enemy()
            };
        }

        public List<EnemyV2> CreateWave()
        {
            List<EnemyV2> enemies = new();
            Random _random = new();
            while (wavePower != 0)      
            {
                int rand = _random.Next(0, 3);
                int emnemyPwr = possibleEnemies[rand].PowerLevel;
                if (wavePower - emnemyPwr >= 0)
                {
                    wavePower -= emnemyPwr;
                    enemies.Add(possibleEnemies[rand].Clone());
                }
            }
            return enemies;
        }
    }
}
