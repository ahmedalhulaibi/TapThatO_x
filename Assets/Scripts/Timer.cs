/* COPYRIGHT (C) Ahmed Al-Hulaibi 2015 
   All rights reserved. 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***********
 * Class timerInfo contains the attributes of each individual timer
 * Class Timer can have multiple timers active at once
 * TAG - name of timer 
 * DURATION - duration of timer in seconds
 * REPEAT- Sets the timer to loop 
 * ***********/
class timerInfo
{
    public timerInfo(float _dur, bool _rep, string _tag)
    {
        duration = _dur;
        repeat = _rep;
        tag = _tag;
    }
    
    public bool repeat = false;
    public string tag = "";
    public float currentTime;
    public float totalTime;
    public float duration = 0;
};

public class Timer : MonoBehaviour {

    public List<string> Tags = new List<string>();
    public List<float> Durations = new List<float>();
    public List<bool> Repeat = new List<bool>();

    private  Dictionary<string, timerInfo> timers = new Dictionary<string, timerInfo>();

    public delegate void onTimerDelegate();
    public  Dictionary<string, onTimerDelegate> OnTimerEvent = new Dictionary<string, onTimerDelegate>();

    void Awake()
    {
        for (int item = 0; item < Tags.Count; item++)
        { 
            StartTimer(Durations[item], Repeat[item], Tags[item]);
        }
    }

    private string OnTimer(string tag)
    {
        if (timers.ContainsKey(tag))
        {
			//call all attached functions
            OnTimerEvent[tag]();
            //print(tag);
            return tag;
        }
        string errMsg = "ERROR! NO TIMER EXISTS WITH TAG " + tag;
        return errMsg;
    }

    /// <summary>
    /// Adds a new timer with the specified params. Multiple timers with different tags can be active at once. Timers are destroyed once they are stopped.
    /// </summary>
    /// <param name="duration">Duration of timer in seconds.</param>
    /// <param name="repeat">Sets the timer to loop.</param>
    /// <param name="tag">The name of the timer.</param>
    /// <returns></returns>
    public bool StartTimer(float duration, bool repeat, string tag)
    { 
        if(!timers.ContainsKey(tag))
        {
            timerInfo newTimer = new timerInfo(duration,repeat,tag);
            timers.Add(tag, newTimer);
            OnTimerEvent.Add(tag, new onTimerDelegate(voidfunc));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Restarts an existing timer with the specified tag. Note: This will not create a new timer.
    /// </summary>
    /// <param name="tag"></param>
    public void RestartTimer(string tag)
    {
        timers[tag].currentTime = 0;
    }

    /// <summary>
    /// Stops and deletes an existing timer with the specified tag.
    /// </summary>
    /// <param name="tag"></param>
    public void StopTimer(string tag)
    {
		OnTimerEvent.Remove(tag);
		timers.Remove(tag);
    }

    /// <summary>
    /// Returns the current time of the timer in seconds.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public float CurrentTime(string tag)
    {
        if (timers.ContainsKey(tag))
        {
            return timers[tag].currentTime;
        }
        return 0;
    }

    /// <summary>
    /// Returns the time since the timer started. For non-looping timers, this is the same as current time.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public float TotalTime(string tag)
    {
        if (timers.ContainsKey(tag))
        {
            return timers[tag].totalTime;
        }
        return 0;
    }

    /// <summary>
    /// Returns the duration of the timer in seconds.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public float Duration(string tag)
    {
        if (timers.ContainsKey(tag))
        {
            return timers[tag].duration;
        }
        return 0;
    }


	// Update is called once per frame
	void FixedUpdate () {
        
//        foreach (var item in timers)
//        {
//            item.Value.currentTime += Time.fixedDeltaTime;
//			item.Value.totalTime += Time.fixedDeltaTime;
//        }
	}

    void Update()
    {
        foreach (var item in timers)
        {
            item.Value.currentTime += Time.deltaTime;
			item.Value.totalTime += Time.deltaTime;
        }

		List<string> remove = new List<string> ();

        foreach (var item in timers)
        {
            if (item.Value.currentTime >= item.Value.duration)
            {
                OnTimer(item.Value.tag);
                if (item.Value.repeat)
                {
                    RestartTimer(item.Value.tag);
                }
                else
                {
                    Debug.LogWarning("Removing timer: " + item.Value.tag);
					remove.Add(item.Value.tag);
                }
            }
        }
		foreach (string s in remove) {
			StopTimer(s);
		}
    }

    //empty function, used when adding new event delegate to dictionary
    void voidfunc()
    {
        Debug.LogWarning("OnTimer() event fired!  " + Time.time.ToString());
    }

}
