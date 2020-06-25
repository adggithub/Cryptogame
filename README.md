# Cryptogame, a fun and simple way to learn cryptography

## Overview

This RPG-styled 2D game is intended to make students from middle and high school interested in cryptography. Having a good knowledge of this topic is necessary in order to understand cybersecurity, an IT branch rapidly growing over the past few years.
Through a simple background story, the user will learn to deal with the most common cypher; thanks to the animation created during the game, the player will be able to see how to encode and decode step by step.
The game has no-lost condition or scoring, as it is intended to be merely instructive: in fact, at the begginning of a new cipher, a tutorial and an explanation are present so that the player can learn the theory.

Here's some screenshots from the game:

![Caesar's cypher](images/figure1.jpg?raw=true)

![Player's house](images/figure2.jpg?raw=true)

![Vigenère](images/figure3.jpg?raw=true)


## Quick start

This game was made with Monogame 3.7.1 and Visual Studio 2019.
The following steps describe how to install Monogame:

### Download and install MonoGame

- Go to http://community.monogame.net/t/monogame-3-7-1-release/11173 *(as of writing this tutorial, MonoGame 3.7.1 is the latest release)*.
- Click **MonoGame 3.7.1 for VisualStudio**.
- This will download MonoGameSetup.exe. Run this program to the end to install MonoGame.

MonoGame will now be installed on your computer, but the installer **does not include templates for Visual Studio 2019**. 
Without these MonoGame templates, you will be unable to start a new MonoGame project.

### Set up Templates for Visual Studio 2019

- Click [here](https://www.industrian.net/assets/MonoGame_Templates.zip) to download the templates.
- Open File Explorer and go to **This PC > Documents > Visual Studio 2019**.
- Unzip the contents of MonoGame_Templates.zip into this folder (as shown on the screenshot).
![VS 2019 folder](images/figure4.png?raw=true)

### Test if the Templates are Set Up

- Open Visual Studio 2019.
- Click Create a new project
- In the **Create a new project** window search bar, search for “monogame”
- If the following projects appear, then the templates have been set up
![Creating a new Monogame project](images/figure5.png?raw=true)