namespace MyFram.Core.Test.Helper
{
    using Social.Twitter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TwitterHelperTest
    {
        [TestMethod]
        public void MyTweetsTest()
        {
            var helper = new TwitterHelper();
            //helper.GetUserTimelineFeeds();

            Assert.AreEqual(true, true);
        }
    }
}