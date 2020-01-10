"use strict";

// **************************************
// Declare 'viewmodel' constant elsewhere
// **************************************

var isMobile = isMobile();

//Get the sound file
let sound = new Audio('/media/new-message.mp3');
var play_sound = false;

//The number of messages currently displayed. Used for getting previous messages.
var numMessages = 0;

var currentGroupID = -1;
var membershipID = -1;

function isMobile() {
    if (window.innerWidth <= 800 && window.innerHeight <= 600) {
        return true;
    } else {
        return false;
    }
}

function appendMessage(message) {
    var messagesList = $("#messages-list");
    var messagesListContainer = $("#messages-list-container");

    messagesList.append(message);
    messagesListContainer.scrollTop(messagesList[0].scrollHeight);
}

function prependMessage(message) {
    var messagesList = $("#messages-list");
    var messagesListContainer = $("#messages-list-container");

    messagesList.prepend(message);
    messagesListContainer.scrollTop(messagesList[0].scrollHeight);
}

function getMessageFromArgs(args) {
    var message = document.createElement("div");
    message.setAttribute("class", "message");

    var message_image_inner1 = document.createElement("div");
    message_image_inner1.setAttribute("class", "message-image-inner");
    message.append(message_image_inner1);

    var message_image = document.createElement("div");
    message_image.setAttribute("class", "message-image");
    message_image_inner1.append(message_image);

    var message_image_inner2 = document.createElement("div");
    message_image_inner2.setAttribute("class", "message-image-inner");
    message_image.append(message_image_inner2);

    var image = document.createElement("img");
    image.setAttribute("src", args.userImage);
    image.setAttribute("width", 32);
    image.setAttribute("height", 32);
    image.setAttribute("class", "rounded-circle img");
    message_image_inner2.append(image);

    var container = document.createElement("div");
    container.setAttribute("class", "message-container");
    message.append(container);

    var header = document.createElement("div");
    header.setAttribute("class", "message-header");
    container.append(header);

    var username = document.createElement("span");
    username.setAttribute("class", "message-username");
    username.textContent = args.userName;
    header.append(username);

    var _break = document.createElement("span");
    _break.innerHTML = "&nbsp;&nbsp;&nbsp;";
    header.append(_break);

    var timestamp = document.createElement("span");
    timestamp.setAttribute("class", "message-timestamp text-muted");
    timestamp.textContent = args.timestamp;
    header.append(timestamp);

    var content = document.createElement("span");
    content.setAttribute("class", "message-content");
    content.textContent = args.message;
    container.append(content);

    return message;
}

function clearMessages() {
    $("#messages-list").empty();
}

function userConnected(userId, userName, userImage, userRankName) {
    var container = document.createElement("li");
    container.setAttribute("id", "user-" + userId);
    container.setAttribute("class", "user-data-container list-group-item");

    var img = document.createElement("img");
    img.setAttribute("src", userImage);
    if (isMobile) {
        img.setAttribute("width", "16");
        img.setAttribute("height", "16");
    } else {
        img.setAttribute("width", "32");
        img.setAttribute("height", "32");
    }
    img.setAttribute("class", "rounded-circle img");
    container.appendChild(img);

    var name = document.createElement("span");
    name.setAttribute("class", "user-data-name");
    if (isMobile) {
        hide(name);
    }
    name.textContent = userName + " ";
    container.appendChild(name);

    var rank = document.createElement("span");
    rank.setAttribute("class", "badge badge-dark text-wrap");
    if (isMobile) {
        hide(rank);
    }
    rank.textContent = "    " + userRankName;
    container.appendChild(rank);

    document.getElementById("online-members-list").appendChild(container);
    var count = document.getElementById("current-group-online-count");
    count.textContent = parseInt(count.textContent) + 1;
}

function userDisconnected(userId) {
    document.getElementById("online-members-list")
        .removeChild(document.getElementById("user-" + userId));
    var count = document.getElementById("current-group-online-count");
    count.textContent = parseInt(count.textContent) - 1;
}

function userActive(userId) {
    document.getElementById("user-" + userId)
        .setAttribute("class", "list-group-item user-data-container");
}

function userInactive(userId) {
    document.getElementById("user-" + userId)
        .setAttribute("class", "list-group-item list-group-item-primary user-data-container user-data-container-inactive");
}

function clearUsers() {
    $("#online-members-list").empty();
}

function userTyping(userId, userProfileImage) {

}

function userNotTyping(userId) {

}

function clearTyping() {

}

function updateGroupData(groupID, groupName, numUsers) {
    var name = document.getElementById("current-group-name");
    name.textContent = groupName;
    name.setAttribute("href", "/Group?groupID=" + groupID);
    var members = document.getElementById("current-group-members-count");
    members.textContent = numUsers;
    var online = document.getElementById("current-group-online-count");
    online.textContent = "1";
    if (isMobile) {
        hide(document.getElementById("current-group-members"));
        hide(document.getElementById("current-group-online"));
        hide(members);
        hide(online);
    } else {
        show(document.getElementById("current-group-members"));
        show(document.getElementById("current-group-online"));
        show(members);
        show(online);
    }
}

function clearData() {
    var name = document.getElementById("current-group-name");
    name.textContent = "No Group Selected";
    name.setAttribute("groupID", -1);
    document.getElementById("current-group-members-count").textContent = "N/A";
    document.getElementById("current-group-online-count").textContent = "N/A";
}

//Clears messages, clears users, disconnects from the current group, and resets data.
function clear() {
    clearMessages();
    clearUsers();
    clearTyping();
    clearData();

    if (membershipID != -1) {
        var args = {
            MembershipID: membershipID
        };

        connection.invoke("DisconnectedFromGroup", args);
    }

    numMessages = 0;
    currentGroupID = -1;
    membershipID = -1;
}

function newGroupMessage(groupId, numNew) {
    var node = document.getElementById("group-data-new-" + groupId);
    node.textContent = numNew;
    show(node);
}

function removeNewGroupMessage(groupId) {
    hide(document.getElementById("group-data-new-" + groupId));
}

function switchGroupTo(groupId, membershipId) {
    clear();

    currentGroupID = groupId;
    membershipID = membershipId;

    var args = {
        MembershipID: membershipID
    };

    connection.invoke("ConnectedToGroup", args);
    connection.invoke("ActiveInGroup", args);

    removeNewGroupMessage(groupId);
}

function addGroup(groupId, groupImage, numNew, groupName, membershipId) {
    var container = document.createElement("li");
    container.setAttribute("class", "group-data-container list-group-item");
    container.setAttribute("id", "group-" + groupId);
    container.setAttribute("groupId", groupId);
    container.setAttribute("membershipId", membershipId);
    container.addEventListener("click", function () {
        switchGroupTo(
            parseInt(container.getAttribute("groupId")),
            parseInt(container.getAttribute("membershipId"))
        );
    });

    var img = document.createElement("img");
    img.setAttribute("src", groupImage);
    if (isMobile) {
        img.setAttribute("width", "16");
        img.setAttribute("height", "16");
    } else {
        img.setAttribute("width", "32");
        img.setAttribute("height", "32");
    }
    img.setAttribute("class", "rounded-circle img");
    container.appendChild(img);

    var name = document.createElement("span");
    name.setAttribute("class", "group-data-name");
    if (isMobile) {
        hide(name);
    }
    name.textContent = groupName + " ";
    container.appendChild(name);


    var newBadge = document.createElement("span");
    newBadge.setAttribute("id", "group-data-new-" + groupId);
    newBadge.setAttribute("class", "badge badge-success");
    hide(newBadge);
    newBadge.textContent = "NEW";
    container.appendChild(newBadge);

    var groupsList = document.getElementById("groups-list");
    groupsList.append(container);

    if (numNew > 0) {
        newGroupMessage(groupId, numNew);
    }
}

function removeGroup(groupId) {
    document.getElementById("groups-list")
        .removeChild(document.getElementById("group-" + groupId));

    if (groupId == currentGroupID) {
        clear();
    }
}

function loadPreviousMessages(startIndex, count) {
    var args = {
        MembershipID: membershipID,
        StartIndex: startIndex,
        Count: count
    };

    connection.invoke("GetPreviousMessages", args);
}

if (isMobile) {
    show(document.getElementById("left-header-mobile"));
    hide(document.getElementById("left-header-desktop"));
} else {
    hide(document.getElementById("left-header-mobile"));
    show(document.getElementById("left-header-desktop"));
}

//Start SignalR connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.onclose(function (e) {
    var head = document.getElementById("head-row");

    var node = document.createElement("div");
    node.setAttribute("class", "alert alert-danger w-100");
    node.setAttribute("role", "alert");
    node.textContent = "Your connection has closed. Refresh the page to reconnect.";

    head.append(node);
});

//On connection established
connection.start().then(function () {
    console.log("connected to chat hub");

    var connectionArgs = {
        UserID: viewmodel.UserID
    };

    //Send the connection event
    connection.invoke("Connected", connectionArgs).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

let isTypingCallback = debounce(
    function () {
        var args = {
            MembershipID: membershipID
        };
        connection.invoke("UserTyping", args);
    },
    600,
    true);

//On Disconnect
window.addEventListener('unload', function (event) {
    clear();

    var args = {
        UserID: viewmodel.UserID
    };

    //Send the disconnection event
    connection.invoke("Disconnected", args);
});

//On Focus
window.addEventListener('focus', function (event) {
    if (membershipID != -1) {
        var args = {
            MembershipID: membershipID
        };

        //Send the active event
        connection.invoke("ActiveInGroup", args);
    }

    play_sound = false;

});

//On Blur
window.addEventListener('blur', function (event) {
    if (membershipID != -1) {
        var args = {
            MembershipID: membershipID
        };

        //Send the active event
        connection.invoke("InactiveInGroup", args);
    }

    play_sound = true;

});

//Load Previous Messages
document.getElementById("load-previous-messages-button").addEventListener("click", function (event) {
    if (membershipID != -1) {
        loadPreviousMessages(numMessages, 50);
    }
});

//Receive Previous Messages
connection.on("ReceivePreviousMessages", function (messages) {
    $.each(messages, function () {
        var args = this;
        prependMessage(getMessageFromArgs(args));
        numMessages++;
    });
});

//Receive Message
connection.on("ReceiveMessage", function (args) {
    if (args.groupID == currentGroupID) {
        appendMessage(getMessageFromArgs(args));
        numMessages++;
    } else {
        newGroupMessage(args.groupID);
    }

    if ((args.UserID != viewmodel.UserID)) {
        if (play_sound) {
            sound.play();
        }

        var node = document.getElementById("group-" + args.groupID);
        var parent = document.getElementById("groups-list");

        parent.removeChild(node);
        parent.prepend(node);
    }

});

//Send Message
document.getElementById("message-input").addEventListener("keydown", function (event) {
    var element = document.getElementById("message-input");
    var message = element.textContent;

    if (message.trim() == "") {
        
    } else {
        if (event.key == "Enter" && !event.shiftKey && currentGroupID != -1) {
            var args = {
                MembershipID: membershipID,
                MinRank: 0,
                Message: message,
                Multimedia: null,
            };
            connection.invoke("SendMessage", args).catch(function (err) {
                return console.error(err.toString());
            });

            element.textContent = "";

            event.preventDefault();
        }
    }
})

//Typing Indicator
document.getElementById("message-input").addEventListener("keydown", isTypingCallback);

connection.on("ReceiveGroupData", function (args) {
    if (args.groupID == currentGroupID) {
        updateGroupData(args.groupID, args.groupName, args.numUsers);
    }
});

//On User Connected to system. May not be for the active group!
connection.on("OtherUserConnectedToGroup", function (args) {
    if (args.userID != parseInt(viewmodel.UserID)) {
        if (args.groupID == currentGroupID) {
            userConnected(args.userID, args.userName, args.userImage, args.userRank);
        }
    }
});

//On User Disconnected from system.
connection.on("OtherUserDisconnectedFromGroup", function (args) {
    if (args.userID != parseInt(viewmodel.UserID)) {
        if (args.groupID == currentGroupID) {
            userDisconnected(args.userID);
        }
    }
});

//On User become Active
connection.on("OtherUserActiveInGroup", function (args) {
    if (args.userID != parseInt(viewmodel.UserID)) {
        if (args.groupID == currentGroupID) {
            userActive(args.userID);
        }
    }
});

//On User become Inactive
connection.on("OtherUserInactiveInGroup", function (args) {
    if (args.userID != parseInt(viewmodel.UserID)) {
        if (args.groupID == currentGroupID) {
            userInactive(args.userID);
        }
    }
});

connection.on("AddGroup", function (args) {
    addGroup(args.groupID, args.groupImage, args.numNew, args.groupName, args.membershipID);
});

connection.on("RemoveGroup", function (args) {
    removeGroup(args.groupID);
});

connection.on("ClientBannedFromGroup", function (args) {
    removeGroup(args.groupID);
});

connection.on("OtherUserTyping", function (args) {
    if (args.groupID == currentGroupID) {
        userTyping(args.userID, args.userProfileImage);
    }
});

connection.on("OtherUserNotTyping", function (args) {
    if (args.groupID == currentGroupID) {
        userNotTyping(args.userID);
    }
});