using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class NewPlayModeTest {

	[Test]
	public void NewPlayModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator _Instantiates_GameObject_From_Prefab() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        var powerupSpawner = new GameObject().AddComponent<SpawnObject>();
        var powerupPrefab = GameObject.FindWithTag("Powerup");
        var prefabOfSpawnedPowerup = PrefabUtility.GetPrefabParent(powerupPrefab);
        Assert.AreEqual(powerupPrefab, prefabOfSpawnedPowerup);
		yield return null;
	}
}
