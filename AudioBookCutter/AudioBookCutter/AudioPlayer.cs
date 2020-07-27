using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioBookCutter
{
    class AudioPlayer
    {
        public enum PlaybackStopTypes
        {
            PlaybackStoppedByUser, PlaybackStoppedReachingEndOfFile
        }

        public PlaybackStopTypes PlaybackStopType { get; set; }

        private AudioFileReader _audioFileReader;

        private DirectSoundOut _output;

        //private string _filepath;

        public event Action PlaybackResumed;
        public event Action PlaybackStopped;
        public event Action PlaybackPaused;

        public AudioPlayer(string filepath)
        {
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;

            _audioFileReader = new AudioFileReader(filepath);

            _output = new DirectSoundOut();
            _output.PlaybackStopped += _output_PlaybackStopped;

            var wc = new WaveChannel32(_audioFileReader);
            wc.PadWithZeroes = false;

            _output.Init(wc);
        }

        public void Play(PlaybackState playbackState)
        {
            if (playbackState == PlaybackState.Stopped || playbackState == PlaybackState.Paused)
            {
                _output.Play();
            }

            if (PlaybackResumed != null)
            {
                PlaybackResumed();
            }
        }

        private void _output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            Dispose();
            if (PlaybackStopped != null)
            {
                PlaybackStopped();
            }
        }

        public void Stop()
        {
            if (_output != null)
            {
                //_output.Stop();
                _output.Pause();
                this.SetPosition(0);
            }
        }

        public void Pause()
        {
            if (_output != null)
            {
                _output.Pause();

                if (PlaybackPaused != null)
                {
                    PlaybackPaused();
                }
            }
        }

        public void TogglePlayPause()
        {
            if (_output != null)
            {
                if (_output.PlaybackState == PlaybackState.Playing)
                {
                    Pause();
                }
                else
                {
                    Play(_output.PlaybackState);
                }
            }
            else
            {
                Play(PlaybackState.Stopped);
            }
        }

        public void Dispose()
        {
            if (_output != null)
            {
                if (_output.PlaybackState == PlaybackState.Playing)
                {
                    _output.Stop();
                }
                _output.Dispose();
                _output = null;
            }
            if (_audioFileReader != null)
            {
                _audioFileReader.Dispose();
                _audioFileReader = null;
            }
        }

        public double GetLenghtInMSeconds()
        {
            if (_audioFileReader != null)
            {
                return _audioFileReader.TotalTime.TotalMilliseconds;
            }
            else
            {
                return 0;
            }
        }

        public double GetPosition()
        {
            return _audioFileReader != null ? _audioFileReader.CurrentTime.TotalMilliseconds : 0;
            //return _audioFileReader.CurrentTime;
        }

        public float GetVolume()
        {
            if (_audioFileReader != null)
            {
                return _audioFileReader.Volume;
            }
            return 1;
        }

        public void SetPosition(double value)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.CurrentTime = TimeSpan.FromMilliseconds(value);
            }
        }
    }
}
