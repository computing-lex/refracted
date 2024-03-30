/*
This file has modifications from the original file:
We use multi-sampled layers of 2D noise to generate 3D or 4D noise
We get 2D noise samples from a texture instead of calculating them
*/

using System;
using System.Collections.Generic;
using UnityEngine;
public class NoiseGregory
{
    public Texture2D NoiseTexture;
    /// <summary>
    /// Generates value, typically in range [-1, 1]
    /// </summary>
    
    public float Evaluate(UnityEngine.Vector3 point)
    {
        return Evaluate3(point);
    }
    public float Evaluate3(UnityEngine.Vector3 point)
    {
        int x = (int) (point.x * 255.0);
        int y = (int) (point.y * 255.0);
        int z = (int) (point.z * 255.0);

        float xy = NoiseTexture.GetPixel(x, y).r;
        float xz = NoiseTexture.GetPixel(x, z).r;

        float yz = NoiseTexture.GetPixel(y, z).r;
        float yx = NoiseTexture.GetPixel(y, x).r;
 
        float zx = NoiseTexture.GetPixel(z, x).r;
        float zy = NoiseTexture.GetPixel(z, y).r;
 
        return (float) ((xy + xz + yz + yx + zx + zy) / 6.0);
    }
    public float Evaluate4(UnityEngine.Vector4 point)
    {
        int x = (int) (point.x * 255.0);
        int y = (int) (point.y * 255.0);
        int z = (int) (point.z * 255.0);
        int w = (int) (point.w * 255.0);

        //X coordinate
        float xy = NoiseTexture.GetPixel(x, y).r;
        float xz = NoiseTexture.GetPixel(x, z).r;
        float xw = NoiseTexture.GetPixel(x, w).r;
        //Ycoordinate
        float yx = NoiseTexture.GetPixel(y, x).r;
        float yz = NoiseTexture.GetPixel(y, z).r;
        float yw = NoiseTexture.GetPixel(y, w).r;

        //Z coordinate
        float zx = NoiseTexture.GetPixel(z, x).r;
        float zy = NoiseTexture.GetPixel(z, y).r;
        float zw = NoiseTexture.GetPixel(z, w).r;

        //W coordinate
        float wx = NoiseTexture.GetPixel(w, x).r;
        float wy = NoiseTexture.GetPixel(w, y).r;
        float wz = NoiseTexture.GetPixel(w, z).r;

        return (float) ((xy + xz + xw + yx + yz + yw + zx + zy + zw + wx + wy + wz) / 12.0);
    }
}

