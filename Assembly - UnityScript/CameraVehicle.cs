using System;
using Boo.Lang.Runtime;
using UnityEngine;

[Serializable]
public class CameraVehicle : MonoBehaviour
{
	public GUILayer guiLayer;

	public Transform arrow;

	public Vector3 lastDir;

	public float lastY;

	public float sensitivityX;

	public float sensitivityY;

	public float rotationX;

	public float rotationY;

	public float heightBoost;

	public float targetHeight;

	public Quaternion gyroTation;

	public Quaternion wr;

	private bool underwater;

	public RaycastHit hit;

	public MotionBlur mb;

	public GlowEffect glowEffect;

	public ColorCorrectionEffect colorEffect;

	public float worldTime;

	public CameraVehicle()
	{
		lastDir = new Vector3(1f, 1f, 1f);
		sensitivityX = 15f;
		sensitivityY = 15f;
		rotationX = 10f;
		rotationY = 10f;
		underwater = false;
		worldTime = 0f;
	}

	public void Start()
	{
		Time.timeScale = 1f;
		float x = 270.01f;
		Quaternion rotation = transform.rotation;
		Vector3 eulerAngles = rotation.eulerAngles;
		float num = (eulerAngles.x = x);
		Vector3 vector = (rotation.eulerAngles = eulerAngles);
		Quaternion quaternion = (transform.rotation = rotation);
	}

	public void LateUpdate()
	{
		if (!Game.Player || !Game.PlayerVeh || !Game.PlayerVeh.ridePos)
		{
			return;
		}
		if (mb.enabled)
		{
			mb.blurAmount -= Time.deltaTime * 0.13f;
			if (mb.blurAmount < 0f)
			{
				mb.enabled = false;
			}
		}
		if (Game.Settings.useHypersound == 1)
		{
			Game.Settings.gameMusic.pitch = Mathf.Clamp(0.5f * -1f + Game.Player.rigidbody.velocity.magnitude / 15f, 0.8f, 1.5f);
		}
		if (Game.Settings.camMode == 0 || Input.GetButton("Fire2") || (Input.GetButton("Snipe") && !Game.Messaging.chatting))
		{
			Screen.lockCursor = true;
		}
		else
		{
			Screen.lockCursor = false;
		}
		if (Input.GetButton("Snipe") && !Game.Messaging.chatting)
		{
			if (Camera.main.fieldOfView == 60f)
			{
				Camera.main.fieldOfView = 10f;
			}
			Camera.main.fieldOfView = Camera.main.fieldOfView + Input.GetAxis("Mouse ScrollWheel") * -1f;
			if (Camera.main.fieldOfView > 50f)
			{
				Camera.main.fieldOfView = 50f;
			}
			else if (Camera.main.fieldOfView < 1.5f)
			{
				Camera.main.fieldOfView = 1.5f;
			}
		}
		else if (Camera.main.fieldOfView != 60f)
		{
			Camera.main.fieldOfView = 60f;
		}
		Vector3 vector = transform.InverseTransformPoint(camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth - 30f - 65f, 30f, camera.nearClipPlane + 0.05f)));
		if (Vector3.Distance(vector, arrow.localPosition) < 0.002f)
		{
			arrow.localPosition = Vector3.Lerp(arrow.localPosition, vector, Time.deltaTime * 0.005f);
		}
		else
		{
			arrow.localPosition = vector;
		}
		arrow.rotation = Quaternion.Lerp(arrow.rotation, Quaternion.LookRotation(((Game.PlayerVeh.isIt == 0 && (bool)Game.QuarryVeh) ? Game.QuarryVeh.gameObject.transform : World.@base).position - Game.Player.transform.position), Time.deltaTime * 15f);
		arrow.localScale = Vector3.one * Mathf.Lerp(0.03f, 1f, Camera.main.fieldOfView / 60f);
		if (Input.GetKeyDown(KeyCode.Escape) && Game.Settings.camMode == 0)
		{
			Game.Settings.camMode = 1;
			PlayerPrefs.SetInt("cam", 1);
		}
		if (!Game.Messaging.chatting)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				Game.Settings.camMode = 0;
				PlayerPrefs.SetInt("cam", 0);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				Game.Settings.camMode = 1;
				PlayerPrefs.SetInt("cam", 1);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				Game.Settings.camDist = 0f;
				PlayerPrefs.SetFloat("camDist", 0f);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				Game.Settings.camDist = 20f;
				PlayerPrefs.SetFloat("camDist", 20f);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				Game.Settings.camMode = 2;
				PlayerPrefs.SetInt("cam", 2);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				Game.Settings.camMode = 3;
				PlayerPrefs.SetInt("cam", 3);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				Game.Settings.gyroCam = !Game.Settings.gyroCam;
				PlayerPrefs.SetInt("gyroCam", Game.Settings.gyroCam ? 1 : 0);
			}
		}
		float num = (float)Game.PlayerVeh.camOffset + Game.Settings.camDist;
		if (worldTime < 7f)
		{
			worldTime = Time.time - Game.Controller.worldLoadTime;
			transform.position = Vector3.Lerp(transform.position, Game.Player.transform.position, Time.deltaTime * 1f);
			wr = Quaternion.LookRotation(Game.Player.transform.position - transform.position, Vector3.up);
			if (worldTime > 1f)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, wr, Time.deltaTime * 0.5f * Mathf.Min(1f, (worldTime - 1f) * 0.5f));
			}
		}
		else if (Game.Settings.camMode == 0 || Input.GetButton("Fire2") || (Input.GetButton("Snipe") && !Game.Messaging.chatting))
		{
			transform.position = Game.PlayerVeh.ridePos.position;
			if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Alpha1))
			{
				rotationX = 0f;
				rotationY = 0f;
				if (Game.Settings.gyroCam)
				{
					gyroTation = Quaternion.Euler(0f, Game.PlayerVeh.ridePos.rotation.eulerAngles.y, 0f);
				}
			}
			if (Game.Settings.gyroCam)
			{
				transform.rotation = gyroTation;
			}
			else
			{
				transform.rotation = Game.PlayerVeh.ridePos.rotation;
			}
			rotationX += Input.GetAxis("Mouse X") * ((!Input.GetButton("Snipe")) ? 2f : 0.5f);
			rotationY += Input.GetAxis("Mouse Y") * ((!Input.GetButton("Snipe")) ? 2f : 0.5f);
			if (rotationX < -360f)
			{
				rotationX += 360f;
			}
			else if (rotationX > 360f)
			{
				rotationX -= 360f;
			}
			if (rotationY < -360f)
			{
				rotationY += 360f;
			}
			else if (rotationY > 360f)
			{
				rotationY -= 360f;
			}
			rotationX = Mathf.Clamp(rotationX, -360f, 360f);
			if (Game.Settings.gyroCam)
			{
				rotationY = Mathf.Clamp(rotationY, -100f, 100f);
			}
			else
			{
				rotationY = Mathf.Clamp(rotationY, -20f, 100f);
			}
			transform.localRotation *= Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		}
		else if (Game.Settings.camMode == 1)
		{
			if (Game.Settings.camChase == 0)
			{
				transform.position = Vector3.Lerp(transform.position, Game.Player.transform.position - Vector3.Normalize(Game.Player.transform.position - transform.position) * num + Vector3.one * ((!Game.PlayerVeh.camSmooth) ? Mathf.Lerp(0f, 15f, num / 30f) : 0f), Time.deltaTime * 3.5f);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Game.Player.transform.position - transform.position, (!Game.Settings.flightCam) ? Vector3.up : Game.Player.transform.up), Time.deltaTime * 4f);
				if (Physics.Linecast(transform.position + Vector3.up * 50f, transform.position + Vector3.down * 1f, out hit, 1 << 8) && !RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(hit.collider, "type"), typeof(TerrainCollider)))
				{
					float y = transform.position.y + (51f - hit.distance);
					Vector3 position = transform.position;
					float num2 = (position.y = y);
					Vector3 vector2 = (transform.position = position);
				}
			}
			else if (Game.Settings.camChase == 1)
			{
				float num3 = Vector3.Distance(Game.Player.transform.position, transform.position);
				if ((bool)Game.Player.transform.gameObject.rigidbody && Game.Player.transform.gameObject.rigidbody.velocity.sqrMagnitude > 0.1f && Game.Player.transform.gameObject.rigidbody.velocity.normalized.y < 0.8f && Game.Player.transform.gameObject.rigidbody.velocity.normalized.y > 0.8f * -1f)
				{
					lastDir = Vector3.Lerp(lastDir, Game.Player.transform.gameObject.rigidbody.velocity.normalized, 0.1f);
				}
				else
				{
					Vector3.Lerp(lastDir, new Vector3(lastDir.x, 0f, lastDir.z), 0.1f);
				}
				Vector3 to = Game.Player.transform.position + lastDir * (num * -1f) + Vector3.up * (num / 3f);
				float y2 = transform.position.y + (Game.Player.transform.position.y - lastY) * Time.deltaTime;
				Vector3 position2 = transform.position;
				float num4 = (position2.y = y2);
				Vector3 vector4 = (transform.position = position2);
				transform.position = Vector3.Lerp(transform.position, to, Time.deltaTime * 4f);
				lastY = Game.Player.transform.position.y;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Game.Player.transform.position - transform.position, (!Game.Settings.flightCam) ? Vector3.up : Game.Player.transform.up), Time.deltaTime * 4f);
				if (Physics.Linecast(transform.position + Vector3.up * 50f, transform.position + Vector3.down * 1f, out hit, 1 << 8) && !RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(hit.collider, "type"), typeof(TerrainCollider)))
				{
					float y3 = transform.position.y + (51f - hit.distance);
					Vector3 position3 = transform.position;
					float num5 = (position3.y = y3);
					Vector3 vector6 = (transform.position = position3);
				}
			}
			else if (Game.Settings.camChase == 2 && Game.Player.transform.rigidbody.velocity.magnitude > 0f)
			{
				float num6 = 3f;
				float num7 = 10f;
				float num8 = 3f;
				float y4 = Quaternion.LookRotation(Game.Player.transform.rigidbody.velocity).eulerAngles.y;
				y4 += Input.GetAxis("Horizontal") * Mathf.Lerp(30f, 10f, num / 30f);
				float b = Game.Player.transform.position.y + Mathf.Lerp(0.1f, 15f, num / 30f) + heightBoost;
				targetHeight = Mathf.Lerp(0.5f, 3f, num / 30f);
				float y5 = transform.eulerAngles.y;
				float y6 = transform.position.y;
				y5 = Mathf.LerpAngle(y5, y4, num8 * Time.deltaTime);
				y6 = Mathf.Lerp(y6, b, num6 * Time.deltaTime);
				Quaternion quaternion = Quaternion.Euler(0f, y5, 0f);
				Vector3 position4 = Game.Player.transform.position;
				position4.y += targetHeight;
				transform.position = position4;
				transform.position -= quaternion * Vector3.forward * num;
				float y7 = y6;
				Vector3 position5 = transform.position;
				float num9 = (position5.y = y7);
				Vector3 vector8 = (transform.position = position5);
				RaycastHit hitInfo = default(RaycastHit);
				if (Physics.Raycast(transform.position + Vector3.up * 49.4f, Vector3.down, out hitInfo, 50f, 1 << 8) && !RuntimeServices.EqualityOperator(RuntimeServices.GetProperty(hitInfo.collider, "type"), typeof(TerrainCollider)))
				{
					Physics.Linecast(transform.position, Game.Player.transform.position + Vector3.up * y6, out hitInfo, 1 << 8);
					transform.position += quaternion * Vector3.forward * hitInfo.distance;
					heightBoost = hitInfo.distance * 0.7f;
				}
				else
				{
					heightBoost = 0f;
				}
				transform.LookAt(position4);
			}
		}
		else if (Game.Settings.camMode == 2)
		{
			transform.position = Vector3.Lerp(transform.position, Game.Player.transform.position + Vector3.up * 40f, Time.deltaTime * 0.3f);
			wr = Quaternion.LookRotation(Game.Player.transform.position - transform.position, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, wr, Time.deltaTime * 2f);
		}
		else if (Game.Settings.camMode == 3)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Game.Player.transform.position - transform.position), Time.deltaTime * 1.5f);
			transform.Translate(new Vector3(Input.GetAxis("camX") * Time.deltaTime * 50f, Input.GetAxis("camY") * Time.deltaTime * 40f, Input.GetAxis("camZ") * Time.deltaTime * 50f));
		}
		else if (Game.Settings.camMode == 4)
		{
			float x = transform.position.x + Input.GetAxis("camX") * Time.deltaTime * 50f;
			Vector3 position6 = transform.position;
			float num10 = (position6.x = x);
			Vector3 vector10 = (transform.position = position6);
			float y8 = transform.position.y + Input.GetAxis("camY") * Time.deltaTime * 50f;
			Vector3 position7 = transform.position;
			float num11 = (position7.y = y8);
			Vector3 vector12 = (transform.position = position7);
			float z = transform.position.z + Input.GetAxis("camZ") * Time.deltaTime * 50f;
			Vector3 position8 = transform.position;
			float num12 = (position8.z = z);
			Vector3 vector14 = (transform.position = position8);
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		}
	}

	public void OnPreRender()
	{
		if (!(Game.Settings.fogColor == Color.clear))
		{
			if (transform.position.y < Game.Settings.lavaAlt || Physics.Raycast(transform.position + Vector3.up * 200f, Vector3.up * -1f, 200f, 1 << 4))
			{
				RenderSettings.fogColor = World.seaFogColor;
				RenderSettings.fogDensity = Game.Settings.lavaFog;
				glowEffect.enabled = Game.Settings.renderLevel > 2;
				glowEffect.glowTint = World.seaGlowColor;
				Camera.main.clearFlags = CameraClearFlags.Color;
			}
			else
			{
				RenderSettings.fogColor = Game.Settings.fogColor;
				RenderSettings.fogDensity = Game.Settings.worldFog;
				glowEffect.enabled = false;
				Camera.main.clearFlags = ((Camera.main.farClipPlane > 2000f) ? CameraClearFlags.Skybox : CameraClearFlags.Color);
			}
			colorEffect.enabled = glowEffect.enabled;
			Camera.main.backgroundColor = RenderSettings.fogColor;
		}
	}

	public void Main()
	{
	}
}
