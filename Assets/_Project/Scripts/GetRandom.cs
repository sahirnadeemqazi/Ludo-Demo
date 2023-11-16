using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Scripts
{
    public static class GetRandom
    {
        private const string RANDOM_ORG_API_URL = "https://www.random.org/integers/?num=1&min=1&max=6&col=1&base=10&format=plain&rnd=new";

        public static IEnumerator GenerateRandomNumber(Action<int> randomNumber)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(RANDOM_ORG_API_URL))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string response = www.downloadHandler.text;
                    int rand;
                    if (int.TryParse(response, out rand))
                    {
                        randomNumber(rand);
                    }
                    else
                    {
                        Debug.LogWarning("Failed to parse the received random number.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to fetch data from Random.org: " + www.error);
                }
            }
        }
    }
}
