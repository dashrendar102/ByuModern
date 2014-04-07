using BYU.Common;
using Common.Authentication;
using Common.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Hub App template is documented at http://go.microsoft.com/fwlink/?LinkId=321221

namespace BYU
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static Frame RootFrame { get; private set; }

        private SettingsCommand loginSetting;
        private SettingsCommand logoutSetting;
        private SettingsCommand privacyPolicySetting;
        private string firstNavState;

        // for registering the live tile
        private const string taskName = "LiveTileBackgroundTask";
        private const string taskEntryPoint = "BackgroundTask.LiveTileBackgroundTask";

        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            this.RegisterBackgroundTask();

            Frame rootFrame = Window.Current.Content as Frame;
            App.RootFrame = rootFrame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                App.RootFrame = rootFrame;
                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(HomePage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
            firstNavState = ((Frame)Window.Current.Content).GetNavigationState();
        }

        private async void RegisterBackgroundTask()
        {
            try
            {
                var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
                if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                    backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                {
                    foreach (var task in BackgroundTaskRegistration.AllTasks)
                    {
                        if (task.Value.Name == taskName)
                        {
                            task.Value.Unregister(true);
                        }
                    }

                    BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                    taskBuilder.Name = taskName;
                    taskBuilder.TaskEntryPoint = taskEntryPoint;
                    taskBuilder.SetTrigger(new TimeTrigger(15, false));
                    var registration = taskBuilder.Register();
                }
            }
            //Ignore the request if an exception is thrown. This allows the simulator to work.
            catch
            { }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;
        }

        private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            loginSetting = new SettingsCommand(
                "Login Setting", "Login", (handler) => ShowLoginSettingFlyout());
            logoutSetting = new SettingsCommand(
                "Logout Setting", "Logout", (handler) => LogoutSettingHandler());
            privacyPolicySetting = new SettingsCommand(
                "View Privacy Policy", "Privacy Policy", (handler) => ShowPrivacyPolicy());

            if (AuthenticationManager.LoggedIn())
            {
                args.Request.ApplicationCommands.Add(logoutSetting);
            }
            else
            {
                args.Request.ApplicationCommands.Add(loginSetting);
            }

            args.Request.ApplicationCommands.Add(privacyPolicySetting);
        }

        public void ShowLoginSettingFlyout()
        {
            LoginSettingFlyout flyout = new LoginSettingFlyout();
            flyout.Show();
        }

        public async Task LogoutSettingHandler()
        {
            await AuthenticationManager.Logout();
            ((Frame)Window.Current.Content).SetNavigationState(firstNavState);
        }

        public async void ShowPrivacyPolicy()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://williamsware.com/byu/index.html"));
        }
    }
}
