using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Molitio
{
    public class SoundController
    {
        private MediaElement mediaElement;

        public SoundController(MediaElement mediaElement)
        {
            this.mediaElement = mediaElement;
        }

        public void Play(string audioUrl)
        {
            mediaElement.Source = new Uri(audioUrl, UriKind.Absolute);
            mediaElement.Play();
        }

        public void Stop()
        {
            mediaElement.Stop();
        }
    }
}
