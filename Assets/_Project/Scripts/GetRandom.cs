using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Scripts
{
    // This class provides a method for generating a random number using the Random.org API.
    public static class GetRandom
    {
        // The URL for the Random.org API, configured to generate a single random integer between 1 and 6.
        private const string RANDOM_ORG_API_URL = "https://www.random.org/integers/?num=1&min=1&max=6&col=1&base=10&format=plain&rnd=new";

        // Coroutine method for generating a random number asynchronously.
        // Takes an Action<int> callback as a parameter to handle the generated random number.
        public static IEnumerator GenerateRandomNumber(Action<int> randomNumber)
        {
            // Using Unity's UnityWebRequest to make a GET request to the Random.org API.
            using (UnityWebRequest www = UnityWebRequest.Get(RANDOM_ORG_API_URL))
            {
                // Yielding the coroutine until the request is completed.
                yield return www.SendWebRequest();

                // Checking if the request was successful.
                if (www.result == UnityWebRequest.Result.Success)
                {
                    // Extracting the response text from the request.
                    string response = www.downloadHandler.text;
                    int rand;

                    // Attempting to parse the received random number as an integer.
                    if (int.TryParse(response, out rand))
                    {
                        // Calling the provided callback with the generated random number.
                        randomNumber(rand);
                    }
                    else
                    {
                        // Logging a warning if parsing fails.
                        Debug.LogWarning("Failed to parse the received random number.");
                    }
                }
                else
                {
                    // Logging an error if the request fails.
                    Debug.LogError("Failed to fetch data from Random.org: " + www.error);
                }
            }
        }
    }
}
