using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoSingleton<StaticData>
{
    public List<Material> M_FootballMat;

    public List<Material> M_SkinClothMaterials;
    private Material[,] M_SkinClothMat=new Material[3,3];

    protected override void Awake()
    {
        base.Awake();
        InitSkinClothArray();
    }

    public FootBallInfo GetFootballInfo(int i)
    {
        return new FootBallInfo() { M_Coin = i * 200, M_Material = M_FootballMat[i] };
    }

    void InitSkinClothArray()
    {
        Debug.Log(M_SkinClothMaterials.Count);
        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                M_SkinClothMat[i,j] = M_SkinClothMaterials[k];
                k++;
            }
        }
    }
    public SkinInfo GetSkinInfo(int i)
    {
        return new SkinInfo() { M_Coin = i * 200, M_Material = M_SkinClothMat[i,0] };
    }
    public ClothInfo GetClothInfo(int i,int j)
    {
        return new ClothInfo() { M_Coin = j * 200, M_Material = M_SkinClothMat[i,j] };
    }
}
