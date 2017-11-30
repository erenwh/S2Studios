using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

	//constants
	private const float FULLBARSCALE = 690.0f;					//how big does one bar have to be at x = 0 to fill the whole bar
	private const float FARTHESTLEFT = -6.9f;					//what is the farthest left that each bar starts at

	//variables
	[HideInInspector] public int maxTime = 120;					//how much time is needed to collect until the bar is filled

	//references
	public GameObject[] bars;									//the 4 colored bars that indicate how much time each player has collected

	//show to the player how much time each player has collected
	public void UpdateBars (int time1, int time2, int time3, int time4) {
		bars [0].transform.localScale = new Vector3(((float)time1 / (float)maxTime) * FULLBARSCALE, bars [0].transform.localScale.y, 1f);
		bars [0].transform.localPosition = new Vector3(FARTHESTLEFT + ((bars [0].transform.localScale.x / FULLBARSCALE) * -FARTHESTLEFT), bars[0].transform.localPosition.y, bars[0].transform.localPosition.z);

		bars [1].transform.localScale = new Vector3(((float)time2 / (float)maxTime) * FULLBARSCALE, bars [1].transform.localScale.y, 1f);
		bars [1].transform.localPosition = new Vector3(bars [0].transform.localPosition.x + ((bars [0].transform.localScale.x / FULLBARSCALE) * -FARTHESTLEFT) + ((bars [1].transform.localScale.x / FULLBARSCALE) * -FARTHESTLEFT), bars[1].transform.localPosition.y, bars[1].transform.localPosition.z);

		bars [2].transform.localScale = new Vector3(((float)time3 / (float)maxTime) * FULLBARSCALE, bars [2].transform.localScale.y, 1f);
		bars [2].transform.localPosition = new Vector3(bars [1].transform.localPosition.x + ((bars [1].transform.localScale.x / FULLBARSCALE) * -FARTHESTLEFT) + ((bars [2].transform.localScale.x / FULLBARSCALE) * -FARTHESTLEFT), bars[2].transform.localPosition.y, bars[2].transform.localPosition.z);

		bars [3].transform.localScale = new Vector3(((float)time4 / (float)maxTime) * FULLBARSCALE, bars [3].transform.localScale.y, 1f);
		bars [3].transform.localPosition = new Vector3(bars [2].transform.localPosition.x + ((bars [2].transform.localScale.x / FULLBARSCALE) * -FARTHESTLEFT) + ((bars [3].transform.localScale.x / FULLBARSCALE) * -FARTHESTLEFT), bars[3].transform.localPosition.y, bars[3].transform.localPosition.z);
	}
}
