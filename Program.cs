using System;
using System.Collections.Generic;

namespace DeckCard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Casino casino = new Casino();
            casino.Work();
        }
    }

    class Casino
    {
        private Croupier _croupier = new Croupier();
        private Player _player = new Player();

        public void Work()
        {
            Console.WriteLine("Вы сели за игральный стол.");

            _player.TakeCards(_croupier.GiveCards(ReadNumberCards()));

            Console.WriteLine("У вас в руках:");

            _player.ShowCardsPlayer();
        }

        private int ReadNumberCards()
        {
            int numberCards;

            do
            {
                Console.WriteLine("Сколько карт вы хотите получить?");
                numberCards = ReadInt();
            }
            while (numberCards <= 0);

            return numberCards;
        }

        private int ReadInt()
        {
            int value;

            while (int.TryParse(Console.ReadLine(), out value) == false)
                Console.WriteLine("Это не число.");

            return value;
        }
    }

    class Croupier
    {
        private Deck _deck = new Deck();

        public Croupier()
        {
            CreateCroupierDeck();
        }

        private void CreateCroupierDeck()
        {
            _deck = CreateRandomDeck();
        }

        private Deck CreateRandomDeck()
        {
            string[] suits = { "Spades", "Hearts", "Tiles", "Clover" };
            string[] nameOfCards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            List<Card> cards = new List<Card>();

            for (int i = 0; i < suits.Length; i++)
            {
                string suit = suits[i];

                foreach (var item in nameOfCards)
                {
                    Card card = new Card(suit, item);
                    cards.Add(card);
                }
            }

            Deck deck = new Deck();
            deck.AddCards(cards);

            return deck;
        }

        public List<Card> GiveCards(int numberOfCards)
        {
            List<Card> giveCards = new List<Card> ();
            giveCards = CreateTransmissionList(numberOfCards);
            return giveCards;
        }

        private List<Card> CreateTransmissionList(int numberOfCards)
        {
            return _deck.CreateTransmissionList(numberOfCards);
        }
    }

    class Player
    {
        private List<Card> _cards = new List<Card>();

        public void ShowCardsPlayer()
        {
            foreach (var element in _cards)
            {
                element.ShowCard();
            }

            Console.WriteLine();
        }

        public void TakeCards(List<Card> cards)
        {
            _cards.AddRange(cards);
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public void AddCards(List<Card> cards)
        {
            _cards = cards;
        }

        public List<Card> CreateTransmissionList(int numberOfCards)
        {
            List<Card> transmissionList = new List<Card>();

            for (int i = 0; i < numberOfCards; i++)
            {
                Card removeCard = _cards[i];
                _cards.Remove(_cards[i]);
                transmissionList.Add(removeCard);
            }

            return transmissionList;
        }

        private void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void ShowCards()
        {
            foreach (var element in _cards)
            {
                element.ShowCard();
            }

            Console.WriteLine();
        }
    }

    class Card
    {
        private string _suit;
        private string _nameOfCard;

        public Card(string suit, string nameOfCard)
        {
            _suit = suit;
            _nameOfCard = nameOfCard;
        }

        public void ShowCard()
        {
            Console.WriteLine($"У вас в руках: {_suit}{_nameOfCard}");
        }
    }
}
