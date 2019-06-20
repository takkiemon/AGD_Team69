# Dungeon Crawler


A Fate- and Diablo-style dungeon crawler game, made with Unity, using C#. Made as part of **OTMK18**.

## Releases

**Most recent release:**
[v1.0.0](https://github.com/sofivanhanen/dungeon-crawler/releases/tag/v1.0.0)

## Documentation

### Files

.md-files are in Finnish, but i.e. the diagrams in the architecture file are in English.

[Määrittelydokumentti (Specification document)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/Documentation/Vaatimusm%C3%A4%C3%A4rittely.md)

[Tuntikirjanpito (Time tracking)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/Documentation/Tuntikirjanpito.md)

[Arkkitehtuurikuvaus (Architecture / diagrams)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/Documentation/Arkkitehtuuri.md)

[Käyttöohje (Install instructions / how to play)](https://github.com/sofivanhanen/dungeon-crawler/blob/master/Documentation/K%C3%A4ytt%C3%B6ohje.md)

### HTML docs from XML-tags

#### Easy way

The ready-generated HTML-files are as a package in the Documentation folder. Clone the repo, unpack the file (HTML-documentation.tar.gz), open the html-folder and open the index.html-file with some browser.

#### Generating by hand

You can probably get this done with something like Visual Studio as well. However, [Doxygen](http://www.stack.nl/~dimitri/doxygen/) offers an easy way to do it and it works on Win, Mac and Linux:

[Install Doxygen](http://www.stack.nl/~dimitri/doxygen/download.html) (simple installation for Linux [here](http://xmodulo.com/how-to-generate-documentation-from-source-code-in-linux.html)), navigate to root of project, run command 'doxygen dungeon-crawler.conf', navigate to the generated html folder, run 'firefox index.html' (or use your favourite web browser).


## Running tests and checks and such

### Testing

Unit tests are located in the Assets/Editor folder. In order to run them, open the project in Unity, open the Test Runner (Window - Test Runner), select EditMode and double-click on a test, file, or folder.

### Code analysis

I use ReSharper for code analysis. In order to run the analysis, open the project in Unity, select Assets - Open C# Project, open the generated .sln file with an IDE with ReSharper (I use JetBrains Rider), and run code analysis (In Rider, Code - Inspect code). My ReSharper settings are still default.
