What's a monoid?
================

A monoid is:
 - A set, S
 - An operation, • : S × S -> S
 - An element of S, e : 1 -> S

Satisfying:
 - Associativity: (a • b) • c = a • (b • c), for all a, b and c in S
 - Neutral Element: e • a = a = a • e, for all a in S

Examples of monoids you already know: 
 - The set of integers, with the addition and the element zero
 - The set of strings, with concatenation and the element empty string
 - The set of lists, with the append operation, and the element empty list
 - Many domain-specific examples...

The kata
========

The domain
----------
There are weather stations spread all over the world. Some weather stations only record temperature, other only record precipitations, and some record both (or even humidity, barometric pressure and air quality). However we can reconstitute “full observations” by merging each “partial observation”. 
  
We want to create a service that can provide weather averages, for a given week or over a given week range, on-demand, in real-time, and over a large historical time range. This means large numbers of data to combine quickly.

Weeks are just numbered from 0 to N. Average of n values X_i is simply SUM(X_i)/n.

TDD proposed steps
------------------
Using TDD, the following tests progression is suggested:
 1. One weather observation (temperature, humidity) is equal to itself
 1. A single weather observation (temperature, humidity) is equal to its average (is there any meaningful difference between an observation and an average from the client perspective?)
 1. Average of two observations (temperature, humidity) (where to move the average method? Can we answer that now? What if the content of the observation changed?)
 1. Extend observation with precipitations (does this questions the design?)
 1. Average of any number of observations (temperature, humidity, precipitations)
 1. Average over 3 weeks is the average of a 2-weeks average and a 1-week average (we want the same accuracy but we need to optimize the computations by reusing observations that are already averaged. How could you relate that to map-reduce?)
 1. Add standard deviation of temperature (the sqroot of the variance, that can be calculated as: SUM(X_i^2)/n – AVG(X_i)^2)
 1. Average and standard deviation of observations with some missing measurements
 1. Extend the observation to be able to report how it was actually built from each intermediate single or averaged observation (does this remind a monad?)


