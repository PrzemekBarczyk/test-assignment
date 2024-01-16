# unity-chess
The repository contains a test task implementing a system for marking and navigating a map with an isometric view using a group of three characters. Character selection can be done through buttons on the user interface or by selecting a character with the left mouse button. The selected character becomes the leader of the subsequent characters, who follow him. Movement commands are issued using the right mouse button. The group has speed, angular speed, and acceleration parameters, which are randomly generated at each start. The built system is based on the Unity Navmesh AI system. During gameplay, it is possible to save and load data to a file. The saved data includes player positions, rotations, and group parameters

## Documentation

### [Class diagram](https://github.com/PrzemekBarczyk/test-assignment/blob/main/Documentation/Class%20diagram.drawio.pdf)

### [State diagram](https://github.com/PrzemekBarczyk/test-assignment/blob/main/Documentation/Character%20state%20diagram.drawio.pdf)

## Technology stack

- Unity 2022.3.16f1
- C#
