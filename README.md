# Genetic-Algorithm-High-Value

This is an implementation of a simple genetic algorithm idea that is presented a lot as beginner friendly which is sometimes call the one max problem. 

I haven't seen it done at a base level in C# and decided to implement it in that since it is the language I am most comfortable with and it improves the learning experience to not simply copy and paste code but rewrite it.  

Two genomes are created at random that could look like:

[0,1,0,0,1,1]

[0,0,1,0,0,0]

These then give birth to children that are a mixture of their parents so there are four genomes:

Parent 1: [0,1,0,0,1,1]

Parent 2: [0,0,1,0,0,0]

Child 1:  [0,1,0,0,0,0] <= First three values of Parent 1 and second three of Parent 2

Child 2:  [0,1,1,0,0,1] <= First three values of Parent 2 and second three of Parent 1

The goal is for them to get to

[1,1,1,1,1,1]

[1,1,1,1,1,1]

[1,1,1,1,1,1]

[1,1,1,1,1,1]

This is done through Darwinian principles like mutation and survival of the fittest. 

