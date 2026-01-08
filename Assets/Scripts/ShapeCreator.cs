using UnityEngine;

[ExecuteAlways]
public class ShapeCreator : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] Vector3Int[] activeCubes;
    [SerializeField] Vector3Int localPosition = new Vector3Int(1, 0, 2);

    [SerializeField] bool activate = false;

    private void Update()
    {
        if (activate)
        {
            activate = false;

#if UNITY_EDITOR
            bool[][][] formingCubes = new bool[CubeShape.MaxCubeXDimensions][][];

            for(int x = 0; x < CubeShape.MaxCubeXDimensions; ++x)
            {
                formingCubes[x] = new bool[CubeShape.MaxCubeYDimensions][];

                for (int y = 0; y < CubeShape.MaxCubeYDimensions; ++y)
                {
                    formingCubes[x][y] = new bool[CubeShape.MaxCubeZDimensions];
                }
            }

            foreach(Vector3Int vec in activeCubes)
            {
                formingCubes[vec.x][vec.y][vec.z] = true;
            }

            CubeShape.CreateNew(formingCubes, cubePrefab, localPosition, transform);
#endif
        }
    }
}
