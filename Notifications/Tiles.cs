using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace Notifications
{
    public sealed class Tiles: IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            SendTileNotification();
        }

        public void SendTileNotification()
        {
            var tileContent = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileSmall = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            TextStacking = TileTextStacking.Center,
                            Children =
                {
                    new AdaptiveText()
                    {
                        Text = "25%",
                        HintStyle = AdaptiveTextStyle.Base,
                        HintAlign = AdaptiveTextAlign.Center
                    },
                    new AdaptiveText()
                    {
                        Text = "2018",
                        HintStyle = AdaptiveTextStyle.CaptionSubtle,
                        HintAlign = AdaptiveTextAlign.Center
                    }
                }
                        }
                    },
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            TextStacking = TileTextStacking.Center,
                            Children =
                {
                    new AdaptiveText()
                    {
                        Text = "25%",
                        HintStyle = AdaptiveTextStyle.Title,
                        HintWrap = true,
                        HintAlign = AdaptiveTextAlign.Center
                    },
                    new AdaptiveText()
                    {
                        Text = "Complete",
                        HintStyle = AdaptiveTextStyle.Base,
                        HintAlign = AdaptiveTextAlign.Center
                    },
                    new AdaptiveText()
                    {
                        Text = "2018",
                        HintStyle = AdaptiveTextStyle.CaptionSubtle,
                        HintAlign = AdaptiveTextAlign.Center
                    }
                }
                        }
                    },
                    TileWide = new TileBinding()
                    {
                        Branding = TileBranding.NameAndLogo,
                        Content = new TileBindingContentAdaptive()
                        {
                            TextStacking = TileTextStacking.Center,
                            Children =
                {
                    new AdaptiveText()
                    {
                        Text = "25% Complete",
                        HintStyle = AdaptiveTextStyle.Title,
                        HintAlign = AdaptiveTextAlign.Center
                    },
                    new AdaptiveText()
                    {
                        Text = "2018 Year Progress",
                        HintStyle = AdaptiveTextStyle.CaptionSubtle,
                        HintAlign = AdaptiveTextAlign.Center
                    }
                }
                        }
                    }
                }
            };

            // Create the tile notification
            var tileNotif = new TileNotification(tileContent.GetXml());

            // And send the notification to the primary tile
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotif);
        }
    }
}
