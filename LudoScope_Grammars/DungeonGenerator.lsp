version: 0.6f
alphabet:
name: "TileAlphabet"
position: (-33,-116)

module:
name: "SetMapSize"
alphabet: "TileAlphabet"
position: (-177,116)
type: Recipe
match: None
recipe: true
showMembers: true

module:
name: "RoomGenerationOrder"
alphabet: "StringAlphabet"
position: (-311,0)
type: Recipe
match: None
maxIterations: 5
grammar: true
recipe: true
showMembers: true

module:
name: "RoomGeneration"
alphabet: "TileAlphabet"
position: (-57,0)
type: Recipe
match: UseAsRecipe
inputs: "SetMapSize" "RoomGenerationOrder"
grammar: true
showMembers: true

module:
name: "CleanUp"
alphabet: "TileAlphabet"
position: (311,6)
type: Recipe
match: None
inputs: "distanceCalc+rooms"
maxIterations: 1000
grammar: true
recipe: true
showMembers: true

alphabet:
name: "StringAlphabet"
position: (-153,-114)

module:
name: "distanceCalc+rooms"
alphabet: "TileAlphabet"
position: (147,3)
type: Recipe
match: None
inputs: "Quest"
grammar: true
recipe: true
showMembers: true

module:
name: "Quest"
alphabet: "TileAlphabet"
position: (46,2)
type: None
match: None
inputs: "RoomGeneration"
showMembers: true

register: distanceFromEntrance null
option: Width 32
option: Height 32
option: Tile "wall"
option: Find "hook"
option: Replace "wall"
option: Edge "edge"
option: Member "index"
option: TextGrammar "distanceFromEntrance"
option: Values 2
option: Register "distanceFromEntrance"
