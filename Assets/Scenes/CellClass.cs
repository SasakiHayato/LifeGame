using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type
{
    Bone,
    Deid,
}

public class CellClass : MonoBehaviour
{
    [SerializeField] Image m_cellImage;

    bool m_boneBool = false;
    bool m_diedBool = false;
    public bool SetBone { get => m_boneBool; set { m_boneBool = value; } }
    public bool SetDeid { get => m_diedBool; set { m_diedBool = value; } }

    Type m_type;
    public Type TypeEnum { get => m_type; set { m_type = value; } }

    public void SetColor(Type type)
    {
        if (type == Type.Bone) m_cellImage.color = Color.black;
        else m_cellImage.color = Color.white;
    }
}
