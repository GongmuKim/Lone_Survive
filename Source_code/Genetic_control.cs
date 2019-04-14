using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Genetic_control : MonoBehaviour
{
    public List<Move_Chromosome> Populations;

    public long Generation;

    private int chromosome_genes;

    public Genetic_control(int geneCount)
    {
        chromosome_genes = geneCount;
        print("적 NPC 1명당 유전자 개수 : " + chromosome_genes);
        Populations = new List<Move_Chromosome>();
    }

    public void AddRandomPopulation(int count)
    {
        //print("chromosome_genes : " + chromosome_genes);
        for (int i = 0; i < count; i++)
        {
            Populations.Add(new Move_Chromosome(chromosome_genes, 0, 5));
        }
        print("적NPC 소환 갯수 : " + Populations.Count);
    }

    private double GetRealFitness(double min, double max, double x)
    {
        return (min - x) + (min - max) / 3.0;
    }

    private int RouletteWheel() // 적합도 계산 부분(룰렛 휠 선택 방법)
    {
        int selected = 0;
        double total = 0;

        for (int i = 0; i < Populations.Count; i++) // .Count : 해시 테이블에 들어있는 요소 수를 반환
        {
            Move_Chromosome chromo = Populations[i];
            //chromo.Fitness = GetRealFitness(minFitness, maxFitness, chromo.Fitness);
            total += chromo.Fitness;
            if (MathLibrary.Random.NextBoolean(chromo.Fitness / total))
            {
                selected = i;
            }
        }
        print("선택한 번호:" + selected);
        return selected;
    }

    public void Select(int count)
    {
        int lastCount = Populations.Count;

        for (int i = 0; i < count; i++)
        {
            int index = RouletteWheel();
            Populations.Add(Populations[index]);
        }
        Populations.RemoveRange(0, lastCount);

    }

    public void CrossOver(int count, double percent) // 교차
    {
        int lastCount = Populations.Count;
        for (int i = 0; i < count; i++)
        {
            Move_Chromosome chromoA = Populations[MathLibrary.Random.Range(lastCount)];
            Move_Chromosome chromoB = Populations[MathLibrary.Random.Range(lastCount)];

            Move_Chromosome newChromo = new Move_Chromosome(chromosome_genes);

            for (int j = 0; j < chromosome_genes; j++)
            {
                if (MathLibrary.Random.NextBoolean(percent))
                {
                    newChromo[j] = chromoA[j];
                }
                else
                {
                    newChromo[j] = chromoB[j];
                }
            }
            Populations.Add(newChromo);
        }

        Populations.RemoveRange(0, lastCount);

    }

    public void Mutation(double percent, int mutationMin, int mutationMax, int rangeMin, int rangeMax) // 변이
    {
        for (int i = 0; i < Populations.Count; i++)
        {
            Move_Chromosome chromo = Populations[i];

            for (int j = 0; j < chromosome_genes; j++)
            {
                if (MathLibrary.Random.NextBoolean(percent))
                {
                    while (true)
                    {
                        int mutation_num = MathLibrary.Random.Range(mutationMin, mutationMax);
                        if ((chromo[j] + mutation_num) >= 0 && (chromo[j] + mutation_num) < 6)
                        {
                            print("변이 시작");
                            chromo[j] += MathLibrary.Random.Range(mutationMin, mutationMin);
                            chromo[j] = Math.Max(Math.Min(chromo[j], rangeMax), rangeMin);
                            break;
                        }
                    }
                }
            }

        }
    }
}
