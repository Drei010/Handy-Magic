using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OutputListenerKeybinds;
public class KeybindB: MonoBehaviour, IKeybind
{
    public GameObject objectPrefab;
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f);
    public bool spawnObject = false;
    public string output = "";

    private Animator anim;
    [SerializeField] private bool isLaunch = false;


    public string GetOutput()
    {
        return output;
    }

    void Update()
    {
        anim = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.B))
        {
            KeybindManager.isProducingKeybind = true;


            if (spawnObject == true)
            {
                Vector3 spawnPosition = spawnOffset;
                GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
            output = "Launch";
            isLaunch = true;
        }
        else
        {
            output = "";
            isLaunch = false;

        }
        anim.SetBool("isLaunch", isLaunch);
    }
    void ResetKeybindFlag()
    {
        KeybindManager.isProducingKeybind = false;
    }
}