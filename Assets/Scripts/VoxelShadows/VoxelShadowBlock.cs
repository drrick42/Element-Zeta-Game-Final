using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Cubiquity;
using System.IO;

public class VoxelShadowBlock : MonoBehaviour {

    /// <summary>
    /// Cubiquity needs time to get its shit sorted before we can start fucking with its voxels.
    /// This is a random number which should hopefully be enough frames.
    /// Voxel Shadows will not work until this many frames has passed.
    /// </summary>
    public static int InitFrames { get { return 120; } }

    public enum BlockType { DarkSolid, LightSolid };
    public BlockType blockType;

    public float blockSize = 1;
    public int blocksPerFrame = 500;

    public bool printNumBlocks = false;

    public int WidthX { get; private set; }
    public int WidthY { get; private set; }
    public int WidthZ { get; private set; }

    private QuantizedColor inLightColor;
    private QuantizedColor inDarkColor;

    private ColoredCubesVolume volume;
    private bool updateBlocks;
    private bool pauseFromNoLight;
    private int init;

    private LayerMask layerMask;

    void Awake () {

        //set appropriate material
        GetComponent<ColoredCubesVolumeRenderer>().material = Settings.DarkSolidMat;
        GetComponent<ColoredCubesVolumeRenderer>().material.color = Color.white;
        //GetComponent<ColoredCubesVolumeRenderer>().material =
        //    (blockType == BlockType.DarkSolid) ?
        //    Settings.DarkSolidMat :
        //    Settings.LightSolidMat;

        ParticleSystem p;
        if(blockType == BlockType.DarkSolid)
            p = Instantiate(Resources.Load<GameObject>("Prefabs/GameObjects/ShadowBlockPrefabs/DarkSolidParticles")).GetComponent<ParticleSystem>();
        else
            p = Instantiate(Resources.Load<GameObject>("Prefabs/GameObjects/ShadowBlockPrefabs/LightSolidParticles")).GetComponent<ParticleSystem>();
        if (p != null)
        {
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(1, 1, 1);
            p.transform.position = transform.TransformPoint(0.5f * localScale);
            p.transform.rotation = transform.rotation;
            transform.localScale = localScale;

            var shape = p.shape;
            shape.box = transform.localScale;

            float v = transform.localScale.x * transform.localScale.y * transform.localScale.z;
            p.emissionRate = (int)(v / 20);

        }

        volume = GetComponent<ColoredCubesVolume>();
        updateBlocks = true;
        pauseFromNoLight = false;
        init = 0;

        Vector3 scale = transform.localScale;
        float scaleX = 1;
        float scaleY = 1;
        float scaleZ = 1;
        WidthX = GetScaledNumBlocks(scale.x, blockSize, out scaleX);
        WidthY = GetScaledNumBlocks(scale.y, blockSize, out scaleY);
        WidthZ = GetScaledNumBlocks(scale.z, blockSize, out scaleZ);

        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        //Debug.Log(widthX + " " + widthY + " " + widthZ);

        transform.position = new Vector3(transform.position.x + blockSize / 2, transform.position.y + blockSize/2, transform.position.z + blockSize / 2);

        int minRegion = 25;
        int regionX = (int)Mathf.Max(minRegion, WidthX);
        int regionY = (int)Mathf.Max(minRegion, WidthY);
        int regionZ = (int)Mathf.Max(minRegion, WidthZ);
        volume.data = VolumeData.CreateEmptyVolumeData<ColoredCubesVolumeData>(new Region(0, 0, 0, regionX, regionY, regionZ));
        
        GetComponent<MeshRenderer>().enabled = false;

        if(blockType == BlockType.DarkSolid)
        {
            inDarkColor = new QuantizedColor(0, 200, 0, 255);
            inLightColor = new QuantizedColor(0, 0, 0, 0);
        }
        else
        {
            inLightColor = new QuantizedColor(255, 0, 255, 255);
            inDarkColor = new QuantizedColor(0, 0, 0, 0);
        }

        if(printNumBlocks)
            Debug.Log(WidthX * WidthY * WidthZ);

        layerMask = ~((1 << LayerMask.NameToLayer("Ignored By Voxel Shadows")) | (1 << LayerMask.NameToLayer("Ignore Raycast")));

        //GetComponent<VolumeRenderer>().enabled = false;

        //StartCoroutine("UpdateBlocks");
        //updateBlocks = false;
        //int t = 20;
        //for (int x = 0; x < t; x++)
        //    for (int y = 0; y < t; y++)
        //        for (int z = 0; z < t; z++)
        //            volume.data.SetVoxel(x, y, z, new QuantizedColor(0, 0, 0, 0));

        //t = size;
        //for (int x = 0; x < t; x++)
        //    for (int y = 0; y < t; y++) 
        //        for(int z = 0; z < t; z++)
        //            volume.data.SetVoxel(x, y, z, new QuantizedColor(1, 0, 0, 255));
        //volume.data.CommitChanges();
    }
	
	void Update () {
        //ColoredCubesVolumeRenderer r = GetComponent<ColoredCubesVolumeRenderer>();

        if (init < InitFrames)
        {
            init++;
            return;
        }
        //else if(init == 5)
        //{
        //    GetComponent<VolumeRenderer>().enabled = true;
        //}

        if (updateBlocks)
        {
            //r.showWireframe = false;
            updateBlocks = false;
            StartCoroutine("UpdateBlocks");
        }


    }

    private int GetScaledNumBlocks(float scale, float block, out float newScale)
    {
        float div = scale / block;
        
        int numBlocks = Mathf.RoundToInt(div);
        
        newScale = block * (numBlocks/div);
        
        return numBlocks;
    }

    IEnumerator UpdateBlocks()
    {
        int count = 0;

        List<ILightEmitter> activeLights = new List<ILightEmitter>();
        foreach(ILightEmitter light in LightDeviceManager.pointLights)
        {
            if (light.Emitting && Vector3.Distance(light.Transform.position, transform.position) <= light.Transform.GetComponent<Light>().range)
                activeLights.Add(light);
        }

        if (activeLights.Count == 0)
        {
            if (!pauseFromNoLight)
            {
                pauseFromNoLight = true;
                for (int x = 0; x < WidthX; x++)
                {
                    int x1 = GetRandomBijection(x, WidthX);
                    for (int y = 0; y < WidthY; y++)
                    {
                        int y1 = GetRandomBijection(y, WidthY);
                        for (int z = 0; z < WidthZ; z++)
                        {
                            int z1 = GetRandomBijection(z, WidthZ);

                            volume.data.SetVoxel(x1, y1, z1, inDarkColor);

                            count++;
                            if (count >= blocksPerFrame)
                            {
                                count = 0;
                                yield return null;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            pauseFromNoLight = false;
            for (int x = 0; x < WidthX; x++)
            {
                int x1 = GetRandomBijection(x, WidthX);
                for (int y = 0; y < WidthY; y++)
                {
                    int y1 = GetRandomBijection(y, WidthY);
                    for (int z = 0; z < WidthZ; z++)
                    {
                        int z1 = GetRandomBijection(z, WidthZ);

                        foreach (ILightEmitter light in activeLights)
                        {
                            count++;

                            RaycastHit info;
                            Vector3 blockPos = transform.TransformPoint(new Vector3(x1, y1, z1));// transform.position + new Vector3(x, y, z);
                            Vector3 lightPos = light.Transform.position;
                            //Debug.DrawLine(blockPos, lightPos);
                            var dir = lightPos - blockPos;
                            bool col = Physics.Raycast(blockPos, dir, out info, dir.magnitude, layerMask);
                            
                            if (!col)
                            {
                                if (volume.data.GetVoxel(x1, y1, z1).alpha == inDarkColor.alpha)
                                {
                                    volume.data.SetVoxel(x1, y1, z1, inLightColor);
                                }
                                break;
                            }
                            else if(volume.data.GetVoxel(x1, y1, z1).alpha == inLightColor.alpha)
                            {
                                volume.data.SetVoxel(x1, y1, z1, inDarkColor);
                            }
                        }
                        if (count >= blocksPerFrame)
                        {
                            count = 0;
                            yield return null;
                        }
                    }
                }
            }
        }
        updateBlocks = true;
    }

    private int GetRandomBijection(int i, int width)
    {
        //return i;
        if (i % 2 != 0)
        {
            int result = width - i;
            if (width % 2 != 0) result--;
            return result;
        }
        else
        {
            return i;
        }
    }
}
