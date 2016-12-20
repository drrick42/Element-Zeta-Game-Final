using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ConvertibleBlock))]
public class NewBehaviourScript : Editor {

    int cubesize = 1;
    int stairsize = 1;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        cubesize = EditorGUILayout.IntField("Cube Size", cubesize);
        if (GUILayout.Button("Convert to Cubes"))
        {
            var orig = ((ConvertibleBlock)target).gameObject;
            var scale = orig.transform.localScale;
            GameObject parent = new GameObject();
            parent.name = orig.name;
            parent.transform.parent = orig.transform.parent;
            Vector3 start = orig.transform.position - scale / 2 + Vector3.one / 2 * cubesize;
            Vector3 end = orig.transform.position + scale / 2;
            for(float x = start.x; x <= end.x; x+= cubesize)
                for(float y = start.y; y <= end.y; y+= cubesize)
                    for (float z = start.z; z <= end.z; z+= cubesize)
                    {
                        var newObject = (GameObject)Instantiate(orig);
                        DestroyImmediate(newObject.GetComponent<ConvertibleBlock>());
                        newObject.transform.parent = parent.transform;
                        newObject.transform.localScale = Vector3.one * cubesize;
                        newObject.transform.position = new Vector3(x, y, z);
                    }
            //DestroyImmediate(orig);
            orig.SetActive(false);
        }
        stairsize = EditorGUILayout.IntField("Stair Size", cubesize);
        if (GUILayout.Button("Convert to Stairs"))
        {
            //keep z constant
            var orig = ((ConvertibleBlock)target).gameObject;
            var scale = orig.transform.localScale;
            GameObject parent = new GameObject();
            parent.name = orig.name;
            parent.transform.position = orig.transform.position;
            parent.transform.parent = orig.transform.parent;
            float startZ = - (-stairsize + scale.z) / 2;
            float startY = - (-stairsize + scale.y) / 2;
            float numStairs = (int) scale.y;
            for (int i = 0; i < numStairs; i+= stairsize)
            {
                var newObject = (GameObject)Instantiate(orig);
                DestroyImmediate(newObject.GetComponent<ConvertibleBlock>());
                newObject.transform.parent = parent.transform;

                newObject.transform.localScale = new Vector3(scale.x, stairsize, stairsize);
                newObject.transform.localPosition = new Vector3(0, startY + i*stairsize, startZ + i*stairsize);
            }
            //DestroyImmediate(orig);
            orig.SetActive(false);
        }        
    }
}
