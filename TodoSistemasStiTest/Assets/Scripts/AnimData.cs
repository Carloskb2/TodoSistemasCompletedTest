using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class AnimData : MonoBehaviour
{
    [SerializeField] float saveEverySeconds = 5f;
    [SerializeField] List<Transform> allBones = new List<Transform>();

    AnimationTransforms myData = new AnimationTransforms();
    float saveTimer;

    private void Start()
    {
        Application.targetFrameRate = 120;
    }

    private void Update()
    {
        foreach (Transform bodyPart in allBones)
        {
            AnimationPart newData = new AnimationPart();
            newData.BodyPart = bodyPart.name;
            newData.Position = bodyPart.position;
            newData.FrameNumber = Time.frameCount;

            myData.Parts.Add(newData);
        }
       
        saveTimer += Time.deltaTime;

        //Every 5 seconds execute this block
        if (saveTimer > saveEverySeconds)
        {
            saveTimer = 0;

            //DO expensive save sequence here

            if (Application.isEditor) 
            {
                string json = JsonUtility.ToJson(myData);

                string savePath = Path.Combine(Application.dataPath + "/Data", "animation.json");
                Debug.Log($"Saving at path[{savePath}] JSON: {json}");
                File.WriteAllText(savePath, json);
            }

            else 
            {
                string json = JsonUtility.ToJson(myData);

                string savePath = Path.Combine(Application.persistentDataPath + "/Data", "animation.json");
                Debug.Log($"Saving at path[{savePath}] JSON: {json}");
                File.WriteAllText(savePath, json);
            }
        }
    }
}