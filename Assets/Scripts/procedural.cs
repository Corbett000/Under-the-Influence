using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class procedural : MonoBehaviour {
    public Tilemap tilemap;
    public Tilemap tileset;
    public GameObject player;
    public Camera camera;

    // private Random Random = new Random();

    private int ybound;
    private int lb_bound;
    private int rb_bound;

    private int l_road_bound;
    private int m_road_bound;
    private int r_road_bound;

    private bool lb_last_building = false;
    private bool rb_last_building = false;
    private bool lb_last_intersection = false;
    private bool rb_last_intersection = false;

    private int leftside = -10;
    private int leftroad;
    private int buildings1;
    private int middleroad;
    private int buildings2;
    private int rightroad;
    private int rightside;
    // private int 
    void placeIntersectionAt(int x,int y,bool leftclose,bool rightclose) {
        for (int xp = 0; xp <= 5; xp++) {
            for (int yp = 0; yp >= -5; yp--) {
                var dump = ((leftclose&&(xp==0||xp==1))||(rightclose&&(xp==4||xp==5)))&&((yp!=0 && yp!=-5)||(xp==0||xp==5));
                tilemap.SetTile(new Vector3Int(xp+x, yp+y, 0),tileset.GetTile(new Vector3Int(xp-5, dump?6:yp+5, 0)));
            }
        }
    }
    void placeBuildingAt(int x,int y,int building) {
        for (int xp = 0; xp < 5; xp++) {
            for (int yp = 0; yp > -5; yp--) {
                tilemap.SetTile(new Vector3Int(xp+x, yp+y, 0),tileset.GetTile(new Vector3Int(xp+2+building*5, yp+5, 0)));
            }
        }
    }
    void placeStationAt(int x,int y,int station) {
        for (int xp = 0; xp < 5; xp++) {
            for (int yp = 0; yp > -3; yp--) {
                tilemap.SetTile(new Vector3Int(xp+x, yp+y, 0),tileset.GetTile(new Vector3Int(xp+6, yp-1-station*3, 0)));
            }
        }
    }
    void placeAlleyAt(int x,int y) {
        for (int xp = 0; xp < 5; xp++) {
            tilemap.SetTile(new Vector3Int(xp+x,y,0),tileset.GetTile(new Vector3Int(1,0,0)));
        }
    }
    void placeGrassAt(int x,int y,int midlayers) {
        for (int xp = 0; xp < 5; xp++) {
            tilemap.SetTile(new Vector3Int(xp+x,y,0),tileset.GetTile(new Vector3Int(xp+1,-1,0)));
        }
        for (int xp = 0; xp < 5; xp++) {
            for (int yp = 0; yp > -midlayers; yp--) {
                tilemap.SetTile(new Vector3Int(xp+x,yp-1+y,0),tileset.GetTile(new Vector3Int(xp+1,-2,0)));
            }
        }
        for (int xp = 0; xp < 5; xp++) {
            tilemap.SetTile(new Vector3Int(xp+x,-midlayers-1+y,0),tileset.GetTile(new Vector3Int(xp+1,-3,0)));
        }
    }
    void placeNorthRoadAt(int x,int y,bool inters) {
        for (int xp = 0; xp <= 5; xp++) {
            tilemap.SetTile(new Vector3Int(xp+x, y, 0),tileset.GetTile(new Vector3Int(xp-5, inters?-1:-2, 0)));
        }
    }
    void placeEastRoadAt(int x,int y) {
        for (int xp = 0; xp < 5; xp++) {
            for (int yp = 0; yp >= -5; yp--) {
                tilemap.SetTile(new Vector3Int(x+xp, yp+y, 0),tileset.GetTile(new Vector3Int(1, yp+5, 0)));
            }
        }
    }
    void generateRoadUnits() {
        var camext = new Vector2(camera.orthographicSize * Screen.width/Screen.height, camera.orthographicSize);
        var cammax = tilemap.WorldToCell((Vector2)camera.transform.position + camext);
        while (ybound<=cammax.y+3) {
            if (lb_bound<=ybound && rb_bound<=ybound && l_road_bound==ybound && m_road_bound==ybound && r_road_bound==ybound && Random.Range(0, 2)==0 && !lb_last_intersection && !rb_last_intersection) {
                l_road_bound+=6;
                m_road_bound+=6;
                r_road_bound+=6;
                placeIntersectionAt(leftroad,l_road_bound,false,false);
                placeIntersectionAt(middleroad,m_road_bound,false,false);
                placeIntersectionAt(rightroad,r_road_bound,false,false);
                lb_bound+=6;
                rb_bound+=6;
                placeEastRoadAt(buildings1,lb_bound);
                placeEastRoadAt(buildings2,rb_bound);
                lb_last_building=false;
                rb_last_building=false;
                lb_last_intersection=true;
                rb_last_intersection=true;
            }
            if (lb_bound<=ybound) {
                while (true) {
                    var sentry = Random.Range(0,lb_last_building?4:3);
                    if (sentry==0) {
                        if (lb_last_building) {
                            lb_bound+=1;
                            placeAlleyAt(buildings1,lb_bound);
                        }
                        lb_bound+=5;
                        placeBuildingAt(buildings1,lb_bound,Random.Range(0, 3));
                        lb_last_building=true;
                        lb_last_intersection=false;
                    } else if (sentry==1) {
                        if (lb_last_building) {
                            lb_bound+=1;
                            placeAlleyAt(buildings1,lb_bound);
                        }
                        lb_bound+=3;
                        placeStationAt(buildings1,lb_bound,Random.Range(0, 3));
                        lb_last_building=true;
                        lb_last_intersection=false;
                    } else if (sentry==2) {
                        if (l_road_bound!=ybound || m_road_bound!=ybound || lb_last_intersection) continue;
                        l_road_bound+=6;
                        m_road_bound+=6;
                        placeIntersectionAt(leftroad,l_road_bound,false,false);
                        placeIntersectionAt(middleroad,m_road_bound,false,true);
                        lb_bound+=6;
                        placeEastRoadAt(buildings1,lb_bound);
                        lb_last_building=false;
                        lb_last_intersection=true;
                    } else if (sentry==3) {
                        var grasswidth = Random.Range(0, 3);
                        lb_bound+=2+grasswidth;
                        placeGrassAt(buildings1,lb_bound,grasswidth);
                        lb_last_building=false;
                        lb_last_intersection=false;
                    }
                    break;
                }
            }
            if (rb_bound<=ybound) {
                while (true) {
                    var sentry = Random.Range(0,rb_last_building?4:3);
                    if (sentry==0) {
                        if (rb_last_building) {
                            rb_bound+=1;
                            placeAlleyAt(buildings2,rb_bound);
                        }
                        rb_bound+=5;
                        placeBuildingAt(buildings2,rb_bound,Random.Range(0, 3));
                        rb_last_building=true;
                        rb_last_intersection=false;
                    } else if (sentry==1) {
                        if (rb_last_building) {
                            rb_bound+=1;
                            placeAlleyAt(buildings2,rb_bound);
                        }
                        rb_bound+=3;
                        placeStationAt(buildings2,rb_bound,Random.Range(0, 3));
                        rb_last_building=true;
                        rb_last_intersection=false;
                    } else if (sentry==2) {
                        if (r_road_bound!=ybound || m_road_bound!=ybound || rb_last_intersection) continue;
                        m_road_bound+=6;
                        r_road_bound+=6;
                        placeIntersectionAt(middleroad,m_road_bound,true,false);
                        placeIntersectionAt(rightroad,r_road_bound,false,false);
                        rb_bound+=6;
                        placeEastRoadAt(buildings2,rb_bound);
                        rb_last_building=false;
                        rb_last_intersection=true;
                    } else if (sentry==3) {
                        var grasswidth = Random.Range(0, 3);
                        rb_bound+=2+grasswidth;
                        placeGrassAt(buildings2,rb_bound,grasswidth);
                        rb_last_building=false;
                        rb_last_intersection=false;
                    }
                    break;
                }
            }
            while (l_road_bound<=ybound) {
                l_road_bound++;
                placeNorthRoadAt(leftroad,l_road_bound,false);
            }
            while (m_road_bound<=ybound) {
                m_road_bound++;
                placeNorthRoadAt(middleroad,m_road_bound,false);
            }
            while (r_road_bound<=ybound) {
                r_road_bound++;
                placeNorthRoadAt(rightroad,r_road_bound,false);
            }
            ybound+=1;

            // placeIntersectionAt(-5,5);
            // placeIntersectionAt(-5,5);
            // placeIntersectionAt(-5,5);
            // placeNorthRoadAt(-5,-1,true);
            // placeNorthRoadAt(-5,-2,false);
            // placeEastRoadAt(1,5);
        }

    }
    void Start() {
        leftroad = leftside+1;
        buildings1 = leftroad+6;
        middleroad = buildings1+5;
        buildings2 = middleroad+6;
        rightroad = buildings2+5;
        rightside = rightroad+1;
        var camext = new Vector2(camera.orthographicSize * Screen.width/Screen.height, camera.orthographicSize);
        var cammin = tilemap.WorldToCell((Vector2)camera.transform.position - camext);
        ybound = cammin.y;
        lb_bound = cammin.y;
        rb_bound = cammin.y;

        l_road_bound = cammin.y;
        m_road_bound = cammin.y;
        r_road_bound = cammin.y;

        generateRoadUnits();
    }
    
    void Update() {
        generateRoadUnits();
    }
}

