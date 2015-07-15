/* COPYRIGHT (C) Ahmed Al-Hulaibi 2015 
   All rights reserved. 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


[CustomEditor(typeof(Timer))]
public class TimerEditor : Editor{
    float duration = 0.0f;
    string tag = "Enter tag here.";
    string guiMessage = "No errors.";
    bool repeat = true;
    List<bool> markForDelete = new List<bool>();
    public override void OnInspectorGUI()
    {
        TagHelper.AddTag("Timer");
       // GUILayoutOption options = new GUILayoutOption();
        Timer myTarget = target as Timer;
        GUILayout.Label("Duration");
        duration = EditorGUILayout.FloatField(duration);

        GUILayout.Label("Tag");
        tag = GUILayout.TextArea(tag);

        GUILayout.Label("Repeat");
        repeat = GUILayout.Toggle(true,"Set timer to looping. Default is looping.");

        GUILayout.Label(guiMessage);

        if(GUILayout.Button("Add timer."))
        {
            if (!myTarget.Tags.Contains(tag))
            {
                if (duration <= 0)
                {
                    guiMessage = "ERROR: Duration must be greater than 0.";
                }
                else 
                {
                    myTarget.Tags.Add(tag);
                    myTarget.Durations.Add(duration);
                    myTarget.Repeat.Add(repeat);
                    duration = 0.0f;
                    tag = "Enter tag here.";
                    repeat = true;
                    markForDelete.Add(false);
                    EditorUtility.SetDirty(myTarget);
                }
            }
            else if (myTarget.Tags.Contains(tag))
            {
                guiMessage = "ERROR: Tag '" + tag + "' already exists.";
            }
        }

        if (GUILayout.Button("Delete all timers."))
        {
            myTarget.Tags.Clear();
            myTarget.Durations.Clear();
            myTarget.Repeat.Clear();
            EditorUtility.SetDirty(myTarget);
        }

        for (int i = 0; i < myTarget.Tags.Count; i++)
        {
            int n = i + 1;
            
            EditorGUILayout.LabelField("Timer " + n.ToString() + " : "
                                        + myTarget.Tags[i] + ", "
                                        + myTarget.Durations[i].ToString() + " seconds"
                                        + ", Looping: " + myTarget.Repeat[i].ToString());
            if (GUILayout.Button("Delete Timer " + n.ToString()))
            {
                markForDelete[i] = true;
            }
		}

		if(markForDelete.Count != myTarget.Tags.Count)
		{
			markForDelete.Clear();
			for (int i = 0; i < myTarget.Tags.Count; i++)
			{
				markForDelete.Add(false);
			}

		}

        for (int i = markForDelete.Count-1; i > -1; i--)
        {
            if (markForDelete[i])
            {
                myTarget.Tags.RemoveAt(i);
                myTarget.Durations.RemoveAt(i);
                myTarget.Repeat.RemoveAt(i);
                markForDelete.RemoveAt(i);
                EditorUtility.SetDirty(myTarget);
            }
        }

        
        
    }
}
