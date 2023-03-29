using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private Text levelName;
    [SerializeField] private Text levelDes;
    [SerializeField] private Image levelImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;

    public void DisplayLevel(Level _map)
    {
        levelName.text = _map.levelName;
        //levelName.color = _map.nameColor;
        levelDes.text = _map.levelDes;
        levelImage.sprite = _map.levelImage;

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _map.level_th;

        lockIcon.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        if (mapUnlocked)
            levelImage.color = Color.white;
        else
            levelImage.color = Color.blue;

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(_map.level_th + 2));
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			SceneManager.LoadScene(0);
	}
}