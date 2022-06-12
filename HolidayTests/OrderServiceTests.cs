using System.Linq;
using Lib;
using NSubstitute;
using NUnit.Framework;

namespace HolidayTests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private FakeOrderService _orderService;
        private IBookDao _bookDao;

        [SetUp]
        public void SetUp()
        {
            _bookDao = Substitute.For<IBookDao>();
            _orderService = new FakeOrderService(_bookDao);
        }


        [Test]
        public void syncBookOrders_3_orders_only_2_book_order()
        {
            GivenOrderList(CreateOrder("Book"), CreateOrder("CD"), CreateOrder("Book"));
            WhenInsertBookOrders();
            ShouldInsertOrders(2, "Book");
            ShouldNotInsert("CD");
        }

        private void WhenInsertBookOrders()
        {
            _orderService.SyncBookOrders();
        }

        private void ShouldNotInsert(string type)
        {
            _bookDao.DidNotReceive().Insert(Arg.Is<Order>(order => order.Type == type));
        }

        private void ShouldInsertOrders(int times, string type)
        {
            _bookDao.Received(times).Insert(Arg.Is<Order>(order => order.Type == type));
        }

        private void GivenOrderList(params Order[] orders)
        {
            _orderService.OrderList = orders.ToList();
        }

        private static Order CreateOrder(string type)
        {
            return new Order {Type = type};
        }
    }
}