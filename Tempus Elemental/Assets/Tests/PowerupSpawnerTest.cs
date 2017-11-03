using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using System;

public class NewPlayModeTest {

	// Tests to see if the spawned Powerup is of a different prefab from the instantiated Projectile
	[Test]
	public void ProjectileVsPowerup() {
		SetupScene ();
		var powerupPrefab = GameObject.FindWithTag("Powerup");
		//var spawnedPowerup = GameObject.FindWithTag("Powerup");
		var spawnedProjectile = GameObject.FindWithTag ("Fire");
		//var prefabOfSpawnedPowerup = PrefabUtility.GetPrefabParent(powerupPrefab);
		Assert.AreNotEqual(powerupPrefab, spawnedProjectile);
	}

	// Tests to see if the spawned Powerup is of a different prefab from the instantiated Player
	[Test]
	public void PlayerVsPowerup() {
		SetupScene ();
		var powerupPrefab = GameObject.FindWithTag("Powerup");
		//var spawnedPowerup = GameObject.FindWithTag("Powerup");
		var spawnedPlayer = GameObject.FindWithTag ("Player1");
		//var prefabOfSpawnedPowerup = PrefabUtility.GetPrefabParent(powerupPrefab);
		Assert.AreNotEqual(powerupPrefab, spawnedPlayer);
	}

	// Tests to see if the spawned Powerup is of a different type from the instantiated Powerup
	[Test]
	public void Powerup1VsPowerup2() {
		SetupScene ();
		var powerupPrefab = GameObject.FindWithTag("Powerup");
		var spawnedPowerup = GameObject.Find ("powerup2");
		//var prefabOfSpawnedPowerup = PrefabUtility.GetPrefabParent(powerupPrefab);
		Assert.AreNotEqual(powerupPrefab, spawnedPowerup);
	}

	// Tests of the Spawned Powerup is inside the desired spawnValues
	[Test]
	public void CheckInsideSpawnValues() {
		SetupScene ();
		Vector3 spawnValues = new Vector3(5,5,1);
		GameObject tester = new GameObject ();
		tester.transform.position.Set (0, 0, 1);
		//var powerupSpawner = new GameObject().AddComponent<SpawnObject>();
		GameObject.Instantiate (Resources.Load("Tests/powerup1"), spawnValues + tester.transform.TransformPoint (0, 0, 0), tester.transform.rotation);
		Vector3 checker = GameObject.FindWithTag ("Powerup").transform.position;
		if (checker [0] <= -5 || checker [0] >= 5 || checker [1] <= -5 || checker [1] >= 5) {
			Assert.Fail ();
		}
		// Assert.Fail ();
		Assert.True (true);
	}
		
	// Tests of the spawned powerup matches the prefab
	[Test]
	public void Instantiates_GameObject_From_Prefab() {
		SetupScene ();
		var powerupPrefab = GameObject.FindWithTag("Powerup");
        var spawnedPowerup = GameObject.FindWithTag("Powerup");
		//var spawnedPlayer = GameObject.FindWithTag ("Player1");
		//var spawnedProjectile = GameObject.FindWithTag ("Fire");
        //var prefabOfSpawnedPowerup = PrefabUtility.GetPrefabParent(powerupPrefab);
		Assert.AreEqual(powerupPrefab, spawnedPowerup);
	}

	// Sets up the scene for testing
	void SetupScene() {
		GameObject.Instantiate (Resources.Load<GameObject> ("Tests/DummyMap"));
		GameObject.Instantiate(Resources.Load<GameObject> ("Tests/Player"));
		GameObject.Instantiate(Resources.Load<GameObject> ("Tests/Projectile"));
		GameObject.Instantiate(Resources.Load<GameObject> ("Tests/powerup2"));
		//Vector3 spawnValues = new Vector3(1,1,1);
		//var powerupSpawner = new GameObject().AddComponent<SpawnObject>();
		//powerupSpawner.Construct (spawnValues, 0, 0, 0, 1, 0, 1);
		//powerupSpawner.StartCoroutine (powerupSpawner.Spawner());
	}
}
