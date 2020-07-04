using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class FileSystemFolder : MonoBehaviour {

	bool isExpanded = false;
	[SerializeField] GameObject children;
	[SerializeField] Sprite selectedSprite;
	[SerializeField] Sprite unselectedSprite;
	[SerializeField] FileSystemFolder[] baseFolders;

	List<FileSystemFolder> folder = new List<FileSystemFolder>();
	Image im;
	void Start() {
		im = GetComponent<Image>();
		foreach(Transform go in children.transform) {
			FileSystemFolder folderChild = go.GetComponentInChildren<FileSystemFolder>();
			if(folderChild != null) {
				folder.Add(folderChild);
			}
		}
	}

	public void Click() {
		if(isExpanded)
			Reduce();
		else
			Expand();
	}

	public void Expand() {
		children.SetActive(true);
		isExpanded = true;
		// change sprite of the folder
		im.sprite = selectedSprite;
		foreach(var f in baseFolders) {
			if(f != this)
				f.Reduce();
		}
	}

	public void Reduce() {
		isExpanded = false;
		if(im != null)
			im.sprite = unselectedSprite;
		foreach(var f in folder) {
			f.Reduce();
		}
		children.SetActive(false);
		// change sprite of the folder

	}
}
