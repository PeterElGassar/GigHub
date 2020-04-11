

$(".attend-gig-btn").on("click", function () {
    var button = $(this);
    //"GigId" is a Name Of Dto Object Of GigId
    //Any Different Name it will not make Binding between them
    $.post("/api/Attendances", { GigId: button.attr("data-gigId") })
        .done(function () {
            button
                .removeClass("btn-default")
                .addClass("btn-info")
                .text("Going");

        })
        .fail(function () {
            alert("Your already Attended this gig..")
        });
});

/////////
//Follwing Function 
//////

$(".followLinkBtn").on("click", function () {
    var button = $(this);
    var allBtns = $(".followLinkBtn");
    $.post("/api/Followers", { FolloweeId: button.attr("data-artist-id") })
        .done(function (date) {
            debugger;
            for (var i = 0; i < allBtns.length; i++) {
                debugger;
                var linkAttr = allBtns[i].getAttribute("data-artist-id")
                if (linkAttr === date.ArtistId) {
                    $(allBtns[i]).text("Following");
                }
            }

            console.log(date.ArtistId)
        }).fail(function () {
            alert("Your already Following This Artist..");
        })



});
