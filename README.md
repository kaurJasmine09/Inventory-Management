# Clothing Stock Management System

Our system will be designed to manage stock in a warehouse, focus on clothing storage. The system will be used by store managers and employees to record each new item in and out of the storage. Managers and employees will be differentiated by the employee ID and they will have different responsibilities. Users can update the quantity of an item and add new category. 

Technologies we have decided to develop our system are MS SQL server to save detail of products and employee information, for structure of the system we are using HTML and Bootstrap framework. For frontend development we are using ReactJS and for backend we are using Dot Net to generate APIs.  

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Examples](#examples)


## Installation

To install and set up the project, follow these steps:

1. Install Node.js latest version, Visual Studio/ Visual Studio Code and Android Studio
2. Install Entity framework nuGet package, Automapper nuGet package, and Swashbuckle.AspNetCore.Swagger for back-end
3. Clone the repository.


## Usage

To use the project, follow these steps:

1. In the User `\your\path\Clothes Stocking Managment\clothing-stock-management` folder, open the folder in Visual Studio Code.
2. Open the terminal in current folder, run `npm start`
3. In `\your\path\Clothes Stocking Managment\Application\WebAPI\StockInventoryWebApi` folder, open StockInventoryWebApi.sln file in Visual Studio
4. Press the play icon to run the back-end

## Examples

1. Login Screen: the user login with their employee ID and pin.

![Alt text](<Stock Management Project/Clothes Stocking Managment/Designs/Picture1.png>)

2. Stock Records Screen: once authentication is successful, user could access to all the records on the system. A Menu to go to other screens or search for specific item will be display at the top of the screen.

![Alt text](<Stock Management Project/Clothes Stocking Managment/Designs/Picture2.png>)

3. Stock In Screen: user must enter all required fields in order to introduce a new item on the system. More product details will be display, according with entered Product ID (first field). If the information is correct, the system will generate a Transaction ID, showing the corresponding message to the user.

![Alt text](<Stock Management Project/Clothes Stocking Managment/Designs/Picture3.png>)

4. Stock Out Screen: user introduce required fields, and take out a specific item from the system.

![Alt text](<Stock Management Project/Clothes Stocking Managment/Designs/Picture4.png>)

5. Current Stock Screen: this screen displays the master table of the information stored through the system, allowing user to search any item. 

![Alt text](<Stock Management Project/Clothes Stocking Managment/Designs/Picture5.png>)

6. Suppliers and Customers Screens: this tables are directories that allow users to look for the contact information of all Suppliers or Customers stored in the system.

![Alt text](<Stock Management Project/Clothes Stocking Managment/Designs/Picture6.png>)![Alt text](<Stock Management Project/Clothes Stocking Managment/Designs/Picture7.png>)