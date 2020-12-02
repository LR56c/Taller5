using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes {Jump,Shoot,Shield }

public class ItemBase : MonoBehaviour
{
    public ItemTypes itemType;
}
