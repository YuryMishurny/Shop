using System;
using System.Collections.Generic;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Seller seller = new Seller();
            seller.IsWorking();
        }
    }

    class Seller
    {
        private int _moneyBalance;

        private List<Goods> _sellerGoods = new List<Goods>();

        Player buyer = new Player(25);

        public Seller()
        {
            _sellerGoods.Add(new Goods("хлеб", 5));
            _sellerGoods.Add(new Goods("молоко", 10));
            _sellerGoods.Add(new Goods("сыр", 10));
            _sellerGoods.Add(new Goods("печенье", 5));
            _sellerGoods.Add(new Goods("сок", 2));
        }

        private void ShowGoods()
        {
            foreach (var item in _sellerGoods)
            {
                item.ShowInfo();
            }
        }

        private void SellGoods()
        {
            Console.WriteLine("Выберите товар для покупки");
            string userGoods = Console.ReadLine();

            for (int i = 0; i < _sellerGoods.Count; i++)
            {
                if (_sellerGoods[i].Name == userGoods && buyer.IsEnoughMoney(_sellerGoods[i]))
                {
                    Console.WriteLine("Вы успешно приобрели  " + _sellerGoods[i].Name);
                    buyer.BuyGoods(_sellerGoods[i]);
                    _moneyBalance += _sellerGoods[i].Price;
                    _sellerGoods.Remove(_sellerGoods[i]);
                }   
            }
        }

        public void IsWorking()
        {
            bool _isOpen = true;

            while (_isOpen)
            {
                Console.SetCursorPosition(0,23);
                Console.Write("Деньги покупателя:");
                buyer.ShowMoney();
                Console.WriteLine("Корзина его покупок:");
                buyer.ShowGoods();

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("\tМагазин\n"+ "\nВыручка продавца: " +_moneyBalance+ "\n\nТовары доступные для покупки:\n");
                ShowGoods();
                Console.WriteLine("\n1 - произвести покупку\n2 - покинуть магазин\n\nВыберите команду:"); 

                switch(Console.ReadLine())
                {
                    case "1":
                        SellGoods();
                       break;
                    case "2":
                        _isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Такой команды нет");
                        break; 
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу.....");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Player
    {
        private int _money;

        private List<Goods> _playerGoods  =  new List<Goods>(); 

        public Player(int money)
        {
            _money = money;
        }

        public void ShowMoney()
        {
            Console.WriteLine(_money);
        }

        public void ShowGoods()
        {
            if (_playerGoods.Count > 0)
            {
                foreach (var goods in _playerGoods)
                {
                    Console.WriteLine(goods.Name);
                }
            }
            else
                Console.WriteLine("Пусто");
        }

        public void BuyGoods(Goods goods)
        {
                _money -= goods.Price;
                _playerGoods.Add(goods);
        }

        public bool IsEnoughMoney(Goods goods)
        {
            if (_money >= goods.Price)
                return true;
            else
            {
                Console.WriteLine("Недостаточно денег");
                return false;
            }
        }
    }

    class Goods
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Goods(string name, int price)
        {
            Name = name;
            Price = price;

        }
        public void ShowInfo()
        {
            Console.WriteLine("Товар: " + Name + " цена: " + Price);
        }
    }

}
