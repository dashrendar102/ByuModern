using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Common.Storage;
using Common.WebServices.DO;
using Common.WebServices.DO.PersonSummary;

namespace Common.Authentication
{
    public static class AuthenticationManager
    {
        static public PasswordCredential credential = null;
        static async public void Login(String netID, String password)
        {
            if (string.IsNullOrEmpty(netID) || string.IsNullOrEmpty(password))
            {
                throw new InvalidCredentialsException(netID,password,"Given null or empty credentials.");
            }

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => );
            WebServiceSession webServiceSession = await Task.Run(() =>
            {
                return WebServiceSession.GetSession(netID, password);
            });

            if (webServiceSession != null)
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                if (credential != null)
                {
                    Logout();
                }
                credential = new Windows.Security.Credentials.PasswordCredential(
                    "byu.edu", netID, password);
                vault.Add(credential);
            }
            else
            {
                throw new InvalidCredentialsException(netID, password, "Invalid credentials.");
            }
        }

        static public async Task Logout()
        {
            if (credential != null)
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                foreach (var credential in vault.FindAllByResource("byu.edu"))
                {
                    vault.Remove(credential);
                }
                //vault.Remove(credential);

                credential = null;
                await WebCache.Instance.ClearCache();
            }
        }

        //static public PasswordCredential GetBYUCredentials()
        //{
        //    var vault = new Windows.Security.Credentials.PasswordVault();
        //    try
        //    {
        //        var credentialList = vault.FindAllByResource("byu.edu");
        //        return credentialList.FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        static public Boolean LoggedIn()
        {
            if (credential != null)
            {
                return true;
            }
            else
            {
                try
                {
                    PasswordVault vault = new Windows.Security.Credentials.PasswordVault();
                    IReadOnlyList<PasswordCredential> credentialList = vault.FindAllByResource("byu.edu");
                    credential = credentialList.FirstOrDefault();

                    return credential != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            //return credential != null;
        }
    }
}
