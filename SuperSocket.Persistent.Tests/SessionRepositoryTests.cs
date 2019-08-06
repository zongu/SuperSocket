
namespace SuperSocket.Persistent.Tests
{
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SuperSocket.Domain.Model;

    [TestClass]
    public class SessionRepositoryTests
    {
        private SessionRepository repo;

        [TestInitialize]
        public void Init()
        {
            this.repo = new SessionRepository(5);
            SessionRepository.webSocketSessions = new System.Collections.Generic.List<MemberStatus>();
        }

        [TestMethod]
        public void AddTest()
        {
            var addresult = this.repo.Add(new WebSocket.WebSocketSession());
            Assert.IsTrue(addresult);

            var addFailResult = this.repo.Add(new WebSocket.WebSocketSession());
            Assert.IsFalse(addFailResult);
        }

        [TestMethod]
        public void GetTest()
        {
            var getNullResult = this.repo.Get(123456);
            Assert.IsNull(getNullResult);

            var session = new WebSocket.WebSocketSession();
            var addresult = this.repo.Add(session);
            Assert.IsTrue(addresult);

            var checkInResult = this.repo.CheckIn(session, 123456);
            Assert.IsTrue(checkInResult);

            var getResult = this.repo.Get(123456);
            Assert.IsNotNull(getResult);
        }

        [TestMethod]
        public void RemoveSessionTest()
        {
            var session = new WebSocket.WebSocketSession();
            var addresult = this.repo.Add(session);
            Assert.IsTrue(addresult);

            var checkInResult = this.repo.CheckIn(session, 123456);
            Assert.IsTrue(checkInResult);

            var getResult = this.repo.Get(123456);
            Assert.IsNotNull(getResult);

            var memberId = this.repo.RemoveAndGetMemberId(session);
            Assert.AreEqual(memberId, 123456);

            var getNullResult = this.repo.Get(123456);
            Assert.IsNull(getNullResult);
        }

        [TestMethod]
        public void CheckInTest()
        {
            var session = new WebSocket.WebSocketSession();
            var checkInFailResult = this.repo.CheckIn(session, 123456);
            Assert.IsFalse(checkInFailResult);

            var addresult = this.repo.Add(session);
            Assert.IsTrue(addresult);

            var member = SessionRepository.webSocketSessions.FirstOrDefault(s => s.Session.SessionID == null);
            Assert.IsNull(member.MemberId);

            var checkInResult = this.repo.CheckIn(session, 123456);
            Assert.IsTrue(checkInResult);

            member = SessionRepository.webSocketSessions.FirstOrDefault(s => s.Session.SessionID == null);
            Assert.AreEqual(member.MemberId, 123456);
        }

        [TestMethod]
        public void GetAndRemoveExpiredSessionsTest()
        {
            var expriedMembers = this.repo.GetAndRemoveExpiredSessions();
            Assert.AreEqual(expriedMembers.Count(), 0);

            var addresult = this.repo.Add(new WebSocket.WebSocketSession());
            Assert.IsTrue(addresult);

            expriedMembers = this.repo.GetAndRemoveExpiredSessions();
            Assert.AreEqual(expriedMembers.Count(), 0);

            var member = SessionRepository.webSocketSessions.FirstOrDefault(s => s.Session.SessionID == null);
            Assert.IsNull(member.MemberId);

            Thread.Sleep(6000);

            expriedMembers = this.repo.GetAndRemoveExpiredSessions();
            Assert.AreEqual(expriedMembers.Count(), 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var session = new WebSocket.WebSocketSession();

            var addresult = this.repo.Add(session);
            Assert.IsTrue(addresult);

            this.repo.UpdateSession(session);
        }
    }
}
