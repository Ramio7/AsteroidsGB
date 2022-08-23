using System;
using UnityEngine;

[Serializable]
public class KeyCommand
{
    [SerializeField] private string keyName;
    public string KeyName { get => keyName; }
    public KeyCode KeyCode { get; }
    public bool KeyDown { get => Input.GetKeyUp(KeyCode); }
    public bool Key { get => Input.GetKey(KeyCode); }
    public bool KeyUp { get => Input.GetKeyUp(KeyCode); }

    public KeyCommand(KeyCode keyCode)
    {
        KeyCode = keyCode;
        keyName = keyCode.ToString();
    }
}
