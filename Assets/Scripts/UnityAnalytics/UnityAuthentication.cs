using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace UnityAnalytics
{
    public class UnityAuthentication : MonoBehaviour
    {
        private async void Start()
        {
            // UnityServices.InitializeAsync() will initialize all services that are subscribed to Core.
            await UnityServices.InitializeAsync();
            Debug.Log(UnityServices.State);

            await SignInAnonymouslyAsync();
        }
        async Task SignInAnonymouslyAsync()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");
        
                // Shows how to get the playerID
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 

            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }
    }
}
