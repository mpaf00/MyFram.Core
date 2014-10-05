namespace MyFram.Core.Test.Helper
{
    using Social.Instagram;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InstagramHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var helper = new InstagramHelper();
            //helper.GetUserTimelineFeeds()
            Assert.AreEqual(true, true);
        }
    }
}