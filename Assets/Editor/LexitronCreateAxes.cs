/* LexitronCreateAxes.cs
 * 
 * Install script that defines Lexitron stick axes (D-Pads) in Input Manager.
 *
 * 1. Place this script in your project's Assets/Editor/ directory. Bam! Installed.
 * 2. Open Edit -> Project Settings -> Input and verify that there are 4 "Lexitron" axes.
 * 3. Delete this script from your project. It's done.
 * 
 * All of this code shamelessly swiped from: 
 *   http://www.plyoung.com/blog/manipulating-input-manager-in-script.html
 *   http://answers.unity3d.com/questions/26994/running-a-script-when-unity-starts.html
 */

using UnityEditor;

public enum AxisType {
  KeyOrMouseButton = 0,
  MouseMovement = 1,
  JoystickAxis = 2
};

public class InputAxis {
  public string name;
  public string descriptiveName;
  public string descriptiveNegativeName;
  public string negativeButton;
  public string positiveButton;
  public string altNegativeButton;
  public string altPositiveButton;
  
  public float gravity;
  public float dead;
  public float sensitivity;
  
  public bool snap = false;
  public bool invert = false;
  
  public AxisType type;
  
  public int axis;
  public int joyNum;
}

[InitializeOnLoad]
public class SetupLexitronInputs {
  static SetupLexitronInputs() {
    AddAxis(new InputAxis() { 
      name = "Lexitron Stick 1 Horizontal", 
      type = AxisType.JoystickAxis, 
      joyNum = 0,
      axis = 9
    });

    AddAxis(new InputAxis() { 
      name = "Lexitron Stick 1 Vertical", 
      type = AxisType.JoystickAxis, 
      joyNum = 0,
      axis = 10
    });

    AddAxis(new InputAxis() { 
      name = "Lexitron Stick 2 Horizontal", 
      type = AxisType.JoystickAxis, 
      joyNum = 1,
      axis = 9
    });

    AddAxis(new InputAxis() { 
      name = "Lexitron Stick 2 Vertical", 
      type = AxisType.JoystickAxis, 
      joyNum = 1,
      axis = 10
    });
  }

  private static void AddAxis(InputAxis axis) {
    if (AxisDefined(axis.name)) return;
    
    SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
    SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
    
    axesProperty.arraySize++;
    serializedObject.ApplyModifiedProperties();
    
    SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);
    
    GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
    GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
    GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
    GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
    GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
    GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
    GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
    GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
    GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
    GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
    GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
    GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
    GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
    GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
    GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;
    
    serializedObject.ApplyModifiedProperties();
  }

  private static bool AxisDefined(string axisName) {
    SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
    SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
    
    axesProperty.Next(true);
    axesProperty.Next(true);
    while (axesProperty.Next(false)) {
      SerializedProperty axis = axesProperty.Copy();
      axis.Next(true);
      if (axis.stringValue == axisName) return true;
    }
    return false;
  }

  private static SerializedProperty GetChildProperty(SerializedProperty parent, string name) {
    SerializedProperty child = parent.Copy();
    child.Next(true);
    do
    {
      if (child.name == name) return child;
    }
    while (child.Next(false));
    return null;
  }

}