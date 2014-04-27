using UnityEngine;
using System.Collections;

public class SetSortingOrder : MonoBehaviour {

	
	
	public string sortingLayerName;       // The name of the sorting layer .
	
	//public int sortingOrder;      //The sorting order
	
	
	
	void Start ()
		
	{
		
		// Set the sorting layer and order.
		
		renderer.sortingLayerName = sortingLayerName;
		
		//renderer.sortingOrder=sortingOrder;
		
	}
}
