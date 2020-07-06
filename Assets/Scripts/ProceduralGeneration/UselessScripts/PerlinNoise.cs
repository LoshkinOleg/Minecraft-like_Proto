using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{

    int[] p = new int[512];

    private int seed = Random.Range(int.MinValue, int.MaxValue);

	

	public float noise1D(float x)
	{
		return noise3D(x, 0, 0);
	}

	public float noise2D(float x, float y)
	{
		return noise3D(x, y, 0);
    }

    public float noise3D(float x, float y, float z)
	{
		//Find unity cube that contains point
        int tx = (int) Mathf.Floor(x); 
        int ty = (int) Mathf.Floor(y);
        int tz = (int) Mathf.Floor(z);

        //Find relative x,y,z of point in cube
        x -= Mathf.Floor(x);
		y -= Mathf.Floor(y);
		z -= Mathf.Floor(z);

		//Compute fade curves for each of x,y,z
		float u = Fade(x);
        float v = Fade(y);
        float w = Fade(z);

        //Hash coordinates of the 8 cube corners
        int a = p[tx] + ty, aa = p[a] + tz, ab = p[a + 1] + tz;
        int b = p[tx + 1] + ty, ba = p[b] + tz, bb = p[b + 1] + tz;

		//Add blended results from 8 corners of cube
		return Lerp(w, Lerp(v, Lerp(u, Grad(p[aa], x, y, z),
			Grad(p[ba], x - 1, y, z)),
			Lerp(u, Grad(p[ab], x, y - 1, z),
				Grad(p[bb], x - 1, y - 1, z))),
			Lerp(v, Lerp(u, Grad(p[aa + 1], x, y, z - 1),
				Grad(p[ba + 1], x - 1, y, z - 1)),
				Lerp(u, Grad(p[ab + 1], x, y - 1, z - 1),
					Grad(p[bb + 1], x - 1, y - 1, z - 1))));
	}

    public void Reseed()
    {
	    for (int i = 0; i < 256; ++i)
	    {
		    p[i] = i;
	    }

        for (int i = 0; i < 256; i++)
        {
            int temp = p[i];
            int randomIndex = Random.Range(i, 256);
            p[i] = p[randomIndex];
            p[randomIndex] = temp;
        }

	    //std::shuffle(std::begin(p), std::begin(p) + 256, std::default_random_engine(seed));

	    for (int i = 0; i < 256; ++i)
	    {
		    p[256 + i] = p[i];
	    }
    }

    float Fade(float t)
	    {
		    return t* t * t * (t* (t* 6 - 15) + 10);	
	    }

	    float Lerp(float t, float a, float b)
	    {
		    return a + t* (b - a);
	    }

 	    float Grad(int hash, float x, float y, float z)
 	    { 
            int h = hash;
            float u = h < 8 ? x : y;
            float v = h < 4 ? y : h == 12 || h == 14 ? x : z;
		    return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
 	    }

	    float Weight(int octaves)
	    {
		    float amp = 1;
            float value = 0;

		    for (int i = 0; i<octaves; i++)
		    {
			    value += amp;
			    amp /= 2;
		    }

		    return value;
	    }
    }
