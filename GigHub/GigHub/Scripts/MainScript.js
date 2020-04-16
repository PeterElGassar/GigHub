/////////
//Attendances  Function 
//////

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

/////////
//cancel Gig Function 
//////


$(".deleteGigBtn").on("click", function () {
    var link = $(this);
    //bootbox confirm
    //important note Don't Initializ Any Var In BootBox Scop
    //bootbox.confirm("Are You sure You Want to Delete this Gig ?", function (result) {


    //});

    var dialog = bootbox.dialog({
        message: "<h2>Are You sure You Want to Delete this Gig</h2>",
        size: 'large',
        buttons: {
            no: {
                label: "No",
                className: 'btn-default',
                callback: function () {
                    //close Dialog
                    bootbox.hideAll();
                }
            },
            yes: {
                label: "Yes",
                className: 'btn-danger',
                callback: function () {
                    //Use Ajax Here Because Request Is a Delete
                    $.ajax({
                        url: "/api/Gigs/" + link.attr("data-gig-id"),
                        method: "DELETE",
                    })
                    .done(function () {
                        link.parents("li").fadeOut("slow", function () {
                            //then remove from Dom 
                            //link.parents("li").remove();
                            $(this).remove();
                        });
                    })
                    .fail(function (data) {
                        alert("SomeThing Failed")
                    })
                }
            }

        }
    });


});


//Notification ==========
$.getJSON("/api/Notifications", function (notifcations) {
    //if (notifcations.length == 0) {
    //    return false;
    //}
    if (notifcations.length > 0) {
        $(".js-notification-count").text(notifcations.length)
            .removeClass("hide")
            .addClass("animated shake");
        //Call popover Plugin
        $('.notification').popover({

            html: true,
            title: "Noitifications",
            content: function () {
                var compiled = _.template($("#notification-template").html());
                return compiled({ notifcations: notifcations })


            },
            placement: "bottom"

        });
    }

});
