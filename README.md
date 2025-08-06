# Unity Junior Programmer Final Project
The final Project from my Junior Programmer course. It's a simple scene with 4 different types of vehicles: A car, a boat, a plane, and a helicopter. <br>
The player can drive each vehicle using the WASD buttons, and E to turn on and off the engine. <br>

Each vehicle has its own script that inherits from a parent class called Vehicle, and each script behaves differently to match how these vehicles behave IRL. <br><br>
They have their own controls: <br>
For boat and Car it's E to turn on and off the engine, WS to move forward and backwards, and AD to rotate left and right <br><br>
The helicopter is E to turn on and off the engine, Space and Shift to move up or down, and sWASD behaves similar to boat and car, just in the air <br><br>
The plane is complicated. Turning on the engine is the same, but WS changes the thrust of the engines instead of just causing the plane to go forward and backwards.
Once the speed reaches a certain threshold the plane's gravity turns off and the steering changes. Space and Shift to pitch the plane Up or Down when the plane is airborne, and A and D to make it roll.<br>
It's finicky and unpolished but it is different to everything else and that's all that matters!
