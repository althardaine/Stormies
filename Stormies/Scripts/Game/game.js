﻿
window.onload = function () {

    var gameHub = $.connection.gameHub;

    var gameArea = new Phaser.Game(800, 600, Phaser.AUTO, "phaser-example", { preload: preload, create: create, update: update });
    var myId;
    var map;
    var layer;
    var players = {};
    var wKey, sKey, aKey, dKey, oneKey;
    var sounds = {};

    gameHub.client.youJoined = function (id, gameState) {
        myId = id;
        $("#players").empty();
        for (var playerId in gameState.Players) {
            players[playerId] = gameArea.add.sprite(gameState.Players[playerId].PositionX, gameState.Players[playerId].PositionY, "player");
            players[playerId].anchor.set(0.5);
            players[playerId].angle = gameState.Players[playerId].Angle;
            $("#players").append("<li>" + htmlEncode(gameState.Players[playerId].Name) + " " + htmlEncode(playerId) + "</li>");
        };
        $("#yourHealth").append(htmlEncode(gameState.Players[myId].Health));
        var player = players[myId];
        gameArea.camera.follow(player);
    };

    gameHub.client.playerJoined = function (playerId, player) {
        $("#players").append("<li>" + htmlEncode(player.Name + " " + playerId) + "</li>");
        if (playerId !== myId) {
            players[playerId] = gameArea.add.sprite(player.PostionX, player.PositionY, "player");
            players[playerId].anchor.set(0.5);
        }
    }

    gameHub.client.playerLeft = function (gameState, id) {
        $("#players").empty();
        for (var playerId in gameState.Players) {
            $("#players").append("<li>" + htmlEncode(gameState.Players[playerId].Name) + " " + htmlEncode(playerId) + "</li>");
        };
        players[id].destroy();
        delete players[id];
    };

    gameHub.client.passErrorMessage = function (message) {
        alert(message);
    };

    gameHub.client.playerMoved = function (playerId, player) {
        if (playerId in players) {
            players[playerId].x = player.PositionX;
            players[playerId].y = player.PositionY;
            players[playerId].angle = player.Angle;
        }
    }

    gameHub.client.playerUsedSkill = function (playerId, gameState, animation, sound) {
        $("#yourHealth").empty();
        $("#yourHealth").append(htmlEncode(gameState.Players[myId].Health));
        var player = gameState.Players[playerId];
        var skill = gameArea.add.sprite(player.PositionX, player.PositionY, animation);
        skill.anchor.set(0.5);
        skill.angle = player.Angle;
        var skillAnimation = skill.animations.add("skillAnimation");
        skillAnimation.killOnComplete = true;
        skill.animations.play("skillAnimation", 20, false);
        sounds[sound].play();
    }


    function preload() {
        gameArea.load.tilemap("stormies", "../../Map/StormiesMap.json", null, Phaser.Tilemap.TILED_JSON);
        gameArea.load.image("tiles", "../../Tiles/stormies.png");
        gameArea.load.image("player", "../../Tiles/warrior.png");
        gameArea.load.spritesheet("slashAnimation", "../../Tiles/slash.png", 160, 160, 5);
        gameArea.load.audio("slashSound", "../../Sound/slash.mp3");
    }

    function create() {
        gameArea.stage.backgroundColor = "#787878";

        map = gameArea.add.tilemap("stormies");
        map.addTilesetImage("StormiesTiles", "tiles");

        sounds["slashSound"] = gameArea.add.audio("slashSound");


        layer = map.createLayer("World1");
        layer.resizeWorld();
        layer.wrap = true;
    }

    function update() {

        if (myId in players) {
            if (aKey.isDown) {
                gameHub.server.rotateLeftRequest();
            } else if (dKey.isDown) {
                gameHub.server.rotateRightRequest();
            }
            if (wKey.isDown) {
                gameHub.server.moveForewardRequest();
            } else if (sKey.isDown) {
                gameHub.server.moveBackwardRequest();
            }

            if (oneKey.isDown) {
                gameHub.server.useSkillRequest(0);
            }
        }
    }


    $.connection.hub.start().done(function () {
        $("#joinGame").click(function () {
            if ($("#playerName").val() !== "") {
                wKey = gameArea.input.keyboard.addKey(Phaser.Keyboard.W);
                sKey = gameArea.input.keyboard.addKey(Phaser.Keyboard.S);
                aKey = gameArea.input.keyboard.addKey(Phaser.Keyboard.A);
                dKey = gameArea.input.keyboard.addKey(Phaser.Keyboard.D);
                oneKey = gameArea.input.keyboard.addKey(Phaser.Keyboard.ONE);
                gameHub.server.joinRequest($("#playerName").val());
            }
        });
        $(window).bind("beforeunload", function () {
            gameHub.server.leaveRequest();
        });
    });
}


function htmlEncode(value) {
    var encodedValue = $("<div />").text(value).html();
    return encodedValue;
}