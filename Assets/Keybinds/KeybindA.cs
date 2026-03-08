using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OutputListenerKeybinds;
public class KeybindA : MonoBehaviour, IKeybind
{
    public GameObject objectPrefab;
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f);
    public bool spawnObject = false;
    public string output = "";

    private Animator anim;
    [SerializeField] private bool isActivate = false;


    public string GetOutput()
    {
        return output;
    }

    void Update()
    {
        anim = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.A))
        {
            KeybindManager.isProducingKeybind = true;


            if (spawnObject == true)
            {
                Vector3 spawnPosition = spawnOffset;
                GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
            output = "Activate";
            isActivate = true;
        }
        else
        {
            output = "";
            isActivate = false;

        }
        anim.SetBool("isActivate", isActivate);
    }
    void ResetKeybindFlag()
    {
        KeybindManager.isProducingKeybind = false;
    }
}