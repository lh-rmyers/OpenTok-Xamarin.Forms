﻿using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.ComponentModel;
using System;
using System.Threading.Tasks;

namespace Xamarin.Forms.OpenTok.Service
{
    public abstract class BaseOpenTokService : IOpenTokService
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action<string> Error;

        public event Action<string> MessageReceived;

        public event Action<string> SubscriberAdded;

        private readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

        public bool IsVideoPublishingEnabled
        {
            get => GetValue(true);
            set => SetValue(value);
        }

        public bool IsAudioPublishingEnabled
        {
            get => GetValue(true);
            set => SetValue(value);
        }

        public bool IsVideoSubscriptionEnabled
        {
            get => GetValue(true);
            set => SetValue(value);
        }

        public bool IsAudioSubscriptionEnabled
        {
            get => GetValue(true);
            set => SetValue(value);
        }

        public bool IsSubscriberVideoEnabled
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        public bool IsSubscriberConnected
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        public string ApiKey
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public string SessionId
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public string UserToken
        {
            get => GetValue(string.Empty);
            set => SetValue(value);
        }

        public bool IsSessionStarted
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        public bool IsPublishingStarted
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        public abstract bool CheckPermissions();

        public abstract bool TryStartSession();

        public abstract void EndSession();

        public abstract void CycleCamera();

        public abstract Task<bool> SendMessageAsync(string message);

        protected void RaiseError(string message) => Error?.Invoke(message);

        protected void RaiseMessageReceived(string message) 
            => MessageReceived?.Invoke(message);

        protected void RaiseSubscriberAdded(string streamId)
           => SubscriberAdded?.Invoke(streamId);

        private T GetValue<T>(T defaultValue, [CallerMemberName] string name = null)
            => (T)(_properties.TryGetValue(name, out object value) ? value : defaultValue);

        private void SetValue<T>(T value, [CallerMemberName] string name = null) where T: IEquatable<T>
        {
            if (_properties.ContainsKey(name) && ((T)_properties[name]).Equals(value))
            {
                return;
            }
            _properties[name] = value;
            RaisePropertyChanged(name);
        }

        private void RaisePropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
