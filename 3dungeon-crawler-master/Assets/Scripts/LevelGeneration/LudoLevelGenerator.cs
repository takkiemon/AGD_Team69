﻿using System.Collections;
using System.Collections.Generic;
using PhantomGrammar.GrammarCore;
using UnityEngine;
using System.IO;

public class LudoLevelGenerator : MonoBehaviour
{
    private GenerationSystem system; //ludoscope generation system

    public GameObject wall;
    public GameObject floor;
    public GameObject enemy;
    public GameObject exit;
    public GameObject entrance;
    public GameObject player;
    public GameObject door;

    private Symbol container = null;

    // Start is called before the first frame update
    void Start()
    {
        // load ludoscope project
        string file = "LudoScope_Grammars/DungeonGenerator.lsp";
        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, file);

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
            //case "ground":
            //    // fill ground tiles
            //    Instantiate(ground, position, Quaternion.identity);
            //    break;
            //case "wall":
            //    // fill wall tiles
            //    Instantiate(wall, position, Quaternion.identity);
            //    break;
            //case "spike":
            //    // fill spike tiles
            //    Instantiate(spike, position, Quaternion.identity);
            //    break;
            //case "enemy":
            //    // fill enemy tiles
            //    Instantiate(enemy, position, Quaternion.identity);
            //    break;
            //case "pit":
            //    // fill pit tiles
            //    Instantiate(pit, position, Quaternion.identity);
            //    break;
            case "entrance":
                Instantiate(entrance, position, Quaternion.identity);
                Instantiate(player, position, Quaternion.identity);
                break;
            case "entranceCalced":
                Instantiate(floor, position, Quaternion.identity);
                break;
            case "door":
                Instantiate(door, position, Quaternion.identity);
                break;
            case "wall":
                Instantiate(wall, position, Quaternion.identity);
                break;
            case "questArea":
                Instantiate(floor, position, Quaternion.identity);
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