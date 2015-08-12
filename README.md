# Documentation

The complete documentation can be found at [url:http://doc.ilambda.me/freemwork/] (not available now).
Disclaimer : this documentation does not contain tutorials, only the detail of each class and method.

# Tutorials

## Initializing The Freemwork

Initiaizing The Freemwork is the one and only thing that depends on the graphical engine you used. The Freemwork works by default with MonoGame, but this engine can be replaced by the one of your choice.

The **Context** class is here to help. It contains the Update() and Draw() methods, that are used by the engine. Just call them whenever you want. See XNAContext.cs for more details.

## How to work with it

The Freemwork relies on a simple workflow that'll simplify a lot of your tasks.

### The play states

First : the concept of  **PlayStateMachine**. This object is a [url:finite state machine|http://en.wikipedia.org/wiki/Finite-state_machine] that handles the game different states.
Let's say you want in your game a Menu view, a Game view, and a Setting view.
Create three classes, **MenuPlayState**, **GamePlayState**, and **SettingsPlayState**, which all override **PlayState**. Give them a unique ID by redefining the _Identifier_ property (let's set them respectively at "Menu", "Game", and "Settings"), and then do the following :

```csharp
context.PlayStateMachine = new FiniteSingleStateMachine<IPlayState>(new MenuPlayState());
context.PlayStateMachine.States.Add(new GamePlayState());
context.PlayStateMachine.States.Add(new SettingsPlayState());

context.PlayStateMachine.Transitions.Add(new FiniteTransition("Menu", "Settings", "MenuToSettings"));
context.PlayStateMachine.Transitions.Add(new FiniteTransition("Menu", "Game", "MenuToGame"));
context.PlayStateMachine.Transitions.Add(new FiniteTransition("Game", "Menu", "GameToMenu"));
```

Then, in your playstates, when you want to change the actual state, do the following :

```csharp
//If in your playstate, when the condition to do the transition is reached (key pressed, button click, ...)
this.Transite("MenuToSettings"); 
//Out of your playstate
myPlaystate.Transite("MenuToSettings");
```

The second parameter is the label of the transition you specified earlier, in **PlayStateMachine**.Transitions.

### The services

One of the advantages of The Freemwork is that you can code your game, regardless of the engines used to make sound, graphics, effects, etc.
This flexibility relies on a simple concept : the **ServiceLocator**.

At startup, you provide the default services for each category : graphics, audio, resource loading, device capabilities, file managing, audio, etc.
In the method Initialize() in XNAContext.cs, you can see the following lines :

```csharp
ServiceLocator.Provide<IGraphicsService>(new XNAGraphicsService(this, graphics, GraphicsDevice, spriteBatch, new Size2D<int>(800, 480)) { KeepViewportRatio = true });
ServiceLocator.Provide<IResourceService>(new XNAResourceService(Content));
ServiceLocator.Provide<IInputService>(new XNAInputService());
ServiceLocator.Provide<IDeviceService>(new RTDeviceService());
ServiceLocator.Provide<IFileService>(new RTFileService());
ServiceLocator.Provide<IAudioService>(new XNAAudioService()); 
```

Those lines provide the services used by the game. They can be changed anytime in the game.
To change the engine provided by default with The Freemwork (for instance, Monogame for audio and graphics), you can just create a class that implements **IGraphicsService** with the engine that you want.
Ditto if the default behavior of some services doesn't please you.

Getting the service back to operate on it can be done just by doing the following :

```csharp
//to retrieve the service of type ISomethingService
var myService = ServiceLocator.Get<ISomethingService>();
```

Always query the service using the generic interface (I...Service), otherwise you'll get an exception.
No casting is required, as it would break all the advantages to operate on an interface without knowing which implementation is used.
When no service is provided, a null object is returned instead (that is, a non-null instance of an object in which all the methods do nothing, except logging).
When the service you want to get is of generic kind (Graphics, Audio, ..), there is a property usually simplifies the code : **ServiceLocator**.SomethingService.

Then, when you've got just operate on the service like you would on any object. For instance :

```csharp
var graphicsService = ServiceLocator.Get<IGraphicsService>(); //note that ServiceLocator.GraphicsService could do the trick.
graphicsService.Draw(sprite, transform);
```

### The worldspawn

Once you've provided all the required services, and set all your **IPlayState** objects, you might want to start coding your game.
Let's assume, for the example, that you have two PlayStates called **GamePlayState** and **MenuPlayState**, and that you can to go from one to another in both directions.
Both of your playstates do have a _Worldspawn_ property. The _Worldspawn_ property is in fact an object of type **Worldspawn**, that is merely the universe of your game. It'll contain all the game objects that your game contains (the hero, the props, everything). 

The worldspawn is different for each play state : **GamePlayState** and **MenuPlayState** will not have the same game objects in it. Each worldspawn has its own game objects.

### The view

To setup your view, change the value of **IGraphicsService**.ViewportSize to the size of your view. The Freemwork will do all the work in background to resize your view and adapt it to the device's real screen size and resolution. The unit of **IGraphicsService**.ViewportSize is pixels. 
If you want to put colored (usually black) rectangles on the sides of the screen if the aspect ratio of the screen differs of the one you've choosen, you can put **IGraphicsService**.KeepViewportRatio to true. If it doesn't matter, put it to false. 

To hide the system cursor, put **IGraphicsService**.IsMouseVisible to false. If you want a custom cursor instead, take a look to the **Cursor** component. 

### The camera

Sometimes, you might want to have a custom camera in your game, for instance a camera that follows the hero, or a camera that reponds to the player mouse input.
The **Worldspawn** class have a _Camera_ property of type *Camera2D*. This class controls all the camera system. Note that the camera depends on the **Worldspawn**, therefore on the **PlayState** you're into. 

The *Camera2D* has three important properties :
* The scrolling strategy (**ICameraScrollingStrategy**), handles the position of the camera
* The rotating strategy (**ICameraRotatingStrategy**), handles its rotation
* The scaling strategy (**ICameraScrollingStrategy**), handles its zoom on the scene

All the three strategies properties do provide to the camera ways to understand how you want it to work.
They all default to null objects (e.g, objects that are not equal to null, but that have a default unaltered behavior).
To create a specific behavior, create a class that implements **ICameraSomethingStrategy** (depends on what you want to do) and make it do what you want.
For instance, you could create a scrolling strategy that makes the camera follow the current player.

### The game object

The **GameObject** is the center of The Freemwork. It is to it what atoms are to matter.
Everything in your game is a **GameObject** : your character, the princess you got to save, the world, the day/night system, the water system, etc.
A game object is a component of your game that you create in a play state, to add it to the corresponding **Worldspawn**.
It has components that define its behavior : 2D position, a sprite, animations, a sound emitter, etc.

The game object cannot be added to multiple worldspawns at the same time, as he might have unpredictable behavior.
To add a game object to a worldspawn, you have to give it an ID (a Game Object ID, aka GOID) that is specific to THIS worldspawn and game object. This ID can be choosen (to be able to retrieve the object in an easy way), or can be given by the worldspawn, using the method GetNextGOID(). It'll return a free and unused ID that'll not collide with any id you might have chosen yourself. 

It is simple to create a **GameObject** :

```csharp
//In a play state
var gameObject = new GameObject();
gameObject.Components.Add(...);
...
Worldspawn.GameObjects.Add(gameObject, GetNextGOID()); //could provide a custom id instead of computing one
```

### The component

The components are very important, because it's on what you'll spend the most of time coding. They define the behavior of game objects, and can be reused infinitly.
For instance, to create a game object that has a sprite, you do the following :

```csharp
//In a play state
var gameObject = new GameObject();
gameObject.Components.Add(new Identity2D());
gameObject.Components.Add(new SpriteHolder(mySpriteResourceName));
Worldspawn.GameObjects.Add(gameObject, GetNextGOID()); //could provide a custom id instead of computing one
```

Some components depends on another : you can't have a **SpriteHolder** if you don't have a *Identity2D*, and you can't have a **SpriteAnimator** if you don't have a **SpriteHolder**.

Components can be queried, with the QueryComponent<T>() method : getting them this way will return a reference to the object. You can therefore modify its properties to alter the behavior of the game object. Null is returned if no component of type T has been found on the component.

You can choose to query

```csharp
var component = gameObject.QueryComponent<Identity2D>(); //Querying the identity2D component
```

All components can only be present one time on each gameobject. For instance, you can't have two **SpriteHolder** or two *Identity2D*.

Listing all the given components would be counter-productive, but one is definitly worth detailling : the *Identity2D* component.
It represents the transformation (position, rotation, scale) of the game object. You have to add it to your game object if it has some identity in space (a monster will have one, but a day/night system will not).
You can toggle the _DependsOnCamera_ boolean on the Identity2D component. If true, the object will scroll with the camera (as a player, or a monster); if false, the object will stay on the screen and will not follow the camera (as a button, or an UI element).

The *Identity2D* component have three more interesting properties :
*_Transform_, the transform of the game object relative to its parent (check next section).
*_GlobalTransform_, the absolute transform of the game object (including the transform of its parent).
*_CameraTransform_, the transform of the game object relative to the camera, as drawn on screen. For instance, if the global transform position of the object is (1000, 1000), and the camera corner position is (800, 800), then the position member of the CameraTransform will be (200, 200).

You can create a component too, by creating a class implementing the **IGameComponent** interface.
You can put two different flags on your class, to specify which components are required, and which components are incompatible with this one.

```csharp
[NeededComponent(typeof(TypeOfNeededComponent))]
[UncompatibleComponent(typeof(TypeOfForbiddenComponent))]
public class MyComponent : IGameComponent
{
    ...
```

### Parent and children game objects

Sometimes, you might want to create game objects that depends on other : for instance, the hat on the face of your favorite wizard, or your character on its favorite boat. Those objects are linked together, in the sense that the position of the hat depends on the position of the wizard's head, and the position of the character depends of the position and orientation of your boat.

To make a game object parent of another, which is like tying the child to its parent, you have to do the following :

```csharp
var children = new GameObject();
var parent = new GameObject();

children.Components.Add(new Identity2D(Transform.Identity, parent));
parent.Components.Add(new Identity2D(Transform.Identity, null)); //null parent means no parent

```

Now, when you'll move parent, it'll move children. You can still move children, but its transform will be relative of parent transform.

### The commands
 
The last important concept, is the concept of commands. If you make a multiplateform game, you might want to have multiple controls for the same action : a W-key down, a virtual joystick pointing up, a xbox controller pressing the up button, are all actions that should make your character go forward.
To tie multiple commands to a same action, use a **CommandMap**.
A command map registers actions (**InputAction**).

It is very simple to use : to test if an action is actually done, just do :
```csharp
var isActionDone = commandMap[myActionName].Evaluate();
```

Some components (*Basic2DController*, ..) take in parameter a CommandMap and the name of the commands you chose. For instance :

```csharp
var commandMap = new CommandMap();
commandMap["Up"] = new InputAction(
    new KeyInputCommand(Key.W, KeyInputCommandType.Down), 
    new XboxControllerJoystickInputCommand(0, ControllerJoystick.LS, Joy => Joy.Y > 0.2f),
    new InclinometerInputCommand(Angle => Angle.Y > 20));
commandMap["Down"] = new InputAction(
    new KeyInputCommand(Key.S, KeyInputCommandType.Down), 
    new XboxControllerJoystickInputCommand(0, ControllerJoystick.LS, Joy => Joy.Y <-0.2f),
    new InclinometerInputCommand(Angle => Angle.Y < -20));
commandMap["Left"] = new InputAction(
    new KeyInputCommand(Key.A, KeyInputCommandType.Down), 
    new XboxControllerJoystickInputCommand(0, ControllerJoystick.LS, Joy => Joy.X <-0.2f),
    new InclinometerInputCommand(Angle => Angle.X < -20));
commandMap["Right"] = new InputAction(
    new KeyInputCommand(Key.D, KeyInputCommandType.Down), 
    new XboxControllerJoystickInputCommand(0, ControllerJoystick.LS, Joy => Joy.X > 0.2f),
    new bO(Angle => Angle.X > 20));

gameObject.Components.Add(new Basic2DController(1.0f, "Up", "Down", "Left", "Right", commandMap));
```

### The space partitionning

You can toggle the space partitionning by toggling the **Worldspawn**._SpacePartitionning_ boolean.
This feature might reveal itself useful when you have a big scene, with a lot of objects, and searching through the **Worldspawn**._GameObjects_ list might become slow. Space partitionning allows you to store objects into a big grid.
To chose the cell size of the grid, edit the **Worldspawn**._CellSize2D_. The cells must not have neither a null width, nor a null height.

Sometimes, you might encounter problems with objects that have a non-neglectable size in regard of the cell size.
![partitionning problem](http://downloads.ilambda.me/freemwork/partitionningIssue.png)
The main issue is that your item will be considered as if it was in the cell where its position is. To tackle this issue, you must use the **GameObject**._PartitionningBoundsComponent_ : some components (**SpriteHolder**, **Hitbox**, ..) are tagged with the **BoundsDefiningProperty** attribute. Set the **GameObject**._PartitionningBoundsComponent_ property to the type of the component you want to give bounds to the game object. The space partitionning algorithm will understand, and will count your object in all the cells he should.
