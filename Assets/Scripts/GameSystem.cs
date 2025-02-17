using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> mapList;
    int currentMapIndex;
    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCamera;
    Vector2 SpawnPos ;
    [SerializeField]Animator animatorSceneTransition;
    public static GameSystem Instance;
    bool isChangingMap;
    void Awake()
    {
        Instance = this;
        currentMapIndex = 0;
        mapList[currentMapIndex].SetActive(true); 
    }
    void Update()
    {
        if(CheckIsEndMap() && !isChangingMap)
        {
            StartCoroutine(ChangeMap(currentMapIndex+1));
        }
    }
    bool CheckIsEndMap()
    {
        return mapList[currentMapIndex].GetComponentsInChildren<Fruit>().Length == 0 ;
    }
    public IEnumerator ChangeMap(int mapId)
    {
        animatorSceneTransition.SetTrigger("End");
        PlayerController.Pc.DisAppear();
        isChangingMap = true;
        yield return new WaitForSeconds(1);
        isChangingMap = false;
        if(currentMapIndex == mapList.Count - 1)
        {
            Debug.Log("End Game");
        }else{
            mapList[mapId].SetActive(false);
            currentMapIndex++;
            mapList[mapId].SetActive(true);
            virtualCamera.transform.position = new Vector3(mapList[mapId].transform.position.x,mapList[mapId].transform.position.y,-10);
            SpawnPos = mapList[mapId].transform.Find("SpawnPos").position;
            PlayerController.Pc.transform.position = SpawnPos;
            PlayerController.Pc.Appear();
        }
        animatorSceneTransition.SetTrigger("Start");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
