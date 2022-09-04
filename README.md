# boids 🐦

<p align="center">
  <img alt="Version" src="https://img.shields.io/github/v/tag/kyuuzou/boids?label=version" />
  <a href="https://github.com/kyuuzou/boids/issues" target="_blank">
     <img alt="GitHub issues" src ="https://img.shields.io/github/issues-raw/kyuuzou/boids" />
  </a>
  <a href="https://github.com/kyuuzou/boids/pulls" target="_blank">
   <img alt="GitHub pull requests" src ="https://img.shields.io/github/issues-pr-raw/kyuuzou/boids" />
  </a>
  <img alt="GitHub last commit" src ="https://img.shields.io/github/last-commit/kyuuzou/boids" />
</p>
<p align="center">
  <a href="https://www.codacy.com/gh/kyuuzou/boids/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=kyuuzou/boids&amp;utm_campaign=Badge_Grade"><img src="https://app.codacy.com/project/badge/Grade/0e148b21eaa742448a210882d88a9f0c"/></a>
  <a href="https://github.com/kyuuzou/boids/blob/master/LICENSE" target="_blank">
    <img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-blue.svg" />
  </a>
</p>

## Description
An experiment with boids in Unity.

## Preview

![Preview](https://github.com/kyuuzou/boids/blob/main/Documentation/preview.gif?raw=true)


## Why it Exists

Mostly because I wanted to play around with Boids! I keep seeing all of these implementations with all kinds of cool techniques, and finally decided to give it a go myself. The intention of this project is as follows:  

- [x] Implement the three core steering behaviours for Simulated Flocks, as per Craig Reynold's original 1987's [Flocks, Herds, and Schools: A Distributed Behavioral Model](https://www.red3d.com/cwr/boids/).
- [x] Optimise collision detection with dynamic spatial partitioning or equivalent technique.
- [ ] Further optimise collision detection with the DOTS system.
- [ ] Implement remaining behaviours described in the model, such as Simulated Perception, Scripted Flocking and Environmental Obstacle Avoidance.
- [ ] Switch between 2D and 3D.
- [ ] Implement additional interesting behaviours, such as perching, hunger and predators.
- [ ] Move to a multiplayer paradigm where each boid runs its own instance, introducing new interesting variables to the problem, like latency, prediction and rollbacks.

Whether the project ever actually sees all of this happen is a different story, but in the end, even a partial journey is more exciting than no journey at all. Let's see how far it gets!

## Version History

* *0.2.0*
    * Neighbours are now calculated via spatial partition.
    * It is now possible to toggle a grid shader, to see how boids are distributed between the cells at any given time.
* *0.1.0*
    * Most settings are now configurable.
    * Added Test Subject mode, to be able to follow a specific boid's decision-making process.
* *0.0.1*
    * Quick and dirty implementation of Collision Avoidance, Velocity Matching and Flock Centering.
    * Ability to toggle between wrapping around a bounding volume or staying within bounds.

## Contacting the Author

* Email: newita@gmail.com
* Github: [@kyuuzou](https://github.com/kyuuzou)
* LinkedIn: [@nelson-rodrigues-ba4ab263](https://linkedin.com/in/nelson-rodrigues-ba4ab263)
* Portfolio: http://kyuandcaffeine.com

## License

Copyright © 2022 [Nelson Rodrigues](https://github.com/kyuuzou).<br />
This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

## Acknowledgments
Thank you [@SebLague](https://github.com/SebLague/Boids) for the inspiration, without which I'd probably be on my couch right now!