using UnityEngine;

public static class Extensions {
	
	public static Bounds OrthographicBounds (this Camera camera) {
		if (!camera.orthographic)
		{
			Debug.Log(string.Format("The camera {0} is not Orthographic!", camera.name), camera);
			return new Bounds();
		}
		
		var t = camera.transform;
		var x = t.position.x;
		var y = t.position.y;
		var size = camera.orthographicSize * 2;
		var width = size * (float)Screen.width / Screen.height;
		var height = size;
		
		return new Bounds(new Vector3(x, y, 0), new Vector3(width, height, 0));
	}
	
	public static Vector3 PointOnCircle(float radius, float angleInDegrees, Vector3 origin) {
		// Convert from degrees to radians via multiplication by PI/180        
		float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F)) + origin.x;
		float y = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F)) + origin.y;
		
		return new Vector3(x, y);
	}

    public static string LoadResourceTextfile(string path)
    {
        TextAsset targetFile = Resources.Load<TextAsset>(path);
        return targetFile.text;
    }
}
