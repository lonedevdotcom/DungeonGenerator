using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonDisplay : MonoBehaviour {
	public GameObject[] shapes;
	public float minimumMazePercentage = 0.6f;

	public GameObject roomObject; // Remove this once dictionary is sorted.
	private MapGenerator mapGenerator;
	private bool[,] visitedCells;
	private Dictionary<char,GameObject> characterGameObjects = new Dictionary<char,GameObject>();


	// Use this for initialization
	void Start () {
		mapGenerator = GetComponent<MapGenerator> ();
		visitedCells = new bool[mapGenerator.mapRows, mapGenerator.mapColumns];

		// LoadDungeonCharacterMappings ();
		GenerateDungeon ();
		mapGenerator.DisplayMap ();
		InstantiateDungeonPieces ();
	}

	private void LoadDungeonCharacterMappings() {
		foreach (GameObject dungeonShape in shapes) {
			char gameObjectCharacter = dungeonShape.GetComponent<DungeonCharacterMapping> ().characterMapping;
			characterGameObjects [gameObjectCharacter] = dungeonShape;
		}
	}

	private void GenerateDungeon() {
		int visitedCellCount = 0;

		int minimumMazeCells = Mathf.FloorToInt((mapGenerator.mapRows - 2) * (mapGenerator.mapColumns - 2) * minimumMazePercentage);

		while (visitedCellCount < minimumMazeCells) {
			Debug.Log ("Current dungeon size = " + visitedCellCount + " which is less than the required " + minimumMazeCells + ". Retrying");
			mapGenerator.InitializeMap ();
			visitedCells = mapGenerator.TraverseMap ();
			visitedCellCount = GetVisitedCellsCount (visitedCells);
			Debug.Log ("visited cell count = " + visitedCellCount);
		}
	}

	private void InstantiateDungeonPieces() {
		for (int r = 1; r < mapGenerator.mapRows-1; r++) {
			for (int c = 1; c < mapGenerator.mapColumns - 1; c++) {
				string ch = mapGenerator.map [r, c].ToString();
				int charPos = mapGenerator.boxCharacters.IndexOf (ch);

				if (ch.Equals("@") || ch.Equals("˂") || ch.Equals("˃") || ch.Equals("˅") || ch.Equals("˄") ||
					ch.Equals("╔") || ch.Equals("╤") || ch.Equals("╧") || ch.Equals("╗") || ch.Equals("╟") || 
					ch.Equals("╢") || ch.Equals("╚") || ch.Equals("╝")) {
					Instantiate (roomObject, new Vector3 (r * 12, 0, c * 12), roomObject.transform.rotation);
				} else	if (charPos < 0 || !visitedCells [r, c]) {
					continue;
				} else {
					Instantiate (shapes [charPos], new Vector3 (r * 12, 0, c * 12), shapes [charPos].transform.rotation);
				}
			}
		}
	}

	private int GetVisitedCellsCount(bool[,] visitedCells) {
		int visitedCellsCount = 0;

		for (int r = 1; r < mapGenerator.mapRows - 1; r++) {
			for (int c = 1; c < mapGenerator.mapColumns - 1; c++) {
				if (visitedCells [r, c]) {
					visitedCellsCount++;
				}
			}
		}

		return visitedCellsCount;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
