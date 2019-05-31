using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Lists for each type of room holding a various selection
	public List<GameObject> desertRoomOne; // Left and right exits
	public List<GameObject> desertRoomTwo; // Left, right and bottom exits
	public List<GameObject> desertRoomThree; // Left, right and top exits
	public List<GameObject> desertRoomFour; // Left, right, top and bottom exits
	public List<GameObject> desertRoomFive; // Left exit
	public List<GameObject> desertRoomSix; // Top exit
	public List<GameObject> desertRoomSeven; // Right exit
	public List<GameObject> desertRoomEight; // Bottom exit
	public List<GameObject> desertRoomNine; // Left and top exits
	public List<GameObject> desertRoomTen; // Top and right exits
	public List<GameObject> desertRoomEleven; // Right and bottom exits
	public List<GameObject> desertRoomTwelve; // Bottom and left exits

	public List<GameObject> desertExitRoomsTopExit;
	public List<GameObject> desertExitRoomsRightExit;
	public List<GameObject> desertExitRoomsBottomExit;
	public List<GameObject> desertExitRoomsLeftExit;

	public List<GameObject> forestRoomOne; // Left and right exits
	public List<GameObject> forestRoomTwo; // Left, right and bottom exits
	public List<GameObject> forestRoomThree; // Left, right and top exits
	public List<GameObject> forestRoomFour; // Left, right, top and bottom exits
	public List<GameObject> forestRoomFive; // Left exit
	public List<GameObject> forestRoomSix; // Top exit
	public List<GameObject> forestRoomSeven; // Right exit
	public List<GameObject> forestRoomEight; // Bottom exit
	public List<GameObject> forestRoomNine; // Left and top exits
	public List<GameObject> forestRoomTen; // Top and right exits
	public List<GameObject> forestRoomEleven; // Right and bottom exits
	public List<GameObject> forestRoomTwelve; // Bottom and left exits

	public List<GameObject> forestExitRoomsTopExit;
	public List<GameObject> forestExitRoomsRightExit;
	public List<GameObject> forestExitRoomsBottomExit;
	public List<GameObject> forestExitRoomsLeftExit;

	public List<GameObject> iceRoomOne; // Left and right exits
	public List<GameObject> iceRoomTwo; // Left, right and bottom exits
	public List<GameObject> iceRoomThree; // Left, right and top exits
	public List<GameObject> iceRoomFour; // Left, right, top and bottom exits
	public List<GameObject> iceRoomFive; // Left exit
	public List<GameObject> iceRoomSix; // Top exit
	public List<GameObject> iceRoomSeven; // Right exit
	public List<GameObject> iceRoomEight; // Bottom exit
	public List<GameObject> iceRoomNine; // Left and top exits
	public List<GameObject> iceRoomTen; // Top and right exits
	public List<GameObject> iceRoomEleven; // Right and bottom exits
	public List<GameObject> iceRoomTwelve; // Bottom and left exits

	public List<GameObject> iceExitRoomsTopExit;
	public List<GameObject> iceExitRoomsRightExit;
	public List<GameObject> iceExitRoomsBottomExit;
	public List<GameObject> iceExitRoomsLeftExit;

		public List<GameObject> voidRoomOne; // Left and right exits
	public List<GameObject> voidRoomTwo; // Left, right and bottom exits
	public List<GameObject> voidRoomThree; // Left, right and top exits
	public List<GameObject> voidRoomFour; // Left, right, top and bottom exits
	public List<GameObject> voidRoomFive; // Left exit
	public List<GameObject> voidRoomSix; // Top exit
	public List<GameObject> voidRoomSeven; // Right exit
	public List<GameObject> voidRoomEight; // Bottom exit
	public List<GameObject> voidRoomNine; // Left and top exits
	public List<GameObject> voidRoomTen; // Top and right exits
	public List<GameObject> voidRoomEleven; // Right and bottom exits
	public List<GameObject> voidRoomTwelve; // Bottom and left exits

	public List<GameObject> voidExitRoomsTopExit;
	public List<GameObject> voidExitRoomsRightExit;
	public List<GameObject> voidExitRoomsBottomExit;
	public List<GameObject> voidExitRoomsLeftExit;
	
	// Lists for each type of room holding a various selection
	public List<GameObject> prototypeRoomOne; // Left and right exits
	public List<GameObject> prototypeRoomTwo; // Left, right and bottom exits
	public List<GameObject> prototypeRoomThree; // Left, right and top exits
	public List<GameObject> prototypeRoomFour; // Left, right, top and bottom exits
	public List<GameObject> prototypeRoomFive; // Left exit
	public List<GameObject> prototypeRoomSix; // Top exit
	public List<GameObject> prototypeRoomSeven; // Right exit
	public List<GameObject> prototypeRoomEight; // Bottom exit
	public List<GameObject> prototypeRoomNine; // Left and top exits
	public List<GameObject> prototypeRoomTen; // Top and right exits
	public List<GameObject> prototypeRoomEleven; // Right and bottom exits
	public List<GameObject> prototypeRoomTwelve; // Bottom and left exits

	public List<GameObject> prototypeExitRoomsTopExit;
	public List<GameObject> prototypeExitRoomsRightExit;
	public List<GameObject> prototypeExitRoomsBottomExit;
	public List<GameObject> prototypeExitRoomsLeftExit;

	public Grid grid;

	public int rowCount;
	public int columnCount;

	public float horizontalMove;
	public float verticalMove;

	public int mainPathDirection; // 1 = up, 2 = right, 3 = down, 4 = left
	public int numberOfRooms;
	public bool prototyping;

	List<int> roomsWithRightExit;
	List<int> roomsWithLeftExit;
	List<int> roomsWithTopExit;
	List<int> roomsWithBottomExit;

	List<int> roomsWithRightExitSidePath;
	List<int> roomsWithLeftExitSidePath;
	List<int> roomsWithTopExitSidePath;
	List<int> roomsWithBottomExitSidePath;

	int startRoomRow;
	int startRoomColumn;
	int endRoomRow;
	int endRoomColumn;

	public bool endRoomGenerationFailed = false;

	public int biome; // 1 == Desert, 2 == Forest, 3 == Ice, 4 == Void 

	public GameObject startRoom;

	LevelManager manager;

	// Use this for initialization
	void Start () {

		Debug.Log("========================NEW LEVEL=====================");

		if (!prototyping) 
		{
			manager = GameObject.Find("Manager").GetComponent<LevelManager>();
		}

		grid = GameObject.Find("EmptyGrid").GetComponent<Grid>();
		
		rowCount = numberOfRooms * 2;
		columnCount = numberOfRooms * 2;
		roomsWithRightExit = new List<int>(){1, 2, 3, 4, 10, 11};
		roomsWithLeftExit = new List<int>(){1, 2, 3, 4, 9, 12};
		roomsWithTopExit = new List<int>(){3, 4, 9, 10};
		roomsWithBottomExit = new List<int>(){2, 4, 11, 12};	

		roomsWithRightExitSidePath = new List<int>(){1, 10, 11};
		roomsWithLeftExitSidePath = new List<int>(){1, 9, 12};
		roomsWithTopExitSidePath = new List<int>(){9, 10};
		roomsWithBottomExitSidePath = new List<int>(){11, 12};	
		int[,] level = generateMainPath(rowCount, columnCount);
		int[,] levelWithSidePaths = generateSidePaths(level);

		// Use the array to fill in the level using prefab rooms
		if (prototyping) 
		{
			generatePrototypeLevel(levelWithSidePaths);
		} else 
		{
			generateLevel(levelWithSidePaths);
		}
		
	}

	void generateLevel(int[,] level) 
	{

		for (int i = 0; i < level.GetLength(0); i++) 
		{
			for (int j = 0; j < level.GetLength(1); j++) 
			{	
				if (level[i, j]	!= 0 && !(i == endRoomRow && j == endRoomColumn))
				{
					GameObject obj;
					switch (biome) 
					{
						case 1:
							obj = Instantiate(getDesertRoom(level[i, j]));
							break;
						case 2:
							obj = Instantiate(getForestRoom(level[i, j]));
							break;
						case 3:
							obj = Instantiate(getIceRoom(level[i, j]));
							break;	
						case 4:
							obj = Instantiate(getVoidRoom(level[i, j]));
							break;	
						default:
							obj = Instantiate(getPrototypeRoom(level[i, j])); // Something went wrong
							break;	
					}
					

					obj.transform.parent = grid.transform;

					obj.transform.position = Vector3.zero;

					// Move the room to its valid position
					obj.transform.position = 
						new Vector3(obj.transform.position.x + j * horizontalMove, 
								obj.transform.position.y + i * verticalMove, 
								0);

					if (i == startRoomRow && j == startRoomColumn) 
					{
						startRoom = obj;
					}

					manager.levelComponents.Add(obj);
				} else if (i == endRoomRow && j == endRoomColumn) 
				{
					GameObject obj;

					Debug.Log("END ROOM ROW:" + endRoomRow + "    END ROOM COLUMN: " + endRoomColumn + "   ROOM NUMBER: " + level[i, j]);

					if (level[i, j] == 6) 
					{
						switch (biome) 
						{
							case 1:
								obj = Instantiate(desertExitRoomsTopExit[Random.Range(0, desertExitRoomsTopExit.Count)]);
								break;
							case 2:
								obj = Instantiate(forestExitRoomsTopExit[Random.Range(0, forestExitRoomsTopExit.Count)]);
								break;
							case 3:
								obj = Instantiate(iceExitRoomsTopExit[Random.Range(0, iceExitRoomsTopExit.Count)]);
								break;	
							case 4:
								obj = Instantiate(voidExitRoomsTopExit[Random.Range(0, voidExitRoomsTopExit.Count)]);
								break;	
							default:
								obj = null; // Something went wrong
								break;	
						}
					} else if (level[i, j] == 7) 
					{
						switch (biome) 
						{
							case 1:
								obj = Instantiate(desertExitRoomsRightExit[Random.Range(0, desertExitRoomsRightExit.Count)]);
								break;
							case 2:
								obj = Instantiate(forestExitRoomsRightExit[Random.Range(0, forestExitRoomsRightExit.Count)]);
								break;
							case 3:
								obj = Instantiate(iceExitRoomsRightExit[Random.Range(0, iceExitRoomsRightExit.Count)]);
								break;	
							case 4:
								obj = Instantiate(voidExitRoomsRightExit[Random.Range(0, voidExitRoomsRightExit.Count)]);
								break;	
							default:
								obj = null; // Something went wrong
								break;	
						}
					} else if (level[i, j] == 8) 
					{
						switch (biome) 
						{
							case 1:
								obj = Instantiate(desertExitRoomsBottomExit[Random.Range(0, desertExitRoomsBottomExit.Count)]);
								break;
							case 2:
								obj = Instantiate(forestExitRoomsBottomExit[Random.Range(0, forestExitRoomsBottomExit.Count)]);
								break;
							case 3:
								obj = Instantiate(iceExitRoomsBottomExit[Random.Range(0, iceExitRoomsBottomExit.Count)]);
								break;	
							case 4:
								obj = Instantiate(voidExitRoomsBottomExit[Random.Range(0, voidExitRoomsBottomExit.Count)]);
								break;	
							default:
								obj = null; // Something went wrong
								break;	
						}
					} else if (level[i, j] == 5) 
					{
						switch (biome) 
						{
							case 1:
								obj = Instantiate(desertExitRoomsLeftExit[Random.Range(0, desertExitRoomsLeftExit.Count)]);
								break;
							case 2:
								obj = Instantiate(forestExitRoomsLeftExit[Random.Range(0, forestExitRoomsLeftExit.Count)]);
								break;
							case 3:
								obj = Instantiate(iceExitRoomsLeftExit[Random.Range(0, iceExitRoomsLeftExit.Count)]);
								break;	
							case 4:
								obj = Instantiate(voidExitRoomsLeftExit[Random.Range(0, voidExitRoomsLeftExit.Count)]);
								break;	
							default:
								obj = null; // Something went wrong
								break;	
						}
					} else 
					{
						obj = null;
						manager.resetLevel(); // Something went wrong, reset the level
					}
					
					obj.transform.parent = grid.transform;

					obj.transform.position = Vector3.zero;

					// Move the room to its valid position
					obj.transform.position = 
						new Vector3(obj.transform.position.x + j * horizontalMove, 
								obj.transform.position.y + i * verticalMove, 
								0);

					manager.levelComponents.Add(obj);
				}
			}		
		}
		manager.setStartPos();
	}
	
	void generatePrototypeLevel(int[,] level) 
	{

		for (int i = 0; i < level.GetLength(0); i++) 
		{
			for (int j = 0; j < level.GetLength(1); j++) 
			{	
				if (level[i, j]	!= 0 && !(i == endRoomRow && j == endRoomColumn))
				{
					GameObject obj = Instantiate(getPrototypeRoom(level[i, j]));

					obj.transform.parent = grid.transform;

					obj.transform.position = Vector3.zero;

					// Move the room to its valid position
					obj.transform.position = 
						new Vector3(obj.transform.position.x + j * horizontalMove, 
								obj.transform.position.y + i * verticalMove, 
								0);
					
					if (i == startRoomRow && j == startRoomColumn) 
					{
						obj.GetComponentInChildren<TextMesh>().text = "STARTING ROOM";
					}
				} else if (i == endRoomRow && j == endRoomColumn) 
				{
					GameObject obj;

					if (level[i, j] == 6) 
					{
						obj = Instantiate(prototypeExitRoomsTopExit[Random.Range(0, prototypeExitRoomsTopExit.Count)]);
					} else if (level[i, j] == 7) 
					{
						obj = Instantiate(prototypeExitRoomsRightExit[Random.Range(0, prototypeExitRoomsRightExit.Count)]);
					} else if (level[i, j] == 8) 
					{
						obj = Instantiate(prototypeExitRoomsBottomExit[Random.Range(0, prototypeExitRoomsBottomExit.Count)]);
					} else if (level[i, j] == 5) 
					{
						obj = Instantiate(prototypeExitRoomsLeftExit[Random.Range(0, prototypeExitRoomsLeftExit.Count)]);
					} else 
					{
						obj = Instantiate(prototypeExitRoomsRightExit[Random.Range(0, prototypeExitRoomsRightExit.Count)]);
						obj.GetComponentInChildren<TextMesh>().text = "ERROR GENERATING EXIT";
					}
					
					obj.transform.parent = grid.transform;

					obj.transform.position = Vector3.zero;

					// Move the room to its valid position
					obj.transform.position = 
						new Vector3(obj.transform.position.x + j * horizontalMove, 
								obj.transform.position.y + i * verticalMove, 
								0);
				}
			}		
		}
	}

	// Pick a random room of the specified type
	GameObject getDesertRoom(int type) 
	{
		if (type == 1) 
		{
			return desertRoomOne[Random.Range(0, desertRoomOne.Count - 1)];
		} else if (type == 2) 
		{
			return desertRoomTwo[Random.Range(0, desertRoomTwo.Count - 1)];
		} else if (type == 3) 
		{
			return desertRoomThree[Random.Range(0, desertRoomThree.Count - 1)];
		} else if (type == 4) 
		{
			return desertRoomFour[Random.Range(0, desertRoomFour.Count - 1)];
		} else if (type == 5) 
		{
			return desertRoomFive[Random.Range(0, desertRoomFive.Count - 1)];
		} else if (type == 6) 
		{
			return desertRoomSix[Random.Range(0, desertRoomSix.Count - 1)];
		} else if (type == 7) 
		{
			return desertRoomSeven[Random.Range(0, desertRoomSeven.Count - 1)];
		} else if (type == 8) 
		{
			return desertRoomEight[Random.Range(0, desertRoomEight.Count - 1)];
		} else if (type == 9) 
		{
			return desertRoomNine[Random.Range(0, desertRoomNine.Count - 1)];
		} else if (type == 10) 
		{
			return desertRoomTen[Random.Range(0, desertRoomTen.Count - 1)];
		} else if (type == 11) 
		{
			return desertRoomEleven[Random.Range(0, desertRoomEleven.Count - 1)];
		} else if (type == 12) 
		{
			return desertRoomTwelve[Random.Range(0, desertRoomTwelve.Count - 1)];
		} else 
		{
			return null;
		}
	}

	GameObject getForestRoom(int type) 
	{
		if (type == 1) 
		{
			return forestRoomOne[Random.Range(0, forestRoomOne.Count - 1)];
		} else if (type == 2) 
		{
			return forestRoomTwo[Random.Range(0, forestRoomTwo.Count - 1)];
		} else if (type == 3) 
		{
			return forestRoomThree[Random.Range(0, forestRoomThree.Count - 1)];
		} else if (type == 4) 
		{
			return forestRoomFour[Random.Range(0, forestRoomFour.Count - 1)];
		} else if (type == 5) 
		{
			return forestRoomFive[Random.Range(0, forestRoomFive.Count - 1)];
		} else if (type == 6) 
		{
			return forestRoomSix[Random.Range(0, forestRoomSix.Count - 1)];
		} else if (type == 7) 
		{
			return forestRoomSeven[Random.Range(0, forestRoomSeven.Count - 1)];
		} else if (type == 8) 
		{
			return forestRoomEight[Random.Range(0, forestRoomEight.Count - 1)];
		} else if (type == 9) 
		{
			return forestRoomNine[Random.Range(0, forestRoomNine.Count - 1)];
		} else if (type == 10) 
		{
			return forestRoomTen[Random.Range(0, forestRoomTen.Count - 1)];
		} else if (type == 11) 
		{
			return forestRoomEleven[Random.Range(0, forestRoomEleven.Count - 1)];
		} else if (type == 12) 
		{
			return forestRoomTwelve[Random.Range(0, forestRoomTwelve.Count - 1)];
		} else 
		{
			return null;
		}
	}

	GameObject getIceRoom(int type) 
	{
		if (type == 1) 
		{
			return iceRoomOne[Random.Range(0, iceRoomOne.Count - 1)];
		} else if (type == 2) 
		{
			return iceRoomTwo[Random.Range(0, iceRoomTwo.Count - 1)];
		} else if (type == 3) 
		{
			return iceRoomThree[Random.Range(0, iceRoomThree.Count - 1)];
		} else if (type == 4) 
		{
			return iceRoomFour[Random.Range(0, iceRoomFour.Count - 1)];
		} else if (type == 5) 
		{
			return iceRoomFive[Random.Range(0, iceRoomFive.Count - 1)];
		} else if (type == 6) 
		{
			return iceRoomSix[Random.Range(0, iceRoomSix.Count - 1)];
		} else if (type == 7) 
		{
			return iceRoomSeven[Random.Range(0, iceRoomSeven.Count - 1)];
		} else if (type == 8) 
		{
			return iceRoomEight[Random.Range(0, iceRoomEight.Count - 1)];
		} else if (type == 9) 
		{
			return iceRoomNine[Random.Range(0, iceRoomNine.Count - 1)];
		} else if (type == 10) 
		{
			return iceRoomTen[Random.Range(0, iceRoomTen.Count - 1)];
		} else if (type == 11) 
		{
			return iceRoomEleven[Random.Range(0, iceRoomEleven.Count - 1)];
		} else if (type == 12) 
		{
			return iceRoomTwelve[Random.Range(0, iceRoomTwelve.Count - 1)];
		} else 
		{
			return null;
		}
	}

		GameObject getVoidRoom(int type) 
	{
		if (type == 1) 
		{
			return voidRoomOne[Random.Range(0, voidRoomOne.Count - 1)];
		} else if (type == 2) 
		{
			return voidRoomTwo[Random.Range(0, voidRoomTwo.Count - 1)];
		} else if (type == 3) 
		{
			return voidRoomThree[Random.Range(0, voidRoomThree.Count - 1)];
		} else if (type == 4) 
		{
			return voidRoomFour[Random.Range(0, voidRoomFour.Count - 1)];
		} else if (type == 5) 
		{
			return voidRoomFive[Random.Range(0, voidRoomFive.Count - 1)];
		} else if (type == 6) 
		{
			return voidRoomSix[Random.Range(0, voidRoomSix.Count - 1)];
		} else if (type == 7) 
		{
			return voidRoomSeven[Random.Range(0, voidRoomSeven.Count - 1)];
		} else if (type == 8) 
		{
			return voidRoomEight[Random.Range(0, voidRoomEight.Count - 1)];
		} else if (type == 9) 
		{
			return voidRoomNine[Random.Range(0, voidRoomNine.Count - 1)];
		} else if (type == 10) 
		{
			return voidRoomTen[Random.Range(0, voidRoomTen.Count - 1)];
		} else if (type == 11) 
		{
			return voidRoomEleven[Random.Range(0, voidRoomEleven.Count - 1)];
		} else if (type == 12) 
		{
			return voidRoomTwelve[Random.Range(0, voidRoomTwelve.Count - 1)];
		} else 
		{
			return null;
		}
	}

	// Pick a random room of the specified type
	GameObject getPrototypeRoom(int type) 
	{
		if (type == 1) 
		{
			return prototypeRoomOne[Random.Range(0, prototypeRoomOne.Count - 1)];
		} else if (type == 2) 
		{
			return prototypeRoomTwo[Random.Range(0, prototypeRoomTwo.Count - 1)];
		} else if (type == 3) 
		{
			return prototypeRoomThree[Random.Range(0, prototypeRoomThree.Count - 1)];
		} else if (type == 4) 
		{
			return prototypeRoomFour[Random.Range(0, prototypeRoomFour.Count - 1)];
		} else if (type == 5) 
		{
			return prototypeRoomFive[Random.Range(0, prototypeRoomFive.Count - 1)];
		} else if (type == 6) 
		{
			return prototypeRoomSix[Random.Range(0, prototypeRoomSix.Count - 1)];
		} else if (type == 7) 
		{
			return prototypeRoomSeven[Random.Range(0, prototypeRoomSeven.Count - 1)];
		} else if (type == 8) 
		{
			return prototypeRoomEight[Random.Range(0, prototypeRoomEight.Count - 1)];
		} else if (type == 9) 
		{
			return prototypeRoomNine[Random.Range(0, prototypeRoomNine.Count - 1)];
		} else if (type == 10) 
		{
			return prototypeRoomTen[Random.Range(0, prototypeRoomTen.Count - 1)];
		} else if (type == 11) 
		{
			return prototypeRoomEleven[Random.Range(0, prototypeRoomEleven.Count - 1)];
		} else if (type == 12) 
		{
			return prototypeRoomTwelve[Random.Range(0, prototypeRoomTwelve.Count - 1)];
		} else 
		{
			return null;
		}
	}

	int[,] generateSidePaths(int[,] level)
	{	

		for (int i = 0; i < level.GetLength(0); i++) 
		{
			for (int j = 0; j < level.GetLength(1); j++) 
			{	
				int currentRoom = level[i, j];
				if (level[i, j]	!= 0)
				{

					if (roomsWithTopExit.Contains(currentRoom)) 
					{
						if (!roomToTop(i, j, level)) 
						{
							level =	finishSidePath(i, j, level);
						}
					}

					if (roomsWithRightExit.Contains(currentRoom)) 
					{
						if (!roomToRight(i, j, level)) 
						{
							level =	finishSidePath(i, j, level);
						}
					}

					if (roomsWithBottomExit.Contains(currentRoom)) 
					{
						if (!roomToBottom(i, j, level)) 
						{
							level =	finishSidePath(i, j, level);
						}
					}

					if (roomsWithLeftExit.Contains(currentRoom)) 
					{
						if (!roomToLeft(i, j, level)) 
						{
							level =	finishSidePath(i, j, level);
						}
					}
				}	
			}		
		}

		return level;
	}

	int[,] finishSidePath(int row, int column, int[,] level)
	{
		Debug.Log("FINISH SIDE PATH CALLED");
		int sidePathLength = numberOfRooms / 3;
		int counter = 0;
		int rowIter = row;
		int columnIter = column;

		bool rightInserted = false;
		bool topInserted = false;
		bool bottomInserted = false;
		bool leftInserted = false;	

		bool lastRightInserted = false;
		bool lastTopInserted = false;
		bool lastBottomInserted = false;
		bool lastLeftInserted = false;	

		int lastRow = -1;
		int lastColumn = -1;

		while (counter < sidePathLength) 
		{	
			// Prevent the algorithm from getting stuck on a room
			if (rowIter == lastRow && columnIter == lastColumn) 
			{
				break;
			}
			lastRow = rowIter;
			lastColumn = columnIter;

			Debug.Log("--------------Side Path--------------");
			Debug.Log("Row: " + rowIter + "      Column: " + columnIter);
			int currentRoom = level[rowIter, columnIter];
			Debug.Log("Counter: " + counter);
			Debug.Log("Side Path Length: " + sidePathLength);
			Debug.Log("Current Room: " + currentRoom);

			// Handle rooms with top exits
			if (roomsWithTopExit.Contains(currentRoom) && (rightInserted == false && topInserted == false && bottomInserted == false && leftInserted == false)) 
			{
				bool result = roomToTop(rowIter, columnIter, level);
				Debug.Log("result = " + result);
				Debug.Log("row = " + rowIter + "column = " + columnIter);
				if (result == false && counter < sidePathLength - 1) 
				{
					int room = insertRoomToTopSidePath();
					level[rowIter + 1, columnIter] = room;
					rowIter++;
					topInserted = true;
					Debug.Log("Inserting  " + room + "to the top");
				} else if (counter == sidePathLength - 1) 
				{
					break;
				}
			}

			// Handle rooms with right exits
			if (roomsWithRightExit.Contains(currentRoom) && (rightInserted == false && topInserted == false && bottomInserted == false && leftInserted == false)) 
			{
				bool result = roomToRight(rowIter, columnIter, level);
				Debug.Log("result = " + result);
				if (result == false && counter < sidePathLength - 1) 
				{
					int room = insertRoomToRightSidePath();
					level[rowIter, columnIter + 1] = room;
					columnIter++;
					rightInserted = true;
					Debug.Log("Inserting  " + room + "to the right");
				} else if (counter == sidePathLength - 1) 
				{
					break;
				}
			}

			// Handle rooms with bottom exits
			if (roomsWithBottomExit.Contains(currentRoom) && (rightInserted == false && topInserted == false && bottomInserted == false && leftInserted == false)) 
			{
				bool result = roomToBottom(rowIter, columnIter, level);
				Debug.Log("result = " + result);
				Debug.Log("row = " + rowIter + "column = " + columnIter);
				if (result == false && counter < sidePathLength - 1) 
				{
					int room = insertRoomToBottomSidePath();
					level[rowIter - 1, columnIter] = room;
					rowIter--;
					bottomInserted = true;
					Debug.Log("Inserting  " + room + "to the bottom");
				} else if (counter == sidePathLength - 1) 
				{
					break;
				}
			}

			// Handle rooms with left exits
			if (roomsWithLeftExit.Contains(currentRoom) && (rightInserted == false && topInserted == false && bottomInserted == false && leftInserted == false)) 
			{
				bool result = roomToLeft(rowIter, columnIter, level);
				Debug.Log("result = " + result);
				if (result == false && counter < sidePathLength - 1) 
				{
					int room = insertRoomToLeftSidePath();
					level[rowIter, columnIter - 1] = room;
					columnIter--;
					leftInserted = true;
					Debug.Log("Inserting  " + room + "to the left");
				} else if (counter == sidePathLength - 1) 
				{
					break;
				}
			}

			lastRightInserted = rightInserted;
			lastTopInserted = topInserted;
			lastBottomInserted = bottomInserted;
			lastLeftInserted = leftInserted;	

			rightInserted = false;
			topInserted = false;
			bottomInserted = false;
			leftInserted = false;	

			counter++;
			Debug.Log("");
			Debug.Log("");
			Debug.Log("");
		}

		if (!endRoomGenerationFailed) 
		{
			// Insert end room
			if (roomsWithTopExit.Contains(level[rowIter, columnIter]) && !roomToTop(rowIter, columnIter, level)) 
			{
				level[rowIter + 1, columnIter] = 8;
				Debug.Log("SidePathEndRow: " + rowIter + 1 + "      SidePathEndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithRightExit.Contains(level[rowIter, columnIter])  && !roomToRight(rowIter, columnIter, level)) 
			{
				level[rowIter, columnIter + 1] = 5;
				Debug.Log("SidePathEndRow: " + endRoomRow + "      SidePathEndColumn: " + endRoomColumn + 1);
				return level;
			} else if (roomsWithBottomExit.Contains(level[rowIter, columnIter]) && !roomToBottom(rowIter, columnIter, level)) 
			{
				level[rowIter - 1, columnIter] = 6;
				Debug.Log("SidePathEndRow: " + (endRoomRow - 1) + "      SidePathEndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithLeftExit.Contains(level[rowIter, columnIter])  && !roomToLeft(rowIter, columnIter, level)) 
			{
				level[rowIter, columnIter - 1] = 7;
				Debug.Log("SidePathEndRow: " + endRoomRow + "     SidePathEndColumn: " + (endRoomColumn - 1));
				return level;
			}
		} else if (endRoomGenerationFailed) 
		{
			Debug.Log("INSERT ENDROOM ON SIDEPATH");
			// Insert end room
			if (roomsWithTopExit.Contains(level[rowIter, columnIter]) && !roomToTop(rowIter, columnIter, level)) 
			{
				endRoomGenerationFailed = false;
				level[rowIter + 1, columnIter] = 8;
				endRoomColumn = columnIter;
				endRoomRow = rowIter + 1;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithRightExit.Contains(level[rowIter, columnIter])  && !roomToRight(rowIter, columnIter, level)) 
			{
				endRoomGenerationFailed = false;
				level[rowIter, columnIter + 1] = 5;
				endRoomColumn = columnIter + 1;
				endRoomRow = rowIter;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithBottomExit.Contains(level[rowIter, columnIter]) && !roomToBottom(rowIter, columnIter, level)) 
			{
				endRoomGenerationFailed = false;
				level[rowIter - 1, columnIter] = 6;
				endRoomColumn = columnIter;
				endRoomRow = rowIter - 1;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithLeftExit.Contains(level[rowIter, columnIter])  && !roomToLeft(rowIter, columnIter, level)) 
			{
				endRoomGenerationFailed = false;
				level[rowIter, columnIter - 1] = 7;
				endRoomColumn = columnIter - 1;
				endRoomRow = rowIter;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			}	
		}

		return level;
	}

	int insertDeadEnd (bool lastRightInserted, bool lastTopInserted, bool lastBottomInserted, bool lastLeftInserted) 
	{
		Debug.Log("Inserting Dead End");
		if (lastRightInserted) 
		{
			return insertRoomToRightSidePath();
		}
		if (lastTopInserted) 
		{
			return insertRoomToTopSidePath();
		}
		if (lastBottomInserted) 
		{
			return insertRoomToBottomSidePath();
		}
		if (lastLeftInserted) 
		{
			return insertRoomToLeftSidePath();
		}
		return 0;
	}

	// Generates the main path of the level, ending with an exit
	// Generates the start of side rooms which will be finished later
	int[,] generateMainPath(int rows, int columns)
	{
		int[,] level = new int[rows, columns];
		Debug.Log("row length = " + level.GetLength(0));
		Debug.Log("column length = " + level.GetLength(1));

		float rowsFloat = rows;
		float columnsFloat = columns;

		float row1;
		float column1;

		int startRoom;
		

		// Place the starting room in the center of the array
		if (mainPathDirection == 1) 
		{
			row1 = Mathf.Round(rowsFloat / 2);
			column1 = Mathf.Round(columnsFloat / 2);
			startRoom = 6;
		} else if (mainPathDirection == 2) 
		{
			row1 = Mathf.Round(rowsFloat / 2);
			column1 = Mathf.Round(columnsFloat / 2);
			startRoom = 7;
		} else if (mainPathDirection == 3) 
		{
			row1 = Mathf.Round(rowsFloat / 2);
			column1 = Mathf.Round(columnsFloat / 2);
			startRoom = 8;
		} else if (mainPathDirection == 4) 
		{
			row1 = Mathf.Round(rowsFloat / 2);
			column1 = Mathf.Round(columnsFloat / 2);
			startRoom = 5;
		} else 
		{
			row1 = Mathf.Round(rowsFloat / 2);
			column1 = Mathf.Round(columnsFloat / 2);
			startRoom = 7;		
		}

		int rowIter = (int) row1;
		int columnIter = (int) column1;

		startRoomRow = rowIter;
		startRoomColumn = columnIter;

		level[rowIter, columnIter] = startRoom; // Assign the start room

		int counter = 0;
		int roomCounter = 0;

		int lastRow = -1;
		int lastColumn = -1;

		while (roomCounter < numberOfRooms && counter < 200 && columnIter < columnCount) 
		{	
			Debug.Log("Row: " + rowIter + "      Column: " + columnIter);
			int currentRoom = level[rowIter, columnIter];
			Debug.Log("Counter: " + counter);
			Debug.Log("Current Room: " + currentRoom);

			bool rightInserted = false;
			bool topInserted = false;
			bool bottomInserted = false;
			bool leftInserted = false;

			// Prevent the algorithm from getting stuck on a room
			if (rowIter == lastRow && columnIter == lastColumn) 
			{
				level[rowIter, columnIter] = 1;
			}

			// Handle rooms with left exits
			if (currentRoom == 1 || currentRoom == 2 || currentRoom == 3 || currentRoom == 4 || currentRoom == 5 || currentRoom == 9 || currentRoom == 12) 
			{
				bool result1 = roomToLeft(rowIter, columnIter, level);
				Debug.Log("result = " + result1);
				if (result1 == false) 
				{
					int room = insertRoomToLeft();
					level[rowIter, columnIter - 1] = room;
					leftInserted = true;
					Debug.Log("Inserting  " + room + "to the left");
					roomCounter++;
				}
			}

			// Handle rooms with top exits
			if (currentRoom == 3 || currentRoom == 4 || currentRoom == 6 || currentRoom == 9 || currentRoom == 10) 
			{
				bool result2 = roomToTop(rowIter, columnIter, level);
				Debug.Log("result = " + result2);
				if (result2 == false) 
				{
					int room = insertRoomToTop();
					level[rowIter + 1, columnIter] = room;
					topInserted = true;
					Debug.Log("Inserting  " + room + "to the top");
					roomCounter++;
				}
			}

			// Handle rooms with right exits
			if (currentRoom == 1 || currentRoom == 2 || currentRoom == 3 || currentRoom == 4 || currentRoom == 10 || currentRoom == 11 || currentRoom == 7) 
			{
				bool result3 = roomToRight(rowIter, columnIter, level);
				Debug.Log("result = " + result3);
				if (result3 == false) 
				{
					int room = insertRoomToRight();
					level[rowIter, columnIter + 1] = room;
					rightInserted = true;
					Debug.Log("Inserting  " + room + "to the right");
					roomCounter++;
				}
			}

			// Handle rooms with bottom exits
			if (currentRoom == 2 || currentRoom == 4 || currentRoom == 8 || currentRoom == 11 || currentRoom == 12) 
			{
				bool result4 = roomToBottom(rowIter, columnIter, level);
				Debug.Log("result = " + result4);
				if (result4 == false) 
				{
					int room = insertRoomToBottom();
					level[rowIter - 1, columnIter] = room;
					bottomInserted = true;
					Debug.Log("Inserting  " + room + "to the bottom");
					roomCounter++;
				}
			}

			lastRow = rowIter;
			lastColumn = columnIter;

			if (mainPathDirection == 1) 
			{
				if (topInserted) 
				{
					rowIter++;
				}
				// top is favoured
 				else if (rightInserted && leftInserted) 
				{
					if (Random.Range(1,3) == 1) 
					{
						columnIter--;
					} 
					else 
					{
						columnIter++;
					}
				} 
				else if (rightInserted) 
				{
					columnIter++;
				} 
				else if (leftInserted) 
				{
					columnIter--;
				} 
				else if (bottomInserted) 
				{
					rowIter--;
				} 
				else 
				{
					Debug.Log("Error");
				}
			} else if (mainPathDirection == 2) 
			{
				// right is favoured
				if (rightInserted) 
				{
					columnIter++;
				} 
				else if (topInserted && bottomInserted) 
				{
					if (Random.Range(1,3) == 1) 
					{
						rowIter--;
					} 
					else 
					{
						rowIter++;
					}
				}
				else if (topInserted) 
				{
					rowIter++;
				}
				else if (bottomInserted) 
				{
					rowIter--;
				} 
				else if (leftInserted) 
				{
					columnIter--;
				} 
				else 
				{
					Debug.Log("Error");
				}
			} 
			else if (mainPathDirection == 3) 
			{
				// Bottom is favoured
				if (bottomInserted) 
				{
					rowIter--;
				}
 				else if (rightInserted && leftInserted) 
				{
					if (Random.Range(1,3) == 1) 
					{
						columnIter--;
					} 
					else 
					{
						columnIter++;
					}
				} 
				else if (rightInserted) 
				{
					columnIter++;
				} 
				else if (leftInserted) 
				{
					columnIter--;
				} 
				else if (topInserted) 
				{
					rowIter++;
				} 
				else 
				{
					Debug.Log("Error");
				}
			} else if (mainPathDirection == 4) 
			{	
				// right is favoured
				if (leftInserted) 
				{
					columnIter--;
				} 
				else if (topInserted && bottomInserted) 
				{
					if (Random.Range(1,3) == 1) 
					{
						rowIter--;
					} 
					else 
					{
						rowIter++;
					}
				}
				else if (topInserted) 
				{
					rowIter++;
				}
				else if (bottomInserted) 
				{
					rowIter--;
				} 
				else if (rightInserted) 
				{
					columnIter++;
				} 
				else 
				{
					Debug.Log("Error");
				}
			} else
			{
				Debug.Log("===========================Main path direction error ========================");
			}

			// If main path generation gets stuck, exit and generate end room from one of the side paths
			if (lastRow == rowIter && lastColumn == columnIter) 
			{
				endRoomGenerationFailed = true;
				Debug.Log ("END ROOM GENERATION FAILED");
				break;
			}



			counter++;
			Debug.Log("");
			Debug.Log("");
			Debug.Log("");
		}

		if (!endRoomGenerationFailed) 
		{
			// Insert end room
			if (roomsWithTopExit.Contains(level[rowIter, columnIter]) && !roomToTop(rowIter, columnIter, level)) 
			{
				level[rowIter + 1, columnIter] = 8;
				endRoomColumn = columnIter;
				endRoomRow = rowIter + 1;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithRightExit.Contains(level[rowIter, columnIter])  && !roomToRight(rowIter, columnIter, level)) 
			{
				level[rowIter, columnIter + 1] = 5;
				endRoomColumn = columnIter + 1;
				endRoomRow = rowIter;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithBottomExit.Contains(level[rowIter, columnIter]) && !roomToBottom(rowIter, columnIter, level)) 
			{
				level[rowIter - 1, columnIter] = 6;
				endRoomColumn = columnIter;
				endRoomRow = rowIter - 1;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			} else if (roomsWithLeftExit.Contains(level[rowIter, columnIter])  && !roomToLeft(rowIter, columnIter, level)) 
			{
				level[rowIter, columnIter - 1] = 7;
				endRoomColumn = columnIter - 1;
				endRoomRow = rowIter;
				Debug.Log("EndRow: " + endRoomRow + "      EndColumn: " + endRoomColumn);
				return level;
			}			
		}

		return level;
	}

	int insertRoomToLeft() 
	{
		return roomsWithRightExit[Random.Range(0, roomsWithRightExit.Count)];
	}
	int insertRoomToTop() 
	{
		return roomsWithBottomExit[Random.Range(0, roomsWithBottomExit.Count)];
	}
	int insertRoomToRight() 
	{
		return roomsWithLeftExit[Random.Range(0, roomsWithLeftExit.Count)];
	}
	int insertRoomToBottom() 
	{
		return roomsWithTopExit[Random.Range(0, roomsWithTopExit.Count)];
	}
	
	int insertRoomToLeftSidePath() 
	{
		return roomsWithRightExitSidePath[Random.Range(0, roomsWithRightExitSidePath.Count)];
	}
	int insertRoomToTopSidePath() 
	{
		return roomsWithBottomExitSidePath[Random.Range(0, roomsWithBottomExitSidePath.Count)];
	}
	int insertRoomToRightSidePath() 
	{
		return roomsWithLeftExitSidePath[Random.Range(0, roomsWithLeftExitSidePath.Count)];
	}
	int insertRoomToBottomSidePath() 
	{
		return roomsWithTopExitSidePath[Random.Range(0, roomsWithTopExitSidePath.Count)];
	}

	// Returns false if the slot is empty
	// Returns true if the slot is out of bounds
	// Returns true if the slot is taken
	// Returns true if failed
	bool roomToLeft(int row, int column, int[,] level) 
	{
		Debug.Log("checking slot to left");
		if ((row < 0 || row > rowCount) || (column - 1 < 0 || column - 1 > columnCount))
		{
			Debug.Log("out of bounds to left");
			return true; // Slot is out of bounds
		}
		else if (level[row, column - 1] != 0) 
		{
			return true; // Return room type
		}		
		else if (level[row, column - 1] == 0) 
		{
			return false; // Slot is empty
		}

		return true; 
	}

	// Returns false if the slot is empty
	// Returns true if the slot is out of bounds
	// Returns true if the slot is taken
	// Returns true if failed
	bool roomToTop(int row, int column, int[,] level) 
	{
		Debug.Log("checking slot to top");
		if (row >= rowCount - 1 || (column < 0 || column > columnCount))
		{
			Debug.Log("out of bounds to top");
			return true; // Slot is out of bounds
		}
		else if (level[row + 1, column] != 0) 
		{
			Debug.Log("already room to top");
			return true; // Return room type
		}		
		else if (level[row + 1, column] == 0) 
		{
			Debug.Log("empty to top");
			return false; // Slot is empty
		}

		Debug.Log("ERROR CHECKING TO TOP");
		return true; 
	}

	// Returns false if the slot is empty
	// Returns true if the slot is out of bounds
	// Returns true if the slot is taken
	// Returns true if failed
	bool roomToRight(int row, int column, int[,] level) 
	{
		Debug.Log("checking slot to right");
		if ((row < 0 || row > rowCount) || (column <= 0 || column >= columnCount - 1))
		{
			Debug.Log("out of bounds to right");
			return true; // Slot is out of bounds
		}
		else if (level[row, column + 1] != 0) 
		{
			return true; // Return room type
		}		
		else if (level[row, column + 1] == 0) 
		{
			return false; // Slot is empty
		}

		return true; 
	}

	// Returns false if the slot is empty
	// Returns true if the slot is out of bounds
	// Returns true if the slot is taken
	// Returns true if failed
	bool roomToBottom(int row, int column, int[,] level) 
	{
		Debug.Log("checking slot to bottom");
		if (row <= 0 || (column < 0 || column > columnCount))
		{
			Debug.Log("out of bounds to bottom");
			return true; // Slot is out of bounds
		}
		else if (level[row - 1, column] != 0) 
		{
			return true; // Return room type
		}		
		else if (level[row - 1, column] == 0) 
		{
			return false; // Slot is empty
		}

		return true; 
	}
}
