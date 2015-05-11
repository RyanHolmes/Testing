using UnityEngine;
using System.Collections;

public class point3D  {
	public int pointx;
	public int pointy;
	public int pointz;

	public point3D (int x, int y, int z){
		pointx = x;
		pointy = y;
		pointz = z;
	}

	public override string ToString(){
		return (pointx.ToString() + "," + pointy.ToString() + "," + pointz.ToString());
	}

}
