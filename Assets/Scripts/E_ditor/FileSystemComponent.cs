using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class FileSystemComponent : Button {
    [SerializeField]protected Sprite sprite;

    new void Start() {
        base.Start();
        onClick.AddListener(() => Click());
        Debug.Log("Start FileSystem");
    }

    public abstract void Click();

}
