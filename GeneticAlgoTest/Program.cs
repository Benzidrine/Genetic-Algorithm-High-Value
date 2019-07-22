using System;
using System.Collections.Generic;

namespace GeneticAlgoTest
{
    class Program
    {
        private static int GlobalID = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Genetic Algorithm Example:");
            Console.WriteLine("Attempting to get to value six per Gene");
            Console.WriteLine("Epochs (Integer Value):");
            int Epoches = 0;
            string EpochStr = Console.ReadLine();
            if (int.TryParse(EpochStr, out Epoches))
            {
                Console.WriteLine(Epoches + " Epochs");
                Console.WriteLine("Crossover Rate (Decimal Value):");
                double CrossoverRate = 0;
                string CrossoverRateStr = Console.ReadLine();
                if (double.TryParse(CrossoverRateStr, out CrossoverRate))
                {
                    Console.WriteLine(CrossoverRate + " Crossover Rate");
                    Console.WriteLine("Mutation Rate (Decimal Value):");
                    double MutationRate = 0;
                    string MutationRateStr = Console.ReadLine();
                    if (double.TryParse(MutationRateStr, out MutationRate))
                    {
                        Console.WriteLine(MutationRate + " Mutation Rate");
                        Console.WriteLine("Running");
                        Console.WriteLine();

                        List<Genome> genomes = new List<Genome>();
                        for (int i = 0; i < Epoches; i++)
                        {
                            //Build list of genomes takign into account any survivors
                            for (int j = genomes.Count; j < 4; j++)
                            {
                                //Make children
                                bool XY = (j % 2 == 0);
                                if (genomes.Count >= 2)
                                    genomes.Add(new Genome(genomes[0], genomes[1], XY, GlobalID++));
                                else //Create Initial Parents
                                    genomes.Add(new Genome(GlobalID++));
                            }
                            Console.WriteLine("Epoch " + (i + 1).ToString());
                            double SummedTotal = 0;
                            foreach (Genome genome in genomes)
                            { 
                                Console.WriteLine(genome.GeneReadout() + " " + genome.Total());
                                SummedTotal += genome.SquaredTotal();
                            }
                            foreach (Genome genome in genomes)
                            {
                                genome.DetermineSurvivalChance(SummedTotal);
                            }
                            //Survived Genomes
                            List<Genome> SurvivedGenomes = new List<Genome>();
                            Random rnd = new Random();
                            double Chance = rnd.NextDouble();
                            double lastChance = 0;
                            //Add genomes to next epoch based on their survival chance
                            foreach (Genome genome in genomes)
                            {
                                if (Chance < (genome.SurvivalChance + lastChance))
                                {
                                    SurvivedGenomes.Add(genome.Clone());
                                    genome.ID = -1;
                                    break;
                                }
                                lastChance += genome.SurvivalChance;
                            }
                            //Remove Surviving Genome
                            genomes.RemoveAll(g => g.ID == -1);

                            Chance = rnd.NextDouble();
                            //Add genomes to next epoch based on their survival chance - 2nd
                            SummedTotal = 0;
                            foreach (Genome genome in genomes)
                            {
                                SummedTotal += genome.SquaredTotal();
                            }
                            foreach (Genome genome in genomes)
                            {
                                genome.DetermineSurvivalChance(SummedTotal);
                            }
                            lastChance = 0;
                            foreach (Genome genome in genomes)
                            {
                                if (Chance < (genome.SurvivalChance + lastChance))
                                {
                                    SurvivedGenomes.Add(genome.Clone());
                                    break;
                                }
                                lastChance += genome.SurvivalChance;
                            }
                            //Mutate Genomes based on mutation rate
                            foreach (Genome genome in SurvivedGenomes)
                            {
                                Chance = rnd.NextDouble();
                                if (Chance < MutationRate)
                                {
                                    genome.Mutate();
                                }
                            }
                            //Mutate Genomes based on mutation rate
                            foreach (Genome genome in SurvivedGenomes)
                            {
                                Chance = rnd.NextDouble();
                                if (Chance < CrossoverRate)
                                {
                                    genome.Crossover(genome,3);
                                }
                            }
                            //Report Survivors
                            Console.WriteLine("Survivors:");
                            foreach (Genome genome in SurvivedGenomes)
                            {
                                Console.WriteLine(genome.GeneReadout() + " Total: " + genome.Total() + " ID: " + genome.ID + " Survival Chance: " + genome.SurvivalChance);
                            }

                            genomes = SurvivedGenomes;
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
