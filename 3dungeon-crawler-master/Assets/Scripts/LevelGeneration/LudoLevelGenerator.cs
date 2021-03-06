﻿using System.Collections;
using System.Collections.Generic;
using PhantomGrammar.GrammarCore;
using UnityEngine;
using System.IO;
using GameObjectControllers;

public class LudoLevelGenerator : MonoBehaviour
{
    private GenerationSystem system; //ludoscope generation system

    public GameObject wall;
    public GameObject floor;
    public GameObject enemy;
    public GameObject exit;
    public GameObject door;
    public GameObject treasure;
    public GameObject key;
    public GameObject lockDoor;
    public CameraController mainCam;
    public GameObject levelParent;

    private Symbol container = null;

    string file, fullPath;

    // Start is called before the first frame update
    void Start()
    {
        InitLudoLevelGen();
    }

    public void InitLudoLevelGen()
    {
        levelParent = new GameObject("levelParentObject");
        
        // load ludoscope project
        file = "LudoScope_Grammars/DungeonGenerator.lsp";
        fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, file);

        //ready the system with ludoscope project
        system = new GenerationSystem();
        system.OpenFromFile(fullPath);

        Debug.Log("System " + file + " loaded");

        Build(Generate());
    }

    internal Expression Generate()
    {
        system.Reset();
        system.Execute();

        return system.Output; // return generated expression
    }

    internal void Build(Expression expression)
    {
        for (int y = 0; y < expression.Height; y++) // loop through the columns of the tilemap
        {
            for (int x = 0; x < expression.Width; x++) // loop through the rows of the tilemap
            {
                Symbol symbol = expression.Symbols[x + y * expression.Width]; // the symbol of the current tile
                Vector3 position = new Vector3(x, 0, -y); // position of the current tile
                BuildContainer(expression, symbol, position);
            }
        }
    }

    internal void BuildContainer(Expression expression, Symbol symbol, Vector3 position)
    {
        switch (symbol.Label) // perform the right action for the found label
        {
            case "entrance":
                Instantiate(wall, position, Quaternion.identity, levelParent.transform);
                //Instantiate(entrance, position, Quaternion.identity);
                break;
            case "player":
                mainCam.Player.transform.position = position;
                //Instantiate(floor, (position - new Vector3(0, .5f, 0)), Quaternion.identity, levelParent.transform);
                break;
            case "entranceCalced":
                //Instantiate(floor, (position - new Vector3(0, .5f, 0)), Quaternion.identity, levelParent.transform);
                break;
            case "door":
                Instantiate(door, (position - new Vector3(0, .5f, 0)), Quaternion.identity, levelParent.transform);
                break;
            case "wall":
                Instantiate(wall, position, Quaternion.identity, levelParent.transform);
                break;
            case "enemy":
                Instantiate(enemy, position, Quaternion.identity, levelParent.transform);
                //Instantiate(floor, (position - new Vector3(0, .5f, 0)), Quaternion.identity, levelParent.transform);
                break;
            case "treasure":
                Instantiate(treasure, position, Quaternion.identity, levelParent.transform);
                //Instantiate(floor, (position - new Vector3(0, .5f, 0)), Quaternion.identity, levelParent.transform);
                break;
            case "exit":
                Instantiate(exit, position, Quaternion.identity, levelParent.transform);
                //Instantiate(floor, (position - new Vector3(0, .5f, 0)), Quaternion.identity, levelParent.transform);
                break;
           default:
                break;
        }

        // if the current symbol's container is not empty, we move into that container and build it recursively until all containers are built
        if (symbol.Container != null)
        {
            symbol = symbol.Container;
            BuildContainer(expression, symbol, position);
        }
    }
}
