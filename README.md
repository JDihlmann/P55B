<p align="center">
  <img src="https://github.com/JDihlmann/P55B/blob/master/docs/Logo_P55B.png">
</p>

Is a game created for mobile platforms with the unity engine. The player manages a small bar in the outskirts of the universe. The goal of the player is to increase the popularity of the bar, so more customers come in and thus he gains more money to realize his own dream of the bar. He is able to collect ingredient, customize, manage and upgrade the bar. 

 ## Gameplay / Story 
For further information related to story and gameplay check out our [website](https://jdihlmann.github.io/P55B/) or one of our videos [teaser](https://youtu.be/nEvUj5SVUBU) / [trailer](https://youtu.be/n8DB6euExGA).

<p align="center">
  <img width="100%" src="https://github.com/JDihlmann/P55B/blob/master/docs/P55B.gif">
</p>

## Requirements
* **Unity Engine Version:** 2018.2.1.18f1
* **Unity Engine Package:** Lightweight Render Pipeline

## Installation
Clone the repository, open it in Unity (Version: 2018.2.1.18f1) and double click on the scene called "Bar". Your game view will be black, install the LWRP via the unity package manager and your game view will render. You are now able to play the game in unity with your mouse. Follow this [unity tutorial](https://unity3d.com/de/learn/tutorials/topics/mobile-touch/building-your-unity-game-android-device-testing) in order to play the game on your phone. 

## Description
The game has multiple modes and views, we will describe each of them and their technical background - Collecting, Bar Keeping, Building, Shopping / Upgrading, State Changing, Saving

### Collecting
The zoomed out view or space mode shows the outer space around the bar, where the ingredients fly across the screen in both directions. The player has to move them into the bar via swiping to collect the ingredients needed for crafting. But he has to carefully avoid the comets that fly around there, because they will destroy some of the ingredients he had stored. Sometimes an event might happen and either a lot or almost no ingredients spawn around the bar, achieved by changing the spawnrate. 

### Bar Keeping
In the close up view the player can switch between 2 modes, the build mode and the bar mode. In the bar mode, the customers come in, head to the center of the bar where the counter sits and orders one of the players selected recipes. The customer then waits for the drink, pays, and finally chooses a seat from a list of available seats located in the bar. After finding a comfortable seat they consume their drink there. 

This customer procedure is achieved by a simplistic AI consisting of different states where when a specific requirement is fulfilled it will move to the next state until the last state is reached. States consist of entering the bar, ordering, waiting for the drink, finding a seat, consuming the drink and lastly leaving the bar. 

If they are satisfied with their chosen drink, the happiness will go up, and more customers enter since the spawnrate of the customers is bound to the happiness. If the customer does not get the drink he wants, or has to wait for too long, he leaves the bar and the happiness will go down. 
 
### Building
In the build mode, the player can change and move the furniture in his bar around, this is possible by a unique gird system. The grid systems is optimized for live changes and saving. By tapping on an object to enter the "edit mode" of the object and then drag and drop it to the place he wants it to be. Additionally he can set the rotation via a rotate button. He can sell the furniture to get back some percentage of the money he payed. 

### Shopping / Upgrading 
From the bar mode, the player can access the store, where he can buy new furniture, ingredients and new recipes. There is also a panel for the machine (worker) that serves the customer at the counter, in wich the player can upgrade the worker by buying more recipe slots, so the worker can satisfy different tastes, or upgrade the speed and the mutitasking ability of the worker for more efficient production. In this menu the player also assigns the recipes he wants his worker to offer.

We designed a minimalistic and clear UI so that it fits the bar setting and is easy to use.
The UI is created dynamically using a JSON file. This way it is easy to manage all the different objects in the game without the need to edit the Canvas Object in Unity. For example changing the name, price, image etc. of an object adding a new object to the game is realized by just changing the related keys in the JSON file. When the game starts every JSON file will initialize a list with the respective objects and for every object the UI will add a prefab changing its text or image to the values from the list. Furthermore the UI is fully responsive so that it looks good on every mobile screen.

### State Changing
While the player is not in the bar view, no customers are in the bar, so he doesn’t have to stress about his ingredient stock running dry. This is achieved by a GameStateChanger, which disables and enables different parts of the game depending on the mode the player is in.  

### Saving 
All the relevant data of the game, like the position and type of objects in the bar, the recipe and worker unlocks, the amount of ingredients and the money and happiness is collected and can be edited by a central script. This script saves the Data in a binary save file and loads it from there at game start too. 

## Disclaimer
All the models and the interface of the game are designed by us (Maya and Substance student license), only the graphics for ingredients, drinks and some buttons are from [Freepik](https://de.freepik.com/) and [Flaticon](https://www.flaticon.com/). 

## Contributing
Feel free to fork and create branches. This is a non comercial students project.\
We can't guarantee for any functionality thus wont take resposibility.

## Meta
Mia Wölm - [miaiam](https://github.com/miaiam)\
Lukas Gehre - [kiusah](https://github.com/kiusah)\
Mike Fu - [thecrusticroc](https://github.com/thecrusticroc)\
Jan-Niklas Dihlmann - [JDihlmann](https://github.com/JDihlmann)
