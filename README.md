# IMGUI-Visualizer
A visualizer for the Unity Immediate UI system that helps with Software Inc UI creation/general UI creation.

Download from releases tab.

## Language tutorial:
(NOT DONE)

The language is pretty simple and goes along this format:

`ELEMENT ARGS`

Example:

`Label x, y, width, height "Text"`

### List of elements:

#### Box

You can create boxes using the Box element.

Example: 

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

You can create simple labels using the 
