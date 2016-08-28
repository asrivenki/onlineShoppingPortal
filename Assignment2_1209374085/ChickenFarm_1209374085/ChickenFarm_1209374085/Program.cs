using System.Text;
using System.Threading;
using System.Collections;
using ChickenFarm_1209374085.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;



namespace ChickenFarm_1209374085
{
    public delegate void orderCompletedEvent(Order orderObj, Double amountOfCharge);
    public delegate void priceCutEvent(double price, double previousPrice, int counter);


    class Time_Thread
    {
        private static Int32 currentTime;
        public Time_Thread()
        {
            currentTime = 0;
        }
        public void start()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                if (currentTime == 23)
                    currentTime = 0;
                else
                    currentTime++;
                //             Console.WriteLine(currentHour);
            }
        }
        public static Int32 getcurrentHour()
        {
            return currentTime;
        }
    }

    public class pricecutLock
    {
        public static Object obj = new Object();
        public static AutoResetEvent Event = new AutoResetEvent(false);
    }



    public class Retailer
    {
        public static ChickenSupplier ChickenSupplier_supply = new ChickenSupplier();
        public static AutoResetEvent[] Event = new AutoResetEvent[5];
        public static int pricecutLockCounter = 0;
        public static DoubleValue ChickenPrice = new DoubleValue();


        int amountOfChicken;
        int[] orderIdsOfAgency = new int[5];


        public void receiveTime(Order orderObj, Double amountOfCharge)
        {
            orderObj.setReceiveTime(DateTime.Now);
            Console.WriteLine();
            Console.WriteLine("Order : {0}, Retialer : {1}, unitprice of Chicken: {2}, Weight of Chicken: {3}, Amount of Charge: {4}, Start Time: {5}, Completed Time: {6}, Time taken to process the order: {7}", orderObj.getOrderId(), orderObj.getsender() + 1, orderObj.getunitprice().ToString("f0"), orderObj.getAmt(), amountOfCharge.ToString("f0"), orderObj.getSenderTime(), orderObj.getReceiveTime(), (orderObj.getReceiveTime() - orderObj.getSenderTime()));
            pricecutLockCounter++;
            if (pricecutLockCounter == 5)
            {
                pricecutLock.Event.Set();
                pricecutLockCounter = 0;

            }
        }

        //Event Handler
        public void chickenAvailable(double price, double previousPrice, int counter)
        {
            ChickenPrice.previousPrice = previousPrice;
            ChickenPrice.price = price;
            // each time PriceCut event is triggered, retailer threads will be released to send orders
            for (int j = 0; j < 5; j++)
                Event[j].Set();

            Console.WriteLine();
            if (counter < 5)
                Console.WriteLine("           No.{0} New Price-cut Availble {1}, {2} Off From the Previous Price {3}", counter + 1, price.ToString("f0"), (previousPrice - price).ToString("f4"), previousPrice.ToString("f0"));
        }

        public void retailerFunc()
        {
            Int32 currentThreadName = Convert.ToInt32(Thread.CurrentThread.Name);
            ChickenSupplier_supply.registerCreditCardNumber(currentThreadName);
            Event[currentThreadName] = new AutoResetEvent(false);

            while (true)
            {
                int eventIndex;
                Int32 threadName, _orderIds;
                int.TryParse(Thread.CurrentThread.Name, out eventIndex);
                Event[eventIndex].WaitOne();
                Order orderObj = new Order();

                amountOfChicken = calculateChickenAmount();
                threadName = Convert.ToInt32(Thread.CurrentThread.Name);
                _orderIds = ++orderIdsOfAgency[threadName];
                orderObj.setAmt(amountOfChicken);
                orderObj.setcardNumber(threadName + 5000);
                orderObj.setsender(threadName);
                orderObj.setOrderId(_orderIds);
                orderObj.setSenderTime(DateTime.Now);
                //                Thread.Sleep(1000);
                orderObj.setunitprice(ChickenPrice.price);
                String str = encode_decode.encoder(orderObj);

                Program.multicellbuffer.setOneCell(str);
            }
        }

       
        // calculate and return the amount of chicken to order
        public int calculateChickenAmount()
        {
            // set a fixed value as a basic amount
            amountOfChicken = 100;

            // The more price cut, the more chickens to order
            amountOfChicken = (int)(amountOfChicken * (ChickenPrice.previousPrice / ChickenPrice.price));
            return amountOfChicken;
        }

        
    }

    public class Order
    {
        private Int32 sender, cardNumber, amountOfChicken, orderId;
        private double unitprice;
        private DateTime senderTime, receiveTime;
        public Order()
        {
            amountOfChicken = 0;
            orderId = 0;
            unitprice = 0;
            sender = 0;
            cardNumber = 0;

            receiveTime = new DateTime();
            senderTime = new DateTime();

        }

        public void setunitprice(double unit_price)
        {
            unitprice = unit_price;
        }

        public double getunitprice()
        {
            return unitprice;
        }

        public void setOrderId(int order_Id)
        {
            orderId = order_Id;
        }

        public int getOrderId()
        {

            return orderId;
        }

        public Int32 getsender()
        {

            return sender;
        }

        public void setsender(int sender_id)
        {
            this.sender = sender_id;
        }

        public Int32 getAmt()
        {
            int _amountOfChicken = amountOfChicken;
            return _amountOfChicken;
        }


        public void setSenderTime(DateTime sender_Time)
        {
            senderTime = sender_Time;
        }

        public DateTime getReceiveTime()
        {
            DateTime receive_Time = receiveTime;
            return receive_Time;
        }
        public void setAmt(Int32 _amountOfChicken)
        {
            this.amountOfChicken = _amountOfChicken;
        }

        public DateTime getSenderTime()
        {
            DateTime _senderTime = senderTime;
            return _senderTime;
        }

        public Int32 getcardNumber()
        {
            int _cardNumber = cardNumber;
            return _cardNumber;
        }

        public void setcardNumber(Int32 _cardNumber)
        {
            this.cardNumber = _cardNumber;
        }
        public void setReceiveTime(DateTime _receiveTime)
        {
            receiveTime = _receiveTime;
        }


    }


    public class ChickenSupplier
    {
        public static event priceCutEvent priceCutevent;


        private DateTime finishingTime;
        private double taxRate = 0.5;
        private double locationCharge = 15;
        private double VAT_tax = 0.45;
        private static ArrayList ChickenPriceList;
        public static Int32 counter;
        private static ArrayList ListOfCreditCardNumbers;
        private DateTime startingTime;
        private static DoubleValue ChickenPrice = new DoubleValue();

        public static event orderCompletedEvent orderCompleted;


        public void PricingModel()
        {
            Time_Thread Time_Thread = new Time_Thread();
            Thread Time_ThreadThread = new Thread(new ThreadStart(Time_Thread.start));
            Time_ThreadThread.Start();
            Int32 time;

            int orderIndex = 0;
            startingTime = DateTime.Now;
            while (counter < 5)
            {
                Thread.Sleep(1000);
                time = Time_Thread.getcurrentHour();
                double current_price = ChickenPrice.price * (1 + Math.Cos(time) / 5.0);
                changePrice(current_price);
                orderIndex++;
            }
            finishingTime = DateTime.Now;
            Console.WriteLine("          Total Time Used by current ChickenSupplier Thread: {0}.", finishingTime - startingTime);
        }

        public ChickenSupplier()
        {
            counter = 0;
            ChickenPrice.previousPrice = 150;
            ChickenPrice.price = 150;
            ChickenPriceList = new ArrayList();
            ListOfCreditCardNumbers = new ArrayList();
        }

     
        public static void changePrice(double price)
        {
            ChickenPrice.previousPrice = ChickenPrice.price;
            ChickenPrice.price = price;
            if (price < ChickenPrice.previousPrice)
            {
                if (priceCutevent != null)
                {
                    priceCutevent(ChickenPrice.price, ChickenPrice.previousPrice, counter);
                    pricecutLock.Event.WaitOne();
                }
                counter++;
            }
        }

        

        public void orderProcessing()
        {

            String orderString;
            while (true)
            {
                orderString = Program.multicellbuffer.getOneCell();
                Order orderObj = encode_decode.decoder(orderString);

                //Locking ChickenPrice 
                Monitor.Enter(ChickenPrice);
                if (ChickenSupplier.checkCreditCardNumberValidity(orderObj.getcardNumber()))
                {
                    double charge = orderObj.getunitprice() * orderObj.getAmt() + taxRate * orderObj.getunitprice() * orderObj.getAmt() * VAT_tax + taxRate * orderObj.getunitprice() * orderObj.getAmt() + locationCharge;
                    orderObj.setReceiveTime(DateTime.Now);
                    if (orderCompleted != null)
                        orderCompleted(orderObj, charge);
                }
                Monitor.Exit(ChickenPrice);

            }
        }

        public static double getPrice()
        {
            return ChickenPrice.price;
        }


        public void registerCreditCardNumber(Int32 retailerId)
        {
            Int32 creditCardNumber = 5000 + retailerId;
            ListOfCreditCardNumbers.Add(creditCardNumber);
        }

        public static double getPreviousPrice()
        {
            return ChickenPrice.previousPrice;
        }


        public static Boolean checkCreditCardNumberValidity(Int32 creditCardNumber)
        {
            if (ListOfCreditCardNumbers.Contains(creditCardNumber))
                return true;
            else
                return false;
        }

        public static Int32 getCreditCardNumber(Int32 retailerId)
        {
            if (ListOfCreditCardNumbers.Contains(5000 + retailerId))
                return 5000 + retailerId;
            else
                return 0;
        }
    }

    public class MultiCellBuffer
    {
        static int no_of_buffers = 2;
        static Semaphore empty_semaphore = new Semaphore(no_of_buffers, no_of_buffers);
        static Semaphore full_semaphore = new Semaphore(0, no_of_buffers);
        private OrderInformation[] buffer;

        static Object mutex = new Object();
        static int index = 0;

        public MultiCellBuffer()
        {
            // There are 3 multi cell buffers
            buffer = new OrderInformation[no_of_buffers];
            for (int i = 0; i < no_of_buffers; i++)
            {
                buffer[i] = new OrderInformation();
                
            }
        }

        
        public string getOneCell()
        {
            full_semaphore.WaitOne();
            string temp;
            //Locking the mutex
            lock (mutex)
            {
                index--;
                temp = buffer[index].str;


            }

            empty_semaphore.Release();

            return temp;
        }

        public void setOneCell(string str)
        {
            empty_semaphore.WaitOne();

            //Locking the mutex
            lock (mutex)
            {

                buffer[index].str = str;
                index++;
            }


            full_semaphore.Release();
        }

    }


    class encode_decode
    {
        public static String encoder(Order order)
        {
            if (order != null)
            {
                String orderId = order.getOrderId().ToString() + "|";
                String sender = order.getsender().ToString() + "|";
                String cardNumber = order.getcardNumber().ToString() + "|";
                String unitprice = order.getunitprice().ToString() + "|";
                String amountOfChicken = order.getAmt().ToString() + "|";
                String senderTime = order.getSenderTime().ToString() + "|";
                String str = orderId + sender + cardNumber + unitprice + amountOfChicken + senderTime;
                ChickenFarm_1209374085.ServiceReference1.ServiceClient encrypt = new ChickenFarm_1209374085.ServiceReference1.ServiceClient();
                //               Console.WriteLine("string: {0}, encoded: {1}", order, encrypt.Encrypt(str));
                return encrypt.Encrypt(str);
            }
            else
            {
                return "No order!";
            }
        }
        public static Order decoder(String encodedString)
        {
            if (encodedString != null)
            {
                ChickenFarm_1209374085.ServiceReference1.ServiceClient decrypt = new ChickenFarm_1209374085.ServiceReference1.ServiceClient();
                String decodedString = decrypt.Decrypt(encodedString);
                //Console.WriteLine(decodedString);
                // int index = 0;
                String date_string = "";
                char[] delimiterChars = { '|' };
                String[] split = decodedString.Split(delimiterChars);



                int sender = Int32.Parse(split[1]);
                int cardNumber = Int32.Parse(split[2]);
                int orderId = Int32.Parse(split[0]);
                double unitprice = Double.Parse(split[3]);
                int ChickenQuantity = Int32.Parse(split[4]);
                date_string = split[5];
                DateTime senderTime = Convert.ToDateTime(date_string);
                //Console.WriteLine("OrderId: {0} sender: {1} cardNumber: {2} Amount: {3} Date: {4}", orderId, sender, cardNumber, ChickenQuantity, senderTime);
                Order order = new Order();
                order.setcardNumber(cardNumber);

                order.setsender(sender);
                order.setunitprice(unitprice);
                order.setAmt(ChickenQuantity);
                order.setOrderId(orderId);
                order.setSenderTime(senderTime);
                return order;
            }
            else
            {
                Console.WriteLine("Null string!");
                return null;
            }
        }
    }



    public class DoubleValue
    {
        public double previousPrice;
        public double price;

    }

    class OrderInformation
    {
        public bool flag = false;
        public string str = "";

    }

    class Program
    {
        public static MultiCellBuffer multicellbuffer = new MultiCellBuffer();

        static void Main(string[] args)
        {
            Retailer retailer = new Retailer();


            // Pricing Model generation
            Thread calculateChickenPrice = new Thread(new ThreadStart(Retailer.ChickenSupplier_supply.PricingModel));
            calculateChickenPrice.Start();

            // start a thread using orderProcessing method to receive orders from Retailers and process
            // the orders, if an order is completed, trigger the orderCompleted event
            Thread order = new Thread(new ThreadStart(Retailer.ChickenSupplier_supply.orderProcessing));
            order.Start();

            // method(event handler) chickenAvailable(..) subscripts priceCut event
            ChickenSupplier.priceCutevent += new priceCutEvent(retailer.chickenAvailable);

            // method receiveTime(..) subscripts orderCompleted event
            ChickenSupplier.orderCompleted += new orderCompletedEvent(retailer.receiveTime);

            // start 5 Chicken Suppliers thread
            Thread[] retailers = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                retailers[i] = new Thread(new ThreadStart(retailer.retailerFunc));
                // evaluate Chicken supplier thread name from 1 to 5

                retailers[i].Name = (i).ToString();
                retailers[i].Start();
            }

            while (true)
            {
                // terminate getOrder thread and all Chicken supplier thread when calculateChickenPrice terminated
                if (!calculateChickenPrice.IsAlive)
                {
                    order.Abort();
                    for (int i = 0; i < 5; i++)
                        retailers[i].Abort();
                }
            }

        }
    }
}