"use strict";

//Start SignalR connection
const notifConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notifHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

//On connection established
var notifConnectionPromise = notifConnection.start().then(function () {
    console.log("connected to notification hub");

    notifConnection.invoke("GetNumNewMessages");
}).catch(function (err) {
    return console.error(err.toString());
});

notifConnection.on("NewNotification", function (args) {
    //New Notification
    addBadge();
});

notifConnection.on("ReceiveNumNewMessages", function (args) {
    if (args.num > 0) {
        var newBadge = document.createElement("span");
        newBadge.setAttribute("class", "badge badge-success");
        newBadge.textContent = args.num;

        var node = document.getElementById("message-new");

        if (!node.hasChildNodes()) {
            node.appendChild(newBadge);
        }
    }
});

function addBadge() {
    var newBadge = document.createElement("span");
    newBadge.setAttribute("class", "badge badge-primary");
    newBadge.textContent = "NEW";

    var node = document.getElementById("notif-new");

    if (!node.hasChildNodes()) {
        node.appendChild(newBadge);
    }
}