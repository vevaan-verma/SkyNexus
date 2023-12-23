using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack {

    [SerializeField] private Item item;
    [SerializeField] private int quantity;

    public ItemStack(Item item, int quantity) {

        this.item = item;
        this.quantity = quantity;

    }

    public Item GetItem() { return item; }

    public void SetItem(Item item) { this.item = item; }

    public int GetQuantity() { return quantity; }

    public void SetQuantity(int quantity) { this.quantity = quantity; }

    public override bool Equals(object other) {

        if (!(other is ItemStack)) return false;

        ItemStack otherStack = (ItemStack) other;

        return item.Equals(otherStack.GetItem()) && quantity.Equals(otherStack.GetQuantity());

    }

    public override int GetHashCode() {

        return base.GetHashCode();

    }
}
