# MazeDemo
 
Concept:
Navigate through the maze to collect points by colliding with blue balls and yellow squares while avoiding red balls.

Objective:
Collect 50 points within 2 minutes..



GameFeatures:==>
Camera Control:
Implemented a slider for zooming in and out for an enhanced view.

Visual Enhancements:
Added post-effects and Epic Toon particle effects for Good Visuals.

Player Controls:
Integrated a dynamic joystick for player movement and rotation.

AI Navigation:
Added NavMesh AI for automatic pathfinding of balls within the maze.

Time Management:
Implemented a 2-minute timer for the player to achieve the goal.

Scoring System:
Points awarded for collisions: +1 (blue), -1 (red), +2 (yellow).

Reward System:
Get stars based on points collected. (5 stars for 50 points)

Audio and vibration:
Added audio effects and vibration.

Assets:
I used a 3D maze model from Sketchfab and icons from Flaticon. Audio is sourced from YouTube.and EpicToon particle form assetstore.
Scripts:



GameScripts:==>
MainCamera Script:
Follows player position and rotation for camera movement.

Sphere Script:
Controls movement and pathfinding for blue balls , yellow squares and redballs.

Player Script:
Manages player movements, rotations using the joystick, and collision handling.

GameManager Script:
Handles game aspects such as timer, score, best score, game restart, loading panel, and audio management.
 

