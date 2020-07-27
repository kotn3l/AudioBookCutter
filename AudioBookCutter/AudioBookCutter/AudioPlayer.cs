using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
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

        //private AudioFileReader _audioFileReader;

        //private DirectSoundOut _output;
        private WaveOutEvent output;
        private Mp3FileReader _audioFileReader;

        //private string _filepath;

        public event Action PlaybackResumed;
        public event Action PlaybackStopped;
        public event Action PlaybackPaused;

        public AudioPlayer(string filepath)
        {
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;

            //_audioFileReader = new AudioFileReader(filepath);

            //_output = new DirectSoundOut();
            output = new WaveOutEvent();
            output.PlaybackStopped += _output_PlaybackStopped;
            //_output.PlaybackStopped += _output_PlaybackStopped;

            _audioFileReader = new Mp3FileReader(filepath);
            //var wc = new WaveChannel32(_audioFileReader);
            //wc.PadWithZeroes = false;


            //_output.Init(wc);
            output.Init(_audioFileReader);
        }

        public void Play(PlaybackState playbackState)
        {
            if (playbackState == PlaybackState.Stopped || playbackState == PlaybackState.Paused)
            {
                output.Play();
            }

            if (PlaybackResumed != null)
            {
                PlaybackResumed();
            }
        }

        private void _output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //Dispose();
            if (PlaybackStopped != null)
            {
                PlaybackStopped();
            }
        }

        public void Stop()
        {
            if (output != null)
            {
                //_output.Stop();
                output.Pause();
                this.SetPosition(0);
            }
        }

        public void Pause()
        {
            if (output != null)
            {
                output.Pause();

                if (PlaybackPaused != null)
                {
                    PlaybackPaused();
                }
            }
        }

        public void TogglePlayPause()
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    Pause();
                }
                else
                {
                    Play(output.PlaybackState);
                }
            }
            else
            {
                Play(PlaybackState.Stopped);
            }
        }

        public void Dispose()
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing)
                {
                    output.Stop();
                }
                output.Dispose();
                output = null;
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

        public void SetPosition(double value)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.CurrentTime = TimeSpan.FromMilliseconds(value);
            }
        }
    }
}
