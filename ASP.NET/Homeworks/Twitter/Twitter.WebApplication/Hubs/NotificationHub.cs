namespace Twitter.WebApplication.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Data;
    using Microsoft.AspNet.Identity;
    using Models;
    using Twitter.Models;
    using UnitOfWork;

    [HubName("notifiaction")]
    public class NotificationHub : Hub
    {
        private ITwitterData data;

        public NotificationHub()
        {
            this.data = new TwitterData(new TwitterContext());
        }

        [Authorize]
        public void sendNoty(TweetOutputModel tweet)
        {

//            var userId = this.Context.User.Identity.GetUserId(); ;
//            var user = this.data.Users.Find(userId);
//            var userFollowers = user.Followers;

//            var msg = string.Format("{0}: {1}", this.Context.ConnectionId, tweet);
//            this.Clients.User("04369016-e3e8-4cfb-b1ca-5837b7693344").addTweet(msg);
        }
    }
}

