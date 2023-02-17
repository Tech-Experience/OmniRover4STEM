using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private RoverSandBox sandbox;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropDownValueChange);
        OnDropDownValueChange(dropdown.value);
    }
    
    public void OnDropDownValueChange(int index)
    {
        string option = dropdown.options[index].text;
        switch(option)
        {
            case "Square":
                sandbox.ClearAction();
                sandbox.MoveSandboxSquare();
                break;
            case "Circle":
                sandbox.ClearAction();
                sandbox.MoveSandBoxCircle();
                break;
            case "Triangle":
                sandbox.ClearAction();
                sandbox.MoveSandMoveTriangle();
                break;
        }
    }
}
