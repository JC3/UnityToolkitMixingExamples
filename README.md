# UnityToolkitMixingExamples

Examples of mixing IMGUI with UI Toolkit in Unity editor GUIs.

## Usage

Add the four source files here to the Editor folder of a Unity project. A new "Example" menu
will appear in the main menu bar and the three example windows can be displayed from there.

## General Idea

The fundamental ideas of combining IMGUI and UI Toolkit elements are pretty straightforward:

- To place UI Toolkit elements in an IMGUI layout, enable absolute positioning for the 
  VisualElements you want IMGUI to manage. Then you can use [`GUILayoutUtility.GetRect`](https://docs.unity3d.com/ScriptReference/GUILayoutUtility.GetRect.html])
  to reserve space in the layout for the elements, and manually position the element in 
  the resulting rectangle. (ExampleWindow1 and 2)
  
- To place IMGUI elements in a UI Toolkit layout, simply use [`UIElements.IMGUIContainer`](https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UIElements.IMGUIContainer.html).
  The container constructor accepts a callback that is equivalent to the `OnGUI` event that
  IMGUI uses. Multiple `IMGUIContainers` can exist in the same window. (ExampleWindow3)
  
If you want to use UI Toolkit to lay out groups of UI Toolkit elements in an IMGUI-based 
window, it's no different than above: You can construct a VisualElement container and put any
UI Toolkit stuff you want in it, doing the usual UI Toolkit layout. All you have to do is
follow the first rule above, make sure the container itself uses absolute positioning, and 
lay the container out in a `GetRect` space -- the child components in the container will be 
laid out by UI Toolkit as usual. ExampleWindow2 demonstrates this.

Each of the example windows here provide a layout that consists of an IMGUI label on top, two
UI Toolkit ListViews in the middle, and some IMGUI buttons on the bottom:

![](https://github.com/JC3/UnityToolkitMixingExamples/raw/master/windows.png)

## ExampleWindow1.cs

This demonstrates using IMGUI to completely lay out all components, including VisualElements.
 
## ExampleWindow2.cs

This demonstrates using IMGUI to lay out most components, but also allowing UI Toolkit to lay
out specific subgroups of VisualElements.

## ExampleWindow3.cs

This demonstrates using UI Toolkit to completely lay out most components, but also allowing 
IMGUI to lay out specific subgroups of IMGUI components (via IMGUIContainer).

## Example.cs

This just has some mostly uninteresting utility functions to keep boilerplate stuff from 
cluttering up the other files. It also has some global constants that make it easier to give
a consistent look to the three example windows.

## Notes

A lot of explanation is in the source as comments, so check those out. 

Also, the IMGUI-based containers may "jitter" a bit as you resize the windows. This is just 
an unavoidable quirk of when IMGUI makes calculate layout rects available. You'll notice
that the UI Toolkit-based example is much snappier and more well-behaved.
