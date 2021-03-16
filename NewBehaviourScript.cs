using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    Texture2D[] tiles;
    int[,] map;
    bool[,] adj;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new Texture2D[16];
        adj = new bool[16,4] {
            {false,false,false,false},
            {true,false,false,false},
            {false,true,false,false},
            {true,true,false,false},
            {false,false,true,false},
            {true,false,true,false},
            {false,true,true,false},
            {true,true,true,false},
            {true,false,false,false},
            {true,false,false,true},
            {false,true,false,true},
            {true,true,false,true},
            {false,false,true,true},
            {true,false,true,true},
            {false,true,true,true},
            {true,true,true,true}};

        for(int i = 0;i<tiles.Length;i++){
            tiles[i] = (Texture2D)Resources.Load("Tiles/Tile_"+i);
        }
        int w = 50;
        int h = 50;
        map = new int[w,h];
        for(int i = 0;i<w;i++){
            for(int j = 0;j<h;j++){
                //Debug.Log("Se va a colocar el punto: " + i+", "+j);
                GenerateTile(i,j);
            }
        }
    }

    void GenerateTile(int x, int z){
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
        int tileUsed = SelectTile(x,z);
        go.GetComponent<Renderer>().material.SetTexture("_MainTex", tiles[tileUsed]);
        go.transform.Rotate(0,180,0);
        go.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        go.transform.position = new Vector3(x,0,z);
        map[x,z] = tileUsed;
    }

    int SelectTile(int x, int z){
        //Debug.Log("Coordenadas: x=" + x+", z="+z);
        int tile = 0;
        if(x==0&&z==0){
            tile = (int)Random.Range(1,15);
            //Debug.Log("En (0,0) locas!!!");
        }else{
            //Debug.Log("No en (0,0) :'v");
            List<int> pos = new List<int>();
            bool below;
            bool left;
            if(x==0){
                below = adj[map[x,z-1],0];
                for(int i = 1;i<tiles.Length;i++){
                    bool temp = adj[i,2];
                    if((below&&temp)||!(below||temp)){
                        pos.Add(i);
                    }
                }
                int selec =((int)Random.Range(0,pos.Count-1)); 
                tile = pos[selec];
            }else if(z==0){
                left = adj[map[x-1,z],1];
                for(int i = 1;i<tiles.Length;i++){
                    bool temp = adj[i,3];
                    if((left&&temp)||!(left||temp)){
                        pos.Add(i);
                    }
                }
                int selec =((int)Random.Range(0,pos.Count-1)); 
                tile = pos[selec];
            }
            else{
                below = adj[map[x,z-1],0];
                left = adj[map[x-1,z],1];
                for(int i = 1;i<tiles.Length;i++){
                    bool temp1 = adj[i,2];
                    bool temp2 = adj[i,3];
                    if(((below&&temp1)||!(below||temp1))&&((left&&temp2)||!(left||temp2))){
                     pos.Add(i);
                    }
                }
                int selec =((int)Random.Range(0,pos.Count-1)); 
                tile = pos[selec];
            }
        }
        return tile;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
