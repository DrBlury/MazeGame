Hello and thank you for checking out this README file.


This is a little maze "game" which allows you to read custom <name>.maze files in the main directory of the executable.
At the time of writing there is a hard coded name for this file which is "test.maze".
So if you want your maze to be used by the program - use "test.maze" as the filename.

The .maze files should be written like the example below:

5
5
#####
#...#
#.@.#
#...#
#...#
#####

The first line containing a 5 tells the program how many columns (5) the maze has.
The second line containing a 6 tells the program how many rows (6) the maze has.
The maze contains three symbols ("#", "." and "@")
The "#" marks walls while the "." marks items to grab.
Lastly the "@" tells the program where your player should be.

The arrow keys are used for movement while you can also press space to move the player towards the next item.

Keep space pressed to auto-complete the maze!

Note: 
You can always resize the window to your liking. 
The tiles will automatically resize to fit your window size.



