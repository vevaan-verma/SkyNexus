using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    [Header("Data")]
    [SerializeField] private Sprite icon;
    [SerializeField] private new string name;

    public abstract void Interact(Inventory inventory);

    public Sprite GetIcon() {

        return icon;

    }

    public string GetName() {

        return name;

    }
}
