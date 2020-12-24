using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbyGeneticAlgo
{

    /*
        * @Author: Rahul Anton and Victor Ouy
        * @Date: 13/04/2020
        * 
        * 
        * The chromosome class contains the fitness(score), an array of alleles and the length of the allele array
        * This is used to represent a generation of Robby
        */
    public class Chromosome : IComparable<Chromosome>
    {

        private Allele[] arrAllele;
        public int Length { get; private set; }
        // public set to test
        public double Fitness { get; set; }


        /*
        * @Author: Rahul Anton and Victor Ouy
        * @Date: 13/04/2020
        * 
        * @Param: length
        * 
        * constructor for the Chromosome class
        */
        public Chromosome(int length)
        {
            this.Length = length;
            this.arrAllele = new Allele[length];
            
            for (int i = 0; i < length; i++)
            {
                this.arrAllele[i] = (Allele)(Helpers.rand.Next(Enum.GetNames(typeof(Allele)).Length));
            }
        }


        /*
        * @Author: Rahul Anton and Victor Ouy
        * @Date: 13/04/2020
        * 
        * @Param: gene
        * 
        * constructor for the Chromosome class
        */
        public Chromosome(Allele[] gene)
        {
            this.arrAllele = new Allele[gene.Length];
            int length = 0;
            for (int i = 0; i < gene.Length; i++)
            {
                this.arrAllele[i] = gene[i];
                length++;
            }
            this.Length = length;
        }



        /*
        * @Author: Rahul Anton and Victor Ouy
        * @Date: 13/04/2020
        * 
        * Indexer for Allele array
        */
        public Allele this[int index]
        {
            get
            {
                if (index >= arrAllele.Length || index < 0)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                return arrAllele[index];
            }
        }



        /*
       * @Author: Rahul Anton and Victor Ouy
       * @Date: 13/04/2020
       * 
       * @Param: spouse, f, mutationRate
       * @Return: offSprings
       * 
       * uses the Crossover delegate to create offsprings using the spouse
       */
        public Chromosome[] Reproduce(Chromosome spouse, Crossover f, double mutationRate)
        {
            Chromosome[] offSprings = f(this, spouse);

            for (int i = 0; i < offSprings.Length; i++)
            {
                for (int j = 0; j < offSprings[i].Length; j++)
                {
                    double rate = Helpers.rand.NextDouble() * (1 - 0) + 0;
                    if (rate < mutationRate)
                    {
                        offSprings[i].arrAllele[j] = (Allele)(Helpers.rand.Next(Enum.GetNames(typeof(Allele)).Length));
                    }
                }
            }
            return offSprings;
        }



        /*
        * @Author: Rahul Anton and Victor Ouy
        * @Date: 13/04/2020
        * 
        * @Param: f
        * 
        * Sets the fitness of the Chromosome object
        */
        public void EvalFitness(Fitness f)
        {
            this.Fitness = f(this);
        }



        /*
       * @Author: Rahul Anton and Victor Ouy
       * @Date: 13/04/2020
       * 
       * @Param: other
       * @Return: -1, 1, 0
       * 
       * compares the fitness of this chromosome and other
       */
        // Assuming fitness was set on both Chromosomes
        public int CompareTo(Chromosome other)
        {
            if (this.Fitness < other.Fitness)
            {
                return -1;
            }
            else if (this.Fitness > other.Fitness)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        /*
       * @Author: Rahul Anton and Victor Ouy
       * @Date: 13/04/2020
       * 
       * @Return: alleleContent
       * 
       * Overrides the toString method
       */
        public override string ToString()
        {
            string alleleContent = "";
            for (int i = 0; i < arrAllele.Length; i++)
            {
                if (i != 0)
                {
                    alleleContent = alleleContent + "," + arrAllele[i];
                }
                else
                {
                    alleleContent = "" + arrAllele[i];
                }
            }
            return alleleContent;
        }



        /*
       * @Author: Rahul Anton and Victor Ouy
       * @Date: 13/04/2020
       * 
       * @Return: copyChromosome
       * 
       * returns a deep copy of this chromosome
       */
        public Chromosome deepCopy()
        {
            Allele[] tempAllele = new Allele[arrAllele.Length];
            for (int i = 0; i < arrAllele.Length; i++)
            {
                tempAllele[i] = arrAllele[i];
            }

            Chromosome copyChromosome = new Chromosome(tempAllele);
            copyChromosome.Fitness = this.Fitness;

            return copyChromosome;
        }


        /*
       * @Author: Rahul Anton and Victor Ouy
       * @Date: 13/04/2020
       * 
       * @Param: a, b
       * @Return: twoOffSpring
       * 
       * Returns twp offspring using a random crossover point
       */
        public static Chromosome[] SingleCrossover(Chromosome a, Chromosome b)
        {
            if (a.Length != b.Length)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            int split = Helpers.rand.Next(1, a.Length);
            
            
            Allele[] childAllele1 = new Allele[a.Length];
            Allele[] childAllele2 = new Allele[b.Length];

            for (int i = 0; i < a.Length; i++)
            {
                if (i < split)
                {
                    childAllele1[i] = a[i];
                    childAllele2[i] = b[i];
                }
                else
                {
                    childAllele1[i] = b[i];
                    childAllele2[i] = a[i];
                }
            }

            Chromosome offSpring1 = new Chromosome(childAllele1);
            Chromosome offSpring2 = new Chromosome(childAllele2);

            Chromosome[] twoOffSpring = { offSpring1, offSpring2 };
            return twoOffSpring;
        }


        /*
      * @Author: Rahul Anton and Victor Ouy
      * @Date: 13/04/2020
      * 
      * @Param: a, b
      * @Return: twoOffSpring
      * 
      * returns two offspring using two random crossover points in the chromosomes
      */
        public static Chromosome[] DoubleCrossover(Chromosome a, Chromosome b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("The 2 input chromosones do not have equal length");
            }
            
            int firstHalfSplit = Helpers.rand.Next(1, a.Length / 2);
            int secondHalfSplit = Helpers.rand.Next(a.Length / 2, a.Length);

            Allele[] childAllele1 = new Allele[a.Length];
            Allele[] childAllele2 = new Allele[b.Length];
            
            for (int i = 0; i < a.Length; i++)
            {
                if (i < firstHalfSplit)
                {
                    childAllele1[i] = a[i];
                    childAllele2[i] = b[i];
                }
                else if ((i >= firstHalfSplit) && (i < secondHalfSplit))
                {
                    childAllele1[i] = b[i];
                    childAllele2[i] = a[i];
                }
                else
                {
                    childAllele1[i] = a[i];
                    childAllele2[i] = b[i];
                }
            }

            Chromosome offSpring1 = new Chromosome(childAllele1);
            Chromosome offSpring2 = new Chromosome(childAllele2);

            Chromosome[] twoOffSpring = { offSpring1, offSpring2 };
            return twoOffSpring;
        }
    }
}
