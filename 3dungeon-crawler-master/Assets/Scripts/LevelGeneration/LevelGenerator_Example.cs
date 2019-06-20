using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomGrammar.GrammarCore; // using the dll
using System.IO;

public class LevelGenerator : MonoBehaviour
{
    private GenerationSystem system; // ludoscope's generation system

    // prefabs for all our building elements
    public GameObject ground;
    public GameObject wall;
    public GameObject spike;
    public GameObject enemy;
    public GameObject pit;

    private Symbol container = null; // the container of the current tile

    void Start()
    {
        // load up the ludoscope project
        string file = "TopdownTutorial/TopdownTutorial.lsp";
        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, file);

        // ready the system with the ludoscope project
        system = new GenerationSystem();
        system.OpenFromFile(fullPath);

        Debug.Log("System " + file + " loaded");

        // start building the world using the generated expression
        Build(Generate());
    }

    internal Expression Generate()
    {
        system.Reset(); // readies the system for generating
        system.Execute(); // execute the grammar to get the result

        return system.Output; // return the generated expression
    }

    internal void Build(Expression expression)
    {
        for(int y = 0; y < expression.Height; y++) // loop through the columns of the tilemap
        {
            for (int x = 0; x < expression.Width; x++) // loop through the rows of the tilemap
            {
                Symbol symbol = expression.Symbols[x + y * expression.Width]; // the symbol of the current tile
                Vector3 position = new Vector3(x, -y, 0); // position of the current tile
                BuildContainer(expression, symbol, position);
            }
        }
    }

    internal void BuildContainer(Expression expression, Symbol symbol, Vector3 position)
    {
        switch (symbol.Label) // perform the right action for the found label
        {
            case "ground":
                // fill ground tiles
                Instantiate(ground, position, Quaternion.identity);
                break;
            case "wall":
                // fill wall tiles
                Instantiate(wall, position, Quaternion.identity);
                break;
            case "spike":
                // fill spike tiles
                Instantiate(spike, position, Quaternion.identity);
                break;
            case "enemy":
                // fill enemy tiles
                Instantiate(enemy, position, Quaternion.identity);
                break;
            case "pit":
                // fill pit tiles
                Instantiate(pit, position, Quaternion.identity);
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