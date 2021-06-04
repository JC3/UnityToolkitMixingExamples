using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GUILayoutExExampleWindow : EditorWindow {

    [MenuItem("Examples/Mixing UI Toolkit and IMGUI: IMGUI + GUILayoutEx.UIElement")]
    public static void ShowMe () => Example.ShowExample<GUILayoutExExampleWindow>(new Vector2(500, 400));

    private VisualElement left, right1, right2, right3;

    private const int TightSpacing = Example.Spacing / 2;
    private GUIStyle right2Spacing; // to show GUIStyle support

    void OnEnable () {

        VisualElement right2a, right2b;
        
        rootVisualElement.Add(left = WithLabel("Regrets", Example.CreateSomeKindOfVisualElement()));
        rootVisualElement.Add(right1 = WithLabel("Enemies", Example.CreateSomeKindOfVisualElement()));
        rootVisualElement.Add(right2 = new VisualElement());
        rootVisualElement.Add(right3 = WithLabel("Pets", Example.CreateSomeKindOfVisualElement()));
        
        right2.style.flexDirection = FlexDirection.Row;
        right2.Add(right2a = WithLabel("Weapons", Example.CreateSomeKindOfVisualElement()));
        right2.Add(right2b = WithLabel("Missing Limbs", Example.CreateSomeKindOfVisualElement()));
        right2a.style.flexGrow = 1;
        right2a.style.marginRight = TightSpacing;
        right2b.style.flexGrow = 3;
        
        right2Spacing ??= new GUIStyle() { margin = new RectOffset(0, 0, TightSpacing, TightSpacing) };
    
    }

    void OnGUI () {

        EditorGUILayout.BeginVertical(Example.MarginStyle);
        GUILayout.Label($"An unnecessarily complicated example of using GUILayoutEX.UIElement():", Example.HeaderStyle);

        using (new EditorGUILayout.HorizontalScope()) {
            GUILayoutEx.UIElement(left);
            GUILayout.Space(TightSpacing);
            using (new EditorGUILayout.VerticalScope()) {
                GUILayoutEx.UIElement(right1, GUILayout.MinHeight(70));
                GUILayoutEx.UIElement(right2, right2Spacing, GUILayout.MinHeight(70));
                GUILayoutEx.UIElement(right3, GUILayout.MinHeight(70));
            }
        }

        using (new EditorGUILayout.HorizontalScope(Example.FooterStyle)) {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("OK", Example.LargeButtonStyle)) Close();
            if (GUILayout.Button("Cancel", Example.LargeButtonStyle)) Close();
        }

        EditorGUILayout.EndVertical();

    }

    private static VisualElement WithLabel (string text, VisualElement element) {

        VisualElement group = new VisualElement(), label;
        group.Add(label = new Label(text + ":"));
        group.Add(element);

        label.style.color = new Color(0.95f, 0.95f, 0.95f);
        label.style.backgroundColor = new Color(0.25f, 0.25f, 0.25f);
        label.style.borderTopColor = label.style.backgroundColor;
        label.style.borderLeftColor = label.style.backgroundColor;
        label.style.borderRightColor = label.style.backgroundColor;
        label.style.borderTopWidth = element.style.borderTopWidth;
        label.style.borderLeftWidth = element.style.borderLeftWidth;
        label.style.borderRightWidth = element.style.borderRightWidth;
        label.style.borderTopLeftRadius = element.style.borderTopLeftRadius;
        label.style.borderTopRightRadius = element.style.borderTopRightRadius;
        label.style.paddingLeft = 2;
        label.style.paddingRight = 2;
        label.style.paddingTop = 2;
        label.style.paddingBottom = 2;

        element.style.borderTopLeftRadius = 0;
        element.style.borderTopRightRadius = 0;
        element.style.flexGrow = 1;

        return group;

    }

}
