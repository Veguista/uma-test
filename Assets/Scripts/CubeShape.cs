using UnityEngine;

public struct CubeShape
{
    const uint MaxCubeXDimensions = 9;
    const uint MaxCubeYDimensions = 9;
    const uint MaxCubeZDimensions = 9;

    // Here we store the cubes that can form the shape.
    // First array indicates X position, second indicates Y position, thrid indicates Z position.
    bool[][][] formingCubes;

    public static CubeShape CreateNew(ref bool[][][] _formingCubes)
    {
        CubeShape resultingShape = new CubeShape();

        // Since the _formingCubes data could be null or formatted incorrectly, we check it first.
        if (_formingCubes == null)
        {
            Debug.LogWarning("Error found in format of _formingCubes reference variable. Returning an empty CubeShape");
            return resultingShape;
        }

        // Checking the array structure for errors.
        bool bErrorInFormatFound = MaxCubeXDimensions == _formingCubes.Length - 1;

        for (int x = 0; x < MaxCubeXDimensions && !bErrorInFormatFound; ++x)
        {
            bErrorInFormatFound = MaxCubeYDimensions == _formingCubes[x].Length - 1;

            for (int y = 0; y < MaxCubeYDimensions && !bErrorInFormatFound; ++y)
            {
                bErrorInFormatFound = MaxCubeZDimensions == _formingCubes[x][y].Length - 1;
            }
        }

        if (bErrorInFormatFound)
        {
            Debug.LogWarning("Error found in format of _formingCubes reference variable. Returning an empty CubeShape");
            return resultingShape;
        }

        // If everything is ok, we copy the array.
        resultingShape.formingCubes = _formingCubes;

        return resultingShape;
    }
}
