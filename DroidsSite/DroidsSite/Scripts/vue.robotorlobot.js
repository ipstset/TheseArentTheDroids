var baseUrl = "http://localhost:49936/";

var vm = new Vue({
    el: '#game',
    data: {
        GameResult: {},
        ImageRootUrl: baseUrl + "content/images/"
    },
    methods: {

        init: function () {
            this.getGame();
        },

        selectCard: function (id) {
            var that = this;
            $.ajax(
               {
                   type: 'POST',
                   url: baseUrl + "robotorlobot/selectCard?id=" + id,
                   dataType: "json",
                   async: false,
                   contentType: "application/json; charset=utf-8",
                   success: function (data) {
                       that.GameResult = data;
                   }
               });
        },

        getGame: function () {
            var that = this;
            $.ajax(
               {
                   type: 'POST',
                   url: baseUrl + "robotorlobot/getgame",
                   dataType: "json",
                   async: false,
                   contentType: "application/json; charset=utf-8",
                   success: function (data) {
                       that.GameResult = data;
                   }
               });


        }
    }


})