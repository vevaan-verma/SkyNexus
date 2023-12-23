using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject {

    [SerializeField] private new string name;
    [SerializeField] private int id;
    [SerializeField] private Sprite icon;

    public string GetName() {

        return name;

    }

    public int GetID() {

        return id;

    }

    public Sprite GetIcon() {

        return icon;

    }

    public override bool Equals(object other) {

        if (!(other is Item)) return false;

        Item otherItem = (Item) other;

        return name.Equals(otherItem.GetName()) && id.Equals(otherItem.GetID()) && icon.Equals(otherItem.GetIcon());

    }

    public override int GetHashCode() {

        return base.GetHashCode();

    }
}