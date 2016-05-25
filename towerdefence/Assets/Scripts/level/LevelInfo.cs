using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class LevelInfo {
    public List<GameObjectPosition> gameObjectPostions { get; set; }
    public int cracks = 2;

    public LevelInfo()
    {
        gameObjectPostions = new List<GameObjectPosition>();
    }

    public LevelInfo(JSONNode levelJson) 
    {
        gameObjectPostions = new List<GameObjectPosition>();
        cracks = levelJson["cracks"].AsInt;
        JSONArray positions = levelJson["ObjectPosition"].AsArray;

        for (int i = 0; i < positions.Count; i++)
        {
            GameObjectPosition gop = new GameObjectPosition();
            gop.name = positions[i]["name"].Value;
            gop.xPos = positions[i]["xPos"].AsFloat;
            gop.yPos = positions[i]["yPos"].AsFloat;
            gop.xRotation = positions[i]["xRotation"].AsFloat;
            gop.yRotation = positions[i]["yRotation"].AsFloat;
            this.gameObjectPostions.Add(gop);
        }
    }

    public class GameObjectPosition
    {
        public float xPos { get; set; }
        public float yPos { get; set; }
        public float xRotation { get; set; }
        public float yRotation { get; set; }
        public string name { get; set; }
    }
}
