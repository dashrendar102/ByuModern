using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
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

        static public void Logout()
        {
            if (credential != null)
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                vault.Remove(credential);

                credential = null;
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
            return credential != null;
        }
    }
}
