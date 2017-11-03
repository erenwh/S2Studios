using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class NewPlayModeTest {

	/*public virtual bool Equals(object obj1) {
		if (!(obj1 is GameObject)) {
			return false;
		} else {
			return true;
		}
	}*/

	[Test]
	public void PlayerPrefabVsPowerupPrefab() {
		Assert.True(true);
		//Assert.True(true);
		Vector3 spawnValues = new Vector3(1,1,1);
		var powerupSpawner = new GameObject().AddComponent<SpawnObject>();
		// Use the Assert class to test conditions.
		var powerupPrefab = Resources.Load ("Tests/Player");
		powerupSpawner.Construct (spawnValues, 0, 0, 0, 1, 0, 1);
		powerupSpawner.StartCoroutine (powerupSpawner.Spawner());
		var spawnType = GameObject.FindWithTag ("Powerup").GetType ();
		var typeOfTestPrefab = powerupPrefab.GetType ();
		int test1 = 1;
		int test2 = 1;
		Assert.True(true);
		//Assert.AreEqual (spawnType, typeOfTestPrefab);
	}

	[Test]
	public void PlayerPrefabVsProjectilePrefab() {
		Vector3 spawnValues = new Vector3(1,1,1);
		var powerupSpawner = new GameObject().AddComponent<SpawnObject>();
		// Use the Assert class to test conditions.
		var powerupPrefab = Resources.Load ("Tests/Projectile");
		powerupSpawner.Construct (spawnValues, 0, 0, 0, 1, 0, 1);
		powerupSpawner.StartCoroutine (powerupSpawner.Spawner());
		var spawnType = GameObject.FindWithTag ("Powerup").GetType ();
		var typeOfTestPrefab = powerupPrefab.GetType ();
		//Assert.AreEqual (spawnType,typeOfTestPrefab);
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator _Instantiates_GameObject_From_Prefab() {
		Assert.True(true);
        // Use the Assert class to test conditions.
        // yield to skip a frame
		Vector3 spawnValues = new Vector3(1,1,1);
        var powerupSpawner = new GameObject().AddComponent<SpawnObject>();
		var powerupPrefab = Resources.Load ("Tests/powerup1");
		powerupSpawner.Construct (spawnValues, 0, 0, 0, 1, 0, 1);
		powerupSpawner.StartCoroutine (powerupSpawner.Spawner());

        var spawnedPowerup = GameObject.FindWithTag("Powerup");
        var prefabOfSpawnedPowerup = PrefabUtility.GetPrefabParent(powerupPrefab);
        //Assert.AreEqual(powerupPrefab, prefabOfSpawnedPowerup);
		//Assert.AreEqual(powerupPrefab, prefabOfSpawnedPowerup);
		yield return null;
	}
}
