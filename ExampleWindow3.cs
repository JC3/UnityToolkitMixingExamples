using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class ExampleWindow3 : EditorWindow {

    [MenuItem("Examples/Mixing UI Toolkit and IMGUI: UI Toolkit-based")]
    public static void ShowMe () => Example.ShowExample<ExampleWindow3>();

    void OnEnable () {

        // Unlike the other two examples, this one primarily uses UI Toolkit for laying
        // things out. If you're familiar with HTML and CSS then the following code should
        // be pretty understandable.

        VisualElement header, footer, content, left, right;

        rootVisualElement.Add(header = new IMGUIContainer(OnIMGUIHeader));
        rootVisualElement.Add(content = new VisualElement());
        rootVisualElement.Add(footer = new IMGUIContainer(OnIMGUIFooter));

        // In the IMGUI examples, the margins were in a top-level vertical layout.
        rootVisualElement.style.marginLeft = Example.MarginStyle.margin.left;
        rootVisualElement.style.marginRight = Example.MarginStyle.margin.right;
        rootVisualElement.style.marginTop = Example.MarginStyle.margin.top;
        rootVisualElement.style.marginBottom = Example.MarginStyle.margin.bottom;

        content.Add(left = Example.CreateSomeKindOfVisualElement());
        content.Add(right = Example.CreateSomeKindOfVisualElement());

        // These margins are equivalent to the GUI.Spacing() in the IMGUI examples.
        content.style.marginTop = Example.Spacing;
        content.style.marginBottom = Example.Spacing;
        content.style.flexGrow = 1; // Content area should expand.
        content.style.flexDirection = FlexDirection.Row; // Horizontal layout.

        // Same deal as in ExampleWindow2.
        left.style.flexGrow = 2.0f;
        left.style.marginRight = Example.Spacing;
        right.style.flexGrow = 3.0f;

    }

    // These are just Example.HeaderStyle and .FooterStyle with the margins cleared to illustrate
    // that UI Toolkit is managing the spacing.
    private GUIStyle headerNoMargins, footerNoMargins;

    // IMGUI OnGUI event for the stuff that was above the list boxes in the other examples. A few 
    // components and styles have been eliminated because we're using UI Toolkit to set margins and
    // spacing.
    private void OnIMGUIHeader () {

        headerNoMargins ??= new GUIStyle(Example.HeaderStyle) { margin = new RectOffset() };
        GUILayout.Label($"An example of combining IMGUI with UI Toolkit:", headerNoMargins);

    }

    // And the same for the stuff that was below the list boxes.
    private void OnIMGUIFooter () {

        footerNoMargins ??= new GUIStyle(Example.FooterStyle) { margin = new RectOffset() };
        using (new EditorGUILayout.HorizontalScope(footerNoMargins)) {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("OK", Example.LargeButtonStyle)) Close();
            if (GUILayout.Button("Cancel", Example.LargeButtonStyle)) Close();
        }

    }

}
