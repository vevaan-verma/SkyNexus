using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

    private List<ItemStack> contents;

    public Inventory() {

        contents = new List<ItemStack>();

    }

    public void AddItemStack(ItemStack itemStack) {

        int index = contents.IndexOf(itemStack);
        int quantity = contents[index].GetQuantity();

        if (ContainsItemStack(itemStack))
            contents[index].SetQuantity(quantity - itemStack.GetQuantity()); // REFRESH UI
        else
            contents.Add(itemStack); // REFRESH UI

    }

    public void RemoveItemStack(ItemStack itemStack) {

        if (ContainsItemStack(itemStack)) {

            int index = contents.IndexOf(itemStack);
            int quantity = contents[index].GetQuantity();

            if (itemStack.GetQuantity() < quantity)
                contents[index].SetQuantity(quantity - itemStack.GetQuantity()); // REFRESH UI
            else
                contents.RemoveAt(index); // REFRESH UI

        }
    }

    public bool ContainsItemStack(ItemStack itemStack) {

        foreach (ItemStack newStack in contents)
            if (newStack.GetItem().Equals(itemStack.GetItem()) && itemStack.GetQuantity() <= newStack.GetQuantity()) return true;

        return false;

    }

    public List<ItemStack> GetInventoryContents() {

        return contents;

    }
}
