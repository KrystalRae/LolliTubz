using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCInBuiltFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCInBuiltFeatures.Managers
{
    public class OrderManager
    {

        private Product GetProduct(string productCode)
        {
            using (var context = new ApplicationDbContext())
            {
                Product item = context.Products.FirstOrDefault(x => x.ProductCode == productCode);
                return (item);
            }
        }

        public int CreateOrder(StockFillModel model)
        {
            model.Stock = model.Stock.Where(x => x.Quantity > 0).ToList();
            List<OrderLine> orderLines = model.Stock.Select(x => new OrderLine(x.ProductCode, x.Quantity)).ToList();

            using (var context = new ApplicationDbContext())
            {
                Order order = new Order(model.Location.Id, orderLines, OrderStatusEnum.NotApproved);
                context.Orders.Add(order);
                context.SaveChanges();

                return order.OrderId;
            }
        }

        public void ApproveOrder(int orderId)
        {
            using (var context = new ApplicationDbContext())
            {
                Order order = context.Orders.FirstOrDefault(x => x.OrderId == orderId);
                order.OrderStatusId = (int)OrderStatusEnum.BuyerApproved;
                order.OrderDate = DateTime.Today;

                context.SaveChanges();
            }
        }

        public void SaveEdittedOrder(OrderSummaryModel model)
        {
            model.OrderProductList = model.OrderProductList.Where(x => x.Quantity > 0).ToList();

            using (var context = new ApplicationDbContext())
            {
                List<OrderLine> orderLines = model.OrderProductList.Select(x => new OrderLine(x.ProductCode, x.Quantity)).ToList();
                Order edittableOrder = context.Orders.Include("OrderLines").FirstOrDefault(x => x.OrderId == model.OrderId);
                edittableOrder.OrderLines.Clear();
                edittableOrder.OrderLines = orderLines;

                context.SaveChanges();
            }
        }

        public OrderSummaryModel CreateOrderSummaryModel(int orderId, ApplicationUser currentUser)
        {
            Order order = GetOrderFromOrderId(orderId);
            List<Product> productList = GetProductsFromOrderLines(orderId);
            foreach ( Product product in productList)
            {
                float totalProductCost = product.Cost * product.Quantity;
                product.Cost = totalProductCost;
            }
        
            return new OrderSummaryModel(new LocationManager().GetLocation(order.LocationId, currentUser), productList, order.OrderId, CalcTotalNumberOfProducts(productList), CalcTotalCostProduct(productList));
        }

        

        public OrderSummaryModel CreateEditOrderSummaryModel(int orderId, ApplicationUser currentUser)
        {
            Order order = GetOrderFromOrderId(orderId);
            LocationManager manager = new LocationManager();
            Location location = manager.GetLocation(order.LocationId, currentUser);

            List<Product> allProducts = manager.GetAllStock();
            List<Product> orderProducts = GetProductsFromOrderLines(orderId);
            foreach(Product product in allProducts)
            {
                foreach (Product orderProduct in orderProducts)
                {
                    if (orderProduct.ProductCode == product.ProductCode)
                    {
                        product.Quantity = orderProduct.Quantity;
                    }
                }
            }
            int totalNumProducts = CalcTotalNumberOfProducts(allProducts);
            float totalCostProducts = CalcTotalCostProduct(orderProducts);

            return new OrderSummaryModel(location, allProducts, orderId, totalNumProducts, totalCostProducts);
        }

        public StockFillModel EditOrder(int orderId, ApplicationUser currentUser)
        {
            Order order = GetOrderFromOrderId(orderId);
            return new StockFillModel(new LocationManager().GetLocation(order.LocationId, currentUser), GetProductsFromOrderLines(orderId));
        }

        public List<PendingOrderModel> CreatePendingOrdersList(int franchiseId)
        {
            List<Order> pendingOrders = GetListOfPendingOrders(franchiseId);
            List<PendingOrderModel> modelList = new List<PendingOrderModel>();
            foreach(Order order in pendingOrders)
            {
                List<Product> orderProductList = GetProductsFromOrderLines(order.OrderId);
                int totalNumProducts = CalcTotalNumberOfProducts(orderProductList);
                modelList.Add(new PendingOrderModel(order.OrderId, order.Location.Name, totalNumProducts, order.OrderDate));
            }
            return modelList;
        }

        public float CalcTotalCostProduct(List<Product> productList)
        {
            float total = 0;
            foreach(Product product in productList)
            {
                total = total + product.Cost;
            }
            return total;
        }

        public int CalcTotalNumberOfProducts(List<Product> productList)
        {
            int count = 0;
            foreach (Product product in productList)
            {
                count = count + product.Quantity;
            }
            return count;
        }

        public void DeleteOrder(int orderId)
        {
            using (var context = new ApplicationDbContext())
            {
                Order order = context.Orders.Include("OrderLines").FirstOrDefault(x => x.OrderId == orderId);
                order.OrderLines.Clear();
                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }

        public float getTotalOrderCost(Order order) { 
     
            if (order.OrderLines == null)
            {
                return 0;
            }

            float orderTotal = 0;
            foreach (OrderLine orderLine in order.OrderLines)
            {
                float cost =GetProduct(orderLine.ProductCode).Cost;
                float quantity = orderLine.Quantity;
                float productCost = cost * quantity;


                orderTotal += productCost;
            }
            return orderTotal;
        }

        public StockOrderModel CreateStockOrderModel(int franchiseId)
        {
            List<Order> buyerApprovedOrders = GetListOfBuyerApprovedOrders(franchiseId);
            List<BuyerApprovedOrderModel> orderModels = new List<BuyerApprovedOrderModel>();
            foreach(Order order in buyerApprovedOrders)
            {
                orderModels.Add(new BuyerApprovedOrderModel(order.Location.Name, TotalProductsInAnOrder(order.OrderId), getTotalOrderCost(order), order.OrderDate));
            }

            return new StockOrderModel(GetFranchiseName(franchiseId), orderModels, orderModels.Count, TotalNumberOfProductsFromAllApprovedOrders(orderModels), TotalValueOfAllApprovedOrders(orderModels));
        }

        private List<Order> GetListOfPendingOrders(int franchiseId)
        {

            using (var context = new ApplicationDbContext())
            {
                List<Order> pendingOrderList = context.Orders.Include("Location").Where(x => x.Location.FranchiseId == franchiseId && x.OrderStatusId == (int)OrderStatusEnum.NotApproved).ToList();
                return pendingOrderList;
            }
        }

        private List<Order> GetListOfBuyerApprovedOrders(int franchiseId)
        {

            using (var context = new ApplicationDbContext())
            {
                List<Order> pendingOrderList = context.Orders.Include("Location").Include("OrderLines").Where(x => x.Location.FranchiseId == franchiseId && x.OrderStatusId == (int)OrderStatusEnum.BuyerApproved).ToList();
                return pendingOrderList;
            }
        }

        private Order GetOrderFromOrderId(int orderId)
        {
            using (var context = new ApplicationDbContext())
            {
                Order order = context.Orders.FirstOrDefault(x => x.OrderId == orderId);
                return order;
            }
        }

        private List<Product> GetProductsFromOrderLines(int orderId)
        {
            using (var context = new ApplicationDbContext())
            {
                List<OrderLine> orderOrderLines = context.OrderLines.Where(x => x.Order.OrderId == orderId).ToList();
                List<Product> productList = new List<Product>();
                foreach (OrderLine orderLine in orderOrderLines)
                {
                    Product product = GetProduct(orderLine.ProductCode);
                    product.Quantity = orderLine.Quantity;
                    productList.Add(product);
                }
                return productList;
            }

        }

        private int TotalProductsInAnOrder(int orderId)
        {
            using (var context = new ApplicationDbContext())
            {
                List<OrderLine> orderOrderLines = context.OrderLines.Where(x => x.Order.OrderId == orderId).ToList();
                int count = 0;
                foreach(OrderLine orderLine in orderOrderLines)
                {
                    count = count + orderLine.Quantity;
                }
                return count;
            }
        }

        private int TotalNumberOfProductsFromAllApprovedOrders(List<BuyerApprovedOrderModel> approvedOrders)
        {
            int count = 0;
            foreach(BuyerApprovedOrderModel order in approvedOrders)
            {
                count = count + order.TotalProductQuantity;
            }
            return count;
        }

        private float TotalValueOfAllApprovedOrders(List<BuyerApprovedOrderModel> approvedOrders)
        {
            float count = 0;
            foreach (BuyerApprovedOrderModel order in approvedOrders)
            {
                count = count + order.OrderValue;
            }
            return count;
        }

        private string GetFranchiseName(int franchiseId)
        {
            using (var context = new ApplicationDbContext())
            {
                Franchise franchise = context.Franchise.FirstOrDefault(x => x.Id == franchiseId);

                return franchise.Name;
            }
        }

    }
}