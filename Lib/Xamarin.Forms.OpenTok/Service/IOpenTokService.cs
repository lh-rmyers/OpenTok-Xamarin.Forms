﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Xamarin.Forms.OpenTok.Service
{
    public interface IOpenTokService : INotifyPropertyChanged
    {
        event Action<string> Error;

        event Action<string> MessageReceived;

        event Action<string> SubscriberAdded;

        bool IsVideoPublishingEnabled { get; set; }

        bool IsAudioPublishingEnabled { get; set; }

        bool IsVideoSubscriptionEnabled { get; set; }

        bool IsAudioSubscriptionEnabled { get; set; }

        bool IsSubscriberVideoEnabled { get; set; }

        string ApiKey { get; set; }

        string SessionId { get; set; }

        string UserToken { get; set; }

        bool IsSessionStarted { get; set; }

        bool IsPublishingStarted { get; set; }

        bool CheckPermissions();

        bool TryStartSession();

        void EndSession();

        void CycleCamera();

        Task<bool> SendMessageAsync(string message);
    }
}