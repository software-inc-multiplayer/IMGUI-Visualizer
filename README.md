# IMGUI-Visualizer
A visualizer for the Unity Immediate UI system that helps with Software Inc UI creation/general UI creation.

Download from releases tab.

## Language tutorial:

The language is pretty simple and goes along this format:

`ELEMENT ARGS`

Example:

`Label x, y, width, height "Text"`

### List of elements:

#### Box

You can create boxes using the Box element.

```
Box x, y, width, height "OptionalTitle"
```

You can create a box with elements automatically added using the BoxStart and BoxEnd elements:

```
BoxStart x, y, width, height "OptionalTitle"
//Elements etc.
BoxEnd
```
#### Label

You can create simple labels using the Label element.

`Label x, y, width, height "Text"`

#### Button

You can create simple buttons using the Button element.

`Button x, y, width, height "Text"`

#### Input

You can create a simple input box by using the Input element.

`Input 10, 70, 80, 75 "Edit me at will"`

### Fully fledged example:

```
Box 5, 5, 90, 145 "Test Box"
Label 10, 25, 80, 25 "Hello!"
Input 10, 70, 80, 75 "Edit me!"
```

![Screenshot](https://i.gyazo.com/88b428c1a102f5bbc92c0a5279e5bc78.png)
