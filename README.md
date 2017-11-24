# M4Graphs
M4Graphs is a framework intended for visualizing model based tests during runtime.

This project was quickly put together as a prototype; a lot of things still need refactoring and there are some bugs.

## Features
* Activating elements by id, showing the path the test is taking
* Heat map
* Filtering elements
* Moving model
* Zooming
* Reads yEd .graphml-files

## TODO
WPF:
* Rewrite Edge and Node controls as custom controls instead of user controls
* Improve positioning of yEd labels

Generally:
* Refactor most code related to drawing (so.. most of the code)