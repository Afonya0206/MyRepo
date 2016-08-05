using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour {

	private Camera _camera;
	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void OnGUI()
	{
		int size = 12;
		float posX = _camera.pixelWidth / 2 - size/4;
		float posY = _camera.pixelHeight / 2 - size/2;
		GUI.Label (new Rect(posX,posY,size,size),"*");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
			Ray ray = _camera.ScreenPointToRay (point); //Создание в этой точке point луча методом ScreenPointToRay()

			RaycastHit hit; //переменная для хранения информации о луче
			if (Physics.Raycast (ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget> ();
				if (target != null)
					target.ReactToHit ();
				else
					StartCoroutine(SphereIndicator(hit.point));//запуск сопрограммы в ответ на попадание
			}
		}
	}

	private IEnumerator SphereIndicator(Vector3 pos)//сопрограммы пользуются функциями IEnumerator
	{
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = pos;
		yield return new WaitForSeconds (1);
		Destroy (sphere);
	}
}
