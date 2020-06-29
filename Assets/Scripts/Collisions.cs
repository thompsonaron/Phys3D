using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Collisions : MonoBehaviour
{
    public GameObject[] solids;
    public GameObject[] liquids;
    private Collider col;
    public float displacedV;
    public bool isInWater = false;
    private void Awake()
    {
        solids = GameObject.FindGameObjectsWithTag("Wall");
        liquids = GameObject.FindGameObjectsWithTag("Liquid");
    }

    public bool CollidingBottomYAxis(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < solids.Length; i++)
        {
            if (obj.transform.position.y - col.bounds.extents.y <= solids[i].transform.position.y + solids[i].transform.GetComponent<Collider>().bounds.extents.y)
            {
                if ((obj.transform.position.y + col.bounds.extents.y >= solids[i].transform.position.y + solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.x - col.bounds.extents.x <= solids[i].transform.position.x + solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.x + col.bounds.extents.x >= solids[i].transform.position.x - solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.z - col.bounds.extents.z <= solids[i].transform.position.z + solids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.z + col.bounds.extents.z >= solids[i].transform.position.z - solids[i].transform.GetComponent<Collider>().bounds.extents.z)) 
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, solids[i].transform.position.y + solids[i].transform.GetComponent<Collider>().bounds.extents.y + col.bounds.extents.y+0.05f, obj.transform.position.z);
                    return true;
                }
            }
        }
        return false;
    }
    public bool CollidingTopYAxis(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < solids.Length; i++)
        {
            if (obj.transform.position.y + col.bounds.extents.y >= solids[i].transform.position.y - solids[i].transform.GetComponent<Collider>().bounds.extents.y)
            {
                if ((obj.transform.position.y - col.bounds.extents.y <= solids[i].transform.position.y - solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.x - col.bounds.extents.x <= solids[i].transform.position.x + solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.x + col.bounds.extents.x >= solids[i].transform.position.x - solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.z - col.bounds.extents.z <= solids[i].transform.position.z + solids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.z + col.bounds.extents.z >= solids[i].transform.position.z - solids[i].transform.GetComponent<Collider>().bounds.extents.z))
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, solids[i].transform.position.y - solids[i].transform.GetComponent<Collider>().bounds.extents.y - col.bounds.extents.y - 0.05f, obj.transform.position.z);
                    return true;
                }
            }
        }
        return false;
    }

    public bool CollidingRight(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < solids.Length; i++)
        {
            if (obj.transform.position.x + col.bounds.extents.x >= solids[i].transform.position.x - solids[i].transform.GetComponent<Collider>().bounds.extents.x)
            {
                if ((obj.transform.position.x - col.bounds.extents.x <= solids[i].transform.position.x + solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.y - col.bounds.extents.y <= solids[i].transform.position.y + solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.y + col.bounds.extents.y >= solids[i].transform.position.y - solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.z - col.bounds.extents.z <= solids[i].transform.position.z + solids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.z + col.bounds.extents.z >= solids[i].transform.position.z - solids[i].transform.GetComponent<Collider>().bounds.extents.z))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CollidingLeft(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < solids.Length; i++)
        {
            if (obj.transform.position.x - col.bounds.extents.x <= solids[i].transform.position.x + solids[i].transform.GetComponent<Collider>().bounds.extents.x)
            {
                if ((obj.transform.position.x + col.bounds.extents.x >= solids[i].transform.position.x + solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.y - col.bounds.extents.y <= solids[i].transform.position.y + solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.y + col.bounds.extents.y >= solids[i].transform.position.y - solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.z - col.bounds.extents.z <= solids[i].transform.position.z + solids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.z + col.bounds.extents.z >= solids[i].transform.position.z - solids[i].transform.GetComponent<Collider>().bounds.extents.z))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CollidingTopZAxis(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < solids.Length; i++)
        {
            if (obj.transform.position.z + col.bounds.extents.z >= solids[i].transform.position.z - solids[i].transform.GetComponent<Collider>().bounds.extents.z)
            {
                if ((obj.transform.position.z - col.bounds.extents.z <= solids[i].transform.position.z + solids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.y - col.bounds.extents.y <= solids[i].transform.position.y + solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.y + col.bounds.extents.y >= solids[i].transform.position.y - solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.x - col.bounds.extents.x <= solids[i].transform.position.x + solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.x + col.bounds.extents.x >= solids[i].transform.position.x - solids[i].transform.GetComponent<Collider>().bounds.extents.x))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CollidingBottomZAxis(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < solids.Length; i++)
        {
            if (obj.transform.position.z - col.bounds.extents.z <= solids[i].transform.position.z + solids[i].transform.GetComponent<Collider>().bounds.extents.z)
            {
                if ((obj.transform.position.z + col.bounds.extents.z >= solids[i].transform.position.z + solids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.y - col.bounds.extents.y <= solids[i].transform.position.y + solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.y + col.bounds.extents.y >= solids[i].transform.position.y - solids[i].transform.GetComponent<Collider>().bounds.extents.y)
                    && (obj.transform.position.x - col.bounds.extents.x <= solids[i].transform.position.x + solids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.x + col.bounds.extents.x >= solids[i].transform.position.x - solids[i].transform.GetComponent<Collider>().bounds.extents.x))
                {
                    return true;
                }
            }
        }
        return false;
    }


    public bool IsCollidingWaterBottom(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < liquids.Length; i++)
        {
            if (obj.transform.position.y - col.bounds.extents.y <= liquids[i].transform.position.y + liquids[i].transform.GetComponent<Collider>().bounds.extents.y)
            {
                if ((obj.transform.position.x - col.bounds.extents.x <= liquids[i].transform.position.x + liquids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.x + col.bounds.extents.x >= liquids[i].transform.position.x - liquids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.z - col.bounds.extents.z <= liquids[i].transform.position.z + liquids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.z + col.bounds.extents.z >= liquids[i].transform.position.z - liquids[i].transform.GetComponent<Collider>().bounds.extents.z))
                {
                    displacedV = Mathf.Abs((obj.transform.position.y - col.bounds.extents.y) - (liquids[i].transform.position.y + liquids[i].transform.GetComponent<Collider>().bounds.extents.y));
                    if (displacedV > obj.GetComponent<PlayerController>().objVol)
                    {
                        displacedV = obj.GetComponent<PlayerController>().objVol;
                    }
                    isInWater = true;
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsCollidingWaterTop(GameObject obj)
    {
        col = obj.GetComponent<Collider>();
        for (int i = 0; i < liquids.Length; i++)
        {
            if (obj.transform.position.y - col.bounds.extents.y >= liquids[i].transform.position.y + liquids[i].transform.GetComponent<Collider>().bounds.extents.y)
            {
                if ((obj.transform.position.x - col.bounds.extents.x <= liquids[i].transform.position.x + liquids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.x + col.bounds.extents.x >= liquids[i].transform.position.x - liquids[i].transform.GetComponent<Collider>().bounds.extents.x)
                    && (obj.transform.position.z - col.bounds.extents.z <= liquids[i].transform.position.z + liquids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && (obj.transform.position.z + col.bounds.extents.z >= liquids[i].transform.position.z - liquids[i].transform.GetComponent<Collider>().bounds.extents.z)
                    && isInWater)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
}
