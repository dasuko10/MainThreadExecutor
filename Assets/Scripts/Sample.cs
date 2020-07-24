using UnityEngine;
using System.Threading.Tasks;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainThreadExecutor.Initialize();

        Task.Run(() => {
            MainThreadExecutor.Instance.Post(() => {
                Debug.Log("Task Completed!!!!");
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
