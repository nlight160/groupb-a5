using System;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace FroggerStarter.Controller
{
    /// <summary>Defines a Sound Manager Object</summary>
    public class SoundManager
    {
        /// <summary>Plays the vehicle collision sound.</summary>
        public async void PlayVehicleCollisionSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\PlayerDying");
            var file = await folder.GetFileAsync("420356__eponn__crash.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        /// <summary>Plays the falling water sound.</summary>
        public async void PlayFallingWaterSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\PlayerDying");
            var file = await folder.GetFileAsync("9508__petenice__splash.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        /// <summary>Plays the wall collision sound.</summary>
        public async void PlayWallCollisionSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\PlayerDying");
            var file = await folder.GetFileAsync("344402__jawbutch__body-wall-impact.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        /// <summary>Plays the time out sound.</summary>
        public async void PlayTimeOutSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\PlayerDying");
            var file = await folder.GetFileAsync("381382__coltonmanz__alarm.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        /// <summary>Plays the home sound.</summary>
        public async void PlayHomeSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\Game Sounds");
            var file = await folder.GetFileAsync("242501__gabrielaraujo__powerup-success.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        /// <summary>Plays the complete level sound.</summary>
        public async void PlayCompleteLevelSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\Game Sounds");
            var file = await folder.GetFileAsync("442943__qubodup__level-up.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        /// <summary>Plays the power up sound.</summary>
        public async void PlayPowerUpSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\Game Sounds");
            var file = await folder.GetFileAsync("220173__gameaudio__spacey-1up-power-up.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        /// <summary>Plays the game over sound.</summary>
        public async void PlayGameOverSound()
        {
            var element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\Game Sounds");
            var file = await folder.GetFileAsync("133283__leszek-szary__game-over.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }
    }
}