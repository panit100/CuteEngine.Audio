using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CuteEngine.Audio
{
    public class AudioUpdater : MonoBehaviour
    {
        private Dictionary<string, float> _trackingDict = new Dictionary<string, float>();

        private const float CHECK_INTERVAL = 0.2f;
        private WaitForSeconds waitInterval;
        private Coroutine checkCoroutine;

        private void Awake()
        {
            waitInterval = new WaitForSeconds(CHECK_INTERVAL);
        }

        public void Track(string id, float clipLength)
        {
            float endTime = Time.time + clipLength;
            if (!_trackingDict.ContainsKey(id))
            {
                _trackingDict.Add(id, endTime);
            }
            else
            {
                _trackingDict[id] = endTime;
            }

            if (checkCoroutine == null)
            {
                checkCoroutine = StartCoroutine(CheckRoutine());
            }
        }

        public void StopTrack(string id)
        {
            _trackingDict.Remove(id);
        }

        private IEnumerator CheckRoutine()
        {
            while (true)
            {
                if (_trackingDict.Count == 0)
                {
                    checkCoroutine = null;
                    yield break;
                }

                List<string> expiredIds = new List<string>();

                foreach (var kvp in _trackingDict)
                {
                    string id = kvp.Key;
                    float endTime = kvp.Value;

                    AudioHandle handle = AudioManager.GetAudioHandle(id);
                    if (handle == null)
                    {
                        expiredIds.Add(id);
                        continue;
                    }

                    if (Time.time >= endTime && !handle.IsActive)
                    {
                        expiredIds.Add(id);
                        AudioManager.RemoveHandle(id);
                    }
                }

                for (int i = 0; i < expiredIds.Count; i++)
                {
                    _trackingDict.Remove(expiredIds[i]);
                }

                yield return waitInterval;
            }
        }
    }
}
