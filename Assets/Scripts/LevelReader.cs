using UnityEngine;
using System.Collections;
using System.Xml;
using System.Linq;

public class LevelReader : MonoBehaviour {

	public EnemySpawner[] lanes = {null,null,null};
	BeatManager beatz;
	XmlDocument chart;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		chart = new XmlDocument();
		chart.Load ("Assets\\Files\\cephalopod.xml");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (beatz.beat) {
			//Stupid case to line up starting beat with start of song
			int beatnum = beatz.beatTracker;
			if(beatz.countingOff) {
				beatnum -= Enemy.beatsToDeath;
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
		}
	}
}
