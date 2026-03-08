using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OutputListenerKeybinds;
public class KeybindZ : MonoBehaviour, IKeybind
{
    public GameObject objectPrefab;
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f);
    public bool spawnObject = false;
    public string output = "";

    private Animator anim;
    [SerializeField] private bool isSupport = false;

    public string GetOutput()
    {
        return output;
    }

    void Update()
    {
        anim = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            KeybindManager.isProducingKeybind = true;

                if (spawnObject == true)
            {
                Vector3 spawnPosition = spawnOffset;
                GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
            output = "Support";
            isSupport = true;
        }
        else
        {
            output = "";
            isSupport = false;

        }
        anim.SetBool("isSupport", isSupport);
    }
    void ResetKeybindFlag()
    {
        KeybindManager.isProducingKeybind = false;
    }
}