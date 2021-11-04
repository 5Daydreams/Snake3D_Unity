#ifndef WHITE_NOISE
#define WHITE_NOISE

//to 1d functions

//get a scalar random value from a 3d value
float rand3dTo1d(float3 value, float3 dotDir = float3(52.9898, 71.233, 33.719))
{
    //get scalar value from 3d vector
    float random = dot(value, dotDir);
    //make value more random (but still continuous) by taking the sine then taking the fractional part
    random = frac(sin(random*0.0124239) * 113.297713);
    return random;
    /* Personal Note: (5Daydreams)
     * It is important to reduce the "random" inside the sine otherwise there will be MANY artifacts
     * that being said, it is then necessary to increase the final amplitude given by the sine function,
        * otherwise you'll see several chunks of randomness 
    */ 
}

float rand2dTo1d(float2 value, float2 dotDir = float2(42.9898, 8.233))
{
    float random = dot(value, dotDir);
    random = frac(sin(random*0.0132357) * 108.5453);
    return random;
}

float rand1dTo1d(float value, float mutator = 0.248416)
{
    float random = frac(sin(value + mutator) * 358.54153);
    return random;
}

//to 2d functions

float2 rand3dTo2d(float3 value)
{
    return float2(
        rand3dTo1d(value, float3(12.989, 43.233, 37.719)),
        rand3dTo1d(value, float3(49.346, 15.135, 33.155))
    );
}

float2 rand2dTo2d(float2 value)
{
    return float2(
        rand2dTo1d(value, float2(54.989, 73.233)),
        rand2dTo1d(value, float2(85.346, 11.135))
    );
}

float2 rand1dTo2d(float value)
{
    return float2(
        rand2dTo1d(value, 3.9812),
        rand2dTo1d(value, 7.1536)
    );
}

//to 3d functions

float3 rand3dTo3d(float3 value)
{
    return float3(
        rand3dTo1d(value, float3(12.989, 38.233, -13.719)),
        rand3dTo1d(value, float3(39.346, -11.135, 18.155)),
        rand3dTo1d(value, float3(-17.156, 20.235, 37.151))
    );
}

float3 rand2dTo3d(float2 value)
{
    return float3(
        rand2dTo1d(value, float2(12.989, 78.233)),
        rand2dTo1d(value, float2(39.346, 11.135)),
        rand2dTo1d(value, float2(73.156, 52.235))
    );
}

float3 rand1dTo3d(float value)
{
    return float3(
        rand1dTo1d(value, 13.9812),
        rand1dTo1d(value, 27.1536),
        rand1dTo1d(value, 25.7241)
    );
}

inline float easeIn(float interpolator)
{
    return interpolator * interpolator;
}

float easeOut(float interpolator)
{
    return 1 - easeIn(1 - interpolator);
}

float easeInOut(float interpolator)
{
    float easeInValue = easeIn(interpolator);
    float easeOutValue = easeOut(interpolator);
    return lerp(easeInValue, easeOutValue, interpolator);
}

float PerlinNoise1D(float value)
{
    float fraction = frac(value);
    float interpolator = easeInOut(fraction);

    float previousCellInclination = rand1dTo1d(floor(value)) * 2 - 1;
    float previousCellLinePoint = previousCellInclination * fraction;

    float nextCellInclination = rand1dTo1d(ceil(value)) * 2 - 1;
    float nextCellLinePoint = nextCellInclination * (fraction - 1);

    return lerp(previousCellLinePoint, nextCellLinePoint, interpolator);
}

float PerlinNoise2D(float2 value)
{
    //generate random directions
    float2 lowerLeftDirection = rand2dTo2d(float2(floor(value.x), floor(value.y))) * 2 - 1;
    float2 lowerRightDirection = rand2dTo2d(float2(ceil(value.x), floor(value.y))) * 2 - 1;
    float2 upperLeftDirection = rand2dTo2d(float2(floor(value.x), ceil(value.y))) * 2 - 1;
    float2 upperRightDirection = rand2dTo2d(float2(ceil(value.x), ceil(value.y))) * 2 - 1;

    float2 fraction = frac(value);

    //get values of cells based on fraction and cell directions
    float lowerLeftFunctionValue = dot(lowerLeftDirection, fraction - float2(0, 0));
    float lowerRightFunctionValue = dot(lowerRightDirection, fraction - float2(1, 0));
    float upperLeftFunctionValue = dot(upperLeftDirection, fraction - float2(0, 1));
    float upperRightFunctionValue = dot(upperRightDirection, fraction - float2(1, 1));

    float interpolatorX = easeInOut(fraction.x);
    float interpolatorY = easeInOut(fraction.y);

    //interpolate between values
    float lowerCells = lerp(lowerLeftFunctionValue, lowerRightFunctionValue, interpolatorX);
    float upperCells = lerp(upperLeftFunctionValue, upperRightFunctionValue, interpolatorX);

    float noise = lerp(lowerCells, upperCells, interpolatorY);
    return noise;
}

float PerlinNoise3D(float3 value)
{
    float3 fraction = frac(value);

    float interpolatorX = easeInOut(fraction.x);
    float interpolatorY = easeInOut(fraction.y);
    float interpolatorZ = easeInOut(fraction.z);

    float cellNoiseZ[2];
    [unroll]
    for (int z = 0; z <= 1; z++)
    {
        float cellNoiseY[2];
        [unroll]
        for (int y = 0; y <= 1; y++)
        {
            float cellNoiseX[2];
            [unroll]
            for (int x = 0; x <= 1; x++)
            {
                float3 cell = floor(value) + float3(x, y, z);
                float3 cellDirection = rand3dTo3d(cell) * 2 - 1;
                float3 compareVector = fraction - float3(x, y, z);
                cellNoiseX[x] = dot(cellDirection, compareVector);
            }
            cellNoiseY[y] = lerp(cellNoiseX[0], cellNoiseX[1], interpolatorX);
        }
        cellNoiseZ[z] = lerp(cellNoiseY[0], cellNoiseY[1], interpolatorY);
    }
    float noise = lerp(cellNoiseZ[0], cellNoiseZ[1], interpolatorZ);
    return noise;
}

float cos01(float value)
{
    return (cos(value) + 1) / 2;
}

float sin01(float value)
{
    return (sin(value) + 1) / 2;
}


#endif