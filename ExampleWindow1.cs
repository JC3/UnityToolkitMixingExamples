using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class ExampleWindow1 : EditorWindow {

    [MenuItem("Examples/Mixing UI Toolkit and IMGUI: IMGUI-based")]
    public static void ShowMe () => Example.ShowExample<ExampleWindow1>();

    private VisualElement leftElement, rightElement;

    void OnEnable () {

        // Create some VisualElements. The most important thing is that they are set
        // for absolute positioning!!! Store them in private fields so we can update
        // their location in OnGUI. Add them to the root as usual.

        rootVisualElement.Add(leftElement = Example.CreateSomeKindOfVisualElement());
        rootVisualElement.Add(rightElement = Example.CreateSomeKindOfVisualElement());

        // This is the magic, it will take them out of the normal flexbox flow:
        leftElement.style.position = Position.Absolute;
        rightElement.style.position = Position.Absolute;

    }

    void OnGUI () {

        EditorGUILayout.BeginVertical(Example.MarginStyle);
        GUILayout.Label($"An example of laying out VisualElements with IMGUI:", Example.HeaderStyle);

        using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandHeight(true))) {

            // Repaint logic explained in http://answers.unity.com/answers/1349110/view.html

            Rect leftRect = GUILayoutUtility.GetRect(0, 4000, 0, 4000);
            if (Event.current.type == EventType.Repaint) {
                leftElement.style.top = leftRect.yMin;
                leftElement.style.left = leftRect.xMin;
                leftElement.style.width = leftRect.width;
                leftElement.style.height = leftRect.height;
            }

            GUILayout.Space(Example.Spacing);

            Rect rightRect = GUILayoutUtility.GetRect(0, 4000, 0, 4000);
            if (Event.current.type == EventType.Repaint) {
                rightElement.style.top = rightRect.yMin;
                rightElement.style.left = rightRect.xMin;
                rightElement.style.width = rightRect.width;
                rightElement.style.height = rightRect.height;
            }

        }

        using (new EditorGUILayout.HorizontalScope(Example.FooterStyle)) {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("OK", Example.LargeButtonStyle)) Close();
            if (GUILayout.Button("Cancel", Example.LargeButtonStyle)) Close();
        }
        
        EditorGUILayout.EndVertical();

    }

}
