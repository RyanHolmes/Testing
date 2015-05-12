using UnityEngine;
using System.Collections;
using System;

public class point3D :IComparable<point3D>{
	public int pointx;
	public int pointy;
	public int pointz;
	private GameObject _block;
	public point3D (int x, int y, int z){
		pointx = x;
		pointy = y;
		pointz = z;
	}

	public void delete(){
		GameObject.Destroy (_block);
	}

	public override string ToString(){
		return (pointx.ToString() + "," + pointy.ToString() + "," + pointz.ToString());
	}
	public int CompareTo(point3D other)
	{
		if(other == null)
		{
			return 1;
		}
		if (other.pointx != this.pointx) {
			return other.pointx - this.pointx;
		} else if(other.pointy != this.pointy){
			return other.pointy - this.pointy;
		} else if(other.pointz != this.pointz){
			return other.pointz - this.pointz;
		}
		return 0;
	}
}
