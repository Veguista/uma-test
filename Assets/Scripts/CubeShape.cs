using UnityEditor.VersionControl;
using UnityEngine;

public class CubeShape : MonoBehaviour
{
    public const uint MaxCubeXDimensions = 9;
    public const uint MaxCubeYDimensions = 9;
    public const uint MaxCubeZDimensions = 9;

    public Vector3Int relativeChunkPos;

    // Here we store the cubes that can form the shape.
    // First array indicates X position, second indicates Y position, thrid indicates Z position.
    bool[][][] formingCubes;

    private CubeShape() { }
    public static CubeShape CreateNew(in bool[][][] _formingCubes, 
        GameObject _cubePrefab,
        Vector3Int _relativeChunkPos, 
        Transform _parent = null)
    {
        // Since the _formingCubes data could be null or formatted incorrectly, we check it first.
        if (_formingCubes == null)
        {
            Debug.LogWarning("Error found in format of _formingCubes reference variable. Returning an empty CubeShape");
            return null;
        }

        // Checking the array structure for errors.
        bool bErrorInFormatFound = MaxCubeXDimensions != _formingCubes.Length;

        for (int x = 0; x < MaxCubeXDimensions && !bErrorInFormatFound; ++x)
        {
            bErrorInFormatFound = MaxCubeYDimensions != _formingCubes[x].Length;

            for (int y = 0; y < MaxCubeYDimensions && !bErrorInFormatFound; ++y)
            {
                bErrorInFormatFound = MaxCubeZDimensions != _formingCubes[x][y].Length;
            }
        }

        if (bErrorInFormatFound)
        {
            Debug.LogWarning("Error found in format of _formingCubes reference variable. Returning an empty CubeShape");
            return null;
        }

        // We try to find the cube prefab;
        if (!_cubePrefab)
        {
            Debug.LogError("Cube prefab was null. Cannot create a CubeShape.");
            return null;
        }

        // If everything is ok, we create a GameObject.
        GameObject shapeObject = new GameObject();
        shapeObject.transform.SetParent(_parent);
        shapeObject.transform.name = "CubeShape";

        // We attach a CubeShape script to that object.
        CubeShape resultingShape = shapeObject.AddComponent<CubeShape>();

        // Then we copy the array.
        resultingShape.formingCubes = _formingCubes;

        // And Spawn the forming cubes.
        for (int x = 0; x < MaxCubeXDimensions; ++x)
        {
            for (int y = 0; y < MaxCubeYDimensions; ++y)
            {
                for (int z = 0; z < MaxCubeYDimensions; ++z)
                {
                    if (_formingCubes[x][y][z])
                    {
                        GameObject subCube = Instantiate(_cubePrefab, shapeObject.transform);
                        subCube.transform.name = "SubCube";
                        subCube.transform.localPosition = new Vector3(x, y, z) *_cubePrefab.transform.localScale.x;
                    }
                }
            }
        }

        // And the relative position.
        resultingShape.relativeChunkPos = _relativeChunkPos;
        shapeObject.transform.localPosition = (Vector3)_relativeChunkPos * _cubePrefab.transform.localScale.x;
        
        return null; // resultingShape;
    }
}
