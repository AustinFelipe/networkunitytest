using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Transform playerPrefab;

	private string ipToConnect = "";
	private int portToConnect = 5000;

	private GUISkin skin;
	private GameManager gm;

	void Start()
	{
		ipToConnect = "192.168.0.22";
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		skin = gm.mainSkin; 
	}

	private void StartServer()
	{
		Network.InitializeServer(2, portToConnect, !Network.HavePublicAddress());
	}

	void OnGUI()
	{
		GUI.skin = skin;

		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 200, 50), "Iniciar Server"))
				StartServer();

			ipToConnect = GUI.TextField(new Rect(100, 200, 200, 20), ipToConnect);

			if (GUI.Button(new Rect(100, 220, 200, 50), "Conectar Server"))
				JoinServer();
		}
	}
	
	private void JoinServer()
	{
		Network.Connect(ipToConnect, portToConnect);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
		SpawnPlayer();
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		SpawnPlayer();
	}
	
	private void SpawnPlayer()
	{
		Vector3 initialPosition = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(20, 0, 0)).x, 0);

		if (!Network.isServer)
		{
			initialPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 20, 0, 0)).x;
		}

		Network.Instantiate(playerPrefab, initialPosition, transform.rotation, 0);
	}
}
