"use strict";

if (invitationsCount != 0) {
    document.getElementById("invitations-badge").removeAttribute("hidden");
}

notifConnectionPromise.then(function () {
    var getNotifsArgs = {
        ChatUserID: viewmodel.UserID,
    };

    notifConnection.invoke("GetNotifications", getNotifsArgs).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

notifConnection.on("ReceiveNotifications", function (list) {
    $.each(list, function () {
        var args = this;

        incrementNotifsCount();

        console.log(args.text);

        var card = document.createElement("div");
        card.setAttribute("class", "card");
        card.setAttribute("id", "notif-" + args.notificationID);
        card.setAttribute("notificationID", args.notificationID);

        var head = document.createElement("div");
        head.setAttribute("class", "card-header");
        card.append(head);

        var title = document.createElement("a");
        title.setAttribute("class", "card-link");
        title.setAttribute("data-toggle", "collapse");
        title.setAttribute("href", "#notif-collapse-" + args.notificationID);
        title.textContent = args.title;
        head.append(title);

        var close = document.createElement("button");
        close.setAttribute("type", "button");
        close.setAttribute("class", "close");
        close.addEventListener("click", function () {
            var id = parseInt(card.getAttribute("notificationID"));
            document.getElementById("notifications-list").removeChild(document.getElementById("notif-" + id));
            decrementNotifsCount();

            var args = {
                ChatUserID: viewmodel.UserID,
                NotificationID: id,
            };

            notifConnection.invoke("RemoveNotification", args);
        });
        close.textContent = "×";
        head.append(close);

        var collapse = document.createElement("div");
        collapse.setAttribute("data-parent", "#notifications-list");
        collapse.setAttribute("class", "collapse");
        collapse.setAttribute("id", "notif-collapse-" + args.notificationID);
        card.append(collapse);

        var body = document.createElement("div");
        body.setAttribute("class", "card-body");
        collapse.append(body);

        var text = document.createElement("p");
        text.setAttribute("class", "card-text");
        text.textContent = args.text;
        body.append(text);

        var link = document.createElement("a");
        link.setAttribute("href", args.viewURL);
        link.setAttribute("class", "card-link");
        //                    link.addEventListener("click", function () {
        //                        var id = parseInt(card.getAttribute("notificationID"));
        //                        document.getElementById("notifications-list").removeChild(document.getElementById("notif-" + id));
        //                        decrementNotifsCount();
        //
        //                        var args = {
        //                            ChatUserID: @Model.ChatUser.Id,
        //                            NotificationID: id
        //                        };
        //
        //                        notifConnection.invoke("RemoveNotification", args);
        //                    });
        link.textContent = "View";
        body.append(link);

        document.getElementById("notifications-list").prepend(card);
    });
});

function decrementInvitationsCount() {
    var node = document.getElementById("invitations-badge");
    var count = parseInt(node.textContent) - 1;

    if (count == 0) {
        node.setAttribute("hidden", "hidden");
    }

    node.textContent = count;
}

function incrementNotifsCount() {
    var node = document.getElementById("notifs-badge");
    var count = parseInt(node.textContent) + 1;

    if (count > 0) {
        node.removeAttribute("hidden");
        document.getElementById("no-notifs").setAttribute("hidden", "hidden");
    }

    node.textContent = count;
}

function decrementNotifsCount() {
    var node = document.getElementById("notifs-badge");
    var count = parseInt(node.textContent) - 1;

    if (count == 0) {
        node.setAttribute("hidden", "hidden");
        document.getElementById("no-notifs").removeAttribute("hidden");
    }

    node.textContent = count;
}