# Ignition tracks
Ignition (racing game, 1997) racetrack files format (.COL, .TEX, .MSH, .PLC)

```c
//file .COL
struct Colours {
  int32 file_length;
  int32 unknown;
  uint8 colours[256][3]; //colour #0 = transparent!
}

//file .TEX
struct Textures {
  struct Texture {
    uint8 colour_indexes[256][256]
  }
  Texture textures[16]; //1024bytes total (except JAPAN.TEX)
}

//file .MSH
struct Mesh {
  struct Poly {
    int32 unknown;
    int32 v[3];  //indexes
    int32 uv[3][2];  //uvs for each vertex
    int16 unknown2;  //unknown1/2 - effects?? (19:78 => clouds; 17:303 => rolling balls)
    uint16 texture;  //byte+byte?
  }
  
  int32 vCount;
  int32 pCount;
  int32[vCount][3] vertices;  //x,y,z
  Poly[pCount] polys;
}

//file .PLC
struct Places {
  int32 meshes_count;
  int32 unknown[2];
  int32 position[3];  //x,-y(??),z
}
```

# Disclaimer:
It is just a draft with Unity3D example not a finished project. The goal of this project is to experiment, research, and educate on the topics of game development and game resource management. All information was obtained via reverse engineering of legally purchased copies of the game.
