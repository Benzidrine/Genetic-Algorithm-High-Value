using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgoTest
{
    public class Genome
    {
        public Genome(int id)
        {
            ID = id;
            this.Genes = new List<Boolean>();
            for (int i = 0; i < 6; i++)
            {
                Random rnd = new Random();
                bool Gene = false;
                int Decision = rnd.Next(0, 2);
                if (Decision == 1) Gene = !Gene;
                this.Genes.Add(Gene);
            }
        }

        public Genome(Genome Father, Genome Mother, bool XY, int id)
        {
            ID = id;
            this.Genes = new List<Boolean>();

            //If XY is true then spawn first three of father and last three of mother otherwise do the opposite
            if (XY)
            {
                //Father
                for (int i = 0; i < 3; i++)
                {
                    Genes.Add(Father.Genes[i]);
                }
                //Mother
                for (int i = 3; i < 6; i++)
                {
                    Genes.Add(Mother.Genes[i]);
                }
            }
            else
            {
                //Mother
                for (int i = 0; i < 3; i++)
                {
                    Genes.Add(Mother.Genes[i]);
                }
                //Father
                for (int i = 3; i < 6; i++)
                {
                    Genes.Add(Father.Genes[i]);
                }
            }
        }

        public int ID { get; set; }
        public List<Boolean> Genes { get; set; }
        public int TotalValue { get; set; }
        public double SurvivalChance { get; set; }

        public Genome Clone()
        {
            Genome newGenome = new Genome(ID);
            newGenome.Genes = new List<bool>();
            newGenome.Genes = Genes;
            newGenome.TotalValue = TotalValue;
            newGenome.SurvivalChance = SurvivalChance;
            return newGenome;
        }

        public int Total()
        {
            int _total = 0;
            foreach(Boolean b in Genes)
            {
                if (b)
                {
                    _total++;
                }
            }
            TotalValue = _total;
            return _total;
        }

        public double SquaredTotal()
        {
            return ((double)TotalValue * (double)TotalValue);
        }

        public string GeneReadout()
        {
            string output = "[";
            foreach (Boolean b in Genes)
            {
                output += b ? " 1" : " 0";
            }
            output += " ]";
            return output;
        }

        //Chance to be included
        public void DetermineSurvivalChance(double SummedTotal)
        {
            SurvivalChance = ((double)SquaredTotal() / (double)SummedTotal);
        }

        //Put the first half at the front and the second half at the back of the genes list
        public void Crossover(Genome genome, int StartingIndex)
        {
            Genome NewGenome = new Genome(genome.ID);
            NewGenome.Genes = new List<Boolean>();

            if (this.Genes.Count == genome.Genes.Count)
            {
                for (int i = 0; i < genome.Genes.Count; i++)
                {
                    if (i < (genome.Genes.Count / 2))
                        NewGenome.Genes.Add(genome.Genes[(i + (genome.Genes.Count / 2))]);
                    else
                        NewGenome.Genes.Add(genome.Genes[(i - (genome.Genes.Count / 2))]);
                }
            }

            genome.Genes = NewGenome.Genes;
        }

        public void Mutate()
        {
            Random rnd = new Random();
            int MutationPoint = rnd.Next(0, Genes.Count);
            Genes[MutationPoint] = !Genes[MutationPoint];
        }
    }
}
