using UnityEngine;
using System.Collections;
using System.Xml;
using System.Linq;

//Reads levels and sends messages to spawners to spawn level
public class LevelReader : MonoBehaviour {

	public EnemySpawner[] lanes = {null,null,null};
	public WormholeSpawner whSpawn = null;
	BeatManager beatz;
	XmlDocument chart;
	int shiftTime = 32;
	Vector3 whCamPosition = new Vector3(-11, 4, 0);
	Vector3 whCamRotation = new Vector3(20, 90, 0);
	Vector3 cruiseCamPosition = new Vector3(0, 0, -10);
	Vector3 cruiseCamRotation = new Vector3(0, 0, 0);

	public enum LevelState {CRUISING, WORMHOLE, ENTERING, EXITING};
	public LevelState state;

	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		chart = new XmlDocument();
		chart.Load ("Assets\\Files\\cephalopod.xml");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (state) {
		case LevelState.CRUISING:
			if (beatz.beat) {
				//Stupid case to line up starting beat with start of song
				int beatnum = beatz.beatTracker;
				if(beatz.countingOff) {
					beatnum -= (beatz.countOff - Enemy.beatsToDeath);
				} else {
					beatnum += Enemy.beatsToDeath;
				}

				XmlNode spawnNode = chart.SelectSingleNode("chart/easy/beat[@num='"+beatnum+"']/top");
				if (spawnNode != null) {
					lanes[0].Spawn(spawnNode.InnerText);
				}

				spawnNode = chart.SelectSingleNode("chart/easy/beat[@num='"+beatnum+"']/mid");
				if (spawnNode != null) {
					lanes[1].Spawn(spawnNode.InnerText);
				}

				spawnNode = chart.SelectSingleNode("chart/easy/beat[@num='"+beatnum+"']/bot");
				if (spawnNode != null) {
					lanes[2].Spawn(spawnNode.InnerText);
				}

				spawnNode = chart.SelectSingleNode("chart/easy/beat[@num='"+beatnum+"']/wormhole");
				if (spawnNode != null) {
					if (spawnNode.InnerText == "entering") {
						state = LevelState.ENTERING;
					}
				}
			}
			break;

		case LevelState.ENTERING:
			if (transform.position != whCamPosition) {
				WormholeCamera();
			}

			if (beatz.beat) {
				//Stupid case to line up starting beat with start of song
				int beatnum = beatz.beatTracker;
				if(beatz.countingOff) {
					beatnum -= (beatz.countOff - Enemy.beatsToDeath);
				} else {
					beatnum += Enemy.beatsToDeath;
				}

                XmlNode spawnNode = chart.SelectSingleNode("chart/easy/beat[@num='"+beatnum+"']/wormhole");
                if (spawnNode != null) {
                    if (spawnNode.InnerText == "start") {
                        state = LevelState.WORMHOLE;
                    }
                }
			}
			break;

		case LevelState.WORMHOLE:
			if (beatz.beat) {
				//Stupid case to line up starting beat with start of song
				int beatnum = beatz.beatTracker;
				if(beatz.countingOff) {
					beatnum -= (beatz.countOff - Enemy.beatsToDeath);
				} else {
					beatnum += Enemy.beatsToDeath;
				}
								
				XmlNode spawnNode = chart.SelectSingleNode("chart/easy/beat[@num='"+beatnum+"']/wormhole");
				if (spawnNode != null) {
					if (spawnNode.InnerText == "leaving") {
						state = LevelState.EXITING;
					} else if (spawnNode.InnerText != "warning"){
						whSpawn.Spawn(spawnNode.InnerText);
					}
				}
            }
            break;

		case LevelState.EXITING:
			//if (transform.position != cruiseCamPosition) {
				CruiseCamera();
            //}

			if (beatz.beat) {
				//Stupid case to line up starting beat with start of song
				int beatnum = beatz.beatTracker;
				if(beatz.countingOff) {
					beatnum -= (beatz.countOff - Enemy.beatsToDeath);
				} else {
					beatnum += Enemy.beatsToDeath;
				}
				
				XmlNode spawnNode = chart.SelectSingleNode("chart/easy/beat[@num='"+beatnum+"']/wormhole");
				if (spawnNode != null) {
					if (spawnNode.InnerText == "end") {
                        state = LevelState.CRUISING;
                    }
                }
            }
            break;
        }
    }
    
    void WormholeCamera () {
		float speed = (Vector3.Magnitude(whCamPosition - cruiseCamPosition))/((60/(float)(beatz.tempo))/8 * shiftTime);
		float step = (float)speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, whCamPosition, step);

		Vector3 rot = (whCamRotation - cruiseCamRotation)/((60/(float)(beatz.tempo))/8 * shiftTime) * Time.deltaTime;
		transform.eulerAngles += rot;
    }
	void CruiseCamera () {
		float speed = (Vector3.Magnitude(cruiseCamPosition - whCamPosition))/((60/beatz.tempo)/8 * shiftTime);
		float step = (float)speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, cruiseCamPosition, step);

		Vector3 rot = (cruiseCamRotation - whCamRotation)/((60/(float)(beatz.tempo))/8 * shiftTime) * Time.deltaTime;
		transform.eulerAngles += rot;
	}
}
