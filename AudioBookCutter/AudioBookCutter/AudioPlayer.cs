﻿using NAudio.Wave;
using System;
using System.IO;

namespace AudioBookCutter
{
    class AudioPlayer
    {
        public enum PlaybackStopTypes
        {
            PlaybackStoppedByUser, PlaybackStoppedReachingEndOfFile
        }

        public PlaybackStopTypes PlaybackStopType { get; set; }
        private WaveOut output;
        private Mp3FileReader _audioFileReader;
        public event Action PlaybackResumed;
        public event Action PlaybackStopped;
        public event Action PlaybackPaused;

        public AudioPlayer(string filepath)
        {
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
            output = new WaveOut();
            output.PlaybackStopped += _output_PlaybackStopped;
            _audioFileReader = new Mp3FileReader(filepath);
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
            if (PlaybackStopped != null)
            {
                PlaybackStopped();
            }
        }

        public void Stop()
        {
            if (output != null)
            {
                output.Pause();
                this.SetPosition(0);
                PlaybackStopType = PlaybackStopTypes.PlaybackStoppedByUser;

                if (PlaybackStopped != null)
                {
                    PlaybackStopped();
                }
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

        public double GetLengthInMSeconds()
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

        public TimeSpan GetLength()
        {
            if (_audioFileReader != null)
            {
                return _audioFileReader.TotalTime;
            }
            else
            {
                return TimeSpan.FromSeconds(0);
            }
        }

        public double GetPosition()
        {
            return _audioFileReader != null ? _audioFileReader.CurrentTime.TotalMilliseconds : 0;
        }

        public void SetPosition(double value)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.Position = _audioFileReader.Seek(_audioFileReader.WaveFormat.AverageBytesPerSecond * (long)TimeSpan.FromMilliseconds(value).TotalSeconds, SeekOrigin.Begin);
            }
        }
    }
}
