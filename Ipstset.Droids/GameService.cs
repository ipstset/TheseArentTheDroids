using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.Droids
{
    public class GameService
    {
        private GameRepository _gameRepository;
        private Player _currentPlayer;
        private string _backImage = "back.png";
        public GameService(string connection)
        {
            _gameRepository = new GameRepository(connection);
        }

        //retrieve player from Id
        public GameResult GetGameResult(string userId)
        {
            var gameResult = _gameRepository.GetLastGame(userId);
            _currentPlayer = gameResult != null ? gameResult.Player : new Player {Id = userId};
            if (gameResult == null || gameResult.GameOver)
            {
                //create new game
                gameResult = CreateRobotOrLobotGame();
            }

            PrepareCardsForView(gameResult.Cards);
            return gameResult;
        }

        public GameResult SelectCard(string cardId, string userId)
        {
            //get persisted game result
            var gameResult = _gameRepository.GetLastGame(userId);
            if (gameResult == null || gameResult.GameOver)
                return GetGameResult(userId);

            var selected = gameResult.Cards.FirstOrDefault(c => c.Id == cardId);
            if (selected != null)
            {
                selected.IsFaceUp = true;
                gameResult.GameMessage = "You chose " + selected.Name;
            }
                
            if (gameResult.Cards.Any(c => c.Name == "Lobot" && c.IsFaceUp))
            {
                gameResult.GameOver = true;
                gameResult.PlayerWon = false;
                gameResult.Player.Losses += 1;
                gameResult.Player.Streak = 0;
            }

            if (!gameResult.GameOver &&
                gameResult.Cards.Count(c => c.IsFaceUp) >= gameResult.Cards.Count - 1)
            {
                //game over, playr wins
                gameResult.GameOver = true;
                gameResult.PlayerWon = true;
                gameResult.Player.Wins += 1;
                gameResult.Player.Streak += 1;

                if (gameResult.Player.Streak > gameResult.Player.LongestStreak)
                    gameResult.Player.LongestStreak = gameResult.Player.Streak;
            }

            if(gameResult.GameOver)
            {
                //flip all cards over
                foreach (var card in gameResult.Cards)
                    card.IsFaceUp = true;
            }

	        //persist changes to gameresult
            _gameRepository.InsertGameResult(gameResult);
	
	        //return GetGameResultWithNoNames();
            PrepareCardsForView(gameResult.Cards);
            return gameResult;
        }

        private void PrepareCardsForView(List<Card> cards)
        {
            foreach (var card in cards.Where(c=>!c.IsFaceUp))
            {
                card.Name = "";
                card.Image = _backImage;
            }
                
        }

        private GameResult CreateRobotOrLobotGame()
        {
            var game = new GameResult();
            game.Id = Guid.NewGuid().ToString();
            game.GameTypeId = 2;
            //cards
            game.Cards = new List<Card>();
            var allCards = _gameRepository.GetCardInfos();
            var lobotCard = allCards.First(c => c.CardInfoId == 1);
            allCards.Remove(lobotCard);
            //shuffle and take 4 from cards
            allCards.Shuffle();
            var deck = allCards.Take(4).ToList();
            //add lobot and shuffle again
            deck.Add(lobotCard);
            deck.Shuffle();

            foreach (var card in deck)
            {
                game.Cards.Add(new Card
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = card.Name,
                    Image = card.Image
                });
            }

            //player
            //get player info...get data from last game result
            game.Player = _currentPlayer;
            game.Date = DateTime.Now;

            //persist changes to gameresult
            _gameRepository.InsertGameResult(game);

            return game;

        }
    }
}
