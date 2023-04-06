using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    //what version of unity you running? Better be the right one :)
    public Player player1;
    public GameObject building;
    public GameObject floor;
    public GameObject obstacle;
    public GameObject platform;
    public GameObject puddle;
    public GameObject pipe;
    public GameObject bountySign;
    public GameObject laserWarn;
    public GameObject telePole;
    public GameObject birdie;
    public GameObject barbedFence;
    public GameObject powerGenerator;
    public GameObject cyberTruck;

    private int spawnNum;
    private bool acidQd;
    private bool bountyQd;
    private bool teleQd;
    private bool barbedQd;
    private bool generatorQd;

    public List<GameObject> floors = new List<GameObject>();
    TruckMovement truckMovement;
    private float buildTime;
    private float elevation = 2;
    private float buildX;
    public float buildDis = 0.5f;
    private float obstacleTimer;
    private Vector2 facingLeft;
    private Vector2 facingRight;

    // Start is called before the first frame update
    void Start()
    {
        buildTime = 0f;
        obstacleTimer = 2f;
        building.transform.localScale = new Vector3(40, 30, 10);
        Instantiate(building, new Vector3(player1.transform.position.x, 8.5f, 10), Quaternion.identity);
        Instantiate(platform, new Vector3(player1.transform.position.x + 1f, 2f, 0), Quaternion.identity);
        buildX = player1.transform.position.x + building.transform.localScale.x;
        facingLeft = new Vector2(-cyberTruck.transform.localScale.x, cyberTruck.transform.localScale.y);
        facingRight = new Vector2(cyberTruck.transform.localScale.x, cyberTruck.transform.localScale.y);
        truckMovement = cyberTruck.GetComponent<TruckMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        buildTime -= 1 * Time.deltaTime;
        if (buildTime <= 0)
        {
            SpawnBuilding();
        }
        //SpawnObstacle();
        //Debug.Log(buildHeight);
        // Debug.Log(buildYPos);
        //Debug.Log(building.transform.localScale.y);
    }

    void SpawnBuilding()
    {
        //buildTime -= 1 * Time.deltaTime;
        //if (buildX < player1.transform.position.x + 40f) {
        if (buildX < player1.transform.position.x + 100f)
        {

            float ranBuildY = UnityEngine.Random.Range(30f, 33f);
            building.transform.localScale = new Vector3(40, ranBuildY, 10);
            Instantiate(building, new Vector3(buildX + buildDis, 11, 10), Quaternion.identity);

            float locationX = buildX + buildDis + 4f - building.transform.localScale.x / 3;
            resetQd();
            for (int i = 0; i < 3; i++) {
                while (spawnNum < i + 1) {
                    spawnObstacle(locationX , 11, ranBuildY);
                }
                locationX += building.transform.localScale.x / 3;
            }
            SpawnPlatform(building.transform.localScale, buildX + building.transform.localScale.x / 2 + 1, 11);
            buildX += building.transform.localScale.x + buildDis;
            cyberTruckSpawn();
        } 
    }

        void resetQd() {
            spawnNum = 0;
            acidQd = false;
            bountyQd = false;
            teleQd = false;
            barbedQd = false;
            generatorQd = false;
        }

            /*if (buildX < player1.transform.position.x + 40f) {
                int random = UnityEngine.Random.Range(0,3);
                if (random == 0) {
                    //tripleBuilding();
                } else if (random == 1) {
                    //doubleBuilding();
                } else {
                    singleBuilding();
                }
            } */
            //}



            void tripleBuilding()
            {

                building.transform.localScale = new Vector3(30, 20, 10);
                //buildHeight = building.transform.localScale.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 6, 10), Quaternion.identity);
                //acidPuddle(buildX + building.transform.localScale.x / 2, building.transform.position.y, building.transform.localScale.y);
                // buildYPos = building.transform.position.y;
                //Debug.Log(buildYPos);
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 6, 10), Quaternion.identity);
                Instantiate(building, new Vector3(buildX + buildDis + building.transform.localScale.x / 2, 6, 10), Quaternion.identity);
                SpawnPlatform(building.transform.localScale, buildX + building.transform.localScale.x / 2 + 1, 6);
                buildX += building.transform.localScale.x;

                building.transform.localScale = new Vector3(42, 30, 10);
                //buildHeight = building.transform.localScale.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 11, 10), Quaternion.identity);
                //acidPuddle(buildX + building.transform.localScale.x / 2, building.transform.position.y, building.transform.localScale.y);
                //buildYPos = building.transform.position.y;
                //Debug.Log(buildYPos);
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 11, 10), Quaternion.identity);
                Instantiate(building, new Vector3(buildX + buildDis + building.transform.localScale.x / 2, 11, 10), Quaternion.identity);
                SpawnPlatform(building.transform.localScale, buildX + building.transform.localScale.x / 2 + 1, 11);
                buildX += building.transform.localScale.x + 1;

                building.transform.localScale = new Vector3(37, 25, 10);
                //buildHeight = building.transform.localScale.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 8.5f, 10), Quaternion.identity);
                //acidPuddle(buildX + building.transform.localScale.x / 2, building.transform.position.y, building.transform.localScale.y);
                //buildYPos = building.transform.position.y;
                //Debug.Log(buildYPos);
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 8.5f, 10), Quaternion.identity);
                Instantiate(building, new Vector3(buildX + buildDis + building.transform.localScale.x / 2, 8.5f, 10), Quaternion.identity);
                SpawnPlatform(building.transform.localScale, buildX + building.transform.localScale.x / 2 + 1, 8.5f);
                buildX += building.transform.localScale.x + 1;
                buildTime = 5f;
            }

            void singleBuilding()
            {
                building.transform.localScale = new Vector3(40, 30, 10);
                //buildHeight = building.transform.localScale.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 11, 10), Quaternion.identity);
                //acidPuddle(buildX + building.transform.localScale.x / 2, building.transform.position.y, building.transform.localScale.y);
                //buildYPos = building.transform.position.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 11, 10), Quaternion.identity);
                Instantiate(building, new Vector3(buildX + buildDis + building.transform.localScale.x / 2, 11, 10), Quaternion.identity);
                SpawnPlatform(building.transform.localScale, buildX + building.transform.localScale.x / 2 + 1, 11);
                buildX += building.transform.localScale.x + 1;
                buildTime = 4f;
            }

            void doubleBuilding()
            {
                building.transform.localScale = new Vector3(50, 25, 10);
                //buildHeight = building.transform.localScale.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2, 8.5f, 10), Quaternion.identity);
                //acidPuddle(buildX + building.transform.localScale.x / 2, building.transform.position.y, building.transform.localScale.y);
                //buildYPos = building.transform.position.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 8.5f, 10), Quaternion.identity);
                Instantiate(building, new Vector3(buildX + buildDis + building.transform.localScale.x / 2, 8.5f, 10), Quaternion.identity);
                SpawnPlatform(building.transform.localScale, buildX + building.transform.localScale.x / 2 + 1, 8.5f);
                buildX += building.transform.localScale.x + 1;

                building.transform.localScale = new Vector3(39, 30, 10);
                //buildHeight = building.transform.localScale.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 11, 10), Quaternion.identity);
                //acidPuddle(buildX + building.transform.localScale.x / 2, building.transform.position.y, building.transform.localScale.y);
                //buildYPos = building.transform.position.y;
                Instantiate(building, new Vector3(buildX + building.transform.localScale.x / 2 + buildDis, 11, 10), Quaternion.identity);
                Instantiate(building, new Vector3(buildX + buildDis + building.transform.localScale.x / 2, 11, 10), Quaternion.identity);
                SpawnPlatform(building.transform.localScale, buildX + building.transform.localScale.x / 2 + 1, 11);
                buildX += building.transform.localScale.x + 1;
                buildTime = 4f;
            }

            void SpawnPlatform(Vector3 buildScale, float xPos, float yPos)
            {
                float scaleX = buildScale.x / 2;
                float scaleY = buildScale.y / 2;
                platform.transform.localScale = new Vector3(scaleX - scaleX / 4, 0.2f, 0);
                Instantiate(platform, new Vector2(xPos - (scaleX / 2), RandomElevation(yPos + scaleY)), Quaternion.identity);
                Instantiate(platform, new Vector2(xPos + (scaleX / 2), RandomElevation(yPos + scaleY)), Quaternion.identity);
            }


            // float RandomHorizontal(float x1, float x2) {
            // float pos = UnityEngine.Random.Range(x1, x2);
            // return pos;
            //   }


            float RandomElevation(float buildScaleY)
            {
                int chance = (int)(UnityEngine.Random.Range(0, 2));
                float change;
                if (chance == 0)
                {
                    change = UnityEngine.Random.Range(1f, 2f);
                    elevation += change;
                    while (elevation >= buildScaleY - 1)
                    {
                        elevation -= change / 4;
                    }
                    //Debug.Log(elevation);
                    return elevation;
                }
                else
                {
                    change = UnityEngine.Random.Range(1f, 2f);
                    elevation -= change;
                    while (elevation <= floor.transform.position.y + 1)
                    {
                        elevation += change / 4;
                    }
                    //Debug.Log(elevation);
                    return elevation;
                }
            }

            void spawnObstacle(float buildingXPos, float buildingYPos, float buildingHeight)
            {
                int ran = UnityEngine.Random.Range(0,5);
                if (ran == 0) {
                    if (!acidQd) {
                        acidPuddleSpawn(buildingXPos, buildingYPos, buildingHeight);
                        spawnNum++;
                        acidQd = true;
                    }
                } else if (ran == 1) {
                    if (!bountyQd) {
                        billboardSpawn(buildingXPos, buildingYPos, buildingHeight);
                        spawnNum++;
                        bountyQd = true;
                    }
                } else if (ran == 2) {
                    if (!teleQd) {
                        telephonePoleSpawn(buildingXPos, buildingYPos, buildingHeight);
                        spawnNum++;
                        teleQd = true;
                    }
                } else if (ran == 3) {
                    if (!barbedQd) {
                        barbedFenceSpawn(buildingXPos, buildingYPos, buildingHeight);
                        spawnNum++;
                        barbedQd = true;
                    }
                } else if (ran == 4) {
                    if (!generatorQd) {
                        powerBoxSpawn(buildingXPos, buildingYPos, buildingHeight);
                        spawnNum++;
                        generatorQd = true;
                    }
                } 
            }

   

                    void acidPuddleSpawn(float buildXPos, float buildYPos, float buildHeight)
                    {
                        //float x = buildX + buildXPos;
                        Instantiate(puddle, new Vector2(buildXPos, buildYPos + buildHeight/2 + 1.1f), Quaternion.identity);
                        Instantiate(pipe, new Vector3(buildXPos + 1.15f, buildYPos + buildHeight/2 + 1.3f, 30), Quaternion.identity);
                    }

                    void billboardSpawn(float buildXPos, float buildYPos, float buildHeight)
                    {
                        //bountySign.transform.localScale = new Vector3(0.5f, bountySign.transform.localScale.y, 20);
                        Instantiate(bountySign, new Vector3(buildXPos, buildYPos + buildHeight/2 + 3f, 20), Quaternion.identity);
                    }

                    void laserShoot() {
                        Instantiate(laserWarn, new Vector3(player1.transform.position.x + 7.5f, player1.transform.position.y, 1), Quaternion.identity);
                    }

                    void telephonePoleSpawn(float buildXPos, float buildYPos, float buildHeight) {
                        Instantiate(telePole, new Vector3(buildXPos, buildYPos + buildHeight / 2 + telePole.transform.localScale.y/2 + 0.75f, 20), Quaternion.identity);
                    }

                    void birdSpawn(float buildXPos, float buildYPos, float buildHeight) {
                        int dodgeMethod = (int) (UnityEngine.Random.Range(0,2));
                        if (dodgeMethod == 0) {
                            Instantiate(birdie, new Vector3(buildXPos + 7.5f, buildYPos + buildHeight/2 + 1.75f, 20), Quaternion.identity);
                        } else if (dodgeMethod == 1) {
                            Instantiate(birdie, new Vector3(buildXPos + 7.5f, buildYPos + buildHeight/2 + 2.5f, 20), Quaternion.identity);
                        }
                    }

                    void cyberTruckSpawn() {
                        int flipChance = (int) (UnityEngine.Random.Range(0,2));
                        if (flipChance == 0) {
                            cyberTruck.transform.localScale = facingLeft;
                            truckMovement.setLeft(true);
                            truckMovement.setRight(false);
                            Instantiate(cyberTruck, new Vector3(player1.transform.position.x + 15f, player1.transform.position.y + 3f, 30), Quaternion.identity);
                        } else if (flipChance == 1) {
                            cyberTruck.transform.localScale = facingRight;
                            truckMovement.setRight(true);
                            truckMovement.setLeft(false);
                            Instantiate(cyberTruck, new Vector3(player1.transform.position.x - 15f, player1.transform.position.y + 3f, 30), Quaternion.identity);
                        }
                    }

                    void barbedFenceSpawn(float buildXPos, float buildYPos, float buildHeight) {
                        Instantiate(barbedFence, new Vector3(buildXPos, buildYPos + buildHeight/2 + 1.375f, 20), Quaternion.identity);
                    }

                    void powerBoxSpawn(float buildXPos, float buildYPos, float buildHeight) {
                        Instantiate(powerGenerator, new Vector3(buildXPos, buildYPos + buildHeight/2 + 0.65f, 20), Quaternion.identity);
                    }
                }
         

    

    //adds 3 floors to an arraylist creates a new floor infront of the newest created floor whenever the floor count drops below 3

   /* void floorSpawn() {
        removeFloor();
        if (floors.Count == 0)
        {
            floors.Add(floor);
        }
        else if (floors.Count < 3) {
            
            floors.Add(Instantiate(floor, new Vector3(floors[floors.Count-1].transform.position.x + 30, floors[floors.Count - 1].transform.position.y, floors[floors.Count - 1].transform.position.z), Quaternion.identity));
        }
    }
    //removes floor from list so floor count drops
    void removeFloor() {
        for (int i = 0; i < floors.Count; i++) {
            if (Math.Abs(floors[floors.Count - 1].transform.position.x - player1.transform.position.x) > 40) {
                floors.RemoveAt(floors.Count - 1);
            }
        }
    }*/ 
//}

