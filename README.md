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
</div>

<br>

<div align="center">
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

With short, but **intense** and **exciting** gameplay, Oscarcat amuses with its mood, unique to each level, and entertains with the adventure of the protagonist – the cat Oscar, who faces `various challenges and rivals` on the way to the highest goal since his birth. Oscarcat gives the player interesting and possibly new information, expanding his knowledge of cats. During the game, the player will get to know these animals from the moment they are born. The gameplay consists of `passing the levels` and stages of the game, which are compared with the **periods of life** of a cat.

<br>

## List of mechanics
  - **Character physics movement** – move, bounce and jump
  - Сharacter control via **mobile joystick**
  - Character's loss of life
  - Hunger, Thirst and Health **Scorecard** – Waste and Replenishment
  - Enemy behavior - **move and attack**
  - **Patrol path** of enemies
  - **Character Interaction System** with Enemies and NPC
  - Using **Abilities** and **Bonuses**
  - **Level system** – transition, opening, resetting
  - **Token gathering** on levels
  - **Saving** game progress
  - Game Settings
  - Help system
  - Learning at play

<br>

## Event System 
The `Core` folder contains the scripts that form the **"core"** of the game, which are aimed at implementing the **system of events** in the game:
  -   `HeapQueue` Class that describes an **ordered queue collection**. This is required to create an event queue;
  -   `Simulation` The **main class** for working with events;
  -   `Simulation.Event` The class describing **the event**;
  -   `Simulation.InstanceRegister` This class provides a **container** for creating **singletons** for any other class within the **Simulation** scope. It is used to store simulation models and configuration classes.

The [Simulation](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Core/Simulation.cs) script implements the **Discrete-event simulation pattern**. It provides methods for creating a new event of a specific type, clearing the event queue, scheduling events and migrating existing events. All events are pooled with the default capacity of **4 instances**.

The [HeapQueue](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Core/HeapQueue.cs) script provides an **always-ordered** collection of queues. The class provides methods for adding and removing elements, and methods such as `SiftDown()` and `SiftUp()` allow you to sort queue elements in the desired direction.

Thus, due to this scripts in the game "Oscarcat" you can easily initiate various events, like the defeat of the main character, his revival, the death of enemies, entering the character in the «dead zone», the collision of the character with enemies, etc. This is done using the **Schedule method** of scheduling events. So, for example, the event of a character's death is triggered - `Schedule<PlayerDeath>()`.

There is also a [Simulation.Event](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Core/Simulation.Event.cs) script that contains the `Precondition()` method used to check if the event needs to be executed, as the conditions in the simulation may have changed since the event was originally scheduled.

<br>

## Events description

The `Gameplay` folder contains all scripts describing the **events** related to gameplay:
  -   `EnablePlayerInput` The event responsible for the ability to control the character;
  -   `EnemyDeath` Event triggered by the death of an enemy;
  -   `PlayerDeath` Event triggered by the death of character;
  -   `PlayerEnemyCollision` Event triggered when a player collides with an enemy;
  -   `PlayerEnteredDeathZone` Event triggered when a player enters a trigger with a **DeathZone** component;
  -   `PlayerEnteredVictoryZone` Event triggered when a player enters the trigger with the **VictoryZone** component;
  -   `PlayerJumped` Event triggered when a player makes a jump;
  -   `PlayerLanded` Event triggered when a player character lands after being in the air;
  -   `PlayerSpawn` Event triggered when a player is revived after death;
  -   `PlayerStopJump` Event triggered when a player stops jumping and lands on the ground;
  -   `PlayerTokenCollision` Event triggered when a player collides with a token;
  -   `ThristUpper` Script that adds to the character’s thirst when the player enters the appropriate trigger.

Using the [EnablePlayerInput](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Gameplay/EnablePlayerInput.cs) script, you can manage your **character control** as follows:

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

It implements the abstract parent method Simulation.Event - `Execute()`. It includes the ability to control the character.

The [EnemyDeath](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Gameplay/EnemyDeath.cs) event is used to handle enemy death:

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
The enemy's collider and animation controller are disabled, making an interesting effect of the enemy falling down after death. The sound of enemy death is also played here.

The [PlayerDeath](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Gameplay/PlayerDeath.cs) event is used to handle character death:

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

The **count of lives** is the count of player attempts to complete the level. When a character’s **health** reaches zero or less, the player must lose one life. If he loses everything - he loses, his progress in this level is reset and the player starts over.

To reset the level in a losing case, at the moment of loss of life, you need to check and make sure if it's the last life the player loses. If so, he is redirected to the `LevelMap` scene, losing all his points and progress through the current level. The next time he enters this level, it will start over.

Thus the **event system** was implemented in the game.

<br>

## Mechanics description
The `Mechanics` folder describes the mechanics of the game using the following scripts:
  -   `KinematicObject` Base class for a player’s inheritance. Implements game **physics** for some game object;
  -   `PlayerController` Primary class used to implement **player control**;
  -   `EnemyController` The class for describing **enemies**;
  -   `TokenInstance` This class contains the values needed to implement the **feed collecting**;
  -   `TokenController` This class **animates** all token instances in the scene;
  -   `DeathZone` The class that tracks the trigger of the player’s entry into the **dead zone**;
  -   `VictoryZone` The class that tracks the trigger of the player’s entry into the **end zone**;
  -   `PatrolPath` The class designed to create a patrol path of enemies – two points between which they will move;
  -   `PatrolPath.Mover` Allows enemies to oscillate between start and end points of the path at a certain speed.

The base class [KinematicObject](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Mechanics/KinematicObject.cs) has all the necessary fields and methods to implement player physics. The fields in this class contain the exact values required for all physics calculations when a player **moves**, **bounces** and **jumps**.

The `Bounce()` method is used to make your character bounce off enemies:

```
  public void Bounce(float value) 
  {
    velocity.y = value;
  }
```
It determines the vertical speed of the object, due to which the character bounce up. This method has an overload to determine the speed of an object in a certain direction:

```
  public void Bounce(Vector2 dir)
  {
    velocity.y = dir.y;
    velocity.x = dir.x;
  }
```

The `PerformMovement()` method describes the implementation of a character's movement:

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

It's necessary to check whether the character has collided with something in the current direction of movement and save the number of collisions for further processing in the loop. If the current surface is flat enough to allow the character to land on it, the character successfully lands on it. In case the character is in the air and collides with something, it's necessary to stop the vertical movement upwards and reset the horizontal speed.

[PlayerController](https://github.com/daridakr/Oscarcat/blob/main/Assets/Scripts/Mechanics/PlayerController.cs) is inherited from the **KinematicObject** class. This is the primary class used to implement player control. It implements the **Singleton pattern** to be able to refer to a single entity and contains all the necessary fields to define all character parameters. 

<br>

## Demonstration

![OscarDemo](readme_assets/animation.gif)

<br>

## Download
Download [Oscarcat.apk](https://drive.google.com/file/d/1pWFiWPu3CZSdQWymcw6B24ji-75C8AP4/view?usp=share_link)

<div align="center">
<img height="300" width="360" src="readme_assets/oscarChar.png" />
</div>

