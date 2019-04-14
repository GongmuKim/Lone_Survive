using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Chromosome : MonoBehaviour
{
    public int[] Genes; // 적 움직임 배열
    public double Fitness; // 살아남은 시간

    public int this[int index] // 값 GET SET
    {
        get
        {
            //print("get 발동");
            return Genes[index];
        }
        set
        {
            //print("set 발동");
            Genes[index] = value;
        }
        
    }

    public Move_Chromosome(int geneCount)
    {
        Genes = new int[geneCount];
    }

    public Move_Chromosome(int geneCount, int rangeMin, int rangeMax)
    {
        Genes = new int[geneCount];
        for (int i = 0; i < Genes.Length; i++)
        {
            int rand = MathLibrary.Random.Range(rangeMin, rangeMax);
            Genes[i] = rand;
            //print(i + "번 유전자 : " + Genes[i]);
        }
    }
}
