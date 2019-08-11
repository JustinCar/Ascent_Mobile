using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupManager : MonoBehaviour {

	public List<GameObject> itemsInRange;
	public GameObject closestItem;
	float closestDistance;
	GameObject player;

	public GameObject pickupButton;

	// Use this for initialization
	void Start () {
		itemsInRange = new List<GameObject>();
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if (itemsInRange.Count > 0) 
		{
			pickupButton.SetActive(true);
		} else 
		{
			pickupButton.SetActive(false);
		}

		if (itemsInRange.Count == 1) 
		{
			if (itemsInRange[0] == null) 
			{
				itemsInRange.RemoveAt(0);
			} else 
			{
				closestItem = itemsInRange[0];
				closestDistance = Vector3.Distance(itemsInRange[0].transform.position, player.transform.position);				
			}
		}
		if (itemsInRange.Count >= 2) 
		{
			foreach (GameObject item in itemsInRange) 
			{
				if (item == null) 
				{
					itemsInRange.Remove(item);
				} else 
				{
					if (Vector3.Distance(item.transform.position, player.transform.position) < closestDistance) 
					{
						closestItem = item;
						closestDistance = Vector3.Distance(item.transform.position, player.transform.position);
					}				
				}
			}
		}
	}

	public void pickupClosestItem() 
	{
		if (closestItem.GetComponent<ItemPickup>()) 
		{
			closestItem.GetComponent<ItemPickup>().showAbilityOptions = true;
		} else 
		{
			closestItem.GetComponent<UseItem>().consume();
		}
		
	}
}
