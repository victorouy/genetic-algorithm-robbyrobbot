using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyGeneticAlgo;

namespace RobbyGeneticAlgoUnitTests
{
    [TestClass]
    public class GenerationTests
    {
        // Used to increment fitness level for Fitness delegate testing
        private double num = 1;

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test the first constructor
      */
        // -- 212 Seed required -- 
        [TestMethod]
        public void GenerationConstructor1Test()
        {
            Random rnd = new Random(212);

            Generation gen = new Generation(3, 5);

            Assert.AreEqual(gen.Population, 3, "Test failed: constructor does not inialize proper poulation");
            Assert.AreEqual(gen[1].Length, 5, "Test failed: constructor does not inialize proper gene length");

            for (int i = 0; i < 5; i++)
            {
                Allele tempAllele = (Allele)(rnd.Next(Enum.GetNames(typeof(Allele)).Length));
                Assert.AreEqual(gen[0][i], tempAllele, "Test failed: error constructor in inputting Chromosone/Alleles into the array");
            }
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
     * 
     * 
     * Test the second constructor
     */
        [TestMethod]
        public void GenerationConstructor2Test()
        {
            Chromosome[] chromosomes = new Chromosome[2];
            chromosomes[0] = new Chromosome(3);
            chromosomes[1] = new Chromosome(3);

            Generation gen = new Generation(chromosomes);

            for (int i = 0; i < gen[0].Length; i++)
            {
                Assert.AreEqual(gen[0][i], chromosomes[0][i], "Test failed: error constructor in inputting Chromosone/Alleles into the array");
            }
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
     * 
     * 
     * Test the indexer
     */
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void indexerExceptionTest()
        {
            Generation gen = new Generation(3, 5);
            Chromosome chromo = gen[5];
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
     * 
     * 
     * Test EvalFitness method
     */
        [TestMethod]
        public void EvalFitnessTest()
        {
            Generation gen = new Generation(3, 5);
            gen.EvalFitness(fitnessDelTest);
            Assert.AreEqual(gen[0].Fitness, 4.0, "Test failed: error in assigning delegate/sort/reverse");
            Assert.AreEqual(gen[1].Fitness, 3.0, "Test failed: error in assigning delegate/sort/reverse");
            Assert.AreEqual(gen[2].Fitness, 2.0, "Test failed: error in assigning delegate/sort/reverse");
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
     * 
     * 
     * Test SelectParent method
     */
        [TestMethod]
        public void SelectParentTest()
        {
            Generation gen = new Generation(3, 5);
            gen.EvalFitness(fitnessDelTest);
            Chromosome chromoTest = gen.SelectParent();
            Assert.AreEqual(chromoTest.Fitness, 4.0, "Test failed: error in selecting best parent");
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Used for Fitness delegate testing
      */
        private double fitnessDelTest(Chromosome c)
        {
            c.Fitness = num++;
            return num;
        }
    }
}
