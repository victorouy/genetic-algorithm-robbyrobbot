using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyGeneticAlgo;

namespace RobbyGeneticAlgoUnitTests
{
    /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * This unit test class is to test the methods and logic of Chromosome class
      */
    [TestClass]
    public class ChromosomeTests
    {
        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * This test will fail since it only work when we use the seed on Random rnd(in Helpers class),
        but it has been commented out for now. 
        Uncomment the seeded version and comment the original for this test method to "PASS"
      */
        // -- 212 Seed required --
        [TestMethod]
        public void Constructor1Test()
        {
            Random rnd = new Random(212);
            Chromosome chromo = new Chromosome(200);

            for (int i = 0; i < chromo.Length; i++)
            {
                Allele tempAllele = (Allele)(rnd.Next(Enum.GetNames(typeof(Allele)).Length));
                Assert.AreEqual(chromo[i], tempAllele, "Test failed: error in inputting random Alleles into the array");
            }
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * This test tests the second constructor inside chromosome class
      */
        [TestMethod]
        public void Constructor2Test()
        {
            Allele[] tempAlleleArr = { (Allele)2, (Allele)0, (Allele)5 };
            Chromosome chromo = new Chromosome(tempAlleleArr);

            Assert.AreEqual(chromo.Length, 3, "Test failed: chromo Length was not 3");

            for (int i = 0; i < chromo.Length; i++)
            {
                Assert.AreEqual(chromo[i], tempAlleleArr[i], "Test failed: error in deep copying Alleles into the array");
            }
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test's the index exception
      * Should pass if it throws ArgumentOutOfRangeException
      */
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void indexerTest()
        {
            Allele[] tempAlleleArr = { (Allele)1, (Allele)3, (Allele)6 };
            Chromosome chromo = new Chromosome(tempAlleleArr);

            // Should throw exception since 4 is out of range
            Allele temp = chromo[4];
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test different chromosome object lengths
      */
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CrossoverExceptionTest()
        {
            Chromosome chromo1 = new Chromosome(100);
            Chromosome chromo2 = new Chromosome(102);

            Chromosome.SingleCrossover(chromo1, chromo2);
            Chromosome.DoubleCrossover(chromo1, chromo2);
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test SingleCrossover method
      */
        // -- 212 Seed required --
        [TestMethod]
        public void SingleCrossoverTest()
        {
            Allele[] tempAlleleArr1 = { (Allele)0, (Allele)2, (Allele)5, (Allele)3, (Allele)4, (Allele)1 };
            Chromosome chromo1 = new Chromosome(tempAlleleArr1);
            Allele[] tempAlleleArr2 = { (Allele)1, (Allele)3, (Allele)5, (Allele)6, (Allele)2, (Allele)0 };
            Chromosome chromo2 = new Chromosome(tempAlleleArr2);

            Allele[] testAlleleArr1 = { (Allele)0, (Allele)2, (Allele)5, (Allele)6, (Allele)2, (Allele)0 };
            Allele[] testAlleleArr2 = { (Allele)1, (Allele)3, (Allele)5, (Allele)3, (Allele)4, (Allele)1 };

            Chromosome[] offSprings = Chromosome.SingleCrossover(chromo1, chromo2);

            for (int i = 0; i < offSprings.Length; i++)
                Assert.AreEqual(offSprings[i].Length, 6, "Test failed: the offSprings do not have the correct length");

            for (int i = 0; i < offSprings.Length; i++)
            {
                for (int j = 0; j < offSprings[i].Length; j++)
                {
                    if (i == 0)
                        Assert.AreEqual(offSprings[i][j], testAlleleArr1[j], "Test failed: the first offSprings is not single crossovered properly");
                    else
                        Assert.AreEqual(offSprings[i][j], testAlleleArr2[j], "Test failed: the second offSprings is not single crossovered properly");
                }
            }
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test DoubleCrossover method
      */
        // -- 212 Seed required --
        [TestMethod]
        public void DoubleCrossoverTest()
        {
            Allele[] tempAlleleArr1 = { (Allele)0, (Allele)2, (Allele)5, (Allele)3, (Allele)4, (Allele)1 };
            Chromosome chromo1 = new Chromosome(tempAlleleArr1);
            Allele[] tempAlleleArr2 = { (Allele)1, (Allele)3, (Allele)5, (Allele)6, (Allele)2, (Allele)0 };
            Chromosome chromo2 = new Chromosome(tempAlleleArr2);

            Allele[] testAlleleArr1 = { (Allele)0, (Allele)3, (Allele)5, (Allele)3, (Allele)4, (Allele)1 };
            Allele[] testAlleleArr2 = { (Allele)1, (Allele)2, (Allele)5, (Allele)6, (Allele)2, (Allele)0 };

            Chromosome[] offSprings = Chromosome.DoubleCrossover(chromo1, chromo2);

            for (int i = 0; i < offSprings.Length; i++)
            {
                for (int j = 0; j < offSprings[i].Length; j++)
                {
                    if (i == 0)
                        Assert.AreEqual(offSprings[i][j], testAlleleArr1[j], "Test failed: the first offSprings is not double crossovered properly");
                    else
                        Assert.AreEqual(offSprings[i][j], testAlleleArr2[j], "Test failed: the second offSprings is not double crossovered properly");
                }
            }
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test Reproduce method
      */
        // -- 212 Seed required --
        [TestMethod]
        public void ReproduceTest()
        {
            Allele[] tempAlleleArr1 = { (Allele)0, (Allele)2, (Allele)5, (Allele)3, (Allele)4, (Allele)1 };
            Chromosome mainChromo = new Chromosome(tempAlleleArr1);
            Allele[] tempAlleleArr2 = { (Allele)1, (Allele)3, (Allele)5, (Allele)6, (Allele)2, (Allele)0 };
            Chromosome spouseChromo = new Chromosome(tempAlleleArr2);
            
            Chromosome[] offSprings = mainChromo.Reproduce(spouseChromo, new Crossover(Chromosome.SingleCrossover), 0.8);
            Allele[] testAlleleArr = { (Allele)6, (Allele)2, (Allele)5, (Allele)6, (Allele)2, (Allele)1 };

            for (int i = 0; i < testAlleleArr.Length; i++)
            {
                Assert.AreEqual(offSprings[0][i], testAlleleArr[i], "Test failed: the mutation in Reproduce did not operate properly");
            }
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test CompareTo method
      */
        [TestMethod]
        public void CompareToTest()
        {
            Allele[] tempAlleleArr1 = { (Allele)0, (Allele)2, (Allele)5, (Allele)3, (Allele)4, (Allele)1 };
            Chromosome mainChromo = new Chromosome(tempAlleleArr1);
            Allele[] tempAlleleArr2 = { (Allele)1, (Allele)3, (Allele)5, (Allele)6, (Allele)2, (Allele)0 };
            Chromosome spouseChromo = new Chromosome(tempAlleleArr2);

            mainChromo.Fitness = 4.5;
            spouseChromo.Fitness = 4;

            // If mainChromo.Fitness is greater than spouseChromo.Fitness
            Assert.AreEqual(mainChromo.CompareTo(spouseChromo), 1, "Test failed: the compareTo method returns a wrong comparison");
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test ToString method
      */
        // -- 212 Seed required --
        [TestMethod]
        public void ToStringTest()
        {
            Chromosome mainChromo = new Chromosome(5);
            string stringAllele = "West,North,Random,PickUp,Random";

            Assert.AreEqual(mainChromo.ToString(), stringAllele, "Test failed: ToString method does not output the correct string");
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * 
      * Test deepCopy method
      */
        [TestMethod]
        public void deepCopyTest()
        {
            Chromosome tempChromo = new Chromosome(5);
            Chromosome copyChromo = tempChromo.deepCopy();
            Assert.AreEqual(tempChromo.CompareTo(copyChromo), 0, "Test failed: does not copy properly");
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
            Chromosome tempChromo = new Chromosome(5);
            tempChromo.EvalFitness(fitnessDelTest);
            Assert.AreEqual(tempChromo.Fitness, 2.0, "Test failed: does not evaluate/set fitness properly");
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
            c.Fitness = 2.0;
            return 2.0;
        }
    }
}