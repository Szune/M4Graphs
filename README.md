**Note:** Most things are currently being refactored.
# M4Graphs
M4Graphs is a framework intended for visualizing model based tests during runtime.

This project was quickly put together as a prototype; a lot of things still need refactoring and there are some bugs.

Feel free to try out the demo in the WpfTest project and/or checkout an image of the demo at the bottom of this readme.

## Features
- [x] Parsing yEd .graphml-files
- [ ] Generating models on the fly
- [x] Heat map

WPF-specific:
- [x] Filtering elements
- [x] Moving the model
- [x] Zooming in/out
- [ ] Edge and Node controls as custom controls instead of user controls
- [ ] Improved positioning of yEd labels
- [ ] Zooming towards mouse position
- [ ] Non-choppy moving

Generally:
- [ ] Refactor more code related to drawing
- [ ] Test coverage

![Demo of M4Graphs](https://raw.githubusercontent.com/Szune/M4Graphs/master/M4GraphsDemo.png)