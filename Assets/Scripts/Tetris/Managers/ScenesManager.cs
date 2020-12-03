using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Patterns;

namespace Tetris.Managers
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        IEnumerator LoadAsynchronously(string levelName, float minWait = 1f) 
        {
            var timer = 0f;
            var minLoadTime = minWait;
     
            AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);;
            operation.allowSceneActivation = false;
     
            while (!operation.isDone) 
            {
                timer += Time.deltaTime;
                if (timer > minLoadTime)
                {
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
            yield return null;
        }

        public void LoadGameScene()
        {
            StartCoroutine(LoadAsynchronously("GameScene", 0f));
        }
    }
}
