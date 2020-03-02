using Google.Apis.Youtube.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using XemYoutubeThoi.Models;
using System.Net.NetworkInformation;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XemYoutubeThoi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private YouTubeService youtubeService =
            new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyDBf8bq5AKUSHfF_CF0eeZ2RCLzyfmOi5s",
                ApplicationName = "youtube"
            });
        List<Video> ListVideo = new List<Video>();
        private string TokenNextPage = null, TokenPrivPage = null;

        public MainPage()
        {
            this.InitializeComponent();
            GetVideo();
        }   

        private async void GetVideo(string PageToken = null)
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    var Request = youtubeService.Search.List("snippet");
                    Request.ChannelId = "UCsooa4yRKGN_zEE8iknghZA";
                    Request.MaxResults = 25;
                    Request.Type = "video";
                    Request.Order = SearchResource.ListRequest.OrderEnum.Date;
                    Request.PageToken = PageToken;
                    var Result = await Request.ExecuteAsync();
                    if (Result.NextPageToken != null)
                        TokenNextPage = Result.NextPageToken;
                    if (Result.PrevPageToken != null)
                        TokenPrivPage = Result.PrevPageToken;

                    foreach( var item in Result.Items)
                    {
                        ListVideo.Add(new Video
                        {
                            Title = item.Snippet.Title,
                            Id = item.Id.VideoId,
                            Img = item.Snippet.Thumbnails.Default_.Url
                        });
                    }
                    lv.ItemsSource = null;
                    lv.ItemsSource = ListVideo;
                }
                else
                {
                    MessageDialog msq = new MessageDialog("Check your internet connection");
                    await msq.ShowAsync();
                }
            }
            catch { }
        }

        private void lv_ItemClick(object sender, ItemClickEventArgs e)
        {
            Video video = e.ClickedItem as Video;
            Frame.Navigate(typeof(VideoPage), video);
        }
    }
}
