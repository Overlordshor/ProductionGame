# ProductionGame
Prototype game "Production".
Game specification:
At the start, the main menu appears, where there are three buttons "1", "2" and "3" and the "start" button.
With numbers, the player chooses the number of resource buildings at the start. Accordingly, one, two or three. These buttons should work like a selector.

After pressing the "start" button, the main gameplay starts.
The following objects are located on the isometric map:
1. one, two or three resource buildings;
2. one processing plant;
3. one market.
The position of buildings is predetermined (construction and movement can be neglected), the camera is stationary.
You can visually execute it at your own will, it doesn’t matter, but different types of buildings should be visually different.

Clicking on a resource building should open the resource building window.
The window initially displays an empty cell and a "start" button.
By clicking on a cell in it, the possible resources for production begin to switch in a circle: wood, stone, iron.
After the player has selected a resource and pressed "start", the "start" button turns into a "stop" button, and the building starts to produce 1 resource every N seconds and send it to the warehouse (conditional inventory). N is set separately for each building.
Visually, the warehouse should be displayed as a panel with a list: the resource icon - the amount of the resource..

By clicking on the processing building, the processing building window should open.
The window displays two cells for resources and one for the finished product.
wood + stone = hammers
wood + iron = pitchfork
stone + iron = drill (drill)
The remaining combinations do not give any finished product.
By clicking on the resource cells, the player cycles through the resources. The finished product is displayed automatically.
After the player has selected resources and pressed "start", the "start" button turns into a "stop" button, and the building starts to produce 1 product every N seconds and send it to the warehouse (conditional inventory). N is set in the processing building settings. Production should automatically stop if there are not enough resources in the warehouse.

By clicking on the market, the market window opens.
The market window displays an empty cell, a price and a "sell" button.
By clicking on a cell in it, the finished products available in the warehouse are switched in a circle. The price is set in the settings for each individual product.
After the player has selected a product and clicked "sell". The goods are deducted from the warehouse, and the player is credited with coins, which are displayed in the main GUI.

As soon as the player collects N coins, the game ends with a victory. The victory window is raised, after which the return to the main menu occurs. N is set in the general settings of the game.

Between sessions should be saved: the number of coins and the contents of the warehouse.

Dependency injection was done by hand specifically for additional requirements.
Selected architecture: MVC
