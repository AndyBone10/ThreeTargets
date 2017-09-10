using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class TwitterLink : MonoBehaviour 
{

	public void OpenLinkJSPlugin()
	{
		#if !UNITY_EDITOR
		openWindow("https://twitter.com/AndyBone10?lang=en");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}