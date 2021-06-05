using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a hexagonal map of <code>tilePrefab</code>s.
/// </summary>
public class HexGrid : MonoBehaviour {
	[Header("Map")]
	public int sizeX = 5;
	public int sizeY = 5;
	[Header("Tile")]
	public GameObject tilePrefab;
	public float tileRad = 1.03f;

	private float scaleFactorX;
	private float scaleFactorY;
	private Transform[,] map;

	void Awake () {
		scaleFactorX = (Mathf.Cos(Mathf.PI/6f) * tileRad);
		scaleFactorY = 1.5f * tileRad;
		map = new Transform[sizeX, sizeY];

		transform.position += new Vector3 (-scaleFactorX * (sizeX-0.5f), 0f, -scaleFactorY * (sizeY-1f)/2f);
	}

	void Start () {
		Generate ();
	}

	void Generate() {
		for (int x = 0; x < sizeX * 2; x++) {
			for (int y = 0; y < sizeY; y++) {
				if ( (x+y) % 2 == 0 ) {
					GameObject spawnedTile = Instantiate(tilePrefab, Vector3.zero, tilePrefab.transform.rotation) as GameObject;
					spawnedTile.transform.parent = transform;
					spawnedTile.transform.localPosition = new Vector3(x * scaleFactorX, 0f, y * scaleFactorY);
					map [(x - y%2)/2, y] = spawnedTile.transform;
				}
			}
		}
	}

	public Transform GetTile(int x, int y) {
		return map [x, y];
	}
}
