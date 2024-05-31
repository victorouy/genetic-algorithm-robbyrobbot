using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbyGeneticAlgo
{
    public delegate int AlleleMoveAndFitness(Chromosome c, Contents[,] grid, ref int x, ref int y);
    public delegate void GenerationEventHandler(int num, Generation g);
    public delegate Chromosome[] Crossover(Chromosome a, Chromosome b);
    public delegate double Fitness(Chromosome c);


    /*
     * @Author: Victor Ouy
     * @Date: 13/04/2024
     * 
     * This class makes all the other classes interact with one another
     */
    public class RobbyRobotProblem
    {
        public int numGenerations { get; private set; }
        public int popSize { get; private set; }
        public int numGenes { get; private set; }
        public int numActions { get; private set; }
        public int gridSize { get; private set; }
        public double mutationRate { get; private set; }

        private double numElite;
        private int currentGenIndex;

        public AlleleMoveAndFitness moveFitnessDel { get; private set; }
        public Generation currentGen;
        public Contents[][,] testGrids { get; private set; }

        public event GenerationEventHandler GenerationReplaced;




      /*
       * @Author: Victor Ouy
       * @Date: 13/04/2024
       * 
       * @Param: numGenerations, popSize, f, numActions, numTestGrids, gridSize,  numGenes, eliteRate, mutationRate
       * 
       * Constructor for RobbyRobotProblem
       */
        public RobbyRobotProblem(int numGenerations, int popSize, AlleleMoveAndFitness f, int numActions, int numTestGrids, int gridSize, int numGenes, double eliteRate, double mutationRate)
        {
            this.numGenerations = numGenerations;
            this.popSize = popSize;
            this.numGenes = numGenes;
            this.numActions = numActions;
            this.gridSize = gridSize;
            this.numElite = popSize * eliteRate;
            this.mutationRate = mutationRate;
            this.currentGenIndex = 1;
            this.moveFitnessDel = f;
            this.testGrids = new Contents[numTestGrids][,];
        }


        /*
       * @Author: Victor Ouy
       * @Date: 13/04/2024
       *
       * invokes the GS process amd instatiates the first genetation
       */
        public void Start()
        {
            currentGen = new Generation(popSize, numGenes);
            EvalFitness(new Fitness(RobbyFitness));
            OnChangedGen();
            GenerateNextGeneration(Chromosome.DoubleCrossover);

            for (int i = 1; i < numGenerations; i++)
            {
                EvalFitness(RobbyFitness);
                OnChangedGen();
                GenerateNextGeneration(Chromosome.DoubleCrossover);
            }
        }


        /*
       * @Author: Victor Ouy
       * @Date: 13/04/2024
       * 
       * helper method
       */
        protected virtual void OnChangedGen()
        {
            if (GenerationReplaced != null)
            {
                 GenerationReplaced(currentGenIndex, currentGen);
            }
        }

        /*
       * @Author: Victor Ouy
       * @Date: 13/04/2024
       * 
       * @Param: f
       * 
       * generates a set of test grids
       */
        public void EvalFitness(Fitness f)
        {
            for (int i = 0; i < testGrids.Length; i++)
            {
                testGrids[i] = Helpers.GenerateRandomTestGrid(gridSize);
            }

            currentGen.EvalFitness(f);
        }

        /*
       * @Author: Victor Ouy
       * @Date: 13/04/2024
       * 
       * @Param: delCross
       * 
       * Creates the next generation using the elite
       */
        public void GenerateNextGeneration(Crossover delCross)
        {
            Chromosome[] nextGen = new Chromosome[popSize];

            for (int i = 0; i < numElite; i++)
            {
                nextGen[i] = currentGen[i];
            }

            for (int i = (int)numElite; i < popSize; i = i + 2)
            {
                Chromosome ctemp1 = currentGen.SelectParent();
                Chromosome ctemp2 = currentGen.SelectParent();

                Chromosome[] tempChromo = currentGen.SelectParent().Reproduce(currentGen.SelectParent(), delCross, mutationRate);
                nextGen[i] = tempChromo[0];
                nextGen[i + 1] = tempChromo[1];
            }

            currentGenIndex++;
            currentGen = new Generation(nextGen);
        }


        /*
       * @Author: Victor Ouy
       * @Date: 13/04/2024
       * 
       * @Param: c
       * @Return: testScore
       * 
       * calculates and returns the average fitness for the given chromosome
       */
        // The Fitness delegate
        public double RobbyFitness(Chromosome c)
        {
            int testScore = 0;

            for (int i = 0; i < testGrids.Length; i++)
            {
                testScore += Helpers.RunRobbyInGrid(testGrids[i], c, numActions, Helpers.ScoreForAllele);
            }
            testScore = testScore / testGrids.Length;

            return testScore;
        }
    }
}
