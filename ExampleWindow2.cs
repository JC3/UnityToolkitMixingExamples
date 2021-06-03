using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class ExampleWindow2 : EditorWindow {

    [MenuItem("Examples/Mixing UI Toolkit and IMGUI: IMGUI-based + UI Toolkit Layouts")]
    public static void ShowMe () => Example.ShowExample<ExampleWindow2>();

    private VisualElement containerElement;

    void OnEnable () {

        VisualElement leftElement, rightElement;

        // Create some VisualElements, then put them in an ABSOLUTE POSITIONED container. We will
        // let UI Toolkit lay out the elements in the container, but we'll store the container
        // itself in a private field so we can lay *that* out in OnGUI. Everything must be added
        // to the root element as usual.

        rootVisualElement.Add(containerElement = new VisualElement());
        containerElement.Add(leftElement = Example.CreateSomeKindOfVisualElement());
        containerElement.Add(rightElement = Example.CreateSomeKindOfVisualElement());

        // We want a horizontal layout and we'll let the right side be a little bigger.
        containerElement.style.flexDirection = FlexDirection.Row;
        leftElement.style.flexGrow = 2.0f;
        rightElement.style.flexGrow = 3.0f; // 3:2 ratio 

        // There's lots of ways to set spacing between elements but we'll just do this:
        leftElement.style.marginRight = Example.Spacing;

        // This is the magic.
        containerElement.style.position = Position.Absolute;

    }

    void OnGUI () {

        EditorGUILayout.BeginVertical(Example.MarginStyle);
        GUILayout.Label($"An example of combining UI Toolkit Layouts with IMGUI:", Example.HeaderStyle);

        // Repaint logic explained in http://answers.unity.com/answers/1349110/view.html
        // This time we're using UI Toolkit to provide the horizontal layout.
        Rect rect = GUILayoutUtility.GetRect(0, 4000, 0, 4000, GUILayout.ExpandHeight(true));
        if (Event.current.type == EventType.Repaint) {
            containerElement.style.top = rect.yMin;
            containerElement.style.left = rect.xMin;
            containerElement.style.width = rect.width;
            containerElement.style.height = rect.height;
        }

        using (new EditorGUILayout.HorizontalScope(Example.FooterStyle)) {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("OK", Example.LargeButtonStyle)) Close();
            if (GUILayout.Button("Cancel", Example.LargeButtonStyle)) Close();
        }

        EditorGUILayout.EndVertical();

    }

}
