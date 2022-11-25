<div align="center">
<img height="100" width="550" src="readme_assets/logogame.png" />
</div>
<br>
<div align = "center">
<a href="https://github.com/topics/unity">
<img height="20" width="60" src="readme_assets/unity-logo.jpg" />
</a>
</div>
<br>
<p align="center"> One of my first Unity games – <span style="color:orange"><b>2021</b></span></p>

#

<br>

<div align="center">

![GitHub top language](https://img.shields.io/github/languages/top/daridakr/oscarcat?style=flat-square)
![GitHub repo file count](https://img.shields.io/github/directory-file-count/daridakr/oscarcat?style=flat-square)
![GitHub watchers](https://img.shields.io/github/watchers/daridakr/oscarcat?style=flat-square)
![GitHub Repo stars](https://img.shields.io/github/stars/daridakr/oscarcat?style=flat-square)
![GitHub last commit](https://img.shields.io/github/last-commit/daridakr/oscarcat?style=flat-square)
![GitHub](https://img.shields.io/github/license/daridakr/oscarcat?style=flat-square)
</div>

<br>

<div align="center">
<b>Arcade 2D platformer</b> about <b>amusing adventures</b> of my cat Oscar. In the development process used a large amount of material on programming and algorithmic. The skills of mastering the methods of <b>robust programming</b> techniques and <b>development efficiency</b> in Visual Studio using C# and Unity were honed.

<br>

<img src="readme_assets/oscarcat.jpg" />
</div>

<br>

## Quick navigation

- [](#)
  - [Quick navigation](#quick-navigation)
  - [About the game](#about-the-game)
  - [List of mechanics](#list-of-mechanics)
  - [Event System](#event-system)
  - [Events description](#events-description)
  - [Mechanics description](#mechanics-description)
  - [Demonstration](#demonstration)
  - [Download](#download)


<br>

## About the game

With short, but **intense** and **exciting** gameplay, Oscarcat amuses with its mood, unique to each level, and entertains with the adventure of the protagonist – the cat Oscar, who faces `various challenges` and `rivals` on the way to the highest goal since his birth. Oscarcat gives the player interesting and possibly new information, expanding his knowledge of cats. During the game, the player will get to know these animals from the moment they are born. The gameplay consists of `passing the levels` and stages of the game, which are compared with the **periods of life** of a cat.

<br>

## List of mechanics
  - **Character movement** – running and jumping
  - Сharacter control via **mobile joystick**
  - **Character Interaction System** with Enemies and NPC
  - Enemy behavior
  - A character's loss of life
  - **Level system** – transition, opening, resetting
  - **Gathering** on Levels
  - Using **Abilities** and **Bonuses**
  - Hunger, Thirst and Health Scorecard – Waste and Replenishment
  - Saving game progress
  - Game Settings
  - Help system
  - Learning at play

<br>

## Event System 
В папке `Core` содержатся скрипты, образующие «ядро» игры, которые направлены на реализацию системы событий в игре:
  -   `HeapQueue` Класс, описывающий упорядоченную коллекцию очередей. Это необходимо для создания очереди событий;
  -   `Simulation` Основной класс для работы с событиями;
  -   `Simulation.Event` Класс, описывающий событие;
  -   `Simulation.InstanceRegister` Данный класс предоставляет контейнер для создания синглтонов для любого другого класса в пределах области действия Simulation. Он используется для хранения моделей симуляции и классов конфигурации.

Скрипт Simulation реализует паттерн Дискретно-событийное моделирование. Он предоставляет методы для создания нового события определенного типа, очистки очереди событий, планирования событий, перенесение существующих событий, а также для получения количества оставшихся событий. Все события объединяются в пул с емкостью 4 экземпляра по умолчанию.

Скрипт HeapQueue предоставляет всегда упорядоченную коллекцию очередей. Описанный класс предоставляет методы для добавления и удаления элементов, а такие методы как SiftDown и SiftUp позволяют отсортировать элементы очереди в нужном направлении.

Таким образом, благодаря данному скрипту в игре «Oscarcat» можно легко запускать различные события, как это было сделано с поражением главного героя, его возрождением, смертью противников, вхождением персонажа в «мертвую зону», столкновением персонажа с противниками и т.п. Это делается с помощью метода планирования событий Schedule.

Так запускается событие смерти персонажа – Schedule<PlayerDeath>().

Имеется также скрипт Simulation.Event, в котором есть метод Precondition, используемый для проверки необходимости выполнения события, поскольку условия в симуляции могли измениться с тех пор, как событие было первоначально запланировано.

<br>

## Events description

В папке `Gameplay` содержаться все скрипты, описывающие события, связанные с игровым процессом:
  -   `EnablePlayerInput` Событие для контроля за возможностью управления персонажем игроком;
  -   `EnemyDeath` Событие, срабатывающее при смерти соперника;
  -   `PlayerDeath` Событие, срабатывающее при смерти персонажа;
  -   `PlayerEnemyCollision` Событие, срабатывающее при столкновении игрока с противником;
  -   `PlayerEnteredDeathZone` Событие, срабатывающее, когда игрок входит в триггер с компонентом DeathZone;
  -   `PlayerEnteredVictoryZone` Событие, срабатывающее, когда игрок входит в триггер с компонентом VictoryZone;
  -   `PlayerJumped` Событие, срабатывающее, когда игрок совершает прыжок;
  -   `PlayerLanded` Событие, срабатывающее, когда персонаж игрока приземляется после нахождения в воздухе;
  -   `PlayerSpawn` Событие, срабатывающее, когда игрок возрождается после смерти;
  -   `PlayerStopJump` Событие, срабатывающее, когда игрок прекращает прыгать и приземляется на землю;
  -   `PlayerTokenCollision` Событие, срабатывающее, когда игрок сталкивается с токеном;
  -   `ThristUpper` Скрипт, пополняющий жажду персонажа, когда игрок входит в соответствующий триггер.

С помощью скрипта [EnablePlayerInput](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Gameplay/EnablePlayerInput.cs) можно контролировать возможность управления персонажем следующим образом:


```
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            player.controlEnabled = true;
        }
    }
```

Здесь реализуется абстрактный метод родителя Simulation.Event – Execute(). В нем включается возможность управления персонажем.

Для обработки смерти соперника используется событие [EnemyDeath](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Gameplay/EnemyDeath.cs):

```
  public class EnemyDeath : Simulation.Event<EnemyDeath>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            enemy._collider.enabled = false;
            enemy.control.enabled = false;
            if (enemy._audio && enemy.ouch)
                enemy._audio.PlayOneShot(enemy.ouch);
        }
    }
```

У врага отключается коллайдер и контроллер анимации, что создает интересный эффект падения соперника вниз после смерти. Здесь также проигрывается звук смерти противника.

Для обработки смерти персонажа используется событие [PlayerDeath](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Gameplay/PlayerDeath.cs):

```
  public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
      ...
        public override void Execute()
        {
            var player = model.player;

            if (player.health.IsAlive)
            {
                if (player.HealthBar.CurrentValue <= 0)
                {
                    player.health.Die();

                    if (player.CountOfLives != 0)
                    {
                        Image currentHealthImage = player.HealthImages[player.CountOfLives - 1];
                        Object.Destroy(currentHealthImage);

                        if (player.CountOfLives == 1)
                        {
                            SceneManager.LoadScene("LevelMap");
                        }

                        player.CountOfLives--;
                    }

                    model.virtualCamera.m_Follow = null;
                    model.virtualCamera.m_LookAt = null;
                    player.controlEnabled = false;

                    if (player.audioSource && player.ouchAudio)
                        player.audioSource.PlayOneShot(player.ouchAudio);

                    player.animator.SetTrigger("hurt");
                    player.animator.SetBool("dead", true);

                    Simulation.Schedule<PlayerSpawn>(2);
                }
                else
                {
                    if (player.audioSource && player.ouchAudio)
                        player.audioSource.PlayOneShot(player.ouchAudio);

                    player.animator.SetTrigger("hurt");
                    player.HealthBar.CurrentValue -= 20;
                }
            }
        }
    }
```

Количество жизней – это количество попыток игрока пройти уровень. Когда здоровье персонажа достигает нуля и менее, игрок должен потерять одну жизнь. Если он теряет все – он проигрывает, его прогресс в данном уровне сбрасывается, и игрок начинает сначала.

Чтобы сбросить уровень при проигрыше, в момент потери жизни необходимо проверить и удостоверится последнюю ли жизнь теряет игрок. Если да, то он перенаправляется в сцену «LevelMap», теряя все свои очки и прогресс прохождения текущего уровня. При последующем заходе в этот уровень, он начнёт сначала.

Таким образом в игре была реализована система событий.

<br>

## Mechanics description
В папке Mechanics описываются механики игры с помощью следующих скриптов:
  -   `KinematicObject` Базовый класс для наследования игрока. Реализует игровую физику для некоторого игрового объекта;
  -   `PlayerController` Основной класс, используемый для реализации управления игроком;
  -   `EnemyController` Класс для описания врагов;
  -   `TokenInstance` Этот класс содержит данные, необходимые для реализации механики сбора корма;
  -   `TokenController` Этот класс анимирует все экземпляры токенов в сцене;
  -   `DeathZone` Класс, отслеживающий триггер на вхождение игрока в «мёртвую зону»;
  -   `VictoryZone` Класс, отслеживающий триггер на вхождение игрока в зону окончания уровня;
  -   `PatrolPath` Класс, предназначенный для создания пути патрулирования врагов – двух точек, между которыми они будут перемещаться;
  -   `PatrolPath.Mover` Осуществляет колебания врагов между начальной и конечной точками пути с определенной скоростью.

Базовый класс [KinematicObject](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Mechanics/KinematicObject.cs) имеет все необходимые поля и методы для реализации игровой физики игрока. В полях данного класса хранятся точные необходимые значения для всех вычислений физики при движении, отскакивании, перемещении и прыжках игрока.

Для реализации отскакивания персонажа от врагов используется метод Bounce:

```
  public void Bounce(float value) 
  {
    velocity.y = value;
  }
```

Он определяет вертикальную скорость объекта, за счёт чего персонаж подскакивает вверх. У данного метода есть перегрузка для определения скорости объекта в определённом направлении:

```
  public void Bounce(Vector2 dir)
  {
    velocity.y = dir.y;
    velocity.x = dir.x;
  }
```

метод PerformMovement описывает реализацию движения персонажа:

```
        void PerformMovement(Vector2 move, bool yMovement)
        {
            var distance = move.magnitude;

            if (distance > minMoveDistance)
            {
                var count = body.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

                for (var i = 0; i < count; i++)
                {
                    var currentNormal = hitBuffer[i].normal;

                    if (currentNormal.y > minGroundNormalY)
                    {
                        IsGrounded = true;
                        
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    if (IsGrounded)
                    {
                        var projection = Vector2.Dot(velocity, currentNormal);
                        
                        if (projection < 0)
                        {
                            velocity = velocity - projection * currentNormal;
                        }
                    }
                    else
                    {
                        velocity.x *= 0;
                        velocity.y = Mathf.Min(velocity.y, 0);
                    }

                    var modifiedDistance = hitBuffer[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            body.position = body.position + move.normalized * distance;
        }
```

Необходимо проверить столкнулся ли с чем-нибудь персонаж в текущем направлении движения и сохранить количество столкновений для последующей обработки в цикле. Если текущая поверхность достаточно плоская, чтобы на нее можно было приземлиться персонажу, то персонаж успешно приземляется на неё. В случае, если персонаж находится в воздухе и сталкивается с чем-либо необходимо отменить вертикальное движение вверх и сбросить горизонтальную скорость.

[PlayerController](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Mechanics/PlayerController.cs) наследуется от класса KinematicObject. Это основной класс, используемый для реализации управления игроком. Он реализует паттерн Singleton для возможности обращения к единой сущности и содержит в себе все необходимые поля для определения всех параметров персонажа. Также описаны все необходимые свойства.

<br>

## Demonstration

<div style='position:relative; padding-bottom:calc(54.27% + 44px)'><iframe src='https://gfycat.com/ifr/BestActualAmericancrow' frameborder='0' scrolling='no' width='100%' height='100%' style='position:absolute;top:0;left:0;' allowfullscreen></iframe></div>

![OscarDemo](https://gfycat.com/bestactualamericancrow)

<br>

## Download
Download [Oscarcat.apk](https://drive.google.com/file/d/1pWFiWPu3CZSdQWymcw6B24ji-75C8AP4/view?usp=share_link)

<div align="center">
<img height="300" width="360" src="readme_assets/oscarChar.png" />
</div>

