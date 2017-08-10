using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipstset.Authentication;
using Ipstset.Droids;
using Microsoft.SqlServer.Server;

namespace DroidsSite.Controllers
{
    public class RobotOrLobotController : Controller
    {

        private GameService _gameService;

        public RobotOrLobotController()
        {
            _gameService = new GameService(MvcApplication.DroidsConnection);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGame()
        {
            var userId = SessionUserIdentity.UserIdentity.IdentityToken;
            var game = _gameService.GetGameResult(userId);
            return Json(game, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectCard(string id)
        {
            var userId = SessionUserIdentity.UserIdentity.IdentityToken;
            var response = _gameService.SelectCard(id,userId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCards()
        {
            var repository = new GameRepository(MvcApplication.DroidsConnection);
            var cards = repository.GetCardInfos();
            return Json(cards, JsonRequestBehavior.AllowGet);
        }

        //tests
        public ActionResult SimpleGame(string cardId = "")
        {
            var userId = SessionUserIdentity.UserIdentity.IdentityToken;
            var model = _gameService.SelectCard(cardId, userId);

            return View(model);
        }


    }
}
