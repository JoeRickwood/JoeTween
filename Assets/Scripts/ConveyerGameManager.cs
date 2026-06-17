using System.Collections.Generic;
using UnityEngine;


public enum TileState
{
    None,
    Conveyer,
    Processor
}

public class Tile
{
    public Vector2Int pos;
    public TileState state;
    public GameObject obj;
    public int direction;

    public Tile(Vector2Int _pos, TileState _state, GameObject _obj)
    {
        pos = _pos;
        state = _state;
        obj = _obj;
        direction = 0;
    }
}

public class ConveyerGameManager : MonoBehaviour
{
    public GameObject[] startConveyers;
    public GameObject startProcessor;
    [Space(15)]
    public GameObject resource;

    public Dictionary<Vector2Int, Tile> dict;
    public List<GameObject> resources;

    private float resourceSpawnTimer;
    public float moveSpeed;

    public static Vector2Int WorldToSnappedPos(GameObject _obj)
    {
        return new Vector2Int
        (
            Mathf.RoundToInt(_obj.transform.position.x),
            Mathf.RoundToInt(_obj.transform.position.y)
        );
    }

    public Tile GetTileAtPosition(Vector2Int _pos)
    {
        if(dict.ContainsKey(_pos))
            return dict[_pos];
        else
            return null;
    }

    public void SpawnResource(GameObject _resource)
    {
        GameObject obj = Instantiate(_resource, startProcessor.transform.position, Quaternion.identity);
        resources.Add(obj);
    }

    public void HandleResources()
    {
        foreach(GameObject obj in resources)
        {
            Vector2Int currentPos = WorldToSnappedPos(obj);
            Tile tileAtPos = GetTileAtPosition(currentPos);

            if (tileAtPos == null)
                continue;

            if (tileAtPos.state == TileState.Processor)
            {
                //Process The Resource
                continue;
            }

            obj.transform.position += Time.deltaTime * new Vector3(Mathf.Sin(tileAtPos.direction * 90 * Mathf.Deg2Rad), Mathf.Cos(tileAtPos.direction * 90 * Mathf.Deg2Rad), 0) * moveSpeed;
        }
    }

    private void Start()
    {
        dict = new Dictionary<Vector2Int, Tile>();
        resources = new List<GameObject>();

        foreach (GameObject obj in startConveyers)
        {
            Vector2Int pos = WorldToSnappedPos(obj);
            Tile tile = new Tile(pos, TileState.Conveyer, obj);
            tile.direction = Mathf.RoundToInt(obj.transform.eulerAngles.z / 90f);

            dict.Add(pos, tile);
        }
    }

    private void Update()
    {
        resourceSpawnTimer -= Time.deltaTime;

        if(resourceSpawnTimer <= 0)
        {
            SpawnResource(resource);
            resourceSpawnTimer = 1.0f;
        }

        HandleResources();
    }
}
