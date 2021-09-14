using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGameManager : MonoBehaviour
{
    [SerializeField] CellClass m_cell;
    CellClass[,] m_cells;

    [SerializeField] int m_wide; // 10
    [SerializeField] int m_hight; // 35
    [SerializeField] bool m_isDebug;
    [SerializeField] float m_time;
    float m_setTime;
    void Start()
    {
        m_cells = new CellClass[m_wide, m_hight];
        CreateCells();
    }

    void Update()
    {
        m_setTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && m_isDebug)
        {
            for (int x = 0; x < m_wide; x++)
            {
                for (int y = 0; y < m_hight; y++)
                {
                    CheckAround(x, y);
                }
            }
        }

        if (!m_isDebug && m_time < m_setTime)
        {
            m_setTime = 0;
            for (int x = 0; x < m_wide; x++)
            {
                for (int y = 0; y < m_hight; y++)
                {
                    CheckAround(x, y);
                }
            }
        }

        for (int x = 0; x < m_wide; x++)
        {
            for (int y = 0; y < m_hight; y++)
            {
                ChengeType(x, y);
            }
        }
        
    }

    void CheckAround(int x, int y)
    {
        bool setBool = true;
        int count = 0;
        for (int targetX = x - 1; targetX <= x + 1; targetX++)
        {
            for (int targetY = y - 1; targetY <= y + 1; targetY++)
            {
                if (targetX < 0 || targetX >= m_wide) continue;
                if (targetY < 0 || targetY >= m_hight) continue;
                if (targetX == x && targetY == y) continue;

                if (m_cells[targetX, targetY].TypeEnum == Type.Bone)
                {
                    count++;
                }
            }
        }
        // íaê∂
        if (count == 3 && setBool)
        {
            if (m_cells[x, y].TypeEnum == Type.Deid)
            {
                //m_cells[x, y].TypeEnum = Type.Bone;
                m_cells[x, y].SetBone = true;
                setBool = false;
            }
        }

        // ê∂ë∂
        if (count == 2 || count == 3 && setBool)
        {
            if (m_cells[x, y].TypeEnum == Type.Bone)
            {
                //m_cells[x, y].TypeEnum = Type.Bone;
                m_cells[x, y].SetBone = true;
                setBool = false;
            }
        }

        // âﬂëa
        if (count <= 1 && setBool)
        {
            if (m_cells[x, y].TypeEnum == Type.Bone)
            {
                //m_cells[x, y].TypeEnum = Type.Deid;
                m_cells[x, y].SetDeid = true;
                setBool = false;
            }
        }

        // âﬂñß
        if (count >= 4 && setBool)
        {
            if (m_cells[x, y].TypeEnum == Type.Bone)
            {
                //m_cells[x, y].TypeEnum = Type.Deid;
                m_cells[x, y].SetDeid = true;
            }
        }

        m_cells[x, y].SetColor(m_cells[x, y].TypeEnum);
    }

    void ChengeType(int x, int y)
    {
        if(m_cells[x, y].SetBone)
        {
            m_cells[x, y].TypeEnum = Type.Bone;
        }
        else if (m_cells[x, y].SetDeid)
        {
            m_cells[x, y].TypeEnum = Type.Deid;
        }

        m_cells[x, y].SetDeid = false;
        m_cells[x, y].SetBone = false;
    }

    void CreateCells()
    {
        for (int x = 0; x < m_wide; x++)
        {
            for (int y = 0; y < m_hight; y++)
            {
                GameObject setObject = Instantiate(m_cell.gameObject, transform);
                setObject.name = $"X :{x} Y :{y}";
                CellClass setCell = setObject.GetComponent<CellClass>();
                m_cells[x, y] = setCell;
                SetType(m_cells[x, y]);
                m_cells[x, y].SetColor(m_cells[x, y].TypeEnum);
            }
        }
    }
    void SetType(CellClass cell)
    {
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                cell.TypeEnum = Type.Bone;
                break;
            case 1:
                cell.TypeEnum = Type.Deid;
                break;
        }
    }
}
