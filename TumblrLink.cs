using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class TumblrLink : MonoBehaviour 
{

	public void OpenLinkJSPlugin()
	{
		#if !UNITY_EDITOR
		openWindow("https://andybones.tumblr.com/");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}