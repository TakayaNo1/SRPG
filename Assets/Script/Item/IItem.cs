using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void Action();
    string GetItemName();
    string GetItemDescription();
    bool IsAvailableInBattle();
    bool IsAvailableInMap();
    bool IsAvailableInShop();
}
