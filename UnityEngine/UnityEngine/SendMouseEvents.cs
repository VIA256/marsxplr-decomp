namespace UnityEngine
{
	public class SendMouseEvents
	{
		private struct MouseOverPair
		{
			public GameObject target;

			public Camera camera;

			public void SendMessage(string name)
			{
				target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			public static bool ComparePair(MouseOverPair lhs, MouseOverPair rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}

			public static implicit operator bool(MouseOverPair exists)
			{
				return exists.target != null && exists.camera != null;
			}
		}

		private static MouseOverPair[] m_OldMouseOver = new MouseOverPair[2]
		{
			default(MouseOverPair),
			default(MouseOverPair)
		};

		private static MouseOverPair[] m_OldMouseDrag = new MouseOverPair[2]
		{
			default(MouseOverPair),
			default(MouseOverPair)
		};

		private static void DoSendMouseEvents()
		{
			MouseOverPair[] array = new MouseOverPair[2]
			{
				default(MouseOverPair),
				default(MouseOverPair)
			};
			Vector3 mousePosition = Input.mousePosition;
			Camera[] allCameras = Camera.allCameras;
			if (!GUIUtility.mouseUsed)
			{
				Camera[] array2 = allCameras;
				int num = array2.Length;
				for (int i = 0; i < num; i++)
				{
					Camera camera = array2[i];
					if (!camera.pixelRect.Contains(mousePosition))
					{
						continue;
					}
					GUILayer gUILayer = (GUILayer)camera.GetComponent(typeof(GUILayer));
					if ((bool)gUILayer)
					{
						GUIElement gUIElement = gUILayer.HitTest(mousePosition);
						if ((bool)gUIElement)
						{
							array[0].target = gUIElement.gameObject;
							array[0].camera = camera;
						}
					}
					RaycastHit hitInfo;
					if (camera.farClipPlane > 0f && Physics.Raycast(camera.ScreenPointToRay(mousePosition), out hitInfo, camera.farClipPlane, camera.cullingMask & -5))
					{
						if ((bool)hitInfo.rigidbody)
						{
							array[1].target = hitInfo.rigidbody.gameObject;
							array[1].camera = camera;
						}
						else
						{
							array[1].target = hitInfo.collider.gameObject;
							array[1].camera = camera;
						}
					}
					else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
					{
						array[1].target = null;
						array[1].camera = null;
					}
				}
			}
			for (int j = 0; j < 2; j++)
			{
				bool mouseButtonDown = Input.GetMouseButtonDown(0);
				bool mouseButton = Input.GetMouseButton(0);
				MouseOverPair mouseOverPair = (mouseButtonDown ? array[j] : (mouseButton ? m_OldMouseDrag[j] : default(MouseOverPair)));
				if (MouseOverPair.ComparePair(mouseOverPair, m_OldMouseDrag[j]))
				{
					if ((bool)mouseOverPair)
					{
						mouseOverPair.SendMessage("OnMouseDrag");
					}
				}
				else
				{
					if ((bool)m_OldMouseDrag[j])
					{
						m_OldMouseDrag[j].SendMessage("OnMouseUp");
					}
					if ((bool)mouseOverPair)
					{
						mouseOverPair.SendMessage("OnMouseDown");
						mouseOverPair.SendMessage("OnMouseDrag");
					}
				}
				m_OldMouseDrag[j] = mouseOverPair;
				MouseOverPair mouseOverPair2 = array[j];
				if (MouseOverPair.ComparePair(mouseOverPair2, m_OldMouseOver[j]))
				{
					if ((bool)mouseOverPair2)
					{
						mouseOverPair2.SendMessage("OnMouseOver");
					}
				}
				else
				{
					if ((bool)m_OldMouseOver[j])
					{
						m_OldMouseOver[j].SendMessage("OnMouseExit");
					}
					if ((bool)mouseOverPair2)
					{
						mouseOverPair2.SendMessage("OnMouseEnter");
						mouseOverPair2.SendMessage("OnMouseOver");
					}
				}
				m_OldMouseOver[j] = mouseOverPair2;
			}
		}
	}
}
