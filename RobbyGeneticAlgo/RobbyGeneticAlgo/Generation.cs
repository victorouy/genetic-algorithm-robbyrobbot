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
      * The Generation class represents a generation with a population size of Chromosome members
      */
    public class Generation
    {
        private Chromosome[] chromosomes;
        public int Population { get; private set; }


        /*
      * @Author: Rahul Anton and Victor Ouy
      * @Date: 13/04/2020
      * 
      *@Param: populationSizem numGenes
      */
        public Generation(int populationSize, int numGenes)
        {
            this.Population = populationSize;
            this.chromosomes = new Chromosome[populationSize];

            for (int i = 0; i < chromosomes.Length; i++)
            {
                this.chromosomes[i] = new Chromosome(numGenes);
            }
        }



        /*
    * @Author: Rahul Anton and Victor Ouy
    * @Date: 13/04/2020
    * 
    *@Param: members
    */
        public Generation(Chromosome[] members)
        {
            this.Population = members.Length;
            this.chromosomes = new Chromosome[members.Length];

            for (int i = 0; i < chromosomes.Length; i++)
            {
                this.chromosomes[i] = members[i];
            }
        }


        /*
    * @Author: Rahul Anton and Victor Ouy
    * @Date: 13/04/2020
    * 
    * 
    * indexer for Chromosome array
    */
        public Chromosome this[int index]
        {
            get
            {
                if (index >= chromosomes.Length || index < 0)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                return chromosomes[index];
            }
        }

        /*
         * @Author: Rahul Anton and Victor Ouy
         * @Date: 13/04/2020
         * 
         * @Param: f
         * 
         * invokes the delegate f on all the chromosomes the sorts and then reverses it so that the highest is first
         */
        public void EvalFitness(Fitness f)
        {
            for (int i = 0; i < chromosomes.Length; i++)
            {
                this.chromosomes[i].EvalFitness(f);
            }

            Array.Sort(chromosomes);
            Array.Reverse(chromosomes);
        }


        /*
         * @Author: Rahul Anton and Victor Ouy
         * @Date: 13/04/2020
         * 
         * @Return: this.chromosomes[index];
         *
         * Sekects the nect parent for the next generation
         */
        // asuming this EvalFitness was invoked beforehand
        public Chromosome SelectParent()
        {
            int index = Helpers.rand.Next(chromosomes.Length); ;
            int counter = 0;

            while (counter < 9)
            {
                int indexTemp = Helpers.rand.Next(chromosomes.Length);
                if (indexTemp < index)
                {
                    index = indexTemp;
                }
                counter++;
            }
            
            return this.chromosomes[index];
        }
    }
}
