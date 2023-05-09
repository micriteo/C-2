<h1> Shogun Stand </h1>
<hr>
<h2> Introduction </h2>

In 16th century Japan during the Sengoku period, Daimyo X was a powerful lord tasked with stopping a skilled general named Sei Shapu and his army from conquering his castle. X's forces prepared for battle, knowing that failure would result in the execution of X's fiancée Yumi. After fighting over 100 waves of enemies, Taro emerged victorious and rescued Yumi. The shogun had no choice but to release her unharmed, and X married her in a grand wedding. Taro was hailed as a hero among his people for his bravery and cunning.


<h3> Game Concept </h3>
Shogun Stand is a 2D tower defense game that offers engaging gameplay mechanics deeply rooted in Japanese mythology. Players will strategically build, upgrade, and deploy various attack units inspired by historical Japanese warriors, such as samurai, archers, and ninjas, to fend off waves of mythical enemies including Yokai, Oni, and Tengu.

The game is based on the arcade style, that means if the player's health is 0,
then he has to start the game all over again.

The game features a diverse array of defense towers, each with unique abilities that can be enhanced through a skill tree system.

Players must carefully select the optimal combination of units and towers to exploit enemy weaknesses and synergize with their own strategy.

During gameplay, players will face intense boss fights that challenge their tactical skills and resource management. 

After defeating each boss, players are rewarded with a buff item to strengthen their defenses and a debuff item which will offer them an advantage over the tough enemies.
Shogun Stand also incorporates a progression system where players can unlock new units, towers, and abilities, allowing them to develop their own unique playstyles.

Embrace the challenge and test your strategic prowess in Shogun Stand, where captivating Japanese elements and dynamic game mechanics come together to create an unforgettable tower defense adventure

The game is developed using Godot 4 game engine and .NET 6. Godot engine utilizes its own custom UI-framework.


<h3> What is a Tower Defense game? </h3>
The tower defense genre is a subgenre of strategy video games that revolves 
around defending a designated area, typically a base or a path, from waves
of incoming enemies. 
Players strategically place various types of defensive structures, 
often referred to as "towers," along the enemies' path to obstruct, damage, or 
eliminate them before they reach their objective.
Each tower has unique abilities, strengths, and weaknesses, requiring players 
to carefully plan their strategy and make tactical decisions to counter different 
enemy types. As players progress, they earn in-game resources to upgrade or purchase 
new towers, enhancing their defenses against increasingly challenging enemy waves.
Tower defense games are known for their addictive gameplay, strategic depth, 
and often for their distinctive visual styles.


<h3> Art style and aesthetics Game world and level design </h3>

The game will feature pixelated graphics, but not simplified it should leave much to interpret to the user but give
enough information to tell the user the story, setting, game style, and most importantly, what's going on
while providing textures which are easy on performance which will allow many units to appear on screen without having
to render many highly complex enemies.

<h4>Art examples:</h4>

<p float="left">
  <img src="images/img_7.jpg" width="150" height="150" />
  <img src="images/img_8.jpg" width="150" height="150" /> 
  <img src="images/img_9.jpg" width="150" height="150" />
</p>

The game aesthetic will be a fictional version of the japanese sengoku, edo and meiji eras. This gives the game that 
cultural japanese vibe which the game will take place in. (See more info in appendix)

The world and level design being the same will feature a standard style of japanese forest. This can include things
such as rivers, cherry blossom trees and a handful of traditional architectures. The level will contain a winding path on which
enemies will pass to reach your castle.

<p float="left">
  <img src="images/img_10.png" width="150" height="150" />
  <img src="images/img_11.png" width="150" height="150" /> 
  <img src="images/img_12.png" width="150" height="150" />
</p>

<h3> Story and Narrative </h3>

Shogun Stand embraces Japanese thematics to offer players an immersive and culturally rich gaming experience. By incorporating elements of Japanese mythology, folklore, and history, the game transports players to a captivating world teeming with legendary creatures and iconic warriors. This unique setting not only enhances the game's visual appeal but also allows players to explore and appreciate the depth and nuances of Japanese culture while engaging in strategic and challenging gameplay, setting Shogun Stand apart from other tower defense games.
<h4> Unit Concept </h4>

<p>Dual Swordsman - Miyamoto Musashi</p>

![img_1.png](images/img_1.png)

<h4>Samurai - Tomoe Gozen </h3>

![img_3.png](images/img_3.png)


<h3> Main Screen </h3>


![img_4.png](images/img_4.png)


<h4> Main screen when hovering on the options </h4>

![img_5.png](images/img_5.png)


<h3> Game screen </h3>

Samurai standing on the side of the road ready for the enemy units to pass through and attack
the base.

![img_6.png](images/img_6.png)

<h3> Buff/debuff selection screen </h3>

After having defeated a boss, a player can choose a buff and a debuff to alter gameplay.

![debuff_screen_mockup.png](images/debuff_screen_mockup.png)

<h3> Game Mechanics </h3>

<h4> Waves </h4>
Waves or Rounds are the main challenge of the game and the main source of income.
Each wave consists of a number of enemies that spawn at a certain time, and every 5 waves a boss will appear.
The wave ends when all enemies are dead.
The next wave starts after a certain amount of time has passed or when the payer presses a button to continue.

<h4> Currency </h4>
The player will be able to acquire gold by killing enemies. The amount of gold acquired depends on the type of enemy killed.
The player will be able to spend the gold on upgrading their units.

<h4> Upgrading units </h4>
Throughout the game, the player will be able to upgrade their units. That can be done by spending the gold acquired from killing enemies.
The player will be able to upgrade the damage, range, and attack speed of their units.

<h4> Scrolls </h4>
Scrolls are another way for the player's units to become stronger.
After a "boss wave", the player will be presented with a choice of 3 scrolls.
Each scroll will have a different effect, either on the player's units or the enemies.

<h3> Game Progression </h3>

As the game progresses, the enemies will become stronger and the player will have to upgrade their units to keep up.
The higher the unit's level, the more expensive it will be to upgrade it. The player will have to choose which units to upgrade and which to leave behind.
Of course, the stronger the enemies, the more gold they will drop. The player will have to find the right balance between upgrading their units and buying new ones.

# Class Diagram
![class.png](images/class.png)


# Development Timeline 

## Pre-Production (Weeks 1-2)

- **Week 1**: Concept development, initial game design document, and team formation
- **Week 2**: Research and planning (game mechanics, art style, audio, and platform requirements)

## Production (Weeks 3-7)

### Art and Audio

- **Week 3-4**: Create character models, animations, and environment assets
- **Week 5**: Design UI elements, including menus, HUD, and in-game prompts
- **Week 6**: Compose music tracks, sound effects, and implement audio

## Programming

- **Week 3-4**: Implement core game mechanics, enemy AI, tower functionality, and pathfinding
- **Week 5**: Develop basic level design and progression system
- **Week 6**: Implement player data management and integrate art and audio assets
- **Week 7**: Implement UI and menus, create a basic tutorial level (if applicable)

## Testing and Polishing (Weeks 8-9)

- **Week 8**: Internal playtesting, bug fixing, and gathering feedback
- **Week 9**: Final bug fixes, optimizations, polish, and prepare for release

# Project Team Members

1. **Mathew Shardin** (Project Leader)  
   Email: mathew.shardin@student.nhlstenden.com  
   The role of a Project Leader is to guide and lead the rest of the team to a better outcome and to a more successful result of the project, by explaining the details of every task that needs to be achieved and giving them feedback on their results to improve. 

2. **Miroslav Penchev** (Secretary)  
   Email: miroslav.penchev@student.nhlstenden.com  
   The purpose of Minutes is to have notes on every single important detail when we are gathering information in meetings with the clients or in team meetings which will guide us on what task needs to be done.

3. **Dimitri Vastenhout** (Co-Team Leader)  
   Email: dimitri.vastenhout@student.nhlstenden.com  
   The role of the Co-leader is to take the role of the team leader when the leader is not available.

4. **Teodor Folea** (Quality Control)  
   Email: teodor.folea@student.nhlstenden.com   
   The role of quality control is to establish and ensure a high quality of work is produced and delivered by the team in regard to documentation and software.

5. **Costache Alin** (Minutes taker)  
   Email: alin.costache@student.nhlstenden.com   
   A minute taker oversees taking the minutes of the meeting. These minutes are required to provide a formal account of who was at the meeting, what was discussed, what actions were agreed upon, and who would carry out these actions.

<h1>Appendix</h1>


<h3>Japanese Time Periods</h3>

<h4>Sengoku Period</h4>

The sengoku jidai (Warring states era) takes place during most of the 15th and 16th century.
This period consisted of many small and large clans vying to take control of territory.

<h4>Edo Period</h4>

The edo jidai, ranging from 1603 to 1867. Coming directly after the warring states era, the country was unified
by the three great unifiers of Japan (In order: Oda Nobunaga, Toyotomi Hideyoshi, Tokugawa Ieyasu), which started this era. The edo
period brought 250 years of stability, economic growth, arts and culture.

<h4>Meiji Restoration</h4>

The end of the edo period due to an alliance of two reformists, which brought the power back to
the emperor (the shogunate had ruled Japan for the edo period). The emperor opened up Japan after its long
isolationist period, this caused an inflow of new technology and culture. This was the most important step into modernization of Japan.

<h1>Work Cited</h1>

Wikipedia contributors. (2023). Sengoku period. Wikipedia. https://en.wikipedia.org/wiki/Sengoku_period

Wikipedia contributors. (2023b). Edo period. Wikipedia. https://en.wikipedia.org/wiki/Edo_period

Wikipedia contributors. (2023b). Meiji Restoration. Wikipedia. https://en.wikipedia.org/wiki/Meiji_Restoration

Wikipedia contributors. (2023a). Japanese architecture. Wikipedia. https://en.wikipedia.org/wiki/Japanese_architecture
