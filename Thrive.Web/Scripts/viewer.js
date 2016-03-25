"use strict";


var canvas;

var playerHubProxy;
var moveTo = { X: 0, Y: 0 };
var game;
var moving = false;
var graphics;

function setupDrawing()
{
    canvas = $('<canvas />')
        .attr('width', 1024)
        .attr('height', 768)
        .width(1024)
        .height(768);
    
    $('#viewContainer')
        .append(canvas)
        .on('mousemove', function (e) {
            moveTo.X = e.clientX;
            moveTo.Y = e.clientY;
        })
        .on('mouseenter', function () {
            moving = true;
            console.log('start move');
        })
        .on('mouseleave', function () {
            moving = false;
            console.log('stop move');
        });

    $(document)
        .on('keydown', function (e) {
            switch (e.which) {
                case 32: // space
                    console.log('split');
                    split();
                    break;
            }
        });

    canvas = canvas[0];
    graphics = canvas.getContext('2d');

    setTimeout(animate, 16);
}

function drawGrid()
{
    graphics.beginPath();
    graphics.lineWidth = 1;
    graphics.strokeStyle = '#cccccc';
    for (var x = 0; x <= canvas.width; x += 40) {
        graphics.moveTo(x, 0);
        graphics.lineTo(x, canvas.width);
    }
    for (var y = 0; y <= canvas.height; y += 40) {
        graphics.moveTo(0, y);
        graphics.lineTo(canvas.width, y);
    }
    graphics.stroke();
}

function animate() {
    // start the timer for the next animation loop
    setTimeout(animate, 16);

    clientFPS++;

    graphics.clearRect(0, 0, canvas.width, canvas.height);
    drawGrid();

    if (game) {
        var actors = game.Actors;
        for (var i = 0; i < actors.length; i++) {
            var actor = actors[i];
            var color;

            switch (actor.Color)
            {
                case 0: color = 'green'; break;
                case 1: color = 'red'; break;
                case 2: color = 'yellow'; break;
            }

            graphics.beginPath();
            graphics.arc(actor.Position.X, actor.Position.Y, actor.Radius, 0, 2 * Math.PI, false);
            graphics.fillStyle = color;
            graphics.fill();
            graphics.lineWidth = 1;
            graphics.strokeStyle = '#003300';
            graphics.stroke();

            if (actor.Note) {
                graphics.font = "30px Arial";
                graphics.textAlign = 'center';
                graphics.fillStyle = 'black';
                graphics.fillText(actor.Note, actor.Position.X, actor.Position.Y);
            }
        }
    }
}

function connect()
{
    var connection = $.hubConnection("https://thrive.violetdata.com/signalr", { useDefaultPath: false });
    playerHubProxy = connection.createHubProxy('playerHub');

    playerHubProxy.on('gameReport', handleGameFrame);

    connection.start().done(function () {
        //contosoChatHubProxy.invoke('newContosoChatMessage', $('#displayname').val(), $('#message').val());
    });
}

function handleGameFrame(state)
{
    game = state;
    var catalog = [];
    catalogObject(game, catalog);
    resolveReferences(game, catalog);
    serverFPS++;
}

setTimeout(reportPosition, 100);
function reportPosition() {
    if (game && moving) {
        playerHubProxy.invoke('Move', game.Players[0].ID, moveTo);
    }
    setTimeout(reportPosition, 100)
}

function split()
{
    playerHubProxy.invoke('Split', game.Players[0].ID);
}

connect();
setupDrawing();

var serverFPS = 0;
var clientFPS = 0;

setTimeout(reportFPS, 1000);
function reportFPS() {
    console.log("serverFPS: " + serverFPS + " clientFPS: " + clientFPS);
    serverFPS = 0;
    clientFPS = 0;
    setTimeout(reportFPS, 1000);
}


function catalogObject(obj, catalog) {
    var i;
    if (obj && typeof obj === 'object') {
        var id = obj.$id;
        if (typeof id === 'string') {
            catalog[id] = obj;
        }
        if (Object.prototype.toString.apply(obj) === '[object Array]') {
            for (i = 0; i < obj.length; i += 1) {
                catalogObject(obj[i], catalog);
            }
        } else {
            for (name in obj) {
                if (typeof obj[name] === 'object') {
                    catalogObject(obj[name], catalog);
                }
            }
        }
    }
}

function resolveReferences(obj, catalog) {
    var i, item, name, id;
    if (obj && typeof obj === 'object') {
        if (Object.prototype.toString.apply(obj) === '[object Array]') {
            for (i = 0; i < obj.length; i += 1) {
                item = obj[i];
                if (item && typeof item === 'object') {
                    id = item.$ref;
                    if (typeof id === 'string') {
                        obj[i] = catalog[id];
                    } else {
                        resolveReferences(item, catalog);
                    }
                }
            }
        } else {
            for (name in obj) {
                if (typeof obj[name] === 'object') {
                    item = obj[name];
                    if (item) {
                        id = item.$ref;
                        if (typeof id === 'string') {
                            obj[name] = catalog[id];
                        } else {
                            resolveReferences(item, catalog);
                        }
                    }
                }
            }
        }
    }
}