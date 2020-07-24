using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MainThreadExecutor : MonoBehaviour
{
    private Queue<Action> actionQueue = new Queue<Action>();

    private static readonly object syncObject = new object();
    private static MainThreadExecutor instance;

    public static MainThreadExecutor Instance
    {
        get
        {

            lock (syncObject)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MainThreadExecutor>();
                }

                if (instance == null)
                {
                    instance = new GameObject(typeof(MainThreadExecutor).ToString()).AddComponent<MainThreadExecutor>();
                }
            }

            return instance;
        }
    }

    public static void Initialize()
    {
        var executor = MainThreadExecutor.Instance;
        Debug.Log("initialized :" + executor ?? executor.name);
    }

    public void Post(Action action)
    {
        lock (syncObject)
        {
            actionQueue.Enqueue(action);
        }
    }

    private void Update()
    {
        while (actionQueue.Any())
        {
            Action action = null;
            lock (syncObject)
            {
                action = actionQueue.Dequeue();
            }

            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}